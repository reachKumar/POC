using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.ComponentModel;

namespace POManagementASPNet
{
    public partial class NumberTextBox : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            rvSpaceNumber.Enabled = this.UseRangeValidator;
            rvSpaceNumber.Visible = this.UseRangeValidator;
            vcRangeSpaceNumber.Enabled = this.UseRangeValidator;

            vcReqSpaceNumber.Enabled = this.UseRequiredFiledValidator;
            rfvSpaceNumber.Enabled = this.UseRequiredFiledValidator;
            rfvSpaceNumber.Visible = this.UseRequiredFiledValidator;
        }

         /// <summary>
        /// The value enetered
        /// </summary>
        public string NumberValue
        {
            get { return txtSpaceNumber.Text; }
            set { txtSpaceNumber.Text = value; }
        }

        /// <summary>
        /// Range Minimum Value
        /// </summary>
        public string RangeMinimumValue { get { return rvSpaceNumber.MinimumValue; } set { rvSpaceNumber.MinimumValue = value; } }

        /// <summary>
        /// Range Maximum Value
        /// </summary>
        public string RangeMaximumValue { get { return rvSpaceNumber.MaximumValue; } set { rvSpaceNumber.MaximumValue = value; } }

        /// <summary>
        /// Range Validator Error Message
        /// </summary>
        public string RangeValidatorErrorMessage { get { return rvSpaceNumber.ErrorMessage; } set { rvSpaceNumber.ErrorMessage = value; } }

        /// <summary>
        /// Required field Validator Error Message
        /// </summary>
        public string RequiredValidatorErrorMessage { get { return rfvSpaceNumber.ErrorMessage; } set { rfvSpaceNumber.ErrorMessage = value; } }

        /// <summary>
        /// Validation Group
        /// </summary>
        public string ValidationGroup
        {
            get
            {
                return txtSpaceNumber.ValidationGroup;
            }
            set
            {
                txtSpaceNumber.ValidationGroup = value;
                rvSpaceNumber.ValidationGroup = value;
                rfvSpaceNumber.ValidationGroup = value;
                //Add more controls if any for further validation controls
            }
        }

        /// <summary>
        /// Number text box
        /// </summary>
        public TextBox NumbersBox { get { return txtSpaceNumber; } }

        /// <summary>
        /// Set to true if using Range Validator
        /// </summary>
        public bool UseRangeValidator { get; set; }

        /// <summary>
        /// Set to true if using Required Filed Validator
        /// </summary>
        public bool UseRequiredFiledValidator { get; set; }

        /// <summary>
        /// Enable or disable the control
        /// </summary>
        public bool Enabled { get { return txtSpaceNumber.Enabled; } set { txtSpaceNumber.Enabled = value; } }

        [DefaultValueAttribute("Numbers only")]
        public string WaterMarkText { get { return tweNumbers.WatermarkText; } set { tweNumbers.WatermarkText = value; } }
    }
}