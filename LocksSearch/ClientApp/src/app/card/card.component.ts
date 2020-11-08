import { Component, Input, OnInit } from '@angular/core';
import { ElementTypes } from '../enums/element-types.enum';
import { Building } from '../models/building.model';
import { Group } from '../models/group.model';
import { Lock } from '../models/lock.model';
import { Media } from '../models/media.model';

@Component({
  selector: 'app-card',
  templateUrl: './card.component.html',
  styleUrls: ['./card.component.css']
})
export class CardComponent implements OnInit {
  public readonly ElementTypes = ElementTypes;

  @Input() element: Building | Lock | Group | Media;

  public elementColor: string;

  constructor() { }

  ngOnInit(): void {
    this.elementColor = this.getElementColor(this.element);
  }

  private getElementColor(element: any): string {
    switch(element.className) {
      case ElementTypes.Building:
        return "#FC766AFF";
      case ElementTypes.Lock:
        return "#5B84B1FF";
      case ElementTypes.Group:
        return "#5F4B8BFF";
      case ElementTypes.Media:
        return "#E69A8DFF";
      default:
        return "#FC766AFF";

    }
  }

}
