using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    
    public class ParkingSpotController : BaseApiController
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public ParkingSpotController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

        }

        [HttpGet("all-free-spots")]
        public async Task<ActionResult<IEnumerable<ParkingSpot>>> GetParkingSpots()
        {
            var parkingSpot = await _unitOfWork.ParkingSpotRepository.GetFreeParkingSpotsAsync();

            return Ok(parkingSpot.Count());


        }
        [HttpGet("one-free-spot")]
        public async Task<ActionResult<ParkingSpotDto>> GetOneFreeParkingSpot()
        {
            var freeSpot = await _unitOfWork.ParkingSpotRepository.GetOneFreeParkingSpotAsync();
            if (freeSpot == null)
            {
                return BadRequest("There is no free spot avaiable at the moment.");
            }
            else
            {
                return Ok(freeSpot);
            }

        }

        [Authorize]
        [HttpDelete("{parkingFloorId}")]
        public async Task<ActionResult> DeleteParkingSpot(int parkingFloorId)
        {
            var parkingSpot = await _unitOfWork.ParkingSpotRepository.GetLastParkingSpot(parkingFloorId);

            _unitOfWork.ParkingSpotRepository.DeleteParkingSpot(parkingSpot);

            if (await _unitOfWork.Complete()) return Ok("deleted");

            return BadRequest("Failed to delete the Parking spot");
        }
        [Authorize]
        [HttpPost("addOneParkingSpot/{parkingFloorId}")]
        public async Task<ActionResult<CreateParkingSpotDto>> CreateParkingSlot(int parkingFloorId)
        {

            var parkingSpot = new ParkingSpot
            {
                ParkingFloorId = parkingFloorId
            };
            ParkingFloor parkingFloor = await _unitOfWork.ParkingFloorRepository.GetParkingFloorByIdAsync(parkingFloorId);
                   parkingFloor.CountSpots += 1;
           
             _unitOfWork.ParkingFloorRepository.Update(parkingFloor);
            _unitOfWork.ParkingSpotRepository.AddParkingSpot(parkingSpot, parkingFloorId);

            if (await _unitOfWork.Complete()) return Ok();

            return BadRequest("Failed to save the parking spot");

        }
        [Authorize]
        [HttpDelete("removeOneParkingSpot/{parkingFloorId}")]
        public async Task<ActionResult<CreateParkingSpotDto>> RemoveParkingSlot(int parkingFloorId)
        {

            var lastParkingSpot = await _unitOfWork.ParkingSpotRepository.GetLastParkingSpot(parkingFloorId);

            ParkingFloor parkingFloor = await _unitOfWork.ParkingFloorRepository.GetParkingFloorByIdAsync(parkingFloorId);

            if (parkingFloor.CountSpots == 1){
                return BadRequest("The parking floor should have minimun 1 parking spot.");
            }
                   parkingFloor.CountSpots -= 1;
           
             _unitOfWork.ParkingFloorRepository.Update(parkingFloor);
            _unitOfWork.ParkingSpotRepository.DeleteParkingSpot(lastParkingSpot);

            if (await _unitOfWork.Complete()) return Ok();

            return BadRequest("Failed to delete the parking spot");

        }

        [HttpPut("check-in")]
        public async Task<ActionResult<ParkingSpotChecksDto>> CheckInSpot(ParkingSpotChecksDto parkingSpotChecksDto)
        {

            var parkingSpot = await _unitOfWork.ParkingSpotRepository.GetParkingSpotByIdAsync(parkingSpotChecksDto.ParkingSpotId);
            Vehicle vehicle = await _unitOfWork.VehicleRepository.GetVehicleByIdAsync(parkingSpotChecksDto.VehicleId);
            parkingSpot.Vehicle = vehicle;
            parkingSpot.FreeSpot = false;

            _unitOfWork.ParkingSpotRepository.Update(parkingSpot);

            if (await _unitOfWork.Complete()) return Ok();

            return BadRequest("Failed to update the parking spot");
        }
        [HttpPut("check-out")]
        public async Task<ActionResult<VehicleDto>> CheckOutSpot(CheckOutDto checkOutDto)
        {
            var vehicle = await _unitOfWork.VehicleRepository.GetVehicleByIdAsync(checkOutDto.VehicleId);
            vehicle.ParkingSpot = null;
            vehicle.ParkingSpotForeignKey = null;
            _unitOfWork.VehicleRepository.Update(vehicle);

            var parkingSpot = await _unitOfWork.ParkingSpotRepository.GetParkingSpotByIdAsync(checkOutDto.ParkingSpotId);
            parkingSpot.Vehicle = null;
            parkingSpot.FreeSpot = true;

            _unitOfWork.ParkingSpotRepository.Update(parkingSpot);

            if (await _unitOfWork.Complete()) return Ok("checked-out");

            return BadRequest("Failed to update the parking spot");
        }

    }
}