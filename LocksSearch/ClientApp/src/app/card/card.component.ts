import { Component, Input, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { ElementTypes } from '../enums/element-types.enum';
import { InfoComponent } from '../info/info.component';
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

  constructor(
    private dialog: MatDialog
  ) { }

  ngOnInit(): void {
    this.elementColor = this.getElementColor(this.element);
  }

  public openDialog(element: any) {
    console.log(element)
    const dialogRef = this.dialog.open(InfoComponent, {
      data: element
    });

    dialogRef.afterClosed().subscribe(result => {
      console.log(`Dialog result: ${result}`);
    });
  }

  private getElementColor(element: any): string {
    switch(element.className) {
      case ElementTypes.Building:
        return "#D1CFE2";
      case ElementTypes.Lock:
        return "#9CADCE";
      case ElementTypes.Group:
        return "#7EC4CF";
      case ElementTypes.Media:
        return "#52B2CF";
      default:
        return "#D1CFE2";
    }
  }
}
