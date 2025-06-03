using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackInvent.BLL
{
    internal class Bens
    {
        public static DataTable GetAll(int utilizadorID)
        {
            DAL dal = new DAL();

            SqlParameter[] sqlParams = new SqlParameter[]
            {
                new SqlParameter("@UtilizadorID", utilizadorID)
            };

            string query = @"
                SELECT B.*
                FROM Bens_Patrimoniais B
                INNER JOIN Bens_Utilizadores BU ON B.ID = BU.Bem_ID
                WHERE BU.Utilizador_ID = @UtilizadorID
            ";

            return dal.executarReader(query, sqlParams);
        }

        public static DataTable Filtrar(int? categoriaId, int? estadoId, int? setorId, DateTime dataInicio, DateTime dataFim)
        {
            List<SqlParameter> parametros = new List<SqlParameter>
    {
        new SqlParameter("@DataInicio", dataInicio),
        new SqlParameter("@DataFim", dataFim)
    };

            string query = @"
        SELECT B.Nome, C.Nome AS Categoria, E.Nome AS Estado, S.Nome AS Setor,
               B.Valor, B.Quantidade, B.Data_Aquisicao,
               (B.Valor * B.Quantidade) AS Total
        FROM Bens_Patrimoniais B
        JOIN Categorias C ON B.Categoria_ID = C.ID
        JOIN Estados E ON B.Estado_ID = E.ID
        JOIN Setores S ON B.Localizacao_ID = S.ID
        WHERE B.Data_Aquisicao BETWEEN @DataInicio AND @DataFim
    ";

            if (categoriaId != null)
            {
                query += " AND B.Categoria_ID = @Categoria";
                parametros.Add(new SqlParameter("@Categoria", categoriaId));
            }

            if (estadoId != null)
            {
                query += " AND B.Estado_ID = @Estado";
                parametros.Add(new SqlParameter("@Estado", estadoId));
            }

            if (setorId != null)
            {
                query += " AND B.Localizacao_ID = @Setor";
                parametros.Add(new SqlParameter("@Setor", setorId));
            }

            DAL dal = new DAL();
            return dal.executarReader(query, parametros.ToArray());
        }

        public static int CriarBem(string nome, string descricao, int categoriaID, decimal valor, decimal quantidade, DateTime dataAquisicao, int estadoID, int setorID, string icone)
        {
            DAL dal = new DAL();
            SqlParameter[] sqlParams = {
                new SqlParameter("@Nome", nome),
                new SqlParameter("@Descricao", (object)descricao ?? DBNull.Value),
                new SqlParameter("@Categoria_ID", categoriaID),
                new SqlParameter("@Valor", valor),
                new SqlParameter("@Quantidade", quantidade),
                new SqlParameter("@Data_Aquisicao", dataAquisicao),
                new SqlParameter("@Estado_ID", estadoID),
                new SqlParameter("@Setor_ID", setorID),
                new SqlParameter("@Icone", (object)icone ?? DBNull.Value)
            };

            string query = @"
                INSERT INTO Bens_Patrimoniais 
                    (Nome, Descricao, Categoria_ID, Valor, Quantidade, Data_Aquisicao, Estado_ID, Localizacao_ID, Icon) 
                VALUES 
                    (@Nome, @Descricao, @Categoria_ID, @Valor, @Quantidade, @Data_Aquisicao, @Estado_ID, @Setor_ID, @Icone);
                SELECT CAST(SCOPE_IDENTITY() AS INT);
            ";

            object result = dal.executarScalar(query, sqlParams);
            return result != null ? Convert.ToInt32(result) : -1;
        }
        public static DataTable GetAll()
        {
            DAL dal = new DAL();
            string query = "SELECT * FROM Bens_Patrimoniais";
            return dal.executarReader(query);
        }
        public static DataTable GetByFiltro(string nome, int? categoriaId, int? estadoId, int? setorId, int utilizadorId)
        {
            DAL dal = new DAL();
            List<string> condicoes = new List<string>();
            List<SqlParameter> parametros = new List<SqlParameter>();

            condicoes.Add("BU.Utilizador_ID = @utilizadorId");
            parametros.Add(new SqlParameter("@utilizadorId", utilizadorId));

            if (!string.IsNullOrWhiteSpace(nome))
            {
                condicoes.Add("B.Nome LIKE @nome");
                parametros.Add(new SqlParameter("@nome", "%" + nome + "%"));
            }

            if (categoriaId.HasValue)
            {
                condicoes.Add("B.Categoria_ID = @cat");
                parametros.Add(new SqlParameter("@cat", categoriaId.Value));
            }

            if (estadoId.HasValue)
            {
                condicoes.Add("B.Estado_ID = @est");
                parametros.Add(new SqlParameter("@est", estadoId.Value));
            }

            if (setorId.HasValue)
            {
                condicoes.Add("B.Localizacao_ID = @setor");
                parametros.Add(new SqlParameter("@setor", setorId.Value));
            }

            string query = @"
        SELECT B.*
        FROM Bens_Patrimoniais B
        INNER JOIN Bens_Utilizadores BU ON B.ID = BU.Bem_ID
        WHERE " + string.Join(" AND ", condicoes);

            return dal.executarReader(query, parametros.ToArray());
        }

        public static DataTable GetByFiltro(string nome, int? categoriaId, int? estadoId, int? setorId)
        {
            DAL dal = new DAL();
            List<string> condicoes = new List<string>();
            List<SqlParameter> parametros = new List<SqlParameter>();

            if (!string.IsNullOrWhiteSpace(nome))
            {
                condicoes.Add("Nome LIKE @nome");
                parametros.Add(new SqlParameter("@nome", "%" + nome + "%"));
            }

            if (categoriaId.HasValue)
            {
                condicoes.Add("Categoria_ID = @cat");
                parametros.Add(new SqlParameter("@cat", categoriaId.Value));
            }

            if (estadoId.HasValue)
            {
                condicoes.Add("Estado_ID = @est");
                parametros.Add(new SqlParameter("@est", estadoId.Value));
            }

            if (setorId.HasValue)
            {
                condicoes.Add("Localizacao_ID = @setor");
                parametros.Add(new SqlParameter("@setor", setorId.Value));
            }

            string query = "SELECT * FROM Bens_Patrimoniais";
            if (condicoes.Count > 0)
                query += " WHERE " + string.Join(" AND ", condicoes);

            return dal.executarReader(query, parametros.ToArray());
        }
        public static DataTable GetById(int bemId)
        {
            DAL dal = new DAL();
            string query = "SELECT * FROM Bens_Patrimoniais WHERE ID = @id";
            SqlParameter[] parametros = { new SqlParameter("@id", bemId) };
            return dal.executarReader(query, parametros);
        }
        public static bool AtualizarBem(int id, string nome, string descricao, int categoriaID, decimal valor, decimal quantidade, DateTime dataAquisicao, int estadoID, int setorID, string icone)
        {
            DAL dal = new DAL();
            SqlParameter[] sqlParams = {
                new SqlParameter("@ID", id),
                new SqlParameter("@Nome", nome),
                new SqlParameter("@Descricao", (object)descricao ?? DBNull.Value),
                new SqlParameter("@Categoria_ID", categoriaID),
                new SqlParameter("@Valor", valor),
                new SqlParameter("@Quantidade", quantidade),
                new SqlParameter("@Data_Aquisicao", dataAquisicao),
                new SqlParameter("@Estado_ID", estadoID),
                new SqlParameter("@Setor_ID", setorID),
                new SqlParameter("@Icone", (object)icone ?? DBNull.Value)
            };

            int rows = dal.executarNonQuery(@"
                UPDATE Bens_Patrimoniais
                SET Nome = @Nome,
                    Descricao = @Descricao,
                    Categoria_ID = @Categoria_ID,
                    Valor = @Valor,
                    Quantidade = @Quantidade,
                    Data_Aquisicao = @Data_Aquisicao,
                    Estado_ID = @Estado_ID,
                    Localizacao_ID = @Setor_ID,
                    Icon = @Icone
                WHERE ID = @ID", sqlParams);

            return rows > 0;
        }

    }


}
