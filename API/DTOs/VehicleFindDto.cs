namespace API.DTOs
{
    public class VehicleFindDto
    {
        public int VehicleId { get; set; }
        public string PlateNumber { get; set; }
        public int? ParkingSpotForeignKey { get; set; }

        public ParkingSpotDto ParkingSpot { get; set; }

    }
}