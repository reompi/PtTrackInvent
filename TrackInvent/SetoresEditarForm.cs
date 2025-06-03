using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TrackInvent.BLL;

namespace TrackInvent
{
    public partial class SetoresEditarForm : Form
    {
        public SetoresEditarForm()
        {
            InitializeComponent();
            CarregarSetores();
        }
        private void CarregarSetores()
        {
            listBox1.DataSource = null;
            DataTable dt = Setores.GetAll();
            listBox1.DataSource = dt;
            listBox1.DisplayMember = "Nome";
            listBox1.ValueMember = "Nome";
        }
        private void button2_Click(object sender, EventArgs e)
        {
            string novo = textBox1.Text.Trim();
            if (string.IsNullOrEmpty(novo))
            {
                MessageBox.Show("Digite um nome válido.");
                return;
            }

            if (Setores.Add(novo))
            {
                MessageBox.Show("Setor adicionado.");
                textBox1.Clear();
                CarregarSetores();
            }
            else
            {
                MessageBox.Show("Erro ao adicionar. Verifique se já existe.");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem == null)
            {
                MessageBox.Show("Selecione um setor para renomear.");
                return;
            }

            int idAntigo = Setores.GetIDByNome(listBox1.SelectedValue.ToString());
            string novo = textBox1.Text.Trim();

            if (string.IsNullOrEmpty(novo))
            {
                MessageBox.Show("Digite o novo nome.");
                return;
            }

            if (Setores.Update(idAntigo, novo))
            {
                MessageBox.Show("Setor renomeado.");
                textBox1.Clear();
                CarregarSetores();
            }
            else
            {
                MessageBox.Show("Erro ao renomear. Nome já está em uso.");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem == null)
            {
                MessageBox.Show("Selecione um setor para eliminar.");
                return;
            }

            var confirm = MessageBox.Show("Deseja mesmo eliminar este setor?", "Confirmar", MessageBoxButtons.YesNo);
            if (confirm == DialogResult.Yes)
            {
                int id = Setores.GetIDByNome(listBox1.SelectedValue.ToString());
                if (Setores.Delete(id))
                {
                    MessageBox.Show("Setor eliminado.");
                    CarregarSetores();
                }
                else
                {
                    MessageBox.Show("Erro ao eliminar. Setor pode estar em uso.");
                }
            }
        }
    }
}
