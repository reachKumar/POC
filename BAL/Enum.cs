using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;

namespace BAL
{
    /// <summary>
    /// Roles in PO System
    /// </summary>
    public enum Roles
    {
        /// <summary>
        /// PO Requester
        /// </summary>
        PORequester,

        /// <summary>
        /// Supervisor
        /// </summary>
        Supervisor,

        /// <summary>
        /// Po Approver for PO value more than 25000 and less than 50000
        /// </summary>
        HOD,

        /// <summary>
        /// CFO
        /// </summary>
        CFO,

        /// <summary>
        /// COO
        /// </summary>
        COO,

        /// <summary>
        /// CEO 
        /// </summary>
        CEO,

        /// <summary>
        /// Admin
        /// </summary>
        Admin,

        /// <summary>
        /// PreApprover
        /// </summary>
        PreApprover,

        /// <summary>
        /// DeputyApprover
        /// </summary>

        DeputyApprover

        

    }
}