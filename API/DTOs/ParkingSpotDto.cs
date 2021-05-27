namespace API.DTOs
{
    public class ParkingSpotDto
    {
        public int ParkingSpotId { get; set; }
        public bool FreeSpot { get; set; }

        public int SpotNr { get; set; }

        public int ParkingFloorId { get; set; }

        public int ParkingFloorFloorNr { get; set; }
    }
}