using Sars.Systems.Data;
using Sars.Systems.Logger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_ConfigureReminders : IncidentTrackingPage
{
    protected IncidentProcess CurrentIncidentProcess;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindServiceConfig();
        }
    }

    private void BindServiceConfig()
    {
       
        CurrentIncidentProcess = CurrentProcess;
        var processConfig = IncidentTrackingManager.GetOocProcessConfiguration();
        if(processConfig != null)
        {
            var fields = processConfig[0];
            drpNotifyUsers.SelectedValue = fields.NotifyUsers.ToString().ToLower();
            radEscalateToManagers.SelectedValue = fields.EscalateToManagers.ToString().ToLower();
            radEscalateToProcessOwner.SelectedValue = fields.EscalateToProcessOwners.ToString().ToLower();
            radEscalateToDeputyComm.SelectedValue = fields.EscalateToDeputyCom.ToString().ToLower();
            radProdServer.SelectedValue = fields.IsProServer.ToString().ToLower();
            txtDeputyComEmail.SetValue(fields.DuputyComEmail);
            txtTestMails.SetValue(fields.TestEmailsGoTo);
        }       

    } 
   

    protected void Save(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            var fields = new ServiceConfig();
            fields.ProcessId = int.Parse(CurrentProcess.ProcessId.ToString());
            fields.NotifyUsers = bool.Parse(drpNotifyUsers.SelectedValue);
            fields.EscalateToManagers = bool.Parse(radEscalateToManagers.SelectedValue );
            fields.EscalateToProcessOwners = bool.Parse(radEscalateToProcessOwner.SelectedValue );
            fields.EscalateToDeputyCom = bool.Parse(radEscalateToDeputyComm.SelectedValue );
            fields.IsProServer = bool.Parse(radProdServer.SelectedValue);
            fields.DuputyComEmail = txtDeputyComEmail.Text;
            fields.TestEmailsGoTo = txtTestMails.Text;
            IncidentTrackingManager.InsertOrUpdateServiceConfig(fields);
            MessageBox.Show("The OOC Service variables update successfully");
        }
    }     
}