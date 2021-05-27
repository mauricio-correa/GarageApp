namespace API.Entities
{
    public class ParkingSpot
    {
        public int ParkingSpotId { get; set; }

        public int SpotNr { get; set; }
        public bool FreeSpot { get; set; } = true;
        public int ParkingFloorId { get; set; }
        public ParkingFloor ParkingFloor { get; set; }
        public Vehicle Vehicle { get; set; }
    }
}