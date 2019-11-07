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
    public partial class frm_report_incoming : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                bool isValid = true;
                string strReportName = System.Web.HttpContext.Current.Session["NamaLaporan"].ToString();
                string ReportTitle = System.Web.HttpContext.Current.Session["Title"].ToString();
                string Param1 =System.Web.HttpContext.Current.Session["Param1"].ToString();
                string Param2 = System.Web.HttpContext.Current.Session["Param2"].ToString();
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
                        TextObject CompanyName = (TextObject)rd.ReportDefinition.Sections["Section2"].ReportObjects["TxtCompanyName"];
                        CompanyName.Text = ReportTitle;
                        rd.SetDataSource(rptSource);
                        
                        rd.SetParameterValue("Date1", Param1);
                        rd.SetParameterValue("Date2", Param2);
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