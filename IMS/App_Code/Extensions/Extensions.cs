using System.Collections;
using System.Data;
using System.Web.UI.HtmlControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace System.Web.UI.WebControls
{


    public static class Extensions
    {

        public static void RegisterClientScriptFiles(this Page page, List<string> fileNames)
        {
            foreach (var file in fileNames)
            {
                using (var js = new HtmlGenericControl("script"))
                {
                    js.Attributes.Add("type", "text/javascript");
                    js.Attributes.Add("src",
                                       string.Format("{0}/{1}/{2}", HttpContext.Current.Request.ApplicationPath, Configurations.ScriptFolder,
                                                     file));
                    page.Header.Controls.Add(js);
                }
            }
        }

        public static void RegisterCSSFiles(this Page page, List<string> fileNames)
        {
            foreach (var file in fileNames)
            {
                using (var css = new HtmlGenericControl("link"))
                {
                    css.Attributes.Add("type", "text/css");
                    css.Attributes.Add("rel", "stylesheet");
                    css.Attributes.Add("href",
                                        string.Format("/{0}/{1}/{2}", HttpContext.Current.Request.ApplicationPath,
                                                      Configurations.StylesFolder, file));
                    page.Header.Controls.Add(css);
                }
            }
        }

        public static Int32 ToInt32(this string value)
        {
            Int32 newValue;
            if (Int32.TryParse(value, out newValue))
                return newValue;
            return 0;
        }
    }
}