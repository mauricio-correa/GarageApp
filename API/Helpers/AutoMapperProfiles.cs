using API.DTOs;
using API.Entities;
using AutoMapper;

namespace API.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Vehicle, VehicleFindDto>();
            CreateMap<Vehicle, VehicleDto>();
            CreateMap<Vehicle, RegisterVehicleDto>();
            CreateMap<ParkingSpot, ParkingSpotDto>();
            CreateMap<ParkingSpot, ParkingSpotChecksDto>();
            CreateMap<ParkingFloor, ParkingFloorDto>();
            CreateMap<ParkingFloor, CreateParkingFloorDto>();
        }
    }
}