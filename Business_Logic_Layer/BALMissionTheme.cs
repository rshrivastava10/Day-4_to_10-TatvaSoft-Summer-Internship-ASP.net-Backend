using Data_Logic_Layer;
using Data_Logic_Layer.Entity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business_Logic_Layer
{
    public class BALMissionTheme
    {
        private readonly IMissionTheme _dalMissionTheme;

        public BALMissionTheme(IMissionTheme dalMissionTheme)
        {
            _dalMissionTheme = dalMissionTheme;
        }

        public async Task<List<Theme>> GetMissionThemes()
        {
            return await _dalMissionTheme.GetMissionThemes();
        }

        public async Task<string> CreateMissionTheme(Theme model)
        {
            
            return await _dalMissionTheme.CreateMissionTheme(model);
        }

        public async Task<string> UpdateMissionTheme(int missionThemeId, Theme model)
        {
            
            return await _dalMissionTheme.UpdateMissionTheme(missionThemeId, model);
        }

        public async Task<Theme> GetMissionThemeById(int missionThemeId)
        {
            return await _dalMissionTheme.GetMissionThemeById(missionThemeId);
        }

        public async Task<string> DeleteMissionTheme(int id)
        {
            return await _dalMissionTheme.DeleteMissionTheme(id);
        }
    }
}
