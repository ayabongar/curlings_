
    using System;
    using System.Configuration;
    using System.Data.Odbc;
    using System.Data.OleDb;
    using System.Data.SqlClient;
    using System.IO;
    using System.Web;
    using System.Xml;

public class SARSExceptionsXML
{
    private int[] iDataBaseDownCodes = new int[] { 11, 0x11 };
    private string mErrMess = "";
    private int mErrNo = 0;
    private string mMessage = "";
    private string mSource = "";
    private string mStackTrace = "";
    private string mType = "";
    private string mUserID = "";
    private string mXMLFilePath = "";
    private XmlTextWriter writer = null;

    private void fGetDownPage()
    {
        string url = "";
        try
        {
            url = ConfigurationManager.AppSettings["DownRedirect"];
        }
        catch
        {
            return;
        }
        if (url != "")
        {
            HttpContext.Current.Response.Redirect(url);
        }
    }

    private bool fWriteExceptionToXML()
    {
        switch (this.mType)
        {
            case "System.Threading.ThreadAbortException":
                return true;

            case "System.IO.FileNotFoundException":
                this.mErrMess = "HTTP 404. The resource you are looking for (or one of its dependencies) could have been removed, had its name changed, or is temporarily unavailable. Please review the following URL and make sure that it is spelled correctly: " + this.mType;
                break;
        }
        if (this.mXMLFilePath == "")
        {
            if (!Directory.Exists(ConfigurationManager.AppSettings["system-errors"]))
                Directory.CreateDirectory(ConfigurationManager.AppSettings["system-errors"]);
            this.mXMLFilePath = ConfigurationManager.AppSettings["system-errors"] + "\\" + DateTime.Now.ToString("yyyyMMMd HH mm ss") + ".xml";
        }
        try
        {
            this.writer = new XmlTextWriter(this.mXMLFilePath, null) {Formatting = Formatting.Indented, Indentation = 2};
            this.writer.WriteStartElement("Exceptions");
            this.fWriteXML("Date", DateTime.Now.ToString());
            this.fWriteXML("Exception_Type", this.mType);
            this.fWriteXML("ErrorNumber", this.mErrNo.ToString());
            this.fWriteXML("ErrorDescription", this.mErrMess);
            this.fWriteXML("Source", this.mSource);
            this.fWriteXML("User", this.mUserID);
            this.fWriteXML("StackTrace", this.mStackTrace);
            this.writer.WriteEndElement();
            this.writer.Close();
        }
        catch (UnauthorizedAccessException exception)
        {
            this.fWritePageIfExceptionError("Access Denied to exceptions folder, please notify the system administrator", exception.Message, exception.Source, exception.StackTrace);
        }
        catch (DirectoryNotFoundException exception2)
        {
            this.fWritePageIfExceptionError("The path for the exceptions folder cannot be found", exception2.Message, exception2.Source, exception2.StackTrace);
        }
       
        return true;
    }

    private bool fWriteExceptionToXML(Exception ex)
    {
        this.mErrNo = 0x22b8;
        this.mMessage = ex.Message;
        this.mSource = ex.Source;
        this.mStackTrace = ex.StackTrace;
        if (HttpContext.Current != null)
        {
            this.mUserID = "Anonymouse";
        }
        this.mType = ex.GetType().ToString();
        if ((HttpContext.Current.Server.GetLastError() != null) && (HttpContext.Current.Server.GetLastError().InnerException != null))
        {
            Exception innerException = HttpContext.Current.Server.GetLastError().InnerException;
            this.mErrMess = this.mErrMess + "<br><b>Inner exception</b>";
            this.mErrMess = this.mErrMess + "<br>Message: " + innerException.Message;
            this.mErrMess = this.mErrMess + "<br>Source: " + innerException.Source;
            this.mErrMess = this.mErrMess + "<br>TargetSite: " + innerException.TargetSite;
            this.mErrMess = this.mErrMess + "<br>Stack Trace:<br>" + innerException.StackTrace;
        }
        return this.fWriteExceptionToXML();
    }

    private bool fWriteOdbcException(OdbcException ex)
    {
        bool flag = false;
        for (int i = 0; i < ex.Errors.Count; i++)
        {
            object mErrMess = this.mErrMess;
            this.mErrMess = string.Concat(new object[] { mErrMess, "Index #", i, "<br>Message: ", ex.Errors[i].Message, "<br>NativeError: ", ex.Errors[i].NativeError, "<br>Source: ", ex.Errors[i].Source, "<br>SQLState: ", ex.Errors[i].SQLState, "<br>" });
            foreach (int num2 in this.iDataBaseDownCodes)
            {
                if (num2 == ex.Errors[i].NativeError)
                {
                    flag = true;
                    break;
                }
            }
        }
        this.fWriteExceptionToXML(ex);
        if (flag)
        {
            HttpContext.Current.Response.Redirect(ConfigurationManager.AppSettings["DownRedirect"]);
        }
        return true;
    }

    private bool fWriteOleDBException(OleDbException ex)
    {
        bool flag = false;
        for (int i = 0; i < ex.Errors.Count; i++)
        {
            object mErrMess = this.mErrMess;
            this.mErrMess = string.Concat(new object[] { mErrMess, "Index #", i, "<br>Message: ", ex.Errors[i].Message, "<br>NativeError: ", ex.Errors[i].NativeError, "<br>Source: ", ex.Errors[i].Source, "<br>SQLState: ", ex.Errors[i].SQLState, "<br>" });
            foreach (int num2 in this.iDataBaseDownCodes)
            {
                if (num2 == ex.Errors[i].NativeError)
                {
                    flag = true;
                    break;
                }
            }
        }
        this.fWriteExceptionToXML(ex);
        if (flag)
        {
            HttpContext.Current.Response.Redirect(ConfigurationManager.AppSettings["DownRedirect"]);
        }
        return true;
    }

    private bool fWritePageIfExceptionError(string pDesc, string pMessage, string pSource, string pStackTrace)
    {
        HttpContext.Current.Response.Write("<HTML>\n");
        HttpContext.Current.Response.Write("<HEAD>\n");
        HttpContext.Current.Response.Write("<title>Exception Manager</title>\n");
        HttpContext.Current.Response.Write("<LINK href=/Itranetstylesheet.css type=text/css rel=stylesheet>\n");
        HttpContext.Current.Response.Write("</HEAD>\n");
        HttpContext.Current.Response.Write("<BODY>\n");
        HttpContext.Current.Response.Write("<h1>SYSTEM FATAL ERROR </h1> <br> <b>Cannot write away Exception</b><br>\n");
        HttpContext.Current.Response.Write("<hr><BR>\n");
        HttpContext.Current.Response.Write("<h2>" + pDesc + "<br></h2>\n");
        HttpContext.Current.Response.Write("<b><u>Exception_Message: </u></b>" + pMessage + "<br>\n");
        HttpContext.Current.Response.Write("<b><u>Exception Source: </u></b>" + pSource + "<br>\n");
        HttpContext.Current.Response.Write("<b><u>Stack Trace </b></u><br>" + pStackTrace + "<br>\n");
        HttpContext.Current.Response.Write("<hr><BR>\n");
        HttpContext.Current.Response.Write("<h1>ORIGINAL EXCEPTION</h1> <br>\n");
        HttpContext.Current.Response.Write("<b><u>Exception Type: </u></b>" + this.mType + "<br>\n");
        HttpContext.Current.Response.Write("<b><u>Error Number: </b></u>" + this.mErrNo + "<br>\n");
        HttpContext.Current.Response.Write("<b><u>Error Description: </b></u>" + this.mErrMess + "<br>\n");
        HttpContext.Current.Response.Write("<b><u>Exception_Message: </b></u>" + this.mMessage + "<br>\n");
        HttpContext.Current.Response.Write("<b><u>Exception Source: </b></u>" + this.mSource + "<br>\n");
        HttpContext.Current.Response.Write("<b><u>Stack Trace </b></u><br>" + this.mStackTrace + "<br>\n");
        HttpContext.Current.Response.Write("</BODY>\n");
        HttpContext.Current.Response.Write("</HTML>");
        HttpContext.Current.Response.End();
        return true;
    }

    private bool fWriteSQLException(SqlException ex)
    {
        bool flag = false;
        for (int i = 0; i < ex.Errors.Count; i++)
        {
            object mErrMess = this.mErrMess;
            this.mErrMess = string.Concat(new object[] { mErrMess, "Index #", i, "<br>Message: ", ex.Errors[i].Message, "<br>LineNumber: ", ex.Errors[i].LineNumber, "vSource: ", ex.Errors[i].Source, "<br>Procedure: ", ex.Errors[i].Procedure, "<br>" });
            foreach (int num2 in this.iDataBaseDownCodes)
            {
                if (num2 == ex.Errors[i].Number)
                {
                    flag = true;
                    break;
                }
            }
        }
        this.fWriteExceptionToXML(ex);
        if (flag)
        {
            this.fGetDownPage();
        }
        return true;
    }

    private int fWriteXML(string pFieldName, string pFieldValue)
    {
        this.writer.WriteStartElement(pFieldName);
        this.writer.WriteString(pFieldValue);
        this.writer.WriteEndElement();
        return 0;
    }

    public bool Log(Exception ex)
    {
        switch (ex.GetType().ToString())
        {
            case "System.Data.OleDb.OleDbException":
                return this.fWriteOleDBException((OleDbException)ex);

            case "System.Data.Odbc.OdbcException":
                return this.fWriteOdbcException((OdbcException)ex);

            case "System.Data.SqlClient.SqlException":
                return this.fWriteSQLException((SqlException)ex);
        }
        return this.fWriteExceptionToXML(ex);
    }

    public bool Log(int iErrNo, string sErrMessage, string sExMessage, string sExSource, string sStackTrace, string sUserID, string sGetType)
    {
        this.mErrNo = iErrNo;
        this.mErrMess = sErrMessage;
        this.mMessage = sExMessage;
        this.mSource = sExSource;
        this.mStackTrace = sStackTrace;
        this.mUserID = sUserID;
        this.mType = sGetType;
        return this.fWriteExceptionToXML();
    }

    public string ErrMessage
    {
        get
        {
            return this.mErrMess;
        }
    }

    public int ErrNumber
    {
        get
        {
            return this.mErrNo;
        }
    }

    public string ErrSource
    {
        get
        {
            return this.mSource;
        }
        set
        {
            this.mSource = value;
        }
    }

    public string StackTrace
    {
        get
        {
            return this.mStackTrace;
        }
        set
        {
            this.mStackTrace = value;
        }
    }

    public string XMLFileDirectory
    {
        get
        {
            return this.mXMLFilePath;
        }
        set
        {
            this.mXMLFilePath = value;
        }
    }
}

