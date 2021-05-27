using System.Threading.Tasks;

namespace API.Interfaces
{
    public interface IUnitOfWork
    {
        IParkingFloorRepository ParkingFloorRepository {get;}
        IParkingSpotRepository ParkingSpotRepository {get;}
        IVehicleRepository VehicleRepository {get;}
        Task<bool> Complete();
    }
}