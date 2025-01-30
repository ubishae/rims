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
    public string? Level { get; set; }
    public int CategoryId { get; set; }
    public string? CategoryName { get; set; }
    
    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Risk, RiskBriefDto>()
                .ForMember(d => d.CategoryName, opt => opt.MapFrom(s => s.Category != null ? s.Category.Name : null))
                .ForMember(d => d.Level, opt => opt.MapFrom(s => s.Level != null ? s.Level.ToString() : null));
        }
    }
}
