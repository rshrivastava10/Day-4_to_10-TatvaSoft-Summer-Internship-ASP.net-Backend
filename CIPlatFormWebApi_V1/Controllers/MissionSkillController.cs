using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Business_Logic_Layer;
using Data_Logic_Layer.Entity;
using Microsoft.AspNetCore.Http;
using System;

namespace CIPlatFormWebApi_V1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MissionSkillController : ControllerBase
    {
        private readonly IMissionSkill _missionSkill;

        public MissionSkillController(IMissionSkill missionSkill)
        {
            _missionSkill = missionSkill;
        }

        [HttpGet]
        [Route("GetMissionSkills")]
        public async Task<IActionResult> GetMissionSkill()
        {
            try
            {
                var skills = await _missionSkill.GetMissionSkills();
                return Ok(skills);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }

        [HttpPost]
        [Route("CreateMissionSkill")]
        public async Task<IActionResult> CreateMissionSkill([FromBody] Skill model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var result = await _missionSkill.CreateMissionSkill(model);
                return Ok(new { message = result });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }


        [HttpPut]
        [Route("UpdateMissionSkill/{missionSkillId}")]
        public async Task<IActionResult> UpdateMissionSkill(int missionSkillId, [FromBody] Skill model)
        {
            if (missionSkillId <= 0)
            {
                return BadRequest("Invalid Mission Skill ID");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var result = await _missionSkill.UpdateMissionSkill(missionSkillId, model);
                return Ok(new { message = result });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }


        [HttpGet]
        [Route("GetMissionSkillById/{missionSkillId}")]
        public async Task<IActionResult> GetMissionSkillById(int missionSkillId)
        {
            if (missionSkillId <= 0)
            {
                return BadRequest("Invalid Mission Skill ID");
            }

            try
            {
                var skill = await _missionSkill.GetMissionSkillById(missionSkillId);
                if (skill == null)
                {
                    return NotFound();
                }
                return Ok(skill);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }

        [HttpDelete]
        [Route("DeleteMissionSkill/{id}")]
        public async Task<IActionResult> DeleteMissionSkill(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Invalid Mission Skill ID");
            }

            try
            {
                var result = await _missionSkill.DeleteMissionSkill(id);
                return Ok(new { message = result });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }
    }
}
