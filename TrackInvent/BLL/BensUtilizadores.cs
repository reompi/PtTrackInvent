using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;

namespace TrackInvent.BLL
{
    internal class BensUtilizadores
    {
        public static DataTable GetUtilizadoresDoBem(int bemID)
        {
            DAL dal = new DAL();
            SqlParameter[] sqlParams = new SqlParameter[]
            {
        new SqlParameter("@BemID", bemID)
            };

            return dal.executarReader(@"
        SELECT U.ID, U.Nome_De_Utilizador 
        FROM Bens_Utilizadores BU
        JOIN Utilizadores U ON BU.Utilizador_ID = U.ID
        WHERE BU.Bem_ID = @BemID", sqlParams);
        }
        static public DataTable GetUtilizadoresPorBem(int bemId)
        {
            DAL dal = new DAL();
            SqlParameter[] parameters = {
                new SqlParameter("@Bem_ID", bemId)
            };

            return dal.executarReader(@"
                SELECT u.ID, u.Nome, u.Nome_De_Utilizador
                FROM Bens_Utilizadores bu
                INNER JOIN Utilizadores u ON bu.Utilizador_ID = u.ID
                WHERE bu.Bem_ID = @Bem_ID", parameters);
        }

        // Adiciona um utilizador ao bem
        static public bool AdicionarPermissao(int bemId, int utilizadorId)
        {
            DAL dal = new DAL();

            // Parâmetros para o SELECT
            SqlParameter[] parametrosSelect = {
        new SqlParameter("@Bem_ID", bemId),
        new SqlParameter("@Utilizador_ID", utilizadorId)
    };

            var existe = dal.executarScalar(@"
        SELECT COUNT(*) FROM Bens_Utilizadores 
        WHERE Bem_ID = @Bem_ID AND Utilizador_ID = @Utilizador_ID", parametrosSelect);

            if (Convert.ToInt32(existe) > 0)
                return false;

            // NOVO conjunto de parâmetros para o INSERT
            SqlParameter[] parametrosInsert = {
        new SqlParameter("@Bem_ID", bemId),
        new SqlParameter("@Utilizador_ID", utilizadorId)
    };

            int linhasAfetadas = dal.executarNonQuery(@"
        INSERT INTO Bens_Utilizadores (Bem_ID, Utilizador_ID)
        VALUES (@Bem_ID, @Utilizador_ID)", parametrosInsert);

            return linhasAfetadas > 0;
        }

        // Remove um utilizador do bem
        static public bool RemoverPermissao(int bemId, int utilizadorId)
        {
            DAL dal = new DAL();
            SqlParameter[] parameters = {
                new SqlParameter("@Bem_ID", bemId),
                new SqlParameter("@Utilizador_ID", utilizadorId)
            };

            int linhasAfetadas = dal.executarNonQuery(@"
                DELETE FROM Bens_Utilizadores
                WHERE Bem_ID = @Bem_ID AND Utilizador_ID = @Utilizador_ID", parameters);

            return linhasAfetadas > 0;
        }
    }

    internal class Utilizador
    {
        public int ID { get; set; }
        public string Nome { get; set; }
    }
}
