using System;
using System.Data;
using System.Data.SqlClient;

namespace DataAccessLayer
{
    public class DAL : IDisposable
    {
        private readonly SqlConnection _SqlConn;

        public DAL()
        {
            _SqlConn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\admin\Documents\GitHub\PtTrackInvent\TrackInvent\Database1.mdf;Integrated Security=True;Connect Timeout=30");
        }

        private void abrirLigacao()
        {
            if (_SqlConn.State == ConnectionState.Closed)
            {
                _SqlConn.Open();
            }
        }

        private void fecharLigacao()
        {
            if (_SqlConn.State == ConnectionState.Open)
            {
                _SqlConn.Close();
            }
        }

        public DataTable executarStoredProcReader(string sqlCmd, SqlParameter[] sqlParams = null)
        {
            DataTable returnTable = new DataTable();

            using (SqlCommand cmd = new SqlCommand(sqlCmd, _SqlConn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                if (sqlParams != null)
                    cmd.Parameters.AddRange(sqlParams);

                abrirLigacao();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    returnTable.Load(reader);
                }
            }

            return returnTable;
        }

        public int executarStoredProcNonQuery(string sqlCmd, SqlParameter[] sqlParams)
        {
            using (SqlCommand cmd = new SqlCommand(sqlCmd, _SqlConn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                if (sqlParams != null)
                    cmd.Parameters.AddRange(sqlParams);

                abrirLigacao();
                return cmd.ExecuteNonQuery();
            }
        }

        public object executarStoredProcScalar(string sqlCmd, SqlParameter[] sqlParams)
        {
            using (SqlCommand cmd = new SqlCommand(sqlCmd, _SqlConn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                if (sqlParams != null)
                    cmd.Parameters.AddRange(sqlParams);

                abrirLigacao();
                return cmd.ExecuteScalar();
            }
        }

        public DataTable executarReader(string sqlCmd, SqlParameter[] sqlParams = null)
        {
            DataTable returnTable = new DataTable();

            using (SqlCommand cmd = new SqlCommand(sqlCmd, _SqlConn))
            {
                cmd.CommandType = CommandType.Text;
                if (sqlParams != null)
                    cmd.Parameters.AddRange(sqlParams);

                abrirLigacao();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    returnTable.Load(reader);
                }
            }

            return returnTable;
        }

        public int executarNonQuery(string sqlCmd, SqlParameter[] sqlParams)
        {
            using (SqlCommand cmd = new SqlCommand(sqlCmd, _SqlConn))
            {
                cmd.CommandType = CommandType.Text;
                if (sqlParams != null)
                    cmd.Parameters.AddRange(sqlParams);

                abrirLigacao();
                return cmd.ExecuteNonQuery();
            }
        }

        public object executarScalar(string sqlCmd, SqlParameter[] sqlParams)
        {
            using (SqlCommand cmd = new SqlCommand(sqlCmd, _SqlConn))
            {
                cmd.CommandType = CommandType.Text;
                if (sqlParams != null)
                    cmd.Parameters.AddRange(sqlParams);

                abrirLigacao();
                return cmd.ExecuteScalar();
            }
        }

        public void Dispose()
        {
            fecharLigacao();
            _SqlConn.Dispose();
        }
    }
}
