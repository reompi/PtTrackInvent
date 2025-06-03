using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DocumentFormat.OpenXml.Office2016.Drawing.ChartDrawing;
using TrackInvent.BLL;

namespace TrackInvent
{
    public partial class Permissoes : Form
    {
        int bemId;
        public Permissoes(int _bemId)
        {
            bemId = _bemId;
            InitializeComponent();
            CarregarPermissoes();
            CarregarUtilizadoresDisponiveis();
        }
        private void CarregarPermissoes()
        {
            DataTable dt = BLL.BensUtilizadores.GetUtilizadoresDoBem(bemId);
            listBox1.DataSource = dt;
            listBox1.ValueMember = "ID";
            listBox1.DisplayMember = "Nome_De_Utilizador";

        }
        private void CarregarUtilizadoresDisponiveis()
        {
            comboBox1.Items.Clear();

            var todos = Utilizadores.GetTodosAtivos();
            var dt = BLL.BensUtilizadores.GetUtilizadoresDoBem(bemId);
            var jaTem = dt.AsEnumerable()
                          .Select(row => row.Field<int>("ID"))
                          .ToHashSet();
            foreach (DataRow row in todos.Rows) // Fix: Iterate over DataTable rows
            {
                int id = Convert.ToInt32(row["ID"]);
                string nome = row["Nome_De_Utilizador"].ToString();

                if (!jaTem.Contains(id))
                    comboBox1.Items.Add(new ComboBoxItem(nome, id));
            }

            comboBox1.SelectedIndex = -1;
        }
        private void Permissoes_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem is ComboBoxItem item)
            {
                int utilizadorID = item.Value;
                BLL.BensUtilizadores.AdicionarPermissao(bemId, utilizadorID);
                CarregarPermissoes();
                CarregarUtilizadoresDisponiveis();
            }
        }
        public class ComboBoxItem
        {
            public string Text { get; set; }
            public int Value { get; set; }

            public ComboBoxItem(string text, int value)
            {
                Text = text;
                Value = value;
            }

            public override string ToString()
            {
                return Text;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem == null)
            {
                MessageBox.Show("Selecione o utlizador para eliminar.");
            }
            var confirm = MessageBox.Show("Deseja mesmo eliminar este estado?", "Confirmar", MessageBoxButtons.YesNo);
            if (confirm == DialogResult.Yes)
            {
                if (listBox1.SelectedValue == null)
                {
                    MessageBox.Show("Erro: Valor selecionado inválido.");
                    return;
                }
                int? id = listBox1.SelectedValue as int?;
                if (id == null)
                {
                    MessageBox.Show("Erro ao obter ID do utilizador selecionado.");
                    return;
                }
                if (BLL.BensUtilizadores.RemoverPermissao(bemId, id.Value))
                {
                    MessageBox.Show("Estado eliminado.");
                    CarregarPermissoes();
                    CarregarUtilizadoresDisponiveis();
                }
                else
                {
                    MessageBox.Show("Erro ao eliminar. Estado pode estar em uso.");
                }
            }
        }
    }
}
