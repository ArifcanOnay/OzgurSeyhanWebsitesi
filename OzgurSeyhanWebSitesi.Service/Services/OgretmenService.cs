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
    public class OgretmenService(IUnitOfWorks unitOfWorks, IGenericRepository<Ogretmen> repository) : Service<Ogretmen>(unitOfWorks, repository), IOgretmenService
    {

    }
}
