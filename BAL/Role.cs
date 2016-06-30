using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BAL
{
     [Serializable]
    public class Role
    {
         public int RoleId { get; set; }

         public string RoleName { get; set; }

         public int DeptID { get; set; }
    }
}