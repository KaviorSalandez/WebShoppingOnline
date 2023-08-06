using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebShoppingOnline.Models;

namespace WebShoppingOnline.Common
{
    public class SettingHelper
    {
        private static ApplicationDbContext db = new ApplicationDbContext();   

        public static string GetValue(string key)
        {
            var item = db.SystemSettings.SingleOrDefault(x => x.SettingKey.ToLower().Equals(key.ToLower()));
            if(item != null)
            {
                return item.SettingValue; 
            }
            else
            {
                return "";
            }
        }
    }
}