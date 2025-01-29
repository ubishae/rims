using AutoMapper;
using RIMS.Domain.Entities;

namespace RIMS.Application.RiskCategories.Queries.GetRiskCategories;

public class RiskCategoryBriefDto
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    
    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<RiskCategory, RiskCategoryBriefDto>();
        }
    }
}
