using OzgurSeyhanWebSitesi.Core.Models;
using OzgurSeyhanWebSitesi.Core.Services;
using OzgurSeyhanWebSitesi.Core.UnitOfWorks;
using OzgurSeyhanWebSitesi.Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OzgurSeyhanWebSitesi.Service.Services
{
    public class SpotifyService(IUnitOfWorks unitOfWorks, IGenericRepository<Spotify> repository) : Service<Spotify>(unitOfWorks, repository), ISpotfiyService
    {
    }
}
