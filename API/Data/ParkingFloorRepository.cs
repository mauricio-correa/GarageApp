using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class ParkingFloorRepository : IParkingFloorRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public ParkingFloorRepository(DataContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }
        
        public void AddParkingFloorBulk(ParkingFloor parkingFloor, int n)
        
        {
            var highestFloor = _context.ParkingFloors.Any() ? _context.ParkingFloors.Max(x => x.FloorNr) : 0;
            parkingFloor.FloorNr = highestFloor + 1;
         
           for(int i = 0; i < n; i++)
                {
                   ParkingSpot ps = new ParkingSpot { SpotNr = i+1 };
                    parkingFloor.ParkingSpots.Add(ps);
                };
            parkingFloor.CountSpots = n;
            
            _context.ParkingFloors.Add(parkingFloor);

        }
        public void DeleteParkingFloor(ParkingFloor parkingFloor)
        {
            _context.ParkingFloors.Remove(parkingFloor);
        }
        public async Task<IEnumerable<ParkingFloorDto>> GetParkingFloorsAsync()
        {
            var query = _context.ParkingFloors.AsQueryable();

            return await query
              .ProjectTo<ParkingFloorDto>(_mapper.ConfigurationProvider)
              .OrderBy(p => p.FloorNr)
              .ToListAsync();
        }

        public async Task<ParkingFloor> GetLastParkingFloor()
        {
            return await _context.ParkingFloors
                .OrderByDescending(p => p.ParkingFloorId)
                .FirstOrDefaultAsync();
        }
        public async Task<ParkingFloor> GetParkingFloorByIdAsync(int id)
        {
            return await _context.ParkingFloors.FindAsync(id);
            
        }
        public void Update(ParkingFloor parkingFloor)
        
        {
            _context.Entry(parkingFloor).State = EntityState.Modified;
        }

    }
}