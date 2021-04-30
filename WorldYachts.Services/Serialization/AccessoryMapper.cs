using AutoMapper;
using WorldYachts.Services.Models;

namespace WorldYachts.Services.Serialization
{
    public class AccessoryMapper:Profile
    {
        public AccessoryMapper()
        {
            //AccessoryModel -> Accessory
            CreateMap<AccessoryModel, Data.Entities.Accessory>()
                .ForMember(dst => dst.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dst => dst.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dst => dst.Price, opt => opt.MapFrom(src => src.Price))
                .ForMember(dst => dst.Vat, opt => opt.MapFrom(src => src.Vat))
                .ForMember(dst => dst.Inventory, opt => opt.MapFrom(src => src.Inventory))
                .ForMember(dst => dst.PartnerId, opt => opt.MapFrom(src => src.PartnerId))
                ;

            //Accessory -> Accessory
            CreateMap<Data.Entities.Accessory, Data.Entities.Accessory>()
                .ForMember(dst => dst.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dst => dst.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dst => dst.Price, opt => opt.MapFrom(src => src.Price))
                .ForMember(dst => dst.Vat, opt => opt.MapFrom(src => src.Vat))
                .ForMember(dst => dst.Inventory, opt => opt.MapFrom(src => src.Inventory))
                .ForMember(dst => dst.PartnerId, opt => opt.MapFrom(src => src.PartnerId))
                .ForMember(dst => dst.Id, opt => opt.Ignore())
                ;

            //AccessoryToBoatModel -> AccessoryToBoat
            CreateMap<AccessoryToBoatModel, Data.Entities.AccessoryToBoat>()
                .ForMember(dst => dst.BoatId, opt => opt.MapFrom(src => src.BoatId))
                .ForMember(dst => dst.AccessoryId, opt => opt.MapFrom(src => src.AccessoryId))
                ;

            //AccessoryToBoat -> AccessoryToBoat
            CreateMap<Data.Entities.AccessoryToBoat, Data.Entities.AccessoryToBoat>()
                .ForMember(dst => dst.BoatId, opt => opt.MapFrom(src => src.BoatId))
                .ForMember(dst => dst.AccessoryId, opt => opt.MapFrom(src => src.AccessoryId))
                .ForMember(dst => dst.Id, opt => opt.Ignore());
                ;

            //Partner -> Partner
            CreateMap<Data.Entities.Partner, Data.Entities.Partner>()
                .ForMember(dst => dst.City, opt => opt.MapFrom(src => src.City))
                .ForMember(dst => dst.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dst => dst.Address, opt => opt.MapFrom(src => src.Address))
                ;
        }
    }
}
