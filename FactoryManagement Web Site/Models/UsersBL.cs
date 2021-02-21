using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FactoryManagement_Web_Site.Models
{
    public class UsersBL
    {
        factoryDBEntities db = new factoryDBEntities();

        public User AuthorizeUser(string usrname, string pswrd)
        {
            return db.Users.Where(x => x.UserName == usrname && x.Password == pswrd).FirstOrDefault();
        }

        public void UpdateActions(UserLogined u)
        {
            u.NumOfActions = u.NumOfActions - 1;
        }

    }
}