using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_SearchFilterTypes : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    public void ShowInputs(string filterType,string description= null)
    {
        HideAll();
        switch (filterType)
        {
            case "0":
                {
                    break;
                }
            case "DueDate":
            case "DateRegistered":
            {
                lblDescription.InnerText = "Date Registered";
                    row_RegisteredDate.Visible = true;
                    break;
                }
            case "RegisteredBy":
            case "AssignedTo":
                {
                    lblDescription.InnerText = "AssignedTo/RegisteredBy";
                    row_user.Visible = true;
                    break;
                }
            case "IncidentNumber":
                {
                    row_ReferenceNo.Visible = true;
                    lblDescription.InnerText = "Incident Number";
                    break;
                }
            case "Subject":
                {
                    row_ReferenceNo.Visible = true;
                    lblDescription.InnerText = "Subject";
                    break;
                }

            default:
                {
                    HideAll();
                    row_ReferenceNo.Visible = true;
                    lblDescription.InnerText = description;
                    break;
                }
        }
    }
    private void HideAll()
    {
        CrlScreen();
        row_ReferenceNo.Visible = false;
        row_RegisteredDate.Visible = false;
        row_user.Visible = false;
    }

    private void CrlScreen()
    {
        this.txtFromDate.Clear();
        this.txtToDate.Clear();
        this.txtRefNo.Clear();
        this.UserSelector1.Clear();
    }

    public string FromDate
    {
        get { return this.txtFromDate.Text; }
    }
    public string ToDate
    {
        get { return this.txtToDate.Text; }
    }
    public string ReferenceNumber
    {
        get { return this.txtRefNo.Text; }
    }
    public string SID
    {
        get { return this.UserSelector1.SelectedAdUserDetails.SID; }
    }
}