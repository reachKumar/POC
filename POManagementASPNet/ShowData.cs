using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Globalization;

namespace POManagementASPNet
{
    public class ShowData
    {
        SqlConnection sqlcon;
        SqlCommand sqlcmd;

        public string DatabaseCredientials = ConfigurationManager.ConnectionStrings["ShipToAdd"].ToString();

        public string City
        {
            get;
            set;
        }
        public string ShipToAddress
        {
            get;
            set;
        }
        public string VAT
        {
            get;
            set;
        }
        public int ID
        {
            get;
            set;
        }
        public string ModifiedBy
        {
            get;
            set;
        }
        public string MessageError
        {
            get;
            set;

        }
        public int RetOut
        {
            get;
            set;

        }
        public string CreatedBy 
        {
            get;
            set;
        }
        public string RequesterName
        {
            get;
            set;
        }

        public string RequesterType
        {
            get;
            set;
        }

        public string Email
        {
            get;
            set;
        }
        public string UserID
        {
            get;
            set;
        }
        public string Role
        {
            get;
            set;
        }
        public int IsActive
        {
            get;
            set;
        }
       
        public void UpdateShipTo()
        {
           try
            {
                sqlcon = new SqlConnection(DatabaseCredientials);
                sqlcon.Open();

                sqlcmd = new SqlCommand();
                sqlcmd.Connection = sqlcon;
                sqlcmd.CommandType = CommandType.StoredProcedure;
                sqlcmd.CommandText = "dbo.Upd_ShipTo";

                sqlcmd.Parameters.Add("@ShipToID", SqlDbType.Int).Value = ID;
                sqlcmd.Parameters.Add("@City", SqlDbType.VarChar, 255).Value = City;
                sqlcmd.Parameters.Add("@Vat", SqlDbType.VarChar, 255).Value = VAT;
                sqlcmd.Parameters.Add("@Address", SqlDbType.VarChar, 255).Value = ShipToAddress;
                sqlcmd.Parameters.Add("@ModifiedBy", SqlDbType.VarChar, 255).Value = ModifiedBy;

                sqlcmd.Parameters.Add("@Retout", SqlDbType.Int).Direction = ParameterDirection.Output;

                sqlcmd.ExecuteNonQuery();

                RetOut = Convert.ToInt32(sqlcmd.Parameters["@Retout"].Value.ToString());

            }

            catch (Exception ex)
            {
                MessageError = ex.Message.ToString();
            }

        }

        public void InsertShipTo()
        {
            try
            {
                sqlcon = new SqlConnection(DatabaseCredientials);
                sqlcon.Open();

                sqlcmd = new SqlCommand();
                sqlcmd.Connection = sqlcon;
                sqlcmd.CommandType = CommandType.StoredProcedure;
                sqlcmd.CommandText = "dbo.InsUp_ShipTo";

                sqlcmd.Parameters.Add("@City", SqlDbType.VarChar, 255).Value = City;
                sqlcmd.Parameters.Add("@Vat", SqlDbType.VarChar, 255).Value = VAT;
                sqlcmd.Parameters.Add("@Address", SqlDbType.VarChar, 255).Value = ShipToAddress;
                sqlcmd.Parameters.Add("@CreatedBy", SqlDbType.VarChar, 255).Value = CreatedBy;

                sqlcmd.Parameters.Add("@Retout", SqlDbType.Int).Direction = ParameterDirection.Output;

                sqlcmd.ExecuteNonQuery();

                RetOut = Convert.ToInt32(sqlcmd.Parameters["@Retout"].Value.ToString());

            }

            catch (Exception ex)
            {
                MessageError = ex.Message.ToString();
            }
        }

        public void UpdateRequester()
        {
            try
            {
                sqlcon = new SqlConnection(DatabaseCredientials);
                sqlcon.Open();

                sqlcmd = new SqlCommand();
                sqlcmd.Connection = sqlcon;
                sqlcmd.CommandType = CommandType.StoredProcedure;
                sqlcmd.CommandText = "dbo.Upd_Requester";


                sqlcmd.Parameters.Add("@EmployeeName", SqlDbType.VarChar, 255).Value = RequesterName;
                sqlcmd.Parameters.Add("@Role", SqlDbType.VarChar, 255).Value = Role;
                sqlcmd.Parameters.Add("@ModifiedBy", SqlDbType.VarChar, 255).Value = ModifiedBy;

                sqlcmd.Parameters.Add("@Retout", SqlDbType.Int).Direction = ParameterDirection.Output;

                sqlcmd.ExecuteNonQuery();

                RetOut = Convert.ToInt32(sqlcmd.Parameters["@Retout"].Value.ToString());

            }

            catch (Exception ex)
            {
                MessageError = ex.Message.ToString();
            }

        }

        public void InsertRequester()
        {
            try
            {
                sqlcon = new SqlConnection(DatabaseCredientials);
                sqlcon.Open();

                sqlcmd = new SqlCommand();
                sqlcmd.Connection = sqlcon;
                sqlcmd.CommandType = CommandType.StoredProcedure;
                sqlcmd.CommandText = "dbo.Ins_Requester";

                sqlcmd.Parameters.Add("@RequesterName", SqlDbType.VarChar, 255).Value = RequesterName;
                sqlcmd.Parameters.Add("@Email", SqlDbType.VarChar, 255).Value = Email;
                sqlcmd.Parameters.Add("@UserID", SqlDbType.VarChar, 255).Value = UserID;
                sqlcmd.Parameters.Add("@Role", SqlDbType.VarChar, 255).Value = Role;
                sqlcmd.Parameters.Add("@CreatedBy", SqlDbType.VarChar, 255).Value = CreatedBy;

                sqlcmd.Parameters.Add("@Retout", SqlDbType.Int).Direction = ParameterDirection.Output;

                sqlcmd.ExecuteNonQuery();

                RetOut = Convert.ToInt32(sqlcmd.Parameters["@Retout"].Value.ToString());

            }

            catch (Exception ex)
            {
                MessageError = ex.Message.ToString();
            }
        }

        public void DeleteShipTo()
        {
            try
            {
                sqlcon = new SqlConnection(DatabaseCredientials);
                sqlcon.Open();

                sqlcmd = new SqlCommand();
                sqlcmd.Connection = sqlcon;
                sqlcmd.CommandType = CommandType.StoredProcedure;
                sqlcmd.CommandText = "Del_ShipTo";
                sqlcmd.Parameters.Add("@ShipToID", SqlDbType.Int).Value = ID;
                sqlcmd.Parameters.Add("@ModifiedBy", SqlDbType.VarChar, 255).Value = ModifiedBy;
                sqlcmd.Parameters.Add("@Retout", SqlDbType.Int).Direction = ParameterDirection.Output;

                sqlcmd.ExecuteNonQuery();

                RetOut = Convert.ToInt32(sqlcmd.Parameters["@Retout"].Value.ToString());

            }

            catch (Exception ex)
            {
                MessageError = ex.Message.ToString();
            }
        }

        public void DeleteRequester()
        {
            try
            {
                sqlcon = new SqlConnection(DatabaseCredientials);
                sqlcon.Open();

                sqlcmd = new SqlCommand();
                sqlcmd.Connection = sqlcon;
                sqlcmd.CommandType = CommandType.StoredProcedure;
                sqlcmd.CommandText = "Del_Requester";
                sqlcmd.Parameters.Add("@RequesterID", SqlDbType.Int).Value = ID;
                sqlcmd.Parameters.Add("@ModifiedBy", SqlDbType.VarChar, 255).Value = ModifiedBy;
                sqlcmd.Parameters.Add("@Retout", SqlDbType.Int).Direction = ParameterDirection.Output;

                sqlcmd.ExecuteNonQuery();

                RetOut = Convert.ToInt32(sqlcmd.Parameters["@Retout"].Value.ToString());

            }

            catch (Exception ex)
            {
                MessageError = ex.Message.ToString();
            }
        }

      }
    }
