using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace DAL
{
    public class DAO
    {
        private SqlConnection mCon;

        public DAO()
        {
            mCon = new SqlConnection(ConfigurationManager.ConnectionStrings["StringConexion"].ConnectionString);
        }

        public DataSet ExecuteDataSet(string pCommandText, Dictionary<string, object> parametros = null)
        {
            try
            {
                using (SqlCommand mCom = new SqlCommand(pCommandText, mCon))
                {
                    mCom.CommandType = CommandType.StoredProcedure;
                    if (parametros != null)
                    {
                        foreach (var param in parametros)
                        {
                            mCom.Parameters.AddWithValue(param.Key, param.Value ?? DBNull.Value);
                        }
                    }

                    using (SqlDataAdapter mDa = new SqlDataAdapter(mCom))
                    {
                        DataSet mDs = new DataSet();
                        mDa.Fill(mDs);
                        return mDs;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (mCon.State != ConnectionState.Closed)
                    mCon.Close();
            }

        }

        public int ExecuteNonQuery(string pCommandText, Dictionary<string, object> parametros = null)
        {
            try
            {
                SqlCommand mCom = new SqlCommand(pCommandText, mCon);
                mCom.CommandType = CommandType.StoredProcedure;
                if (parametros != null)
                {
                    foreach (var param in parametros)
                        mCom.Parameters.AddWithValue(param.Key, param.Value);
                }
                mCon.Open();
                return mCom.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (mCon.State != ConnectionState.Closed)
                    mCon.Close();
            }
        }
        public int ExecuteScalar(string pCommandText, Dictionary<string, object> parametros = null)
        {
            int valorDevuelto;
            try
            {
                SqlCommand mCom = new SqlCommand(pCommandText, mCon);
                mCom.CommandType = System.Data.CommandType.StoredProcedure;
                if (parametros != null)
                {
                    foreach (var param in parametros)
                    {
                        mCom.Parameters.AddWithValue(param.Key, param.Value);
                    }
                }
                mCon.Open();
                valorDevuelto = int.Parse(mCom.ExecuteScalar().ToString());
                return valorDevuelto;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                if (mCon.State != ConnectionState.Closed)
                    mCon.Close();
            }
        }
        public string ExecuteScalarString(string pCommandText, Dictionary<string, object> parametros = null)
        {
            try
            {
                using (SqlCommand mCom = new SqlCommand(pCommandText, mCon))
                {
                    mCom.CommandType = CommandType.StoredProcedure;
                    if (parametros != null)
                    {
                        foreach (var param in parametros)
                        {
                            mCom.Parameters.AddWithValue(param.Key, param.Value);
                        }
                    }

                    mCon.Open();
                    object result = mCom.ExecuteScalar();
                    return result != null ? result.ToString() : null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (mCon.State != ConnectionState.Closed)
                    mCon.Close();
            }
        }
    }
}
