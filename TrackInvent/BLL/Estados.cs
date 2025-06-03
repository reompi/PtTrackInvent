using DataAccessLayer;
using System;
using System.Data;
using System.Data.SqlClient;

namespace TrackInvent.BLL
{
    internal class Estados
    {
        public static DataTable GetAll()
        {
            DAL dal = new DAL();
            return dal.executarReader("SELECT Nome,Id FROM Estados ORDER BY Nome");
        }
        public static int GetIDByNome(string nome)
        {
            DAL dal = new DAL();
            SqlParameter[] sqlParams = {
              new SqlParameter("@nome", nome)
               };

            object result = dal.executarScalar("SELECT Id FROM Estados WHERE Nome = @nome", sqlParams);

                return Convert.ToInt32(result);
        }
        public static bool Add(string nome)
        {
            DAL dal = new DAL();

            SqlParameter[] param = {
                new SqlParameter("@nome", nome)
            };

            try
            {
                int rows = dal.executarNonQuery("INSERT INTO Estados (Nome) VALUES (@nome)", param);
                return rows > 0;
            }
            catch (SqlException ex)
            {
                // Handle duplicates or other errors as needed
                return false;
            }
        }

        public static bool Rename(int id, string newNome)
        {
            DAL dal = new DAL();
            SqlParameter[] param = {
                new SqlParameter("@novoNome", newNome),
                new SqlParameter("@id", id)
            };

            int rows = dal.executarNonQuery("UPDATE Estados SET Nome = @novoNome WHERE Id = @id", param);
            return rows > 0;
        }

        public static bool Delete(int id)
        {
            if (IsInUse(id))
                return false;

            DAL dal = new DAL();
            SqlParameter[] param = {
                new SqlParameter("@id", id)
            };

            int rows = dal.executarNonQuery("DELETE FROM Estados WHERE Id = @id", param);
            return rows > 0;
        }

        public static bool IsInUse(int id)
        {
            DAL dal = new DAL();
            SqlParameter[] param = {
                new SqlParameter("@id", id)
            };

            int count = (int)dal.executarScalar("SELECT COUNT(*) FROM Bens_Patrimoniais WHERE Estado_ID = @id", param);
            return count > 0;
        }
    }
}
