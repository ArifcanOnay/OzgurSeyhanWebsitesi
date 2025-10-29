using OzgurSeyhanWebSitesi.Core.Models;
using OzgurSeyhanWebSitesi.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OzgurSeyhanWebSitesi.Repository.Repositories
{
    public class KursRepository(AppDbContext context):GenericRepository<Kurs>(context),IKursRepository
    {
    }
}
