using Application;
using Application.Models;
using Authentication.Models;
using Microsoft.EntityFrameworkCore;

namespace Authentication.Repositories
{
    public interface IApplicationRepository
    {
        UserInfoDo? GetUserInfo(ApplicationUser appUser);
        void AddUserInfo(ApplicationUser appUser, UpdateUserDo user);
        void UpdateUserInfo(ApplicationUser appUser, UpdateUserDo user);
    }

    public class ApplicationRepository : IApplicationRepository, IDisposable
    {
        private ApplicationDbContext db { get; set; }

        public ApplicationRepository(ApplicationDbContext db)
        {
            this.db = db;
        }

        public UserInfoDo? GetUserInfo(ApplicationUser appUser)
        {
            return (from ui in this.db.UserInfos.AsNoTracking()
                    where ui.Id == appUser.Id
                    select new UserInfoDo()
                    {
                        UserNumber = ui.UserNumber,
                        UserName = ui.UserName,
                        Name = ui.FirstName + " " + ui.LastName,
                        LanguageCode = ui.LanguageCode
                    }).FirstOrDefault();
        }
        public void AddUserInfo(ApplicationUser appUser, UpdateUserDo user)
        {
            try
            {
                using (var transaction = this.db.Database.BeginTransaction())
                {
                    int maxUserNumber = 0;
                    if (this.db.UserInfos.Count() > 0)
                        maxUserNumber = this.db.UserInfos.Max(x => x.UserNumber);

                    tb_UserInfo userInfo = new tb_UserInfo()
                    {
                        Id = appUser.Id,
                        UserNumber = maxUserNumber + 1,
                        UserName = appUser.UserName,
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        Remark = user.Remark,
                        LanguageCode = user.LanguageCode,
                        CreateDate = user.CreateDate.Value,
                        CreateBy = user.CreateBy.Value,
                        UpdateDate = user.CreateDate.Value,
                        UpdateBy = user.CreateBy.Value
                    };
                    this.db.UserInfos.Add(userInfo);

                    this.db.SaveChanges();
                    transaction.Commit();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void UpdateUserInfo(ApplicationUser appUser, UpdateUserDo user)
        {
            try
            {
                tb_UserInfo? userInfo = this.db.UserInfos
                                            .Where(x => x.Id == appUser.Id)
                                            .FirstOrDefault<tb_UserInfo>();
                if (userInfo != null)
                {
                    userInfo.FirstName = user.FirstName;
                    userInfo.LastName = user.LastName;
                    userInfo.Remark = user.Remark;
                    userInfo.LanguageCode = user.LanguageCode;
                    userInfo.UpdateDate = user.UpdateDate.Value;
                    userInfo.UpdateBy = user.UpdateBy.Value;

                    this.db.SaveChanges();
                }
            }
            catch (Exception)
            {
                throw;
            }
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
