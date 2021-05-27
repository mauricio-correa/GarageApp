using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using AutoMapper.QueryableExtensions;

namespace API.Data
{
    public class VehicleRepository : IVehicleRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public VehicleRepository(DataContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }
        public void AddVehicle(Vehicle vehicle)
        {
            _context.Vehicles.Add(vehicle);
        }

        public async Task<VehicleDto> GetVehicleIdAsync(string plateNumber)
        {
            return await _context.Vehicles
                .Where(x => x.PlateNumber == plateNumber.ToLower())
                .ProjectTo<VehicleDto>(_mapper.ConfigurationProvider)
                .SingleOrDefaultAsync();
        }

        public async Task<VehicleFindDto> GetVehicleByPlateNumberAsync(string plateNumber)
        {
            return await _context.Vehicles
                .Where(v => v.PlateNumber == plateNumber.ToLower())
                .ProjectTo<VehicleFindDto>(_mapper.ConfigurationProvider)
                .SingleOrDefaultAsync();
        }

        public async Task<bool> VehicleExists(string plateNumber)
        {
            return await _context.Vehicles.AnyAsync(x => x.PlateNumber == plateNumber.ToLower());
        }

        public async Task<Vehicle> GetVehicleByIdAsync(int id)
        {
            return await _context.Vehicles.FindAsync(id);
            
        }
        public void Update(Vehicle vehicle)
        {
            _context.Entry(vehicle).State = EntityState.Modified;
        }

    }
}