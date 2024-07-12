using Data_Logic_Layer.Entity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Data_Logic_Layer
{
    public class DALMissionTheme : IMissionTheme
    {
        private readonly AppDbContext _context;

        public DALMissionTheme(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Theme>> GetMissionThemes()
        {
            return await _context.Themes.ToListAsync();
        }

        public async Task<string> CreateMissionTheme(Theme model)
        {        
            await _context.Themes.AddAsync(model);
            await _context.SaveChangesAsync();
            return "Theme Created Successfully.";
        }

        public async Task<string> UpdateMissionTheme(int missionThemeId, Theme model)
        {
            var existingTheme = await _context.Themes.FindAsync(missionThemeId);
            if (existingTheme == null)
            {
                return "Theme Not Found.";
            }
                        
            existingTheme.ThemeName = model.ThemeName;
            existingTheme.ThemeDescription = model.ThemeDescription;
            existingTheme.ThemeImage = model.ThemeImage;

            _context.Themes.Update(existingTheme);
            await _context.SaveChangesAsync();
            return "Theme Updated Successfully.";
        }

        public async Task<Theme?> GetMissionThemeById(int missionThemeId)
        {
            return await _context.Themes.FindAsync(missionThemeId);
        }

        public async Task<string> DeleteMissionTheme(int id)
        {
            var theme = await _context.Themes.FindAsync(id);
            if (theme == null)
            {
                return "Theme Not Found.";
            }

            _context.Themes.Remove(theme);
            await _context.SaveChangesAsync();
            return "Theme Deleted Successfully.";
        }
    }
}
