public partial class ErrorPage : System.Web.UI.Page
{
    //string mapPath = WebConfigurationManager.AppSettings["ErrorPath"].ToString();
    //string strRole = string.Empty;
    //protected void Page_Load(object sender, EventArgs e)
    //{
    //    //lblLoggedOnUser.Text = getEmployeeName(SarsUser.SID) + " (" + SarsUser.SID + ") " + strRole;
    //    if (!IsPostBack)
    //    {
    //        WriteError(Session["Error"].ToString());
    //    }
    //}
    //public void WriteError(string errorMessage)
    //{
    //    try
    //    {
    //        string path = mapPath + DateTime.Today.ToString("dd-MMM-yyyy") + ".txt";
    //        if (!File.Exists(path))
    //        {
    //            File.Create(path).Close();
    //        }
    //        using (StreamWriter w = File.AppendText(path))
    //        {
    //            w.WriteLine("Log Entry :");
    //            w.WriteLine("{0}", DateTime.Now.ToString(CultureInfo.InvariantCulture));
    //            string err = "Error Message:" + Environment.NewLine + errorMessage;
    //            w.WriteLine(err);
    //            w.WriteLine("_______________________________________________________________________");
    //            w.Flush();
    //            w.Close();
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        WriteError(ex.Message);
    //    }

    //}
    //public static string getEmployeeName(string SID)
    //{
    //    SqlConnection cn = new SqlConnection(Common.AssetsConnectionString());
    //    cn.Open();
    //    SqlDataAdapter da = new SqlDataAdapter("GetEmployeeBySID", cn);
    //    da.SelectCommand.CommandType = CommandType.StoredProcedure;
    //    da.SelectCommand.Parameters.Add(new SqlParameter("@SID", SqlDbType.VarChar));

    //    da.SelectCommand.Parameters["@SID"].Value = SID;
    //    DataTable dt = new DataTable();
    //    da.Fill(dt);
    //    da.Dispose();
    //    cn.Close();
    //    string strEmployeeName = string.Empty;
    //    foreach (DataRow r in dt.Rows)
    //    {
    //        strEmployeeName = r["fullName"].ToString();
    //    }
    //    return strEmployeeName;
    //}
    protected void btnOk_Click(object sender, System.EventArgs e)
    {
        Response.Redirect("http://sarsonlinedev");
    }
}