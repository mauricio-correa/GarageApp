using System.Collections.Generic;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;

namespace API.Interfaces
{
    public interface IParkingFloorRepository
    {

        void AddParkingFloorBulk(ParkingFloor parkingFloor, int n);
        void DeleteParkingFloor(ParkingFloor parkingFloor);
        Task<IEnumerable<ParkingFloorDto>> GetParkingFloorsAsync();
    
        Task<ParkingFloor> GetParkingFloorByIdAsync(int id);
        Task<ParkingFloor> GetLastParkingFloor();
        void Update(ParkingFloor parkingFloor);
  

        
    }
}