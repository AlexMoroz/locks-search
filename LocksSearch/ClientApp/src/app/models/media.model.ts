import { assign } from "lodash";

export class Media {

  constructor(object: any) {
    assign(this, object);
  }

  public guid: string;
  public description: string;
  public type: string;
  public owner: string;
  public serialNumber: string;
}
