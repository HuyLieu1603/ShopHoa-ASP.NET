using ShopHoa.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopHoa.Areas.Admin.Services.Auth.Service
{
    public class AuthService
    {
        ShopHoaEntities db = new ShopHoaEntities();
        //Check Account exist
        public bool CheckExistAccount(string email,string password)
        {
            var IsExist = db.Staff.Where(user => user.Email == email && user.MatKhau==password).FirstOrDefault();
            if(IsExist == null)
                return false;
            return true;
        }
    }
}