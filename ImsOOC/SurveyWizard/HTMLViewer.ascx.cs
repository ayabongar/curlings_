using System;
using System.ComponentModel;

public partial class HTMLViewer : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    [Bindable(true)]
    [Category("Data")]
    [Localizable(true)]

    public string HtmlString
    {
        get { return this.dvHtmlInput.InnerHtml; }
        set { this.dvHtmlInput.InnerHtml = value; }
    }
}
