using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Data_Logic_Layer.Entity;

namespace Data_Logic_Layer
{
    public class DALMissionSkill : IMissionSkill
    {
        private readonly AppDbContext _context;

        public DALMissionSkill(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Skill>> GetMissionSkills()
        {
            return await _context.Skills.ToListAsync();
        }

        public async Task<string> CreateMissionSkill(Skill model)
        {
            
            _context.Skills.Add(model);
            await _context.SaveChangesAsync();
            return "Skill Created Successfully.";
        }

        public async Task<string> UpdateMissionSkill(int missionSkillId, Skill model)
        {
            var skill = await _context.Skills.FindAsync(missionSkillId);
            if (skill != null)
            {              
                skill.SkillName = model.SkillName;
                skill.Status = model.Status;
                await _context.SaveChangesAsync();
                return "Skill Updated Successfully.";
            }
            return "Skill Not Found.";
        }

        public async Task<Skill?> GetMissionSkillById(int missionSkillId)
        {
            return await _context.Skills.FindAsync(missionSkillId);
        }

        public async Task<string> DeleteMissionSkill(int id)
        {
            var skill = await _context.Skills.FindAsync(id);
            if (skill != null)
            {
                _context.Skills.Remove(skill);
                await _context.SaveChangesAsync();
                return "Skill Deleted Successfully.";
            }
            return "Skill Not Found.";
        }
    }
}
