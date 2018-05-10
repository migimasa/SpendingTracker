using AutoMapper;
using Tracker.DAL.Tables;
using TrackerService.Base;
using TrackerService.DTO;
using TrackerService.Enums;

namespace TrackerService.Mappings
{
    public class EnvelopeMappingProfile : Profile
    {
        public EnvelopeMappingProfile()
        {
            CreateMap<MoneyEnvelopeDto, Envelope>()
                .ForMember(dest => dest.RolloverType, opts => opts.MapFrom(src => src.RolloverType.Value));
            CreateMap<Envelope, MoneyEnvelopeDto>()
                .ForMember(dest => dest.RolloverType,
                    opts => opts.MapFrom(src => Enumeration.FromValue<BalanceRolloverType>(src.RolloverType)));
        }
    }
}