using System.Collections.Generic;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Authorize]
    public class ParkingFloorController : BaseApiController
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public ParkingFloorController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;

        }

        [HttpPost("{spotsNumber}")]
        public async Task<ActionResult<CreateParkingFloorDto>> CreateParkingFloorBulk(int spotsNumber)
        {

            var parkingFloor = new ParkingFloor { };

            _unitOfWork.ParkingFloorRepository.AddParkingFloorBulk(parkingFloor, spotsNumber);

            if (await _unitOfWork.Complete()) return Ok(_mapper.Map<CreateParkingFloorDto>(parkingFloor));

            return BadRequest("Failed to save the Parking Floor");

        }

        [HttpDelete]
        public async Task<ActionResult> DeleteParkingFloor()
        {
            var parkingFloor = await _unitOfWork.ParkingFloorRepository.GetLastParkingFloor();

            _unitOfWork.ParkingFloorRepository.DeleteParkingFloor(parkingFloor);

            if (await _unitOfWork.Complete()) return Ok();

            return BadRequest("Failed to delete the Parking floor");
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ParkingFloor>>> GetParkingFloors()
        {
            var parkingFloor = await _unitOfWork.ParkingFloorRepository.GetParkingFloorsAsync();

            return Ok(parkingFloor);

        }
    }
}