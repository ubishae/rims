using AutoMapper;
using RIMS.Domain.Entities;

namespace RIMS.Application.RiskCategories.Queries.GetRiskCategoryById;

public class RiskCategoryDetailsDto
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public DateTime Created { get; set; }
    public string? CreatedBy { get; set; }
    public DateTime? LastModified { get; set; }
    public string? LastModifiedBy { get; set; }
    
    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<RiskCategory, RiskCategoryDetailsDto>();
        }
    }
}
