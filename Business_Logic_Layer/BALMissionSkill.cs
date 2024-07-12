using System.Collections.Generic;
using System.Threading.Tasks;
using Data_Logic_Layer.Entity;

namespace Business_Logic_Layer
{
    public class BALMissionSkill : IMissionSkill
    {
        private readonly IMissionSkill _dal;

        public BALMissionSkill(IMissionSkill dal)
        {
            _dal = dal;
        }

        public Task<List<Skill>> GetMissionSkills()
        {
            return _dal.GetMissionSkills();
        }

        public Task<string> CreateMissionSkill(Skill model)
        {
            return _dal.CreateMissionSkill(model);
        }

        public Task<string> UpdateMissionSkill(int missionSkillId, Skill model)
        {
            return _dal.UpdateMissionSkill(missionSkillId, model);
        }

        public Task<Skill> GetMissionSkillById(int missionSkillId)
        {
            return _dal.GetMissionSkillById(missionSkillId);
        }

        public Task<string> DeleteMissionSkill(int id)
        {
            return _dal.DeleteMissionSkill(id);
        }
    }
}
