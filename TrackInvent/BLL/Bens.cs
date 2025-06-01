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
        public static bool CriarBem(string nome, string descricao, int categoriaID, decimal valor, decimal quantidade, DateTime dataAquisicao, int estadoID, int setorID, string icone)
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

            int rows = dal.executarNonQuery(@"INSERT INTO Bens_Patrimoniais 
        (Nome, Descricao, Categoria_ID, Valor, Quantidade, Data_Aquisicao, Estado_ID, Localizacao_ID, Icon) 
        VALUES (@Nome, @Descricao, @Categoria_ID, @Valor, @Quantidade, @Data_Aquisicao, @Estado_ID, @Setor_ID, @Icone)", sqlParams);

            return rows > 0;
        }
        public static DataTable GetAll()
        {
            DAL dal = new DAL();
            string query = "SELECT * FROM Bens_Patrimoniais";
            return dal.executarReader(query);
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


    }


}
