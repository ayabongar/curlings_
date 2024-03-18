using System;
using System.Web.UI.WebControls;

public partial class CreateNewProcess : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        var userId = IncidentTrackingManager.GetInitUser();
        if (userId == 0)
        {
            Response.Redirect("~/NoAccessForm.aspx");
        }
    }
    protected void CreateProcess(object sender, EventArgs e)
    {
        if (!ValidateForm())
        {
            return;
        }

        var processName = txtsurveyName.Text;
        var maFileSize = (Convert.ToDouble(txtFileSize.Text)*1024D)*1024D;
        var processId = IncidentTrackingManager.SaveProcess(0, processName, true, txtPrefix.Text.Trim(), Convert.ToString(Math.Floor(maFileSize)), ckAddCoverPage.Checked, chkCreater.Checked, ckCanShareIncidents.Checked);

        if (processId > 0)
        {
            foreach (ListItem listItem in lbProcessOwners.Items)
            {
                SarsUser.SaveUser(listItem.Value.Trim());
                IncidentTrackingManager.AddProcessOwner(processId, listItem.Value.Trim());
                IncidentTrackingManager.AddUserToAProcess(listItem.Value.Trim(), Convert.ToString(processId), "1");
            }
            UserSelector1.Clear();
            txtsurveyName.Clear();
            IncidentTrackingManager.SaveAuditTrail(SarsUser.SID, string.Format(ResourceString.GetResourceString("createdNewProcess"), processName));

            if (processId > 0)
            {
                IncidentTrackingManager.UpdateProcessVersion(SarsUser.SID, processId.ToString());
            }
            Response.Redirect(string.Format("AddFields.aspx?procId={0}", processId));
        }
    }


    private bool ValidateForm()
    {
        if(lbProcessOwners.Items.Count == 0)
        {
            UserSelector1.Focus();
            MessageBox.Show("You must add at least one Process Admin");
            return false;
        }
        if (string.IsNullOrEmpty( txtsurveyName.Text.Trim()))
        {
            txtsurveyName.Focus();
            MessageBox.Show("Process Name is required.");
            return false;
        }
        if(string.IsNullOrEmpty(txtFileSize.Text))
        {
            txtFileSize.Focus();
            MessageBox.Show("Muximum file ulpoad size is required.");
            return false;
        }
        Decimal fSize;
        if (!Decimal.TryParse(txtFileSize.Text, out fSize))
        {
            txtFileSize.Focus();
            MessageBox.Show("Muximum file ulpoad size must be a number.");
            return false;
        }
        return true;
    }
    protected void btnAddOwner_Click(object sender, EventArgs e)
    {
        try
        {
            var sid = UserSelector1.SelectedAdUserDetails.SID;

            if(!string.IsNullOrEmpty(sid))
            {
                var item = new ListItem(UserSelector1.SelectedAdUserDetails.FoundUserName, sid);
                if(!lbProcessOwners.Items.Contains(item))
                {
                    lbProcessOwners.Items.Add(item);
                    UserSelector1.Clear();
                }
                else
                {
                    MessageBox.Show("You have already added this user.");
                }
                return;
            }

            MessageBox.Show("Please search for the user");
        }
        catch (Exception)
        {
            MessageBox.Show("Please search for the user");
        }
    }
}