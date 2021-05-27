using System.Threading.Tasks;
using API.Data;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class VehicleController : BaseApiController
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public VehicleController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;

        }
        [HttpPost("plate-number/{plateNumber}")]
        public async Task<ActionResult<VehicleDto>> RegisterVehiclePlateNumber(string plateNumber)
        {

            if (await _unitOfWork.VehicleRepository.VehicleExists(plateNumber))
            {
                var vehicle = await _unitOfWork.VehicleRepository.GetVehicleByPlateNumberAsync(plateNumber);
                if (vehicle.ParkingSpotForeignKey != null)
                {
                    return BadRequest("Your Vehicle is already in.");
                }

                return await _unitOfWork.VehicleRepository.GetVehicleIdAsync(plateNumber);

            }
            var newVehicle = new Vehicle
            {
                PlateNumber = plateNumber.ToLower()
            };

           _unitOfWork.VehicleRepository.AddVehicle(newVehicle);

            if (await _unitOfWork.Complete()) return Ok(newVehicle);

            return BadRequest("Failed to save the vehicle spot");

        }

        [HttpPut]
        public async Task<ActionResult> UpdateVehicle(VehicleDto vehicleDto)
        {

            var vehicle = await _unitOfWork.VehicleRepository.GetVehicleByIdAsync(vehicleDto.VehicleId);

            vehicle.Type = vehicleDto.Type;

            _unitOfWork.VehicleRepository.Update(vehicle);

            if (await _unitOfWork.Complete()) return Ok(vehicle);

            return BadRequest("Failed to update the vehicle");
        }



        [HttpGet("find-vehicle/{plateNumber}")]
        public async Task<ActionResult<VehicleFindDto>> GetVehicle(string plateNumber)
        {
            if(plateNumber.Length == 0) return BadRequest("You have to give us your vehicle plate  number.");
            if (await _unitOfWork.VehicleRepository.VehicleExists(plateNumber))
            {
                var vehicle = await _unitOfWork.VehicleRepository.GetVehicleByPlateNumberAsync(plateNumber);
                if (vehicle.ParkingSpotForeignKey == null)
                {
                    return BadRequest("Your Vehicle is not here.");
                }
                return await _unitOfWork.VehicleRepository.GetVehicleByPlateNumberAsync(plateNumber);
            }
            return BadRequest("Your vehicle has never been here");
        }

        [HttpGet("find-vehicle")]
        public ActionResult<string> GetBadRequest()
        {
            return BadRequest("We need the plate number to store your vehicle.");
        }

    }
}