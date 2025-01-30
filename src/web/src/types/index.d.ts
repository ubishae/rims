enum RiskStatus {
    Identified,
    Active,
    Mitigated,
    Closed
}

interface IRisk {
    id: number;
    title: string 
    description?: string 
    impactScore?: number 
    probabilityScore?: number 
    riskScore?: number 
    status?: RiskStatus 
    level?: string
    riskCategoryId?: number 
    created?: string;
    createdBy?: string 
    lastModified?: string;
    lastModifiedBy?: string 
}

interface IRiskCategory {
    id: number;
    name: string 
    description?: string 
    created?: string;
    createdBy?: string 
    lastModified?: string;
    lastModifiedBy?: string 
}