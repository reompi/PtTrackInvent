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
    public partial class BensMovimentação : Form
    {
        
        int idBem;
        int idSetoratual;
        public BensMovimentação(int id)
        {
            InitializeComponent();
            idBem = id;
            dateTimePicker1.Format = DateTimePickerFormat.Custom;
            dateTimePicker1.CustomFormat = "dd/MM/yyyy";
        }

        private void BensMovimentação_Load(object sender, EventArgs e)
        {
            // Buscar dados do bem
            DataTable dt = BLL.Bens.GetById(idBem);
            if (dt.Rows.Count == 0) return;
            DataRow row = dt.Rows[0];
            textBox6.Text = Estados.GetNomeById(Convert.ToInt32(row["Estado_ID"]));
            textBox3.Text = Categorias.GetNomeById(Convert.ToInt32(row["Categoria_ID"]));
            idSetoratual = Convert.ToInt32(row["Localizacao_ID"]);
            textBox7.Text = Setores.GetNomeById(idSetoratual);
            textBox2.Text = row["Nome"].ToString();
            textBox4.Text = row["Valor"].ToString();
            textBox5.Text = row["Quantidade"].ToString();

            textBox8.Text = Convert.ToDateTime(row["Data_Aquisicao"]).ToString();
            textBox1.Text = row["Icon"].ToString();

            CarregarSetores();

        }
        private void CarregarSetores()
        {
            DataTable dt = Setores.GetAll();

            // Remover a linha cujo ID é igual a idSetoratual
            for (int i = dt.Rows.Count - 1; i >= 0; i--)
            {
                if (dt.Rows[i]["ID"] != DBNull.Value && Convert.ToInt32(dt.Rows[i]["ID"]) == idSetoratual)
                {
                    dt.Rows.RemoveAt(i);
                }
            }

            DataRow novaLinha = dt.NewRow();
            novaLinha["Nome"] = "Adicionar outro/Editar";
            dt.Rows.Add(novaLinha);

            comboBox1.DataSource = dt;
            comboBox1.DisplayMember = "Nome";
            comboBox1.ValueMember = "Nome";

            comboBox1.SelectedIndex = -1;  // Nenhum selecionado
        }
        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
          
            DataRowView row = comboBox1.SelectedItem as DataRowView;
            if (row != null)
            {
                string selected = row["Nome"].ToString();

                if (selected == "Adicionar outro/Editar")
                {
                    SetoresEditarForm fe = new SetoresEditarForm();
                    fe.ShowDialog();

                    // Recarregar e resetar seleção
                    CarregarSetores();
                    textBox7.Text = Setores.GetNomeById(idSetoratual);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show(
                "Tem certeza que deseja continuar?",
                "Confirmação",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result != DialogResult.Yes)
                return;

            // Validação do setor de destino
            if (comboBox1.SelectedIndex == -1)
            {
                MessageBox.Show("Selecione o setor de destino.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DataRowView row = comboBox1.SelectedItem as DataRowView;
            if (row == null || row["Nome"].ToString() == "Adicionar outro/Editar")
            {
                MessageBox.Show("Selecione um setor de destino válido.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int destinoID = Convert.ToInt32(row["ID"]);
            if (destinoID == idSetoratual)
            {
                MessageBox.Show("O setor de destino deve ser diferente do setor atual.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string motivo = richTextBox1.Text;

            DateTime dataHora = dateTimePicker1.Value;

            // Inserção
            bool sucesso = BLL.Movimentação.RegistarMovimentacao(idBem, idSetoratual, destinoID, motivo, dataHora,SessaoAtual.Id);

            if (sucesso)
            {
                MessageBox.Show("Movimentação registada com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            else
            {
                MessageBox.Show("Erro ao registar a movimentação.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
