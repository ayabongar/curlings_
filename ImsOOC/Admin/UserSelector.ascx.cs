using System;
using System.Web.UI.WebControls;

public partial class Admin_UserSelector : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        txtSearchName.Attributes["onkeyup"] = string.Format("findAdUser(this.id,{0})", userContainer.ClientID);
    }

    public SelectedUserDetails SelectedAdUserDetails
    {
        get { return new SelectedUserDetails {FullName = FullName, SID = SID, FoundUserName = FoundUserName}; }
        set
        {
            this.txtSearchName.SetValue(value.FoundUserName);
        }
    }

    public void Clear()
    {
        txtSearchName.Clear();
    }

   public void Disable()
    {
        txtSearchName.Enabled  =false;
    }


    public override void Focus()
    {
        this.txtSearchName.Focus();
    }
    public void SetValidationColours()
    {
        if(String.IsNullOrEmpty(txtSearchName.Text.Trim()))
        {
           // this.txtSearchName.BackColor = System.Drawing.Color.Red;
            txtSearchName.BorderStyle = BorderStyle.Solid;
            txtSearchName.BorderColor =  System.Drawing.Color.Red;
       
            txtSearchName.BorderWidth = 2;
        }
        else
        {
            txtSearchName.BorderColor = System.Drawing.Color.Empty ;
            txtSearchName.BorderStyle =  BorderStyle.NotSet ;
            txtSearchName.BorderWidth = 1;
            
        }
    }
    public String FullName
    {
        get
        {
            if (!String.IsNullOrEmpty(txtSearchName.Text))
            {
                var name = txtSearchName.Text.Split("|".ToCharArray());
                if (name.Length == 2)
                {
                    return name[0].Trim();
                }
            }
            return string.Empty;
        }
    }

    public String SID
    {
        get
        {
            if (!String.IsNullOrEmpty(txtSearchName.Text))
            {
                var name = txtSearchName.Text.Split("|".ToCharArray());
                if (name.Length == 2)
                {
                    return name[1].Trim();
                }
            }
            return string.Empty;
        }
    }

    public String FoundUserName
    {
        get { return txtSearchName.Text.Trim(); }
    }
}