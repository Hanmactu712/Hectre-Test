import { notification } from "antd";
import Fields from "gql-query-builder/build/Fields";
import { useMutation, UseMutationResult } from "react-query";
import { CreateResponse, dataProvider } from "../../providers";

export type UseCreateProps<TVariables> = {
  operation: string;
  variables?: TVariables;
  fields: Fields;
};

export type UseCreateChemicalReturnType<TData, TVariables = {}> = UseMutationResult<CreateResponse<TData>, unknown, UseCreateProps<TVariables>, unknown>;

export const useCreateChemical = <TData, TVariables = {}>(): UseCreateChemicalReturnType<TData, TVariables> => {
  const { create } = dataProvider();

  const queryResponse = useMutation<CreateResponse<TData>, unknown, UseCreateProps<TVariables>, unknown>(
    "useCreateChemical",
    ({ operation, variables, fields }: UseCreateProps<TVariables>) => create({ operation, variables, fields }),
    {
      onError: (error: any) => {
        notification.error({
          type: "error",
          message: error?.name || "Error while creating chemical",
          description: error?.message || "Error while creating chemical",
          key: "createError",
        });
      },
    }
  );

  return queryResponse;
};
