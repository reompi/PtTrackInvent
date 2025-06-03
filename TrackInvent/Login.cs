using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TrackInvent
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string nome = textBox1.Text.Trim();
            string password = textBox2.Text.Trim();
            var loginResult = Utilizadores.queryLogin(nome, password);

            if (loginResult != null && loginResult.Success)
            {
                // Define os dados do utilizador logado na sessão global
                SessaoAtual.Id = loginResult.Id;
                SessaoAtual.Nome = loginResult.Nome;
                SessaoAtual.Cargo = loginResult.Cargo;

                TelaPrincipal mainForm = new TelaPrincipal();
                mainForm.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Login falhou!");
            }
        }
    }
}
