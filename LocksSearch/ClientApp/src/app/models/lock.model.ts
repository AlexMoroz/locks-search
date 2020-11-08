import { assign } from "lodash";

export class Lock {

  constructor(object: any) {
    assign(this, object);
  }

  public guid: string;
  public name: string;
  public description: string;
  public type: string;
  public serialNumber: string;
  public floor: string;
  public roomNumber: string;
}
