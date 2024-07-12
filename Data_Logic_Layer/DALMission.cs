using Data_Logic_Layer.Entity;
using Data_Logic_Layer.Migrations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Data_Logic_Layer
{
    public class DALMission : IMission
    {
        private readonly AppDbContext _authContext;

        public DALMission(AppDbContext authContext)
        {
            _authContext = authContext;
        }

        public async Task<List<MissionDto>> GetMissionsWithDetails()
        {
            try
            {
                var missionsWithDetails = await _authContext.Missions
                    .Select(mission => new MissionDto
                    {
                        MissionId = mission.MissionId,
                        Title = mission.Title,
                        Description = mission.Description,
                        StartDate = mission.StartDate,
                        EndDate = mission.EndDate,
                        Deadline = mission.Deadline
                        // Add other properties as needed
                    })
                    .ToListAsync();

                return missionsWithDetails;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error retrieving missions with details.", ex);
            }
        }

        public async Task<MissionDto> GetMissionDetailsById(int missionId)
        {
            try
            {
                var missionWithDetails = await _authContext.Missions
                    .Where(mission => mission.MissionId == missionId)
                    .Select(mission => new MissionDto
                    {
                        MissionId = mission.MissionId,
                        Title = mission.Title,
                        Description = mission.Description,
                        StartDate = mission.StartDate,
                        EndDate = mission.EndDate,
                        Deadline = mission.Deadline
                        // Add other properties as needed
                    })
                    .FirstOrDefaultAsync();

                return missionWithDetails;
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Error retrieving mission with ID {missionId}.", ex);
            }
        }

        public async Task<string> CreateMission(MissionDto model)
        {
            try
            {               
                var mission = new MissionDto
                {
                    Title = model.Title,
                    Description = model.Description,
                    StartDate = model.StartDate,
                    EndDate = model.EndDate,
                    Deadline = model.Deadline
                    // Set other properties as needed
                };

                _authContext.Missions.Add(mission);
                await _authContext.SaveChangesAsync();

                return "Mission created successfully.";
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error creating mission.", ex);
            }
        }

        public async Task<string> UpdateMission(int missionId, MissionDto model)
        {
            try
            {
                var mission = await _authContext.Missions.FindAsync(missionId);
                if (mission == null)
                {
                    return $"Mission with ID {missionId} not found.";
                }

                mission.Title = model.Title;
                mission.Description = model.Description;
                mission.StartDate = model.StartDate;
                mission.EndDate = model.EndDate;
                mission.Deadline = model.Deadline;
                // Update other properties as needed

                await _authContext.SaveChangesAsync();

                return "Mission updated successfully.";
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Error updating mission with ID {missionId}.", ex);
            }
        }

        public async Task<string> DeleteMission(int missionId)
        {
            try
            {
                var mission = await _authContext.Missions.FindAsync(missionId);
                if (mission == null)
                {
                    return $"Mission with ID {missionId} not found.";
                }

                _authContext.Missions.Remove(mission);
                await _authContext.SaveChangesAsync();

                return "Mission deleted successfully.";
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Error deleting mission with ID {missionId}.", ex);
            }
        }
    }
}
