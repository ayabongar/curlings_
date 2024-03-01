using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_SurveyClientImage : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    public Image SurveClientImage 
    {
        get
        {
            return this.img;
        }       

    }
    public string ImageUrl
    {
        get
        {
            return this.img.ImageUrl;
        }
        set
        {
            this.img.ImageUrl = value;
        }
    }
}