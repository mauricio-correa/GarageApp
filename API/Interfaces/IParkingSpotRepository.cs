using System.Collections.Generic;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;

namespace API.Interfaces
{
    public interface IParkingSpotRepository
    {
        void AddParkingSpot(ParkingSpot parkingSpot, int id);
        void DeleteParkingSpot(ParkingSpot parkingSlot);
        Task<IEnumerable<ParkingSpotDto>> GetFreeParkingSpotsAsync();
        Task<ParkingSpotDto> GetOneFreeParkingSpotAsync();
        Task<ParkingSpot> GetLastParkingSpot(int id);
        Task<ParkingFloor> GetParkingFloorAsync(int id);
        Task<ParkingSpot> GetParkingSpotByIdAsync(int id);
        void Update(ParkingSpot parkingSpot);
   
    }
}