using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_AddDependantFields : IncidentTrackingPage
{
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            var parentField = IncidentTrackingManager.GetFieldById(ParentFieldID);
            if (parentField != null)
            {
                txtFieldName.SetValue(parentField.Display);
            }
            

            var options = IncidentTrackingManager.ReadQuestionOptions(this.ParentFieldID);
            if (options != null && options.Count > 0)
            {
                lstOptions.Bind(options, "OptionDescription", "MultichoiceOptionId");
                lstOptions.Items.RemoveAt(0);

                var childFields = IncidentTrackingManager.GetPossibleChildFields(ParentFieldID);
                if (childFields != null && childFields.Any())
                {
                    lstChildFields.Bind(childFields, "FieldName", "FieldId");
                    lstChildFields.Items.RemoveAt(0);
                }
            }
        }
    }

    private string ParentFieldID
    {
        get { return Request.Params["pFId"]; }
    }
    //private string FieldTypeID
    //{
    //    get { return Request.Params["ftId"]; }
    //}
    protected void Toolbar1_ButtonClicked(object sender, SCS.Web.UI.WebControls.ButtonEventArgs e)
    {
        switch (e.CommandName)
        {
            case "Add":
                {
                    var i = 0;
                    if (lstOptions.SelectedIndex > -1)
                    {
                        foreach (ListItem item in lstChildFields.Items)
                        {
                            if (item.Selected)
                            {
                                IncidentTrackingManager.RemoveChildFields(this.ProcessID, item.Value, ParentFieldID,
                                     lstOptions.SelectedValue); 
                            }
                            
                        }
                        foreach (ListItem item in lstChildFields.Items)
                        {
                            if (item.Selected)
                            {
                                var saved = IncidentTrackingManager.AddChildFields(this.ProcessID, item.Value, ParentFieldID,
                                                                       lstOptions.SelectedValue);
                                if (saved > 0)
                                {
                                    i++;
                                }
                            }
                        }

                        MessageBox.Show(i > 0 ? "Child Question(s) added successfully" : "Child Question(s) not added successfully");
                    }
                    else
                    {
                        MessageBox.Show("Please select an option on the left then select mapping questions on the right.");
                    }
                    break;
                }
            case "Back":
                {
                    Response.Redirect(String.Format("ViewFields.aspx?procId={0}", ProcessID) );
                    break;
                }
        }
    
    }
    protected void lstOptions_SelectedIndexChanged(object sender, EventArgs e)
    {
        lstChildFields.SelectedIndex = -1;
        var parentOption = lstOptions.SelectedValue;
        var childFields = IncidentTrackingManager.GetChildFields(ParentFieldID, parentOption);
        if(childFields != null && childFields.Any())
        {
            foreach (ChildField childField in childFields)
            {
                var item = lstChildFields.Items.FindByValue(childField.FieldId.ToString());
                if(item != null)
                {
                    item.Selected = true;
                }
            }
        }
    }
}