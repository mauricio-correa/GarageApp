using System.Threading.Tasks;
using API.DTOs;
using API.Entities;

namespace API.Interfaces
{
    public interface IVehicleRepository
    {
        void AddVehicle(Vehicle vehicle);
        Task<VehicleFindDto> GetVehicleByPlateNumberAsync(string PlateNumber);
        Task<VehicleDto> GetVehicleIdAsync(string plateNumber);

        Task<Vehicle> GetVehicleByIdAsync(int id);
        Task<bool> VehicleExists(string plateNumber);
        void Update(Vehicle vehicle);

    }
}