using Microsoft.EntityFrameworkCore;
using OzgurSeyhanWebSitesi.Data;
using OzgurSeyhanWebSitesi.Models;

namespace OzgurSeyhanWebSitesi.Services
{
    public class OgretmenService : IOgretmenService
    {
        private readonly ApplicationDbContext _context;
        public OgretmenService(ApplicationDbContext contex)
        {
            _context = contex;
         }

        public async Task<bool> CreateOgretmenAsync(Ogretmen ogretmen)
        {
            try
            {
                ogretmen.Id = new Guid();//Yeni bir tane ıd guid atadım öğretmen üretmek için.
                ogretmen.OlusturmaTarihi = DateTime.Now;
                _context.Ogretmenler.Add(ogretmen);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> DeleteOgretmenAsync(Guid id)
        {
            try
            {
                var ogretmen = await _context.Ogretmenler.FirstOrDefaultAsync();
                if (ogretmen != null)
                {
                    _context.Ogretmenler.Remove(ogretmen);
                    await _context.SaveChangesAsync();
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }

        }

        public async Task<Ogretmen?> GetOgretmenAsync()
        {
            return await _context.Ogretmenler.FirstOrDefaultAsync();//Tek öğretmeni getirecek olan  fonksiyon.
        }

       

        public async Task<bool> UpdateOgretmenAsync(Ogretmen ogretmen)
        {

            try
            {
                _context.Ogretmenler.Update(ogretmen);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
