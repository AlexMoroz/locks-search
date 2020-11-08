  import { Component, HostListener, OnInit } from '@angular/core';
import { FormControl } from '@angular/forms';
import { debounceTime, filter, tap } from 'rxjs/operators';
import { SearchService } from './search.service';

@Component({
  selector: 'app-search',
  templateUrl: './search.component.html',
  styleUrls: ['./search.component.css'],
})
export class SearchComponent implements OnInit {
  userInput: FormControl;
  elements: any[] = []

  constructor(private searchService: SearchService) {}

  ngOnInit() {
    this.userInput = new FormControl('');
    this.userInput.valueChanges
      .pipe(
        debounceTime(300),
        // filter for empty values
        filter((value) => {
          return value !== '' && value && typeof value === 'string';
        })
      )
      .subscribe((input) => {
        this.elements.push(this.searchService.getElementsForQuery(input, 0, 10));
      });
  }
}
