using AutoMapper;
using RIMS.Domain.Entities;
using RIMS.Domain.Enums;

namespace RIMS.Application.Risks.Queries.GetRiskById;

public class RiskDetailsDto
{
    public int Id { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public decimal ImpactScore { get; set; }
    public decimal ProbabilityScore { get; set; }
    public decimal RiskScore { get; set; }
    public RiskStatus Status { get; set; }
    public int RiskCategoryId { get; set; }
    public RiskCategoryDto? RiskCategory { get; set; }
    public DateTime Created { get; set; }
    public string? CreatedBy { get; set; }
    public DateTime? LastModified { get; set; }
    public string? LastModifiedBy { get; set; }
    
    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Risk, RiskDetailsDto>();
        }
    }
}

public class RiskCategoryDto
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    
    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<RiskCategory, RiskCategoryDto>();
        }
    }
}
