using System.Collections.Generic;
using System.Linq;
using API.Entities;

namespace API.DTOs
{
    public class ParkingFloorDto
    {
        public int ParkingFloorId { get; set; }
        public int FloorNr { get; set; }

        public int CountSpots {get; set;}
    }
}