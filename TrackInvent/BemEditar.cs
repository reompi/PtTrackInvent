using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using TrackInvent.BLL;
namespace TrackInvent
{
    public partial class BemEditar : Form
    {
        [DllImport("user32.dll")]

        static extern void keybd_event(byte bVk, byte bScan, int dwFlags, int dwExtraInfo);
        int idBem;
        string previousText;
        public BemEditar(int _idBem)
        {
            InitializeComponent();
            idBem = _idBem;
        }

        private void BemEditar_Load(object sender, EventArgs e)
        {
            // Buscar dados do bem
            DataTable dt = BLL.Bens.GetById(idBem);
            if (dt.Rows.Count == 0) return;
            DataRow row = dt.Rows[0];
            CarregarEstados(Convert.ToInt32(row["Estado_ID"]));
            CarregarCategorias(Convert.ToInt32(row["Categoria_ID"]));
            CarregarSetores(Convert.ToInt32(row["Localizacao_ID"]));
            textBox2.Text = row["Nome"].ToString();
            textBox4.Text = row["Valor"].ToString();
            textBox5.Text = row["Quantidade"].ToString();

            dateTimePicker1.Value = Convert.ToDateTime(row["Data_Aquisicao"]);
            textBox1.Text = row["Icon"].ToString();
            richTextBox1.Text = row["Descricao"].ToString();
        }
        private void CarregarEstados(int idEstadoSelecionado)
        {

            DataTable dt = Estados.GetAll();

            DataRow novaLinha = dt.NewRow();
            novaLinha["ID"] = -1;
            novaLinha["Nome"] = "Adicionar outro/Editar";
            dt.Rows.InsertAt(novaLinha, 0);

            comboBox2.DisplayMember = "Nome";
            comboBox2.ValueMember = "ID";
            comboBox2.DataSource = dt;

            comboBox2.SelectedValue = idEstadoSelecionado;
        }

        private void CarregarCategorias(int id)
        {

            DataTable dt = Categorias.GetAll();

            DataRow novaLinha = dt.NewRow();
            novaLinha["ID"] = -1;
            novaLinha["Nome"] = "Adicionar outro/Editar";
            dt.Rows.InsertAt(novaLinha, 0);

            comboBox1.DisplayMember = "Nome";
            comboBox1.ValueMember = "ID";
            comboBox1.DataSource = dt;

            comboBox1.SelectedValue = id;
        }
        private void CarregarSetores(int id)
        {

            DataTable dt = Setores.GetAll();

            DataRow novaLinha = dt.NewRow();
            novaLinha["ID"] = -1;
            novaLinha["Nome"] = "Adicionar outro/Editar";
            dt.Rows.InsertAt(novaLinha, 0);

            comboBox3.DisplayMember = "Nome";
            comboBox3.ValueMember = "ID";
            comboBox3.DataSource = dt;

            comboBox3.SelectedValue = id;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            // Validação simples (pode ser expandida conforme necessário)
            if (string.IsNullOrWhiteSpace(textBox2.Text))
            {
                MessageBox.Show("O nome do bem é obrigatório.");
                return;
            }

            // Obter valores dos controles
            string nome = textBox2.Text;
            string descricao = richTextBox1.Text;
            int categoriaID = (comboBox1.SelectedValue != null) ? Convert.ToInt32(comboBox1.SelectedValue) : -1;
            decimal valor = 0;
            decimal.TryParse(textBox4.Text, out valor);
            decimal quantidade = 0;
            decimal.TryParse(textBox5.Text, out quantidade);
            DateTime dataAquisicao = dateTimePicker1.Value;
            int estadoID = (comboBox2.SelectedValue != null) ? Convert.ToInt32(comboBox2.SelectedValue) : -1;
            int setorID = (comboBox3.SelectedValue != null) ? Convert.ToInt32(comboBox3.SelectedValue) : -1;
            string icone = textBox1.Text;

            // Chamar método de atualização
            bool atualizado = Bens.AtualizarBem(
                idBem,
                nome,
                descricao,
                categoriaID,
                valor,
                quantidade,
                dataAquisicao,
                estadoID,
                setorID,
                icone
            );

            if (atualizado)
            {
                MessageBox.Show("Bem atualizado com sucesso!");
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("Erro ao atualizar o bem.");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Focus();
            // Simulate Win + .
            // Adicione estas constantes na classe BemEditar (fora de qualquer método)
            const byte VK_LWIN = 0x5B;
            const byte VK_OEM_PERIOD = 0xBE;
            const int KEYEVENTF_KEYUP = 0x0002;
            keybd_event(VK_LWIN, 0, 0, 0);
            keybd_event(VK_OEM_PERIOD, 0, 0, 0);
            keybd_event(VK_OEM_PERIOD, 0, KEYEVENTF_KEYUP, 0);
            keybd_event(VK_LWIN, 0, KEYEVENTF_KEYUP, 0);
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)8) // Backspace
            {
                e.Handled = false; // allow backspace
                return;
            }

            // Block all other keys except surrogate pairs (for emojis)
            if (!char.IsSurrogate(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            // Block paste (Ctrl+V)
            if (e.Control && e.KeyCode == Keys.V)
            {
                e.SuppressKeyPress = true;
                e.Handled = true;
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text.Length > 4)
            {
                // Revert to previous text
                textBox1.Text = previousText;
                // Set cursor to the end
                textBox1.SelectionStart = textBox1.Text.Length;

            }
            else
            {
                // Update previousText only if within limit
                previousText = textBox1.Text;
            }
        }
    }
    }
