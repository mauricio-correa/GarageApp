namespace API.DTOs
{
    public class VehicleDto
    {
        public int VehicleId { get; set; }
        public string PlateNumber { get; set; }
        public string Type { get; set; }
        public int? ParkingSpotForeignKey { get; set; }
        
    }
}