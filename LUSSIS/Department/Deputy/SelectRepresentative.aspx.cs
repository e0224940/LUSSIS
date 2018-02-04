using LUSSIS_Backend;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            updateCurrentRepresentativeLabel();
            updateAlternativeRepresentativeDropDown();
        }
    }

    protected void Button_Click(object sender, EventArgs e)
    {
        Button buttonPressed = (Button)sender;
        int newRepresentativeNo = -1;

        // Change Representative Button
        if (buttonPressed.CommandArgument == "ChangeRepresentative")
        {
            if (int.TryParse(DepRepDropDownList.SelectedValue, out newRepresentativeNo))
            {
                if (!UpdateRepresentativeController.SetNewDepartmentRepresentativeTo(Profile.EmpNo, newRepresentativeNo))
                {
                    ViewState["Error"] = "An Error Has Occured. Please Try Again After Sometime.";
                }
            }
            else
            {
                ViewState["Error"] = "Invalid Employee for Representative Selected.";
            }

            updateCurrentRepresentativeLabel();
            updateAlternativeRepresentativeDropDown();
        }

    }

    private void updateAlternativeRepresentativeDropDown()
    {
        DepRepDropDownList.DataSource = UpdateRepresentativeController
            .GetAlternativeRepresentativesFor(Profile.EmpNo)
            .Select(
                emp => new
                {
                    EmployeeDetails = emp.EmpName + "(" + emp.Email + ")",
                    EmpNo = emp.EmpNo
                }
            ).ToList();
        DepRepDropDownList.DataTextField = "EmployeeDetails";
        DepRepDropDownList.DataValueField = "EmpNo";
        DepRepDropDownList.DataBind();
    }

    private void updateCurrentRepresentativeLabel()
    {
        var representative = UpdateRepresentativeController.GetCurrentRepresentativeOf(Profile.EmpNo);
        DepRepLabel.Text = "<span>"
            + representative.EmpName
            + "</span>"
            + "<span>("
            + representative.Email
            + ")</span>";
    }
}