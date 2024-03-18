using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Common;

/// <summary>
/// Utility class for CMS progect
/// </summary>
public class Utils
{
    public static string getshortString(string longNotes)
    {
        if (longNotes.Length > 70)
            return longNotes.Substring(0, 70) + "...";
        else
            return longNotes;
    }
    public static string GetLongString(string longNotes)
    {
        if (longNotes.Length > 110)
            return longNotes.Substring(0, 110) + "...";
        else
            return longNotes;
    }
    public static bool IsValidSID(string SID)
    {
        if (String.IsNullOrEmpty(SID))
        {
            return false;
        }
        if (SID.Length != 8)
        {
            return false;
        }
        if (SID[0].ToString().ToUpper() != "S")
        {
            return false;
        }
        if (!IsNumeric(SID.Substring(1, 3)))
        {
            return false;
        }
        return true;
    }
    public static bool IsNumeric(string value)
    {
        foreach (char c in value)
        {
            if (!Char.IsDigit(c))
                return false;
        }
        return true;
    }
    public static bool IsNumeric(TextBox value)
    {
        return IsNumeric(value.Text);
    }
    public static bool IsValidCompanyRegNumber(string regNumber)
    {
        var segments = regNumber.Split("/".ToCharArray());
        if (segments.Length != 3)
            return false;
        if (segments.Any(segment => !IsNumeric(segment)))
        {
            return false;
        }
        if (segments[0].Length != 4)
            return false;
        if (segments[1].Length != 6)
            return false;
        if (segments[2].Length != 2)
            return false;
        return true;
    }
    public static bool IsMoney(string money)
    {
        var _money = 0D;
        var ismoney = Double.TryParse(money, NumberStyles.Currency, null, out _money);
        return ismoney;
    }
    public static bool IsDecimal(string value)
    {
        var holder = 0M;
        return Decimal.TryParse(value, out holder);
    }

    public static bool IsTelephonePrefixZero(string value)
    {
        return value.StartsWith("0");
    }
    public static void SelectItemByText(RadioButtonList lst, string text)
    {
        var itm = lst.Items.FindByText(text);
        if (itm == null) return;
        var index = lst.Items.IndexOf(itm);
        lst.SelectedIndex = index;
    }
    public static void SelectItemByValue(RadioButtonList lst, string value)
    {
        var itm = lst.Items.FindByValue(value);
        if (itm != null)
        {
            int index = lst.Items.IndexOf(itm);
            lst.SelectedIndex = index;
        }
    }
    public static ListItem SelectItemByText(DropDownList lst, string text)
    {
        var itm = lst.Items.FindByText(text);
        if (itm != null)
        {
            var index = lst.Items.IndexOf(itm);
            lst.SelectedIndex = index;
            return itm;
        }
        return itm;
    }
    public static ListItem SelectItemByValue(DropDownList lst, string value)
    {
        if (null == lst)
            return null;
        var itm = lst.Items.FindByValue(value);
        if (itm != null)
        {
            var index = lst.Items.IndexOf(itm);
            lst.SelectedIndex = index;
            return itm;
        }
        return itm;
    }
    public static void Alert(string message, Page page)
    {
        var script = "<script type='text/javascript'>alert('" + message + "');</script>";
        page.ClientScript.RegisterClientScriptBlock(page.GetType(), "-alert-", script);
    }

    public static void HandleUnKnownErrors()
    {
        MessageBox.Show("UNKNOWN ERROR: Please try again.");
    }
    public static void SortGridView(GridView gridView, List<DbDataRecord> data, string sortExpression, SortDirection direction)
    {
        if (direction == SortDirection.Ascending)
        {
            direction = SortDirection.Descending;
            data.Sort(delegate(DbDataRecord r1, DbDataRecord r2)
            {
                return r1[sortExpression].ToString().CompareTo(r2[sortExpression].ToString());
            });
            gridView.DataSource = data;
            gridView.DataBind();
        }
        else
        {
            direction = SortDirection.Ascending;
            data.Sort(delegate(DbDataRecord r1, DbDataRecord r2)
            {
                return r1[sortExpression].ToString().CompareTo(r2[sortExpression].ToString());
            });
            data.Reverse();
            gridView.DataSource = data;
            gridView.DataBind();
        }
    }

    public static string CleanHTMLData(string htmlsource)
    {          
        var objRegEx = new Regex("<[^>]*>");
        return objRegEx.Replace(htmlsource, "");
    }

    public static void Enable(Control main, bool status)
    {
        foreach (Control control in main.Controls)
        {
            var textBox = control as TextBox;
            if (textBox != null)
            {
                textBox.Enabled = status;
            }
            var radioButton = control as RadioButton;
            if (radioButton != null)
            {
                radioButton.Enabled = status;
            }
            var buttonList = control as RadioButtonList;
            if (buttonList != null)
            {
                buttonList.Enabled = status;
            }
            var button = control as Button;
            if (button != null)
            {
                button.Enabled = status;
            }
            var dropDownList = control as DropDownList;
            if (dropDownList != null)
            {
                dropDownList.Enabled = status;
            }
            var view = control as GridView;
            if (view != null)
            {
                view.Enabled = status;
            }
            var checkBox = control as CheckBox;
            if (checkBox != null)
            {
                checkBox.Enabled = status;
            }
            Enable(control, false);
        }
    }

    public static string GetMessage(int id)
    {
        if (HttpContext.Current.Request.PhysicalApplicationPath != null)
        {
            var file = Path.Combine(HttpContext.Current.Request.PhysicalApplicationPath, "App_Data", "Messages.xml");
            if (!File.Exists(file))
                return "Could not obtain messages";
            var messages = Sars.Systems.Serialization.XmlObjectSerializer.Read<List<Message>>(file);
            if (messages != null && messages.Any())
            {
                var message = messages.Find(msg => msg.Id == id);
                if (message != null)
                {
                    return message.Text;
                }
                return string.Empty;
            }

        }
        return string.Empty;
    }
}
