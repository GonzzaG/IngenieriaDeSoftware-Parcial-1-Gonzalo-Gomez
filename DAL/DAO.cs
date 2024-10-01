using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace DAL
{
    public class DAO : IDisposable
    {
        readonly SqlConnection mCon = new SqlConnection(ConfigurationManager.ConnectionStrings["mStrCon"].ConnectionString);


        public DataSet ExecuteDataSet(string pCommandText)
        {
            try
            {
                using (SqlDataAdapter mDa = new SqlDataAdapter(pCommandText, mCon))
                {
                    DataSet mDs = new DataSet();
                    mDa.Fill(mDs);

                    return mDs;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int ExecuteNonQuery(string pCommandText)
        {
            try
            {
                using (SqlCommand mComm = new SqlCommand(pCommandText, mCon))
                {
                    mCon.Open();

                    return mComm.ExecuteNonQuery();

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }     
        }
        public int ObtenerUltimoId(string pTabla, string pColumnaId)
        {
            try
            {
                using (SqlCommand mComm = new SqlCommand("SELECT ISNULL(MAX(" + pColumnaId + "),0) FROM " + pTabla, mCon))
                {
                    mCon.Open();

                    return (int)mComm.ExecuteScalar();

                }
            }
            catch(Exception ex)
            {
                throw ex;
            }          
        }

        public void Dispose()
        {
            if (mCon != null && mCon.State != ConnectionState.Closed)
                mCon.Close();
        }
    }
}
