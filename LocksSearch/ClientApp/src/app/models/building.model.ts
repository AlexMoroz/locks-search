import { assign } from "lodash";

export class Building {

  constructor(object: any) {
    assign(this, object);
  }

  public guid: string;
  public name: string;
  public shortCut: string;
  public description: string;
}
