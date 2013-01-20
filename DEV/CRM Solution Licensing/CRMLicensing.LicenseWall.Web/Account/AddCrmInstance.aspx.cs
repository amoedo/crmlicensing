using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CRMLicensing.LicenseWall.Web.Account
{
    public partial class AddCrmInstance : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void SqlDataSource1_Selecting(object sender, SqlDataSourceSelectingEventArgs e)
        {
            MembershipUser currentUser = Membership.GetUser();
            Guid id = (Guid)currentUser.ProviderUserKey;
            e.Command.Parameters["@UserID"].Value = id; 
        }

        protected void SqlDataSource2_Inserting(object sender, SqlDataSourceCommandEventArgs e)
        {
            MembershipUser currentUser = Membership.GetUser();
            Guid id = (Guid)currentUser.ProviderUserKey;
            e.Command.Parameters["@UserID"].Value = id; 
        }
    }
}