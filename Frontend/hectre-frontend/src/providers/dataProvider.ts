import * as gql from "gql-query-builder";
import Fields from "gql-query-builder/build/Fields";
import { GraphQLClient } from "graphql-request";
import { Configuration } from "../common/configuration";
import { IDataProvider, IPagination, MetaData, Variables } from "../interfaces";

const defaultCommonFields = ["total", "code", "message"];

export type GetListProps = {
  operation: string;
  metaData: MetaData;
  pagination: IPagination;
};

export type CreateProps = {
  operation: string;
  variables: Variables;
  fields: Fields;
};

export type QueryListResponse<TData> = {
  data: TData[];
  total: number;
  //   code: string;
  //   message: string;
};

export type CreateResponse<TData> = {
  data: TData;
};

const client = new GraphQLClient(Configuration.GraphQlApiUrl);

export const dataProvider = (): IDataProvider => {
  return {
    getList: async <TData>({ operation, metaData, pagination }: GetListProps): Promise<QueryListResponse<TData>> => {
      const current = pagination?.current || 1;
      const pageSize = pagination?.pageSize || 10;
      const { query, variables } = gql.query({
        operation,
        variables: {
          input: {
            value: {
              ...metaData?.variables,
              start: (current - 1) * pageSize,
              limit: pageSize,
            },
            type: "ChemicalListInputType",
          },
        },
        fields: [{ data: metaData?.fields }, ...defaultCommonFields],
      });

      const response = await client.request(query, variables);

      if (response[operation].data) {
        return {
          data: response[operation].data,
          total: response[operation].total,
          //   code: "",
          //   message: "",
        };
      } else {
        return Promise.reject({
          code: response[operation].code,
          message: response[operation].message,
        });
      }
    },
    create: async <TData>({ operation, variables, fields }: CreateProps): Promise<CreateResponse<TData>> => {
      const { query, variables: gqlVariables } = gql.mutation({
        operation,
        variables: {
          input: {
            value: variables,
            type: "createChemicalInput",
          },
        },
        fields: [{ data: fields ?? ["id"] }, ...defaultCommonFields],
      });

      //   const client = new GraphQLClient(Configuration.GraphQlApiUrl);
      const response = await client.request(query, gqlVariables);

      if (response && response[operation] && response[operation].data) {
        return {
          data: response[operation].data,
        };
      } else {
        return Promise.reject({
          code: response[operation].code,
          message: response[operation].message,
        });
      }
    },
  };
};

export default dataProvider;
