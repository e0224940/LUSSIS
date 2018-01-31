using LUSSIS_Backend;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Store_Supervisor_ApproveInventoryAdjustmentList : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        var pendingInvAdjList = ApproveInventoryAdjustmentController.getInvAdjList();

        approveInventoryAdjustmentListGridView.DataSource = pendingInvAdjList;
        approveInventoryAdjustmentListGridView.DataBind();
    }
}