import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import {map as loMap} from 'lodash'

import { ElementTypes } from '../enums/element-types.enum';
import { Building } from '../models/building.model';
import { Lock } from '../models/lock.model';
import { Group } from '../models/group.model';
import { Media } from '../models/media.model';

@Injectable()
export class SearchService {

  constructor(
    private http: HttpClient,
    @Inject('BASE_URL') private baseUrl: string
  ) { }

  getElementsForQuery(query: string, skip: number, take: number) : Observable<any[]> {
    return this.http.get(this.baseUrl + '/search/find', {
      params: {
        'query': query,
        'skip': skip.toString(),
        'take': take.toString()
      }
    }).pipe(
      map(elements => loMap(elements, this.elementToType))
    )
  }

  private elementToType(element: any) : Building | Lock | Group | Media {
    console.log(element);
    switch (element['className']) {
      case ElementTypes.Building: {
        return new Building(element);
      }
      case ElementTypes.Lock: {
        return new Lock(element);
      }
      case ElementTypes.Group: {
        return new Group(element);
      }
      case ElementTypes.Media: {
        return new Media(element);
      }
      default:
        throw new Error(`Unknown type: ${element}`)
    }
  }
}
