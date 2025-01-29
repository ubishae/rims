using AutoMapper;
using RIMS.Domain.Entities;

namespace RIMS.Application.RiskCategories.Queries.GetRiskCategoriesWithPagination;

public class RiskCategoryPaginationDto
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public DateTime Created { get; set; }
    
    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<RiskCategory, RiskCategoryPaginationDto>();
        }
    }
}
