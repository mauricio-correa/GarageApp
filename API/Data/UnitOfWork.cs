using System.Threading.Tasks;
using API.Interfaces;
using AutoMapper;

namespace API.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public UnitOfWork(DataContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        public IParkingFloorRepository ParkingFloorRepository => new ParkingFloorRepository(_context, _mapper);

        public IParkingSpotRepository ParkingSpotRepository => new ParkingSpotRepository(_context, _mapper);

        public IVehicleRepository VehicleRepository => new VehicleRepository(_context, _mapper);

        public async Task<bool> Complete()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}