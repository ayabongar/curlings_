using System;
using Sars.Systems.Data;

public partial class SystemAdmins_ReportErrors : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Session.Remove("quesnn_data");
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            if (!IsValidForm())
                return;
            
            var logged = LogABug();
            if(logged > 0)
            {
                MessageBox.Show("Error sent to the administrators.");
                return;
            }
           MessageBox.Show("There was a problem saving your error description.");

        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }
    }

    private int LogABug()
    {
        var oParams = new DBParamCollection
                          {
                              {"@Description", txtErrorDescription.Text},
                              {"@SurveyName", txtsurveyName.Text},
                          };
        using (var oCommand = new DBCommand("uspINSERT_ReportedBugs", QueryType.StoredProcedure, oParams, db.Connection))
        {
            var added = oCommand.Execute();
            txtsurveyName.Enabled = false;
            txtErrorDescription.Enabled = false;
            btnSubmit.Enabled = false;
            return added;
        }
    }
    private bool IsValidForm()
    {
        if (txtErrorDescription.Text.Length < 10)
        {
            MessageBox.Show("Please type in a full description of the error so that the administrators can help you.");
            return false;
        }
        if (string.IsNullOrEmpty(txtsurveyName.Text))
        {
            MessageBox.Show("Please provide the name of the survey you were working with.");
                return false;
        }
        return true;
    }
}