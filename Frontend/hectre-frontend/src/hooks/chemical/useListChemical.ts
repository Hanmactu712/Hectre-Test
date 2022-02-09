import { notification } from "antd";
import { useQuery, UseQueryOptions, UseQueryResult } from "react-query";
import { HttpError, IPagination, MetaData } from "../../interfaces";
import { dataProvider, QueryListResponse } from "../../providers";

export type UseListProps<TData, TError> = {
  operation: string;
  metaData: MetaData;
  pagination: IPagination;
  options?: UseQueryOptions<QueryListResponse<TData>, TError>;
};

export type UseListChemicalReturnType<TData, TError> = UseQueryResult<QueryListResponse<TData>, TError>;

export const useListChemical = <TData, TError = HttpError>({
  operation,
  metaData,
  pagination,
  options,
}: UseListProps<TData, TError>): UseListChemicalReturnType<TData, TError> => {
  const { getList } = dataProvider();

  const queryResponse = useQuery<QueryListResponse<TData>, TError>("useListChemical", () => getList({ operation, metaData, pagination }), {
    ...options,

    onError: (error: any) => {
      notification.error({
        message: error?.name || "Error while creating chemical",
        description: error?.message || "Error while creating chemical",
        key: "createError",
      });
    },
  });

  return queryResponse;
};
