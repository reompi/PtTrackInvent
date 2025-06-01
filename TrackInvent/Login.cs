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
            bool login = Utilizadores.queryLogin(nome, password);
            if (login) {
                TelaPrincipal mainForm = new TelaPrincipal();
                mainForm.Show();
                this.Hide(); // Hide the login form
            } else {
                MessageBox.Show("Login falhou!");
            }
        }
    }
}
