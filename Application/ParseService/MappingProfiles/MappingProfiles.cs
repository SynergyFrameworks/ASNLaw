using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ParseService.Dtos;
using Datalayer.Domain;

namespace ParseService.MappingProfiles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            //parse parameter
            CreateMap<Parameter, ParseParameterDTO>();
            CreateMap<ParseParameterDTO, Parameter>();

        }
    }
}
