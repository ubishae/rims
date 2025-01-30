import { useMutation, useQuery, useQueryClient } from "@tanstack/react-query";
import { api } from "../lib/http";


export const keys = {
    GET_RISKS: 'GET_RISKS',
    CREATE_RISK: 'CREATE_RISK',
};

export function useGetRisks() {
    return useQuery({
        queryKey: [keys.GET_RISKS],
        queryFn: async () => (await api.get<IRisk[]>('/risks')).data,
    })
}

export function useCreateRisk<T>() {
    const queryClient = useQueryClient();

    return useMutation({
        mutationKey: [keys.CREATE_RISK],
        mutationFn: async (data: T) => (await api.post<IRisk>('/risks', data)).data,
        onSuccess: () => {
            queryClient.invalidateQueries({ queryKey: [keys.GET_RISKS] });
        },
        onError: (error) => {
            console.log(error)
        }
    })
}