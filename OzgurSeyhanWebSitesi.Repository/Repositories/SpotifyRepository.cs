using OzgurSeyhanWebSitesi.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OzgurSeyhanWebSitesi.Repository.Repositories
{
   public class SpotifyRepository(AppDbContext context):GenericRepository<Spotify>(context), ISpotifyRepository
    {
    }
}
