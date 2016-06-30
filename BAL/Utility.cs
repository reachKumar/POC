using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace BAL
{
    public class Utility
    {

        /// <summary>
        /// Returns true if dataset has at least one table and one row. Otherwise returns false
        /// </summary>
        /// <param name="dsToValidate"></param>
        /// <returns></returns>
        public static bool HasDataSetRows(DataSet dsToValidate)
        {
            if (dsToValidate != null)
                if (dsToValidate.Tables != null)
                    if (dsToValidate.Tables.Count > 0)
                        if (dsToValidate.Tables[0].Rows != null)
                            if (dsToValidate.Tables[0].Rows.Count > 0)
                                return true;
            return false;
        }
    }
}