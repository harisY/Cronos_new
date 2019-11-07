using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Cronos.Report
{
    public partial class frm_report_fa : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                bool isValid = true;
                string strReportName = System.Web.HttpContext.Current.Session["NamaLaporan"].ToString();
                string ReportTitle = System.Web.HttpContext.Current.Session["Title"].ToString();
                int Param =Convert.ToInt32(System.Web.HttpContext.Current.Session["Param"]);
                var rptSource = System.Web.HttpContext.Current.Session["Datasource"];
                //var rptDetailSource = System.Web.HttpContext.Current.Session["ReportIMDetails"];
                if (string.IsNullOrEmpty(strReportName))
                {
                    isValid = false;
                }
                if (isValid)
                {
                    ReportDocument rd = new ReportDocument();
                    //string strRptPath = Server.MapPath("~/") + "/Report/IM//" + strReportName;
                    rd.Load(Server.MapPath("../Report/FA/" + strReportName));
                    //rd.Load(strRptPath);
                    //rd.SetDatabaseLogon("sa", "fid123!!");
                    if (rptSource != null && rptSource.GetType().ToString() != "System.String")
                    {
                        TextObject TxtCompanyName = (TextObject)rd.ReportDefinition.Sections["Section2"].ReportObjects["TxtCompanyName"];
                        TxtCompanyName.Text = ReportTitle;
                        rd.SetDataSource(rptSource);
                        rd.SetParameterValue("nPeriod", Param);
                        //rd.Subreports[0].SetDataSource(rptDetailSource);
                    }
                        
                    
                    CrystalReportViewer1.ReportSource = rd;
                    CrystalReportViewer1.ToolPanelView = ToolPanelViewType.None;
                }
                else
                {
                    Response.Write("<H2>Nothing Found; No Report name found</H2>");
                }
            }
            catch (Exception ex)
            {
                Response.Write(ex.ToString());
            }
        }
    }
}