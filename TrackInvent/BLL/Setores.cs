using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TrackInvent.BLL
{
    internal class Setores
    {
        public static DataTable GetAll()
        {
            DAL dal = new DAL();
            return dal.executarReader("SELECT * FROM Setores ORDER BY Nome");
        }
        public static int GetIDByNome(string nome)
        {
            DAL dal = new DAL();
            SqlParameter[] sqlParams = {
              new SqlParameter("@nome", nome)
               };

            object result = dal.executarScalar("SELECT ID FROM Setores WHERE Nome = @nome", sqlParams);

                return Convert.ToInt32(result);
        }
        public static bool Add(string nome)
        {
            DAL dal = new DAL();
            SqlParameter[] sqlParams = {
                new SqlParameter("@nome", nome),
            };
            try
            {
                int rows = dal.executarNonQuery("INSERT INTO Setores (Nome) VALUES (@nome)", sqlParams);
                return rows > 0;
            }
            catch (SqlException ex)
            {
                return false;
            }
        }

        public static bool Update(int id, string nome)
        {
            DAL dal = new DAL();

            // Verifica se já existe outro setor com o mesmo nome (exceto o próprio)
            SqlParameter[] checkParams = {
                new SqlParameter("@nome", nome),
                new SqlParameter("@id", id)
            };
            object exists = dal.executarScalar(
                "SELECT COUNT(*) FROM Setores WHERE Nome = @nome AND ID <> @id", checkParams);

            if (Convert.ToInt32(exists) > 0)
                return false; // Nome já existe em outro setor

            SqlParameter[] sqlParams = {
                new SqlParameter("@id", id),
                new SqlParameter("@nome", nome),
            };

            int rows = dal.executarNonQuery("UPDATE Setores SET Nome = @nome WHERE ID = @id", sqlParams);
            return rows > 0;
        }

        public static bool Delete(int id)
        {
            DAL dal = new DAL();
            SqlParameter[] sqlParams = {
                new SqlParameter("@id", id)
            };
            int rows = dal.executarNonQuery("DELETE FROM Setores WHERE ID = @id", sqlParams);
            return rows > 0;
        }

        public static bool IsInUse(int id)
        {
            DAL dal = new DAL();
            SqlParameter[] param = {
                new SqlParameter("@id", id)
            };

            int count = (int)dal.executarScalar("SELECT COUNT(*) FROM Bens_Patrimoniais WHERE Localizacao_Id = @id", param);
            return count > 0;
        }
        public static string GetNomeById(int id)
        {
            DAL dal = new DAL();
            SqlParameter[] sqlParams = {
                new SqlParameter("@id", id)
            };
            object result = dal.executarScalar("SELECT Nome FROM Setores WHERE ID = @id", sqlParams);
            return result != null && result != DBNull.Value ? result.ToString() : null;
        }
    }
}
