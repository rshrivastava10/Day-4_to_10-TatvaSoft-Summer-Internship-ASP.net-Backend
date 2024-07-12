using System.Collections.Generic;
using System.Threading.Tasks;

namespace Data_Logic_Layer.Entity
{
    public interface IMissionSkill
    {
        Task<List<Skill>> GetMissionSkills();
        Task<string> CreateMissionSkill(Skill model);
        Task<string> UpdateMissionSkill(int missionSkillId, Skill model);
        Task<Skill> GetMissionSkillById(int missionSkillId);
        Task<string> DeleteMissionSkill(int id);
    }
}
