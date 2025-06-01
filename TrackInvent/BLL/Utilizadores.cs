using DataAccessLayer;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

public class Utilizadores
{
    static public bool queryLogin(string username, string password)
    {
        DAL dal = new DAL();

        // Buscar apenas o hash da senha pelo username
        SqlParameter[] sqlParams = new SqlParameter[]{
            new SqlParameter("@username", username)
        };

        DataTable dt = dal.executarReader("SELECT Senha_Hash FROM Utilizadores WHERE Nome_De_Utilizador=@username AND Archived = 0", sqlParams);

        if (dt.Rows.Count == 1)
        {
            string storedHash = dt.Rows[0]["Senha_Hash"].ToString();

            // Verificar se a senha digitada corresponde ao hash armazenado
            if (AuthHelper.VerifyPassword(password, storedHash))
            {
                return true; // Login bem-sucedido
            }
        }

        return false; // Login falhou
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

}
