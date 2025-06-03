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
    public partial class CategoriaEditorForm : Form
    {
        public CategoriaEditorForm()
        {
            InitializeComponent();
            CarregarCategorias();
        }
        private void CarregarCategorias()
        {
            listBox1.DataSource = null;
            DataTable dt = Categorias.GetAll();
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

            if (Categorias.Add(novo))
            {
                MessageBox.Show("Categoria adicionada.");
                textBox1.Clear();
                CarregarCategorias();
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
                MessageBox.Show("Selecione uma categoria para renomear.");
                return;
            }

            int idAntigo = Categorias.GetIDByNome(listBox1.SelectedValue.ToString());
            string novo = textBox1.Text.Trim();

            if (string.IsNullOrEmpty(novo))
            {
                MessageBox.Show("Digite o novo nome.");
                return;
            }

            if (Categorias.Rename(idAntigo, novo))
            {
                MessageBox.Show("Catgoria renomeada.");
                textBox1.Clear();
                CarregarCategorias();
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
                MessageBox.Show("Selecione uma categoria para eliminar.");
                return;
            }

            var confirm = MessageBox.Show("Deseja mesmo eliminar esta categoria?", "Confirmar", MessageBoxButtons.YesNo);
            if (confirm == DialogResult.Yes)
            {
                int id = Categorias.GetIDByNome(listBox1.SelectedValue.ToString());
                if (Categorias.Delete(id))
                {
                    MessageBox.Show("Categoria eliminado.");
                    CarregarCategorias();
                }
                else
                {
                    MessageBox.Show("Erro ao eliminar. Categoria pode estar em uso.");
                }
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
