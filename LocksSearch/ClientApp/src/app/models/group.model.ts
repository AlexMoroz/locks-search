import { assign } from "lodash";

export class Group {

  constructor(object: any) {
    assign(this, object);
  }

  public className: string;
  public guid: string;
  public name: string;
  public description: string;
}
