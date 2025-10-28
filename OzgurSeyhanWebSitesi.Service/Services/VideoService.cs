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
    public class VideoService(IUnitOfWorks unitOfWorks, IGenericRepository<Video> repository) : Service<Video>(unitOfWorks, repository), IVideoService
    {
    }
}
