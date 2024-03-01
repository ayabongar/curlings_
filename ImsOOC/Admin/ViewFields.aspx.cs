using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_ViewFields : IncidentTrackingPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(ProcessID))
        {
            Response.Redirect("~/InvalidProcessOrIncident.aspx");
        }
        var userId = IncidentTrackingManager.GetInitUser();
        if (userId == 0)
        {
            Response.Redirect("SelectNormalUserProcess.aspx");
        }
        if (!IsPostBack)
        {
            UpdateQuestions();
        }
    }

    private void UpdateQuestions()
    {
        var fields = IncidentTrackingManager.GetProcessField(this.ProcessID);
        if (fields != null && fields.Any())
        {
            ViewState["fc"] = fields.Count;
            gvProcessFields.Bind(fields);
        }
        else
        {
            gvProcessFields.Bind(null);
        }
    }

    protected void gvProcessFields_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //e.Row.Attributes.Add("onclick",Page.ClientScript.GetPostBackEventReference((Control) sender,"Select$" + e.Row.RowIndex));

            var desctiontion = DataBinder.Eval(e.Row.DataItem, "FieldName").ToString();
            e.Row.Attributes.Add("onmouseover", "this.style.cursor='cursor'");
            e.Row.Attributes.Add("title",
                                 "cssbody=[dvbdy1] cssheader=[dvhdr1] header=[<b><font color='blue'>Full Field Name</font></b>] body=[<font color='red'><b>" +
                                 desctiontion + "</b></font>]");

            var ddlOrder = e.Row.FindControl("ddlOrder") as DropDownList;
            if (ddlOrder != null)
            {
                ddlOrder.DataSource = GetNumbers();
                ddlOrder.DataBind();
                ddlOrder.Items.Insert(0, new ListItem("Select Order", "-100"));
                var pf = e.Row.DataItem as ProcessField;
                if (pf != null) ddlOrder.SelectItemByValue(pf.SortOrder.ToString());
            }
        }
    }

    private IEnumerable<int> GetNumbers()
    {
        var numFields = ViewState["fc"] != null
                            ? Convert.ToInt32(ViewState["fc"])
                            : IncidentTrackingManager.GetProcessField(ProcessID).Count;

        var order = new List<int>();
        for (var i = 1; i <= numFields; i++)
        {
            order.Add(i);
        }
        return order;
    }

    protected void gvProcessFields_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvProcessFields.NextPage(IncidentTrackingManager.GetProcessField(this.ProcessID), e.NewPageIndex);
    }

    protected void ModifyField()
    {

        object fieldId;
        if (gvProcessFields.SelectedDataKey != null &&
            (fieldId = gvProcessFields.SelectedDataKey["FieldId"]) != null)
        {
            Response.Redirect(String.Format("ModifyProcessField.aspx?fieldId={0}&procId={1}", fieldId,
                                            this.ProcessID));
        }
    }

    protected void Toolbar1_ButtonClicked(object sender, SCS.Web.UI.WebControls.ButtonEventArgs e)
    {

        switch (e.CommandName)
        {
            case "Back":
                {
                    Response.Redirect("ModifyProcess.aspx");
                    break;
                }
            case "Delete":
                {
                    if (!CanContinue())
                        return;
                    object fieldId;
                    if (gvProcessFields.SelectedDataKey != null &&
                        (fieldId = gvProcessFields.SelectedDataKey.Value) != null)
                    {
                        RemoveProcessField(fieldId.ToString());
                    }
                    break;
                }
            case "Modify":
                {
                    if (!CanContinue())
                        return;
                    ModifyField();
                    break;
                }
            case "ConfigureChildFields":
                {
                    if (!CanContinue())
                        return;
                    object fieldId;
                    object fieldTypeId;
                    if (gvProcessFields.SelectedDataKey != null && (fieldId = gvProcessFields.SelectedDataKey.Value) != null && (fieldTypeId = gvProcessFields.SelectedDataKey["FieldTypeId"]) != null)
                    {
                       // Response.Redirect(String.Format("AddDependantFields.aspx?procId={0}&pFId={1}&ftId={2}", ProcessID, fieldId, fieldTypeId));
                        Response.Redirect(String.Format("AddDependantFields.aspx?procId={0}&pFId={1}", ProcessID, fieldId));

                    }
                    break;
                }
                  case "AddListItems":
                {
                    if (!CanContinue())
                        return;
                    ModifyField();
                    break;
                }
        }
    }

    private bool CanContinue()
    {
        if (gvProcessFields.SelectedIndex == -1)
        {
            if (gvProcessFields.Rows.Count == 1)
            {
                gvProcessFields.SelectedIndex = 0;
            }
            else
            {
                MessageBox.Show("Please click on a field to select it before you can continue.");
                return false;
            }

        }
        return true;
    }

    private void RemoveProcessField(string fielId)
    {
        var deleted = IncidentTrackingManager.DeleteProcessField(fielId);
        if(deleted > 0)
        {
            UpdateQuestions();
            MessageBox.Show("Field removed successfully.");
        }
    }
  
    protected void SelectedOrderChanges(object sender, EventArgs e)
    {
        var ddl = sender as DropDownList;
        if (ddl != null)
        {
            if (ddl.SelectedIndex > 0)
            {
                var row = ddl.Parent.Parent as GridViewRow;
                if (row != null)
                {
                    gvProcessFields.SelectRow(row.RowIndex);

                    if (gvProcessFields.SelectedDataKey != null)
                    {
                        var fieldId = gvProcessFields.SelectedDataKey["FieldId"];

                        if (IncidentTrackingManager.ChangeFieldOrder(fieldId.ToString(), ddl.SelectedValue, this.ProcessID) > 0)
                        {
                            UpdateQuestions();
                            MessageBox.Show("Order changed.");
                        }
                    }
                }
            }
        }
    }
    protected void gvProcessFields_SelectedIndexChanged(object sender, EventArgs e)
    {
        var incidents = CurrentProcess.Incidents;
        if(incidents != null && incidents.Any())
        {
            Toolbar1.Items[3].Visible = false;
        }
        else
        {
            Toolbar1.Items[3].Visible = true;
        }
    }
}