using DataAccessLayer;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using TrackInvent;

public class Utilizadores
{
    public static DataTable GetTodosAtivos()
    {
        DAL dal = new DAL();
        return dal.executarReader("SELECT * FROM Utilizadores Where Cargo = 'Utilizador'");
    }
    static public LoginResult queryLogin(string username, string password)
    {
        DAL dal = new DAL();

        SqlParameter[] sqlParams = new SqlParameter[] {
        new SqlParameter("@username", username)
    };

        DataTable dt = dal.executarReader(
            "SELECT Id, Nome, Cargo, Senha_Hash FROM Utilizadores WHERE Nome_De_Utilizador=@username AND Archived = 0",
            sqlParams
        );

        if (dt.Rows.Count == 1)
        {
            string storedHash = dt.Rows[0]["Senha_Hash"].ToString();

            if (AuthHelper.VerifyPassword(password, storedHash))
            {
                return new LoginResult
                {
                    Success = true,
                    Id = Convert.ToInt32(dt.Rows[0]["Id"]),
                    Nome = dt.Rows[0]["Nome"].ToString(),
                    Cargo = dt.Rows[0]["Cargo"].ToString()
                };
            }
        }

        return new LoginResult { Success = false };
    }
    static public bool queryRegister(string nome, string username, string password, string cargo)
    {
        DAL dal = new DAL();

        // Verificar se já existe um utilizador com o mesmo username
        SqlParameter[] checkParams = new SqlParameter[] {
        new SqlParameter("@username", username)
    };

        DataTable dt = dal.executarReader(
            "SELECT 1 FROM Utilizadores WHERE Nome_De_Utilizador=@username AND Archived = 0",
            checkParams
        );

        if (dt.Rows.Count > 0)
        {
            // Já existe utilizador com esse username
            return false;
        }

        // Gerar hash da password
        string hash = AuthHelper.HashPassword(password);

        // Inserir novo utilizador
        SqlParameter[] insertParams = new SqlParameter[] {
           new SqlParameter("@nome", nome),
        new SqlParameter("@username", username),
        new SqlParameter("@senhaHash", hash),
        new SqlParameter("@cargo", cargo)
    };

        string query = @"
        INSERT INTO Utilizadores (Nome, Nome_De_Utilizador, Senha_Hash, Cargo, Archived)
        VALUES (@nome, @username, @senhaHash, @cargo, 0)
    ";

        try
        {
            dal.executarNonQuery(query, insertParams);
            return true; // Registro bem-sucedido
        }
        catch (Exception ex)
        {
            MessageBox.Show("Erro: " + ex.Message);
            return false; // Falha na inserção
        }
    }
    public static int? GetIdByNome(string nome)
    {
        DAL dal = new DAL();

        SqlParameter[] sqlParams = new SqlParameter[]
        {
            new SqlParameter("@nome", nome)
        };

        DataTable dt = dal.executarReader(
            "SELECT Id FROM Utilizadores WHERE Nome = @nome AND Archived = 0",
            sqlParams
        );

        if (dt.Rows.Count == 1)
        {
            return Convert.ToInt32(dt.Rows[0]["Id"]);
        }

        // Retorna null se não encontrar ou se houver duplicidade
        return null;
    }

}
