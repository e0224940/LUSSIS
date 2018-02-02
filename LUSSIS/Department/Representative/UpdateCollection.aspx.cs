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

        string oldLoc = UpdateCollectionPoint.SearchLocation(empNo);

        string newloc = NewLocationDDL.SelectedItem.Text;
       

        int newPoint = Int32.Parse(NewLocationDDL.SelectedItem.Value);
        if (newPoint<0 || newPoint>6)
        {
            Label4.Text = "Choose New Location.";
        }
        else
        {
           
            if (oldLoc != newloc)
            {
                UpdateCollectionPoint.UpdatePoint(empNo, newPoint);
            }
            else
            {
                Label6.Text = "Choose the New Location";
            }
            
            
            //Choose Loc and Time
            if (newPoint == 1)
            {
                newCollectionTimeText.Text = "9:30 AM";
                Label5.Text = "Stationery Store - Administration Building";
            }
            else if (newPoint == 2)
            {
                newCollectionTimeText.Text = "11:00 AM";
                Label5.Text = "Management School";
            }
            else if (newPoint == 3)
            {
                newCollectionTimeText.Text = "9:30 AM";
                Label5.Text = "Medical School";
            }
            else if (newPoint == 4)
            {
                newCollectionTimeText.Text = "11:00 AM";
                Label5.Text = "Engineering School";
            }
            else if (newPoint == 5)
            {
                newCollectionTimeText.Text = "9:30 AM";
                Label5.Text = "Science School";
            }
            else if (newPoint == 6)
            {
                newCollectionTimeText.Text = "11:00 AM";
                Label5.Text = "University Hospital";
            }
            else if (newPoint == 0)
            {
                newCollectionTimeText.Text = "9:30 AM";
                Label5.Text = "Management Store";
            }
            else
            {
                newCollectionTimeText.Text = "";
            }

            Session["NewTime"] = newCollectionTimeText.Text;
            Session["NewLoc"] = Label5.Text;

            Response.Redirect("UpdateCollectionSuccessPage.aspx");
            
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Updated'); window.location = 'UpdateCollection.aspx';", true);
        }
    }

    protected void cancel_Click(object sender, EventArgs e)
    {
        Label4.Visible = false;
    }
}
