﻿using AutoMapper;
using HackSystem.WebAPI.Services.API.Program.ProgramAsset;
using HackSystem.WebDataTransfer.Program.ProgramAsset;

namespace HackSystem.WebAPI.Mappers.ProgramMapper.ProgramAssertMapper;
public class ProgramAssertMapperProfile : Profile
{
    public ProgramAssertMapperProfile()
    {
        this.CreateMap<ProgramAsset, ProgramAssetDTO>();
        this.CreateMap<ProgramAssetPackage, ProgramAssetPackageDTO>();
    }
}
