using System.ComponentModel.DataAnnotations;

namespace API.Entities
{
    public class Vehicle
    {
        public int VehicleId { get; set; }
        [MaxLength(7)]
        public string PlateNumber { get; set; }
        public string Type { get; set; }
        public int? ParkingSpotForeignKey { get; set; }
        public ParkingSpot ParkingSpot { get; set; } = null;

    }
}