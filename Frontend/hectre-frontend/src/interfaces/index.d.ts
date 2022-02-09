import Fields from "gql-query-builder/build/Fields";

export interface IRoute {
  id: string;
  name: string;
  path: string;
  element: JSX.Element;
}

export interface IDataProvider {
  getList: (param: any) => Promise<any>;
  create: (param: any) => Promise<any>;
}

export interface IPagination {
  current?: number;
  pageSize?: number;
}

export interface MetaData {
  variables?: [k: string, any];
  fields?: Fields;
}

export interface Variables {
  [k: string]: any;
}

export interface IChemical {
  id: string;
  chemicalType: string;
  preHarvestIntervalInDays: string;
  activeIngredient: string;
  name: string;
  creationDate: Date;
  modificationDate?: Date;
  deletionDate?: Date;
}

export interface HttpError extends Record<string, any> {
  message: string;
  statusCode: number;
}
