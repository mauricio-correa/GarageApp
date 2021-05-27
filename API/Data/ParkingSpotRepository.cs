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
    public class ParkingSpotRepository : IParkingSpotRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public ParkingSpotRepository(DataContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        public void AddParkingSpot(ParkingSpot parkingSpot, int id)
        {
            var ParkingFloors = _context.ParkingSpots.Where(p => p.ParkingFloorId == id);
            
            var BiggestSpotNumber = ParkingFloors.Any() ? ParkingFloors.Max(x => x.SpotNr) : 0;
            parkingSpot.SpotNr = BiggestSpotNumber + 1;
      
             _context.ParkingSpots.Add(parkingSpot);
        }


        public void DeleteParkingSpot(ParkingSpot parkingSpot)
        {
            _context.ParkingSpots.Remove(parkingSpot);
        }

        public async Task<IEnumerable<ParkingSpotDto>> GetFreeParkingSpotsAsync()
        {
            var query = _context.ParkingSpots.AsQueryable();
            query = query.Where(v => v.FreeSpot == true);
               return await query
              .ProjectTo<ParkingSpotDto>(_mapper.ConfigurationProvider)
              .ToListAsync();
        }

        public async Task<ParkingSpotDto> GetOneFreeParkingSpotAsync()
        {
             return await _context.ParkingSpots
                .ProjectTo<ParkingSpotDto>(_mapper.ConfigurationProvider)
                .OrderBy(x => x.ParkingFloorId) 
                .FirstOrDefaultAsync(x => x.FreeSpot == true);
                
        }
        public async Task<ParkingSpot> GetLastParkingSpot(int id)
        {
            return await _context.ParkingSpots
                .Where(p => p.ParkingFloorId == id)
                .OrderByDescending(p => p.ParkingSpotId)
                .FirstOrDefaultAsync();
        }
        public async Task<ParkingSpot> GetParkingSpotByIdAsync(int id)
        {
            return await _context.ParkingSpots.FindAsync(id);
        }


        public async Task<ParkingFloor> GetParkingFloorAsync(int id)
        {
            return await _context.ParkingFloors
                .Where(x => x.ParkingFloorId == id)
                .SingleOrDefaultAsync();
        }
        public void Update(ParkingSpot parkingSpot)
        
        {
            _context.Entry(parkingSpot).State = EntityState.Modified;
        }


    }
}