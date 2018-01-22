using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LUSSIS_Backend;

public partial class Department_Representative_UpdateCollection : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        int empno = Profile.EmpNo;
       
        string oldPoint =  UpdateCollectionPoint.SearchLocation(empno);
        oldLocationText.Text = oldPoint;
        
    }

    
    protected void ChooseLocation_DDList(object sender, EventArgs e)
    {

        int value = Int32.Parse(NewLocationDDL.SelectedItem.Value);
        if (value == 1 )
        {
            newCollectionTimeText.Text = "9:30 AM";
        }
        else if(value == 2)
        {
            newCollectionTimeText.Text = "11:00 AM";
        }
        else if(value == 3)
        {
            newCollectionTimeText.Text = "9:30 AM";
        }
        else if(value == 4)
        {
            newCollectionTimeText.Text = "11:00 AM";
        }
        else if(value == 5)
        {
            newCollectionTimeText.Text = "9:30 AM";
        }
        else if (value == 6)
        {
            newCollectionTimeText.Text = "11:00 AM";
        }
        else if(value == 0 )
        {
            newCollectionTimeText.Text = "9:30 AM";
        }
        else
        {
            newCollectionTimeText.Text = "";
        }
       

    }

    protected void confirm_Click(object sender, EventArgs e)
    {
        int empNo = Profile.EmpNo;
        //try
        //{
        int newPoint = Int32.Parse(NewLocationDDL.SelectedItem.Value);
        UpdateCollectionPoint.UpdatePoint(empNo, newPoint);
            Response.Redirect("Default.aspx");
        //}
        //catch (Exception exp)
        //{
        //    Response.Write(exp.ToString());
        //}
    }
}