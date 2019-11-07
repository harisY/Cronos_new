using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Cronos.dtReport;
using Cronos.Services;
using Cronos.Models;

namespace Cronos.Models.Repo
{
    public class ReportIMrepo
    {
        private SqlConnection con;
        public static TransactionHelper gh_Trans;
        private DeptService ObjDept;
        public ReportIMrepo()
        {

        }
        private static string Connection()
        {
            string constr = ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString();
            return constr;

        }

        public static DSREPORT GetDataSetByCommand1(string pQuery, string dt, SqlParameter[] pParam = null, int pTimeOut = 0)
        {
            SqlDataAdapter da = null;
            DSREPORT dsa = new DSREPORT();
            try
            {
                if (gh_Trans != null && gh_Trans.Command != null)
                {
                    gh_Trans.Command.CommandType = CommandType.StoredProcedure;
                    gh_Trans.Command.CommandText = pQuery;
                    gh_Trans.Command.CommandTimeout = pTimeOut;
                    gh_Trans.Command.Parameters.Clear();
                    if (pParam != null)
                    {
                        for (int i = 0; i <= pParam.Length - 1; i++)
                        {
                            gh_Trans.Command.Parameters.Add(pParam[i]);
                        }
                    }
                    da = new SqlDataAdapter(gh_Trans.Command);
                    da.Fill(dsa);
                }
                else
                {
                    using (SqlConnection conn = new SqlConnection())
                    {
                        conn.ConnectionString = Connection();
                        SqlCommand cmd = new SqlCommand();
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = pQuery;
                        cmd.CommandTimeout = pTimeOut;
                        cmd.Connection = conn;
                        if (pParam != null)
                        {
                            for (int i = 0; i <= pParam.Length - 1; i++)
                            {
                                cmd.Parameters.Add(pParam[i]);
                            }
                        }
                        conn.Open();
                        da = new SqlDataAdapter(cmd);
                        da.Fill(dsa, dt);
                    }
                }
                da = null;
                return dsa;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static DataTable GetDataTableByCommand(string pQuery, SqlParameter[] pParam = null, int pTimeOut = 30)
        {
            DataTable dta = new DataTable();
            SqlDataAdapter da = null;
            try
            {
                if (gh_Trans != null && gh_Trans.Command != null)
                {
                    gh_Trans.Command.CommandType = CommandType.StoredProcedure;
                    gh_Trans.Command.CommandText = pQuery;
                    gh_Trans.Command.CommandTimeout = pTimeOut;
                    gh_Trans.Command.Parameters.Clear();
                    if (pParam != null)
                    {
                        for (int i = 0; i <= pParam.Length - 1; i++)
                        {
                            gh_Trans.Command.Parameters.Add(pParam[i]);
                        }
                    }
                    da = new SqlDataAdapter(gh_Trans.Command);
                    da.Fill(dta);
                }
                else
                {
                    using (SqlConnection conn = new SqlConnection())
                    {
                        conn.ConnectionString = Connection();
                        SqlCommand cmd = new SqlCommand();
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = pQuery;
                        cmd.CommandTimeout = pTimeOut;
                        cmd.Connection = conn;
                        if (pParam != null)
                        {
                            for (int i = 0; i <= pParam.Length - 1; i++)
                            {
                                cmd.Parameters.Add(pParam[i]);
                            }
                        }
                        conn.Open();
                        da = new SqlDataAdapter(cmd);
                        da.Fill(dta);
                    }
                }
                da = null;
                return dta;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public static DSREPORT GetDataSet(string pQuery, string dt, int pTimeOut = 300)
        {
            SqlDataAdapter da = null;
            DSREPORT dsa = new DSREPORT();
            try
            {
                if (gh_Trans != null && gh_Trans.Command != null)
                {
                    gh_Trans.Command.CommandType = CommandType.Text;
                    gh_Trans.Command.CommandText = pQuery;
                    gh_Trans.Command.CommandTimeout = pTimeOut;
                    da = new SqlDataAdapter(gh_Trans.Command);
                    da.Fill(dsa);
                }
                else
                {
                    using (SqlConnection conn = new SqlConnection())
                    {
                        conn.ConnectionString = Connection();
                        conn.Open();
                        da = new SqlDataAdapter(pQuery, conn);
                        da.Fill(dsa, dt);
                    }
                }
                da = null;
                return dsa;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static DataTable GetDataTable(string pQuery, int pTimeOut = 0)
        {
            DataTable dta = new DataTable();
            SqlDataAdapter da = null;
            try
            {
                if (gh_Trans != null && gh_Trans.Command != null)
                {
                    gh_Trans.Command.CommandType = CommandType.Text;
                    gh_Trans.Command.CommandText = pQuery;
                    gh_Trans.Command.CommandTimeout = pTimeOut;
                    da = new SqlDataAdapter(gh_Trans.Command);
                    da.Fill(dta);
                }
                else
                {
                    using (SqlConnection conn = new SqlConnection())
                    {
                        conn.ConnectionString = Connection();
                        conn.Open();
                        da = new SqlDataAdapter(pQuery, conn);
                        da.Fill(dta);
                    }
                }
                da = null;
                return dta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<StandartComboBox> GetPeriod()
        {
            List<StandartComboBox> Result = new List<StandartComboBox>();
            string Query = @"SELECT TOP (100) PERCENT bulan Text, kode Value 
                             FROM dbo.data_bulan
                             ORDER BY kode";
            DataTable dt = new DataTable();
            dt = GetDataTable(Query);

            foreach (DataRow dr in dt.Rows)
            {
                Result.Add(
                    new StandartComboBox
                    {
                        Value = Convert.ToString(dr["Value"]),
                        Text = Convert.ToString(dr["Text"])
                    });
            }
            return Result;

        }

        public List<StandartComboBox> GetOutgoingBcType()
        {
            List<StandartComboBox> Result = new List<StandartComboBox>();
            string Query = @"SELECT DISTINCT TOP (100) PERCENT bc_type Value
                             FROM dbo.vw_rpt_customs_do 
                             WHERE (bc_type IS NOT NULL)
                             ORDER BY bc_type";
            DataTable dt = new DataTable();
            dt = GetDataTable(Query);

            foreach (DataRow dr in dt.Rows)
            {
                Result.Add(
                    new StandartComboBox
                    {
                        Value = Convert.ToString(dr["Value"]),
                        Text = Convert.ToString(dr["Value"])
                    });
            }
            return Result;

        }
        public List<StandartComboBox> GetIncomingBcType()
        {
            List<StandartComboBox> Result = new List<StandartComboBox>();
            string Query = @"SELECT DISTINCT TOP (100) PERCENT bc_type Value
                             FROM dbo.vw_rpt_customs_received_note 
                             WHERE (bc_type IS NOT NULL)
                             ORDER BY bc_type";
            DataTable dt = new DataTable();
            dt = GetDataTable(Query);

            foreach (DataRow dr in dt.Rows)
            {
                Result.Add(
                    new StandartComboBox
                    {
                        Value = Convert.ToString(dr["Value"]),
                        Text = Convert.ToString(dr["Value"])
                    });
            }
            return Result;

        }
        public string CompanyName()
        {
            string Name = string.Empty;
            try
            {
                string Query = @"SELECT cab_desc AS org_name, cab_id AS org_id, org_default
                                FROM dbo.tbl_wilayah
                                WHERE (seq_acc = 4)";
                DataTable dt = new DataTable();
                dt = GetDataTable(Query);
                Name = dt.Rows[0][0].ToString();
            }
            catch (Exception)
            {

                throw;
            }
            return Name;
        }

        public DataSet PrintReportCustomFA(string facility, int period, int yearperiod)
        {
            try
            {
                DSREPORT ds = new DSREPORT();
                string strQuery = "rpt_mutasi_asset";

                SqlParameter[] pParam = new SqlParameter[3];

                pParam[0] = new SqlParameter("@facility", SqlDbType.VarChar);
                pParam[0].Value = facility;
                pParam[1] = new SqlParameter("@period", SqlDbType.Int);
                pParam[1].Value = period;
                pParam[2] = new SqlParameter("@yearperiod", SqlDbType.Int);
                pParam[2].Value = yearperiod;
                ds = GetDataSetByCommand1(strQuery, "dtCustomFA", pParam);
                return ds;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public DataTable PrintReportCustomFA_table(string facility, int period, int yearperiod)
        {
            try
            {
                DataTable dt = new DataTable();
                string strQuery = "rpt_mutasi_asset";

                SqlParameter[] pParam = new SqlParameter[3];

                pParam[0] = new SqlParameter("@facility", SqlDbType.VarChar);
                pParam[0].Value = facility;
                pParam[1] = new SqlParameter("@period", SqlDbType.Int);
                pParam[1].Value = period;
                pParam[2] = new SqlParameter("@yearperiod", SqlDbType.Int);
                pParam[2].Value = yearperiod;
                dt = GetDataTableByCommand(strQuery, pParam);
                return dt;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        public DataSet PrintReportCustomIncoming(string facility, string tgl1, string tgl2, string bctype)
        {
            try
            {
                DSREPORT ds = new DSREPORT();
                string strQuery = "rpt_custom_incoming";

                SqlParameter[] pParam = new SqlParameter[4];

                pParam[0] = new SqlParameter("@facility", SqlDbType.VarChar);
                pParam[0].Value = facility;
                pParam[1] = new SqlParameter("@DateFilter1", SqlDbType.VarChar);
                pParam[1].Value = tgl1;
                pParam[2] = new SqlParameter("@DateFilter2", SqlDbType.VarChar);
                pParam[2].Value = tgl2;
                pParam[3] = new SqlParameter("@BCType", SqlDbType.VarChar);
                pParam[3].Value = bctype;
                ds = GetDataSetByCommand1(strQuery, "dtCustomIncoming", pParam);
                return ds;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public DataSet PrintReportCustomOutgoing(string facility, string tgl1, string tgl2, string bctype)
        {
            try
            {
                DSREPORT ds = new DSREPORT();
                string strQuery = "rpt_custom_outgoing";

                SqlParameter[] pParam = new SqlParameter[4];

                pParam[0] = new SqlParameter("@facility", SqlDbType.VarChar);
                pParam[0].Value = facility;
                pParam[1] = new SqlParameter("@DateFilter1", SqlDbType.VarChar);
                pParam[1].Value = tgl1;
                pParam[2] = new SqlParameter("@DateFilter2", SqlDbType.VarChar);
                pParam[2].Value = tgl2;
                pParam[3] = new SqlParameter("@BCType", SqlDbType.VarChar);
                pParam[3].Value = bctype;
                ds = GetDataSetByCommand1(strQuery, "dtCustomOutgoing", pParam);
                return ds;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public DataSet PrintReportRawMaterial(string facility, int period, int yearperiod)
        {
            try
            {
                DSREPORT ds = new DSREPORT();
                string strQuery = "rpt_custom_mutasi_rawmat";

                SqlParameter[] pParam = new SqlParameter[3];

                pParam[0] = new SqlParameter("@facility", SqlDbType.VarChar);
                pParam[0].Value = facility;
                pParam[1] = new SqlParameter("@period", SqlDbType.Int);
                pParam[1].Value = period;
                pParam[2] = new SqlParameter("@yearperiod", SqlDbType.Int);
                pParam[2].Value = yearperiod;
                ds = GetDataSetByCommand1(strQuery, "dtCustomRM", pParam);
                return ds;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public DataSet PrintReportFG(string facility, int period, int yearperiod)
        {
            try
            {
                DSREPORT ds = new DSREPORT();
                string strQuery = "rpt_custom_mutasi_fg";

                SqlParameter[] pParam = new SqlParameter[3];

                pParam[0] = new SqlParameter("@facility", SqlDbType.VarChar);
                pParam[0].Value = facility;
                pParam[1] = new SqlParameter("@period", SqlDbType.Int);
                pParam[1].Value = period;
                pParam[2] = new SqlParameter("@yearperiod", SqlDbType.Int);
                pParam[2].Value = yearperiod;
                ds = GetDataSetByCommand1(strQuery, "dtCustomFG", pParam);
                return ds;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public DataSet PrintReportScrap(string facility, int period, int yearperiod)
        {
            try
            {
                DSREPORT ds = new DSREPORT();
                string strQuery = "rpt_custom_mutasi_scrap";

                SqlParameter[] pParam = new SqlParameter[3];

                pParam[0] = new SqlParameter("@facility", SqlDbType.VarChar);
                pParam[0].Value = facility;
                pParam[1] = new SqlParameter("@period", SqlDbType.Int);
                pParam[1].Value = period;
                pParam[2] = new SqlParameter("@yearperiod", SqlDbType.Int);
                pParam[2].Value = yearperiod;
                ds = GetDataSetByCommand1(strQuery, "dtCustomScrap", pParam);
                return ds;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public DataSet PrintReportWIP(string facility, int period, int yearperiod)
        {
            try
            {
                DSREPORT ds = new DSREPORT();
                string strQuery = "rpt_custom_mutasi_wip";

                SqlParameter[] pParam = new SqlParameter[3];

                pParam[0] = new SqlParameter("@facility", SqlDbType.VarChar);
                pParam[0].Value = facility;
                pParam[1] = new SqlParameter("@period", SqlDbType.Int);
                pParam[1].Value = period;
                pParam[2] = new SqlParameter("@yearperiod", SqlDbType.Int);
                pParam[2].Value = yearperiod;
                ds = GetDataSetByCommand1(strQuery, "dtCustomWIP", pParam);
                return ds;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}