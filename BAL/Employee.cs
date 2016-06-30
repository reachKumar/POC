using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DAL;
using System.Data;

namespace BAL
{
    [Serializable]
    public class Employee
    {

        /// <summary>
        /// User id of Employee
        /// </summary>
        public string UserID { get; set; }

        /// <summary>
        /// Employee roles
        /// </summary>
        public List<Role> EmployeeRoles { get; set; }

        /// <summary>
        /// Check user belong to Service Delivery Group
        /// </summary>
        public bool IsServiceDeliveryGroup { get; set; }


        public Employee()
        {

        }

        public Employee(string strLoginID)
        {
          
            CommonDataHandler dbHandler = new CommonDataHandler();
            try
            {
                DataSet dsEmpdetails = dbHandler.GetEmployeeDetails(strLoginID);
                if (Utility.HasDataSetRows(dsEmpdetails))
                {
                    this.UserID = strLoginID;
                    this.IsServiceDeliveryGroup = Convert.ToBoolean(dsEmpdetails.Tables[0].Rows[0]["IsServiceDeliveryGroup"]!= DBNull.Value ? dsEmpdetails.Tables[0].Rows[0]["IsServiceDeliveryGroup"]: false);
                    this.EmployeeRoles = new List<Role>();
                    foreach (DataRow row in dsEmpdetails.Tables[0].Rows)
                    {
                        Role objRole = new Role();
                        objRole.RoleId = Convert.ToInt32(row["RoleId"]);
                        objRole.RoleName = Convert.ToString(row["RoleName"]);
                        if(row["DepartmentID"] != DBNull.Value)
                            objRole.DeptID = Convert.ToInt32(row["DepartmentID"]);

                        this.EmployeeRoles.Add(objRole);
                        objRole = null;
                    }
                }
            }
            catch (Exception ex)
            { }
        }

        /// <summary>
        /// Returns true if any one role from "roles" is assigned to employee. Otherwise false
        /// </summary>
        /// <param name="roles"></param>
        /// <returns></returns>
        public bool HasEmployeeAccess(List<Role> roles)
        {
            if (this.EmployeeRoles != null)
                if (this.EmployeeRoles.Capacity > 0)
                    if (this.EmployeeRoles.Exists(delegate(Role match)
                    {
                        return roles.Exists(delegate(Role role)
                        {
                            return match.RoleName == role.ToString();
                        }
                        );
                    }
                    ))
                        return true;
            return false;
        }

        /// <summary>
        /// Returns true if employee has access to role
        /// </summary>
        /// <param name="roles"></param>
        /// <returns></returns>
        public bool HasEmployeeAccess(Roles role)
        {
            if (this.EmployeeRoles != null)
                if (this.EmployeeRoles.Count > 0)
                    if (this.EmployeeRoles.Exists(delegate(Role match)
                    {
                        return match.RoleName == role.ToString();
                    }
                    ))
                        return true;
            return false;
        }

        /// <summary>
        /// Returns true if any one role from "roles" is assigned to employee. Otherwise false
        /// </summary>
        /// <param name="roles">Comma separated roleids. Id shall have comma on both sides</param>
        /// <returns></returns>
        public bool HasEmployeeAccess(string roles)
        {    

            if (this.EmployeeRoles != null)
                if (this.EmployeeRoles.Count > 0)
                    if (this.EmployeeRoles.Exists(delegate(Role match)
                    {
                        return roles.Contains("," + match.RoleId.ToString() + ",");
                    }
                    ))
                        return true;
            return false;
        }

        public bool IsEmployeeHOD(Int32 deptID)
        {
            if (this.EmployeeRoles != null)
                if (this.EmployeeRoles.Count > 0)
                    if (this.EmployeeRoles.Exists(delegate(Role match)
                    {
                        return match.DeptID == deptID;
                    }
                        ))
                        return true;
            return false;
        }
    }
}