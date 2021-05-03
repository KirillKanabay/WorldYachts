using AutoMapper;
using WorldYachts.Data.ViewModels;


namespace WorldYachts.Services.Serialization
{
    public class BoatMapper:Profile
    {
        public BoatMapper()
        {
            //Boat -> BoatModel
            CreateMap<Data.Entities.Boat, BoatModel>()
                .ForMember(dst => dst.Model, opt => opt.MapFrom(src => src.Model))
                .ForMember(dst => dst.TypeId, opt => opt.MapFrom(src => src.TypeId))
                .ForMember(dst => dst.WoodId, opt => opt.MapFrom(src => src.WoodId))
                .ForMember(dst => dst.NumberOfRowers, opt => opt.MapFrom(src => src.NumberOfRowers))
                .ForMember(dst => dst.Mast, opt => opt.MapFrom(src => src.Mast))
                .ForMember(dst => dst.Color, opt => opt.MapFrom(src => src.Color))
                .ForMember(dst => dst.BasePrice, opt => opt.MapFrom(src => src.BasePrice))
                .ForMember(dst => dst.Vat, opt => opt.MapFrom(src => src.Vat))
                ;

            //BoatTypeModel -> BoatType
            CreateMap<BoatTypeModel, Data.Entities.BoatType>()
                .ForMember(dst => dst.Type, opt => opt.MapFrom(src => src.Type))
                ;

            //BoatType -> BoatType
            CreateMap<Data.Entities.BoatType, Data.Entities.BoatType>()
                .ForMember(dst => dst.Type, opt => opt.MapFrom(src => src.Type))
                .ForMember(dst => dst.Id, opt => opt.Ignore())
                ;

            //BoatTypeWood -> BoatWood
            CreateMap<BoatWoodModel, Data.Entities.BoatWood>()
                .ForMember(dst => dst.Wood, opt => opt.MapFrom(src => src.Wood))
                ;


            //BoatWood -> BoatWood
            CreateMap<Data.Entities.BoatWood, Data.Entities.BoatWood>()
                .ForMember(dst => dst.Wood, opt => opt.MapFrom(src => src.Wood))
                .ForMember(dst => dst.Id, opt => opt.Ignore())
                ;

            //Boat->Boat
            CreateMap<Data.Entities.Boat, Data.Entities.Boat>()
                .ForMember(dst => dst.Model, opt => opt.MapFrom(src => src.Model))
                .ForMember(dst => dst.TypeId, opt => opt.MapFrom(src => src.TypeId))
                .ForMember(dst => dst.WoodId, opt => opt.MapFrom(src => src.WoodId))
                .ForMember(dst => dst.NumberOfRowers, opt => opt.MapFrom(src => src.NumberOfRowers))
                .ForMember(dst => dst.Mast, opt => opt.MapFrom(src => src.Mast))
                .ForMember(dst => dst.Color, opt => opt.MapFrom(src => src.Color))
                .ForMember(dst => dst.BasePrice, opt => opt.MapFrom(src => src.BasePrice))
                .ForMember(dst => dst.Vat, opt => opt.MapFrom(src => src.Vat))
                .ForMember(dst => dst.Id, opt=>opt.Ignore())
                ;
        }
    }
}
