using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace POManagementASPNet
{
    public partial class UIUtility : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Returns true if gidview's selected row has value. Otherwise returns false
        /// </summary>
        /// <param name="gv"></param>
        /// <param name="rowIndex"></param>
        /// <param name="columnName"></param>
        /// <param name="selectedValue">Out parameter. Value of datakey will be set if found otherwise ZERO 0.</param>
        /// <returns></returns>
        internal static bool TryParseDakaKeyValue(ref GridView gv, int rowIndex, string columnName, out string selectedValue)
        {
            selectedValue = string.Empty;
            if (TryParseDakaKeyValue(ref gv, rowIndex, columnName))
            {
                selectedValue = Convert.ToString(gv.DataKeys[rowIndex].Values[columnName]);
                return true;
            }
            else
                return false;
        }

        /// <summary>
        /// Returns true if gidview's selected row has value. Otherwise returns false
        /// </summary>
        /// <param name="gv"></param>
        /// <param name="rowIndex"></param>
        /// <param name="columnName"></param>
        /// <param name="selectedValue">Out parameter. Value of datakey will be set if found otherwise ZERO 0.</param>
        /// <returns></returns>
        internal static bool TryParseDakaKeyValue(ref GridView gv, int rowIndex, string columnName, out long selectedValue)
        {
            selectedValue = 0;
            if (TryParseDakaKeyValue(ref gv, rowIndex, columnName))
            {
                selectedValue = Convert.ToInt64(gv.DataKeys[rowIndex].Values[columnName]);
                return true;
            }
            else
                return false;
        }

        /// <summary>
        /// Returns true if gidview's selected row has value. Otherwise returns false
        /// </summary>
        /// <param name="gv"></param>
        /// <param name="rowIndex"></param>
        /// <param name="columnName"></param>
        /// <returns></returns>
        internal static bool TryParseDakaKeyValue(ref GridView gv, int rowIndex, string columnName)
        {
            if (gv.DataKeys != null)
                if (gv.DataKeys.Count >= rowIndex)
                    if (gv.DataKeys[rowIndex].Values.Contains(columnName))
                        if (gv.DataKeys[rowIndex].Values[columnName] != null)
                            return true;
            return false;
        }

        /// <summary>
        /// Returns true if gidview's selected row has value. Otherwise returns false
        /// </summary>
        /// <param name="gv"></param>
        /// <param name="rowIndex"></param>
        /// <param name="columnName"></param>
        /// <param name="selectedValue">Out parameter. Value of datakey will be set if found otherwise ZERO 0.</param>
        /// <returns></returns>
        internal static bool TryParseDakaKeyValue(ref GridView gv, int rowIndex, string columnName, out int selectedValue)
        {
            selectedValue = 0;
            if (TryParseDakaKeyValue(ref gv, rowIndex, columnName))
            {
                selectedValue = Convert.ToInt32(gv.DataKeys[rowIndex].Values[columnName]);
                return true;
            }
            else
                return false;
        }
    }
}