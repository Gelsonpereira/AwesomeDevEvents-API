using AutoMapper;
using AwesomeDevEvents.Api.Entities;
using AwesomeDevEvents.Api.Models;

namespace AwesomeDevEvents.Api.Mappers
{
    public class DevEventProfile : Profile
    {
        public DevEventProfile()
        {
            CreateMap<DevEvents, DevEventViewModel>();
            CreateMap<DevEventSpeaker, DevEventSpeakerViewModel>();

            CreateMap<DevEventInputModel, DevEvents>();
            CreateMap<DevEventSpeakerInputModel, DevEventSpeaker>();
        }
    }
}
