using AutoMapper;
using Srv_Sale.Models;
using Srv_Sale.DTOs;
using Transit;

namespace Srv_Sale.RequestHelpers;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Sale, SaleDto>()
            .IncludeMembers(x => x.Item);

        CreateMap<Item, SaleDto>()
            .ForMember(dest => dest.Brand, opt => opt.MapFrom(src => src.Brand))
            .ForMember(dest => dest.Model, opt => opt.MapFrom(src => src.Model))
            .ForMember(dest => dest.Year, opt => opt.MapFrom(src => src.Year))
            .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.ImageUrl))
            .ForMember(dest => dest.Frame, opt => opt.MapFrom(src => src.AdditionalProperties.Frame))
            .ForMember(dest => dest.Handlebar, opt => opt.MapFrom(src => src.AdditionalProperties.Handlebar))
            .ForMember(dest => dest.Brakes, opt => opt.MapFrom(src => src.AdditionalProperties.Brakes))
            .ForMember(dest => dest.WheelsTires, opt => opt.MapFrom(src => src.AdditionalProperties.WheelsTires))
            .ForMember(dest => dest.Seat, opt => opt.MapFrom(src => src.AdditionalProperties.Seat))
            .ForMember(dest => dest.DerailleursDrive, opt => opt.MapFrom(src => src.AdditionalProperties.DerailleursDrive))
            .ForMember(dest => dest.AdditionalAccessories, opt => opt.MapFrom(src => src.AdditionalProperties.AdditionalAccessories));
        //----- 1
        CreateMap<NewSaleDto, Sale>()
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
            .ForMember(dest => dest.Author, opt => opt.MapFrom(src => src.Author))
            .ForMember(dest => dest.Item, opt => opt.MapFrom(src => src));

        CreateMap<NewSaleDto, Item>()
            .ForMember(dest => dest.Brand, opt => opt.MapFrom(src => src.Brand))
            .ForMember(dest => dest.Model, opt => opt.MapFrom(src => src.Model))
            .ForMember(dest => dest.Year, opt => opt.MapFrom(src => src.Year))
            .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.ImageUrl))
            .ForMember(dest => dest.AdditionalProperties, opt => opt.MapFrom(src => new ItemProperties
            {
                Frame = src.Frame,
                Handlebar = src.Handlebar,
                Brakes = src.Brakes,
                WheelsTires = src.WheelsTires,
                Seat = src.Seat,
                DerailleursDrive = src.DerailleursDrive,
                AdditionalAccessories = src.AdditionalAccessories
            }));

        //---------------------------------------------------------
        CreateMap<SaleDto, SaleCreated>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
            .ForMember(dest => dest.PublicationDate, opt => opt.MapFrom(src => src.PublicationDate))
            .ForMember(dest => dest.Author, opt => opt.MapFrom(src => src.Author))
            .ForMember(dest => dest.Brand, opt => opt.MapFrom(src => src.Brand))
            .ForMember(dest => dest.Model, opt => opt.MapFrom(src => src.Model))
            .ForMember(dest => dest.Year, opt => opt.MapFrom(src => src.Year))
            .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.ImageUrl))
            .ForMember(dest => dest.Frame, opt => opt.MapFrom(src => src.Frame))
            .ForMember(dest => dest.Handlebar, opt => opt.MapFrom(src => src.Handlebar))
            .ForMember(dest => dest.Brakes, opt => opt.MapFrom(src => src.Brakes))
            .ForMember(dest => dest.WheelsTires, opt => opt.MapFrom(src => src.WheelsTires))
            .ForMember(dest => dest.Seat, opt => opt.MapFrom(src => src.Seat))
            .ForMember(dest => dest.DerailleursDrive, opt => opt.MapFrom(src => src.DerailleursDrive))
            .ForMember(dest => dest.AdditionalAccessories, opt => opt.MapFrom(src => src.AdditionalAccessories));

/*
        CreateMap<Sale, SaleCreated>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()))
            .ForMember(dest => dest.PublicationDate, opt => opt.MapFrom(src => src.PublicationDate))
            .ForMember(dest => dest.Author, opt => opt.MapFrom(src => src.Author));
        CreateMap<Item, SaleCreated>()
            .ForMember(dest => dest.Brand, opt => opt.MapFrom(src => src.Brand))
            .ForMember(dest => dest.Model, opt => opt.MapFrom(src => src.Model))
            .ForMember(dest => dest.Year, opt => opt.MapFrom(src => src.Year))
            .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.ImageUrl))
            .ForMember(dest => dest.Frame, opt => opt.MapFrom(src => src.AdditionalProperties.Frame))
            .ForMember(dest => dest.Handlebar, opt => opt.MapFrom(src => src.AdditionalProperties.Handlebar))
            .ForMember(dest => dest.Brakes, opt => opt.MapFrom(src => src.AdditionalProperties.Brakes))
            .ForMember(dest => dest.WheelsTires, opt => opt.MapFrom(src => src.AdditionalProperties.WheelsTires))
            .ForMember(dest => dest.Seat, opt => opt.MapFrom(src => src.AdditionalProperties.Seat))
            .ForMember(dest => dest.DerailleursDrive, opt => opt.MapFrom(src => src.AdditionalProperties.DerailleursDrive))
            .ForMember(dest => dest.AdditionalAccessories, opt => opt.MapFrom(src => src.AdditionalProperties.AdditionalAccessories));
*/
        CreateMap<Sale, SaleUpdated>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()))
            .ForMember(dest => dest.PublicationDate, opt => opt.MapFrom(src => src.PublicationDate))
            .ForMember(dest => dest.Author, opt => opt.MapFrom(src => src.Author));
        CreateMap<Item, SaleUpdated>()
            .ForMember(dest => dest.Brand, opt => opt.MapFrom(src => src.Brand))
            .ForMember(dest => dest.Model, opt => opt.MapFrom(src => src.Model))
            .ForMember(dest => dest.Year, opt => opt.MapFrom(src => src.Year))
            .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.ImageUrl))
            .ForMember(dest => dest.Frame, opt => opt.MapFrom(src => src.AdditionalProperties.Frame))
            .ForMember(dest => dest.Handlebar, opt => opt.MapFrom(src => src.AdditionalProperties.Handlebar))
            .ForMember(dest => dest.Brakes, opt => opt.MapFrom(src => src.AdditionalProperties.Brakes))
            .ForMember(dest => dest.WheelsTires, opt => opt.MapFrom(src => src.AdditionalProperties.WheelsTires))
            .ForMember(dest => dest.Seat, opt => opt.MapFrom(src => src.AdditionalProperties.Seat))
            .ForMember(dest => dest.DerailleursDrive, opt => opt.MapFrom(src => src.AdditionalProperties.DerailleursDrive))
            .ForMember(dest => dest.AdditionalAccessories, opt => opt.MapFrom(src => src.AdditionalProperties.AdditionalAccessories));
        

        CreateMap<Srv_Sale.Component, Transit.Component>();
    }
}

