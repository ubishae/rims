using AutoMapper;
using RIMS.Domain.Entities;
using RIMS.Domain.Enums;

namespace RIMS.Application.Risks.Queries.GetRisks;

public class RiskBriefDto
{
    public int Id { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public decimal ImpactScore { get; set; }
    public decimal ProbabilityScore { get; set; }
    public decimal RiskScore { get; set; }
    public RiskStatus Status { get; set; }
    public int RiskCategoryId { get; set; }
    public string? RiskCategoryName { get; set; }
    
    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Risk, RiskBriefDto>()
                .ForMember(d => d.RiskCategoryName, opt => opt.MapFrom(s => s.RiskCategory != null ? s.RiskCategory.Name : null));
        }
    }
}
