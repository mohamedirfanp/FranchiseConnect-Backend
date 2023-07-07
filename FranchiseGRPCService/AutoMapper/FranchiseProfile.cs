using AutoMapper;
using FranchiseGRPCService.Models;
using Google.Protobuf.WellKnownTypes;

namespace FranchiseGRPCService.AutoMapper
{
    public class FranchiseProfile : Profile
    {
        public FranchiseProfile() {
            CreateMap<FranchiseModel, Franchise.Franchise>()
            .ForMember(dest => dest.FranchiseName, opt => opt.MapFrom(src => src.FranchiseName))
            .ForMember(dest => dest.FranchiseAbout, opt => opt.MapFrom(src => src.FranchiseAbout))
            .ForMember(dest => dest.FranchiseCurrentCount, opt => opt.MapFrom(src => src.FranchiseCurrentCount))
            .ForMember(dest => dest.FranchiseCustomizedOption, opt => opt.MapFrom(src => src.FranchiseCustomizedOption))
            .ForMember(dest => dest.FranchiseFee, opt => opt.MapFrom(src => src.FranchiseFee))
            .ForMember(dest => dest.FranchiseId, opt => opt.MapFrom(src => src.FranchiseId))
            .ForMember(dest => dest.FranchiseIndustry, opt => opt.MapFrom(src => src.FranchiseIndustry))
            .ForMember(dest => dest.FranchiseInvestment, opt => opt.MapFrom(src => src.FranchiseInvestment))
            .ForMember(dest => dest.FranchiseOwnerId, opt => opt.MapFrom(src => src.FranchiseOwnerId))
            .ForMember(dest => dest.FranchisePreferredExpansionLocation, opt => opt.MapFrom(src => src.FranchisePreferredExpansionLocation))
            .ForMember(dest => dest.FranchiseSampleBoxOption, opt => opt.MapFrom(src => src.FranchiseSampleBoxOption))
            .ForMember(dest => dest.FranchiseSegment, opt => opt.MapFrom(src => src.FranchiseSegment))
            .ForMember(dest => dest.FranchiseSpace, opt => opt.MapFrom(src => src.FranchiseSpace))
            .ForMember(dest => dest.FranchiseViewCount, opt => opt.MapFrom(src => src.FranchiseViewCount))
            // Map other properties as needed
            .ReverseMap(); // Optionally, configure reverse mapping


            CreateMap<FranchiseRequestModel, Franchise.FranchiseRequestModel>()
             .ForMember(dest => dest.FranchiseRequestId, opt => opt.MapFrom(src => src.FranchiseRequestId))
             .ForMember(dest => dest.FranchiseSampleBoxOption, opt => opt.MapFrom(src => src.FranchiseSampleBoxOption))
             .ForMember(dest => dest.FranchiseCustomizedOption, opt => opt.MapFrom(src => src.FranchiseCustomizedOption))
             .ForMember(dest => dest.OwnerId, opt => opt.MapFrom(src => src.ownerId))
             .ForMember(dest => dest.CreatedId, opt => opt.MapFrom(src => src.CreatedId))
             .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => Timestamp.FromDateTime(src.CreatedAt.ToUniversalTime())))
             .ForMember(dest => dest.IsRequestStatus, opt => opt.MapFrom(src => src.IsRequestStatus))
             .ForMember(dest => dest.FranchiseId, opt => opt.MapFrom(src => src.FranchiseId));



        }

            

    }
}
