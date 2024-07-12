using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Logic_Layer.Entity
{
    public interface IMissionTheme
    {
        Task<List<Theme>> GetMissionThemes();
        Task<string> CreateMissionTheme(Theme model);
        Task<string> UpdateMissionTheme(int missionThemeId, Theme model);
        Task<Theme> GetMissionThemeById(int missionThemeId);
        Task<string> DeleteMissionTheme(int id);

    }
}
