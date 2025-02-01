using Application;
using Application.Models;
using Authentication.Constants;
using Authentication.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Security;

namespace Authentication.Repositories
{
    public interface ISSS091Repository
    {
        List<UserLogTrailDo>? GetUerLogTrails(UserLogTrailCriteriaDo appUser);
    }

    public class SSS091Repository : ISSS091Repository, IDisposable
    {
        private ApplicationDbContext db { get; set; }

        public SSS091Repository(ApplicationDbContext db)
        {
            this.db = db;
        }


        public List<UserLogTrailDo>? GetUerLogTrails(UserLogTrailCriteriaDo appUser)
        {

            return (from ul in this.db.Userlogtrails.AsNoTracking()
                    join ui in this.db.UserInfos.AsNoTracking()
                    on ul.user_id.ToString() equals ui.Id
                    where ui.UserName.ToLower().Contains(appUser.username.ToLower())
                          && ul.create_date >= appUser.date_from && ul.create_date <= appUser.date_to
                    select new UserLogTrailDo()
                    {
                        username = ui.UserName,
                        action_name = ul.action_name,
                        data_before = ul.data_before,
                        data_after = ul.data_after,
                        create_date = ul.create_date,
                        create_user = ul.create_user,
                    }).ToList();


        }

        protected void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (db != null)
                {
                    db.Dispose();
                    db = null;
                }
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
