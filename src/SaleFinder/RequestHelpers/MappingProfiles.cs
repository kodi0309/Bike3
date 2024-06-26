﻿using AutoMapper;
using Transit;

namespace SaleFinder;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<SaleCreated, Item>();
        CreateMap<SaleUpdated, Item>();

        CreateMap<Transit.Component, Component>();
    }
}