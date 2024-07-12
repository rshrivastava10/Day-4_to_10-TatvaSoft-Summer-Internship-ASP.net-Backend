using Data_Logic_Layer;
using Data_Logic_Layer.Entity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business_Logic_Layer
{
    public class BALMission
    {
        private readonly IMission _dalMission;

        public BALMission(IMission dalMission)
        {
            _dalMission = dalMission;
        }

        public async Task<string> CreateMission(MissionDto model)
        {
            // Add business logic here if needed
            return await _dalMission.CreateMission(model);
        }

        public async Task<List<MissionDto>> GetMissionsWithDetails()
        {
            return await _dalMission.GetMissionsWithDetails();
        }

        public async Task<MissionDto> GetMissionDetailsById(int missionId)
        {
            return await _dalMission.GetMissionDetailsById(missionId);
        }

        public async Task<string> UpdateMission(int missionId, MissionDto model)
        {
            // Add business logic here if needed
            return await _dalMission.UpdateMission(missionId, model);
        }

        public async Task<string> DeleteMission(int id)
        {
            return await _dalMission.DeleteMission(id);
        }
    }
}
