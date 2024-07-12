using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Logic_Layer.Entity
{
    public interface IMission
    {
        Task<List<MissionDto>> GetMissionsWithDetails();
        Task<string> CreateMission(MissionDto model);
        Task<string> UpdateMission(int missionId, MissionDto model);
        Task<MissionDto> GetMissionDetailsById(int missionId);

        Task<string> DeleteMission(int id);
    }


}
