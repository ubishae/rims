import { useQuery } from "@tanstack/react-query";
import { api } from "../lib/http";


export const keys = {
    GET_RISKS: ['GET_RISKS'],
};

export const useGetRisks = () => useQuery({
    queryKey: keys.GET_RISKS,
    queryFn: async () => (await api.get<IRisk[]>('/risks')).data,
})