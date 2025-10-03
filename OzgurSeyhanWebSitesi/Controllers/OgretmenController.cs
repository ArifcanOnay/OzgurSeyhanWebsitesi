using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OzgurSeyhanWebSitesi.Data;
using OzgurSeyhanWebSitesi.Models;

namespace OzgurSeyhanWebSitesi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class OgretmenController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public OgretmenController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Ogretmen>>> GetOgretmenler()
        {
            return await _context.Ogretmenler.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Ogretmen>> GetOgretmen(int id)
        {
            var ogretmen = await _context.Ogretmenler.FindAsync(id);

            if (ogretmen == null)
            {
                return NotFound();
            }

            return ogretmen;
        }
    }
}
