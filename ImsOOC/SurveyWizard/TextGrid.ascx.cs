using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Sars.Systems.Data;
using System.Collections;
using System.Data;
using System.Xml;
using System.Text;
using System.IO;

public partial class SurveyWizard_TextGrid : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void Toolbar1_ButtonClicked(object sender, SCS.Web.UI.WebControls.ButtonEventArgs e)
    {
        switch (e.CommandName)
        {
            case "Rows":
                {
                    this.row_add_rows.Visible = true;
                    this.row_add_columns.Visible = false;
                    break;
                }
            case "Columns":
                {
                    if (lbRows.Items.Count == 0)
                    {
                        MessageBox.Show("You must first add rows before adding columns.");
                        return;
                    }
                    this.row_add_columns.Visible = true;
                    this.row_add_rows.Visible = false;
                    break;
                }
        }
    }
    protected void btnAddRow_Click(object sender, EventArgs e)
    {
        if (txtRowDescription.Text.Trim() == string.Empty)
        {
            MessageBox.Show("You must enter a description.");
            return;
        }
        var itm = new ListItem(txtRowDescription.Text);
        if (lbRows.Items.Contains(itm))
        {
            MessageBox.Show("You can not repeate the same description.");
        }
        else
        {
            lbRows.Items.Add(itm);
        }
    }


    protected void btnAddColumn_Click(object sender, EventArgs e)
    {
        if (txtColumnDescription.Text.Trim() == string.Empty)
        {
            MessageBox.Show("You must enter a description.");
            return;
        }
        var itm = new ListItem(txtColumnDescription.Text);
        if (lbColumns.Items.Contains(itm))
        {
            MessageBox.Show("You can not repeate the same description.");
        }
        else
        {
            lbColumns.Items.Add(itm);
        }
    }
    protected void btnRemoveRow_Click(object sender, EventArgs e)
    {
        var removeItms = lbRows.Items.Cast<ListItem>().Where(itm => itm.Selected).ToList();
        if (lbRows.Items.Count == 0)
        {
            MessageBox.Show("There are no items to remove");
            return;
        }
        if (removeItms.Count == 0)
        {
            MessageBox.Show("Please click on an item to remove.");
            return;
        }
        foreach (var itm in removeItms)
        {
            lbRows.Items.Remove(itm);
        }
    }
    protected void btnRemoveColumn_Click(object sender, EventArgs e)
    {
        var removeItms = lbColumns.Items.Cast<ListItem>().Where(itm => itm.Selected).ToList();
        if (lbColumns.Items.Count == 0)
        {
            MessageBox.Show("There are no items to remove");
            return;
        }
        if (removeItms.Count == 0)
        {
            MessageBox.Show("Please click on an item to remove.");
            return;
        }
        foreach (var itm in removeItms)
        {
            lbColumns.Items.Remove(itm);
        }
    }

    private void AddRowDescriptions(string description)
    {
        var oParams = new DBParamCollection
        {
            {"@HeadingId", 0},
            {"@Description", description},
            {"@QuestionId",Request["quesId"]},
            {"@SectionId", Request["secId"]},
            {"@QuestionnaireId", Request["aId"]}            
        };

        using (var oComm = new DBCommand("uspUPSERT_TextGridRowHeadings", QueryType.StoredProcedure, oParams, db.Connection))
        {            
            oComm.Execute();            
        }
    }

    private void AddColumnDescriptions(string description)
    {
        var oParams = new DBParamCollection
        {
            {"@HeadingId", 0},
            {"@Description", description},
            {"@QuestionId",Request["quesId"]},
            {"@SectionId", Request["secId"]},
            {"@QuestionnaireId", Request["aId"]}            
        };

        using (var oComm = new DBCommand("uspUPSERT_TextGridColumnHeadings", QueryType.StoredProcedure, oParams, db.Connection))
        {
            oComm.Execute();
        }
    }

    protected void btnSaveRows_Click(object sender, EventArgs e)
    {
        foreach (ListItem item in lbRows.Items)
        {
            AddRowDescriptions(item.Text);
        }
    }
    protected void btnSaveColumns_Click(object sender, EventArgs e)
    {
        foreach (ListItem item in lbRows.Items)
        {
            AddColumnDescriptions(item.Text);
        }
        IncidentTrackingManager.CreateXml(Convert.ToInt32(Request["quesId"]));
    }

    
}