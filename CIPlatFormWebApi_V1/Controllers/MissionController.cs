using Business_Logic_Layer;
using Data_Logic_Layer.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace CIPlatFormWebApi_V1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MissionController : ControllerBase
    {
        private readonly BALMission _balMission;

        public MissionController(BALMission balMission)
        {
            _balMission = balMission;
        }

        [HttpPost]
        [Route("CreateMission")]
        public async Task<IActionResult> CreateMission([FromBody] MissionDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ResponseResult result = new ResponseResult();

            try
            {
                var creationResult = await _balMission.CreateMission(model);
                if (creationResult == "Mission with the same title and start date already exists.")
                {
                    return Conflict(new { message = creationResult });
                }
                result.Data = creationResult;
                result.Result = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
                result.Result = ResponseStatus.Error;
                return StatusCode(StatusCodes.Status500InternalServerError, result);
            }

            return Ok(result);
        }

        [HttpGet]
        [Route("GetMissions")]
        public async Task<IActionResult> GetMissionsWithDetails()
        {
            ResponseResult result = new ResponseResult();

            try
            {
                result.Data = await _balMission.GetMissionsWithDetails();
                result.Result = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
                result.Result = ResponseStatus.Error;
                return StatusCode(StatusCodes.Status500InternalServerError, result);
            }

            return Ok(result);
        }

        [HttpGet]
        [Route("GetMissionById/{MissionId}")]
        public async Task<IActionResult> GetMissionDetailsById(int MissionId)
        {
            if (MissionId <= 0)
            {
                return BadRequest("Invalid Mission ID");
            }

            ResponseResult result = new ResponseResult();

            try
            {
                result.Data = await _balMission.GetMissionDetailsById(MissionId);
                result.Result = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
                result.Result = ResponseStatus.Error;
                return StatusCode(StatusCodes.Status500InternalServerError, result);
            }

            return Ok(result);
        }

        [HttpPut]
        [Route("UpdateMission/{MissionId}")]
        public async Task<IActionResult> UpdateMission(int MissionId, [FromBody] MissionDto model)
        {
            if (MissionId <= 0)
            {
                return BadRequest("Invalid Mission ID");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ResponseResult result = new ResponseResult();

            try
            {
                var updateResult = await _balMission.UpdateMission(MissionId, model);
                if (updateResult == "Another mission with the same title already exists.")
                {
                    return Conflict(new { message = updateResult });
                }
                result.Data = updateResult;
                result.Result = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
                result.Result = ResponseStatus.Error;
                return StatusCode(StatusCodes.Status500InternalServerError, result);
            }

            return Ok(result);
        }

        [HttpDelete]
        [Route("DeleteMission/{id}")]
        public async Task<IActionResult> DeleteMission(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Invalid Mission ID");
            }

            ResponseResult result = new ResponseResult();

            try
            {
                result.Data = await _balMission.DeleteMission(id);
                result.Result = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
                result.Result = ResponseStatus.Error;
                return StatusCode(StatusCodes.Status500InternalServerError, result);
            }

            return Ok(result);
        }
    }
}
