using AutoMapper;
using RIMS.Domain.Entities;
using RIMS.Domain.Enums;

namespace RIMS.Application.Risks.Queries.GetRisksWithPagination;

public class RiskPaginationDto
{
    public int Id { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public decimal RiskScore { get; set; }
    public RiskStatus Status { get; set; }
    public string? RiskCategoryName { get; set; }
    public DateTime Created { get; set; }
    
    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Risk, RiskPaginationDto>()
                .ForMember(d => d.RiskCategoryName, opt => opt.MapFrom(s => s.RiskCategory != null ? s.RiskCategory.Name : null));
        }
    }
}
