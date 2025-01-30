import { useQuery } from "@tanstack/react-query";
import { api } from "../lib/http";


export const keys = {
    GET_RISK_CATEGORIES: ['GET_RISK_CATEGORIES'],
};

export const useGetRiskCategories = () => useQuery({
    queryKey: keys.GET_RISK_CATEGORIES,
    queryFn: async () => (await api.get<IRiskCategory[]>('/risks/categories')).data,
})