using System;
using System.ComponentModel;

public partial class HR_Controls_HTMLViwerPlain : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    [Bindable(true)]
    [Category("Data")]
    [Localizable(true)]
    public string Value
    {
        get { return this.dvHtml.InnerHtml; }
        set{ this.dvHtml.InnerHtml = value;}
    }
}
