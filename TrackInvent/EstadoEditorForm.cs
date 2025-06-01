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
    public partial class EstadoEditorForm : Form
    {
        public EstadoEditorForm()
        {
            InitializeComponent();

        }
        private void CarregarEstados()
        {
            listBox1.DataSource = null;
            DataTable dt = Estados.GetAll();
            listBox1.DataSource = dt;
            listBox1.DisplayMember = "Nome";
            listBox1.ValueMember = "Nome";
        }

        private void EstadoEditorForm_Load(object sender, EventArgs e)
        {
            CarregarEstados();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string novo = textBox1.Text.Trim();
            if (string.IsNullOrEmpty(novo))
            {
                MessageBox.Show("Digite um nome válido.");
                return;
            }

            if (Estados.Add(novo))
            {
                MessageBox.Show("Estado adicionado.");
                textBox1.Clear();
                CarregarEstados();
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
                MessageBox.Show("Selecione um estado para renomear.");
                return;
            }

            int idAntigo = Estados.GetIDByNome(listBox1.SelectedValue.ToString());
            string novo = textBox1.Text.Trim();

            if (string.IsNullOrEmpty(novo))
            {
                MessageBox.Show("Digite o novo nome.");
                return;
            }

            if (Estados.Rename(idAntigo, novo))
            {
                MessageBox.Show("Estado renomeado.");
                textBox1.Clear();
                CarregarEstados();
            }
            else
            {
                MessageBox.Show("Erro ao renomear. Estado pode estar em uso.");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem == null)
            {
                MessageBox.Show("Selecione um estado para eliminar.");
                return;
            }


            var confirm = MessageBox.Show("Deseja mesmo eliminar este estado?", "Confirmar", MessageBoxButtons.YesNo);
            if (confirm == DialogResult.Yes)
            {
                int id = Estados.GetIDByNome(listBox1.SelectedValue.ToString());

                if (Estados.Delete(id))
                {
                    MessageBox.Show("Estado eliminado.");
                    CarregarEstados();
                }
                else
                {
                    MessageBox.Show("Erro ao eliminar. Estado pode estar em uso.");
                }
            }

        }
    }
}
