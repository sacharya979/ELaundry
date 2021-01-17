using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ELaundry.Models
{
    public  class ItemDB
    {
       public  ELaundryDBEntities _db = new ELaundryDBEntities();
        public  List<tblGarment> GetGarmentByCatId(int catid)
        {
            return _db.tblGarments.Where(p => p.CategoryId == catid).ToList();
        }
    }
}