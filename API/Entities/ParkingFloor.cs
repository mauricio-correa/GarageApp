using System.Collections.Generic;
using System.Linq;

namespace API.Entities
{
    public class ParkingFloor
    {
        public int ParkingFloorId { get; set; }

        public int FloorNr { get; set; }
        public List<ParkingSpot> ParkingSpots { get; set; } = new List<ParkingSpot>();
       
        public int CountSpots { get; set; }
    }
}