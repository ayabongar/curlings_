using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class CheckParameters : System.Web.UI.Page
{
    protected List<HttpKeyNameValue> KeyNameValues;
    protected void Page_Load(object sender, EventArgs e)
    {
        KeyNameValues = new List<HttpKeyNameValue>();
        foreach (var variable in Request.ServerVariables.AllKeys)
        {
          KeyNameValues.Add(new HttpKeyNameValue
                                {
                                    Key = variable,
                                    Value = Request.ServerVariables[variable]
                                });

        }
        rptValues.DataSource = KeyNameValues;
        rptValues.DataBind();
    }
}

public class HttpKeyNameValue
{
    public string Key { get; set; }
    public string Value { get; set; }
}