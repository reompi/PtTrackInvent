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
    public partial class Registro : Form
    {
        public Registro()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string nome = textBox3.Text.Trim();
            string username = textBox1.Text.Trim();
            string password = textBox2.Text.Trim();
            string cargo = groupBox1.Controls.OfType<RadioButton>()
                .FirstOrDefault(r => r.Checked)?.Text;
            bool login = Utilizadores.queryRegister(nome, username, password,cargo);
            if (login)
            {
                MessageBox.Show("Cadastro bem-sucedido!");
            }
            else
            {
                MessageBox.Show("Falha, noome de utilizador já existe.");
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }

     
    }
}
