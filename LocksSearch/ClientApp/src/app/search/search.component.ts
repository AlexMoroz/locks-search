  import { Component, HostListener, OnInit } from '@angular/core';
import { FormControl } from '@angular/forms';
import { each } from 'lodash';
import { debounceTime, filter, tap } from 'rxjs/operators';
import { SearchService } from './search.service';

@Component({
  selector: 'app-search',
  templateUrl: './search.component.html',
  styleUrls: ['./search.component.css'],
})
export class SearchComponent implements OnInit {
  public userInput: FormControl;
  public elements: any[] = []

  private offset: number = 0;
  private limit: number = 20;
  private count: number = 0;


  constructor(private searchService: SearchService) {}

  ngOnInit() {
    this.userInput = new FormControl('');
    this.userInput.valueChanges
      .pipe(
        tap(() => {
          // clean all
          this.count = 0;
          this.offset = 0;
          this.elements = [];
        }),
        // add typing delay
        debounceTime(300),
        filter((value) => {
          // filter for empty values and if all values are shown
          return value !== '' && value && typeof value === 'string' && this.count == this.offset;
        })
      )
      .subscribe((input) => {
        this.fetchData(input);
      });
  }

  public onScroll() {
    this.fetchData(this.userInput.value);
  }

  public clearSearchField() {
    this.userInput.setValue('');
  }

  private fetchData(input: string) {
    console.log(input)
    this.searchService.getElementsForQuery(input, this.offset, this.limit).subscribe((data: any[]) => {
      console.log(data)
      each(data, (item) => this.elements.push(item));
      this.count = this.elements.length;
      this.offset += data.length;
    });
  }
}
