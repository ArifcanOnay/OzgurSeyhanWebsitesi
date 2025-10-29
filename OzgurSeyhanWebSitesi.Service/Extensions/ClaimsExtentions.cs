using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace OzgurSeyhanWebSitesi.Service.Extensions
{
    public  static class ClaimsExtentions
    {
        public static  void AddName(this ICollection<Claim> claims,string name)
        {
            claims.Add(new Claim(ClaimTypes.Name, name));
        }
        public static void AddRoles(this ICollection<Claim> claims, string role)
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }   
    }
}
