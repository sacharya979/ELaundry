using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ELaundry.Models
{
    public class ClosingDB
    {
        ELaundryDBEntities _db = new ELaundryDBEntities();
        public bool CheckClosingDate(int userid)
        {
            
            tblClosing tb = _db.tblClosings.Where(b => b.UserId == userid).OrderByDescending(b=>b.ClosingId).Take(1).FirstOrDefault();
            if (tb != null)
            {
                DateTime d = Convert.ToDateTime(tb.ClosingDateTime);
                string dt1 = d.ToString("d");
                string dt2 = DateTime.Today.ToString("d");
                if (dt1 == dt2)
                    return true;
                else
                    return false;
            }
            else
            {
                return false;
            }
           
        }
        
       
        }
    }
