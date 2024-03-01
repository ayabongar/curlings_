using System;

public partial class TestUserControls : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void ShowFullName_Click(object sender, EventArgs e)
    {
        MessageBox.Show(UserSelector1.FullName);
    }
    protected void ShowSID_Click(object sender, EventArgs e)
    {
        MessageBox.Show(UserSelector1.SID);
    }
    protected void ShowAll_Click(object sender, EventArgs e)
    {
        MessageBox.Show(UserSelector1.FoundUserName);
    }
}