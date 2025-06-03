using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;

namespace TrackInvent.BLL
{
    internal class Movimentação
    {
        // Inserir nova movimentação
        public static bool RegistarMovimentacao(int bemID, int origemID, int destinoID, string motivo, DateTime dataHora, int idUser)
        {
            DAL dal = new DAL();

            // Inserir movimentação
            SqlParameter[] insertParams = new SqlParameter[]
            {
                new SqlParameter("@Bem_ID", bemID),
                new SqlParameter("@Origem_ID", origemID),
                new SqlParameter("@Destino_ID", destinoID),
                new SqlParameter("@Motivo", motivo),
                new SqlParameter("@Data_Hora", dataHora),
                new SqlParameter("@IdUser", idUser)
            };

            int linhasAfetadas = dal.executarNonQuery(
                "INSERT INTO Movimentacoes (Bem_ID, Setor_Origem_ID, Setor_Destino_ID, Motivo, Data_Movimentacao,Utilizador_ID) " +
                "VALUES (@Bem_ID, @Origem_ID, @Destino_ID, @Motivo, @Data_Hora, @IdUser)", insertParams
            );

            if (linhasAfetadas > 0)
            {
                // Atualizar o setor atual do bem
                SqlParameter[] updateParams = new SqlParameter[]
                {
                    new SqlParameter("@SetorNovo", destinoID),
                    new SqlParameter("@BemID", bemID)
                };

                int update = dal.executarNonQuery(
                    "UPDATE Bens_Patrimoniais SET Localizacao_ID = @SetorNovo WHERE ID = @BemID", updateParams
                );

                return update > 0;
            }

            return false;
        }

        // Obter todas as movimentações
        static public DataTable GetAll()
        {
            DAL dal = new DAL();
            return dal.executarReader("SELECT * FROM Movimentacoes");
        }

        // Obter movimentações por bem
        static public DataTable GetByBem(int bemID)
        {
            DAL dal = new DAL();

            SqlParameter[] parametros = new SqlParameter[]
            {
                new SqlParameter("@BemID", bemID)
            };

            return dal.executarReader("SELECT * FROM Movimentacoes WHERE Bem_ID = @BemID", parametros);
        }

        public static DataTable GetHistoricoFiltrado(DateTime? dataInicio, DateTime? dataFim, int? setorId, int? estadoId, int? categoriaId)
        {
            DAL dal = new DAL();

            List<SqlParameter> parametros = new List<SqlParameter>();

            string query = @"
        SELECT m.ID, m.Data_Movimentacao, m.Bem_ID, b.Nome AS Bem, 
               sOrig.Nome AS Setor_Origem, sDest.Nome AS Setor_Destino,
               u.Nome_De_Utilizador AS Movimentado_Por
        FROM Movimentacoes m
        INNER JOIN Bens_Patrimoniais b ON m.Bem_ID = b.ID
        LEFT JOIN Setores sOrig ON m.Setor_Origem_ID = sOrig.ID
        LEFT JOIN Setores sDest ON m.Setor_Destino_ID = sDest.ID
        LEFT JOIN Utilizadores u ON m.Utilizador_ID = u.ID
        WHERE 1 = 1
    ";

            if (dataInicio.HasValue)
            {
                query += " AND m.Data_Movimentacao >= @DataInicio";
                parametros.Add(new SqlParameter("@DataInicio", dataInicio.Value));
            }

            if (dataFim.HasValue)
            {
                query += " AND m.Data_Movimentacao < @DataFim";
                parametros.Add(new SqlParameter("@DataFim", dataFim.Value.AddDays(1)));
            }

            if (setorId.HasValue)
            {
                query += " AND (m.Setor_Origem_ID = @SetorID OR m.Setor_Destino_ID = @SetorID)";
                parametros.Add(new SqlParameter("@SetorID", setorId.Value));
            }

            if (estadoId.HasValue)
            {
                query += " AND b.Estado_ID = @EstadoID";
                parametros.Add(new SqlParameter("@EstadoID", estadoId.Value));
            }

            if (categoriaId.HasValue)
            {
                query += " AND b.Categoria_ID = @CategoriaID";
                parametros.Add(new SqlParameter("@CategoriaID", categoriaId.Value));
            }

            return dal.executarReader(query, parametros.ToArray());
        }

        /// <summary>
        /// Obtém o histórico de movimentações filtrado por Bem_ID.
        /// </summary>
        /// <param name="bemId">ID do bem patrimonial para filtrar (obrigatório).</param>
        /// <returns>DataTable com o histórico filtrado pelo Bem_ID.</returns>
        public static DataTable GetHistoricoFiltrado(int? bemId)
        {
            DAL dal = new DAL();

            List<SqlParameter> parametros = new List<SqlParameter>();
            string query = @"
        SELECT m.ID, m.Data_Movimentacao, m.Bem_ID, b.Nome AS Bem, 
               sOrig.Nome AS Setor_Origem, sDest.Nome AS Setor_Destino,
               u.Nome_De_Utilizador AS Movimentado_Por
        FROM Movimentacoes m
        INNER JOIN Bens_Patrimoniais b ON m.Bem_ID = b.ID
        LEFT JOIN Setores sOrig ON m.Setor_Origem_ID = sOrig.ID
        LEFT JOIN Setores sDest ON m.Setor_Destino_ID = sDest.ID
        LEFT JOIN Utilizadores u ON m.Utilizador_ID = u.ID
        WHERE 1 = 1
    ";

            if (bemId.HasValue)
            {
                query += " AND m.Bem_ID = @BemID";
                parametros.Add(new SqlParameter("@BemID", bemId.Value));
            }

            return dal.executarReader(query, parametros.ToArray());
        }
    }
}
