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
    public partial class CadastroBens : Form
    {
        [DllImport("user32.dll")]

        static extern void keybd_event(byte bVk, byte bScan, int dwFlags, int dwExtraInfo);

        const int KEYEVENTF_KEYUP = 0x0002;
        const byte VK_LWIN = 0x5B;
        const byte VK_OEM_PERIOD = 0xBE;
        string previousText;

        public CadastroBens()
        {
            InitializeComponent();
            CarregarEstados();
            CarregarCategorias();
            CarregarSetores();  
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Focus();
            // Simulate Win + .
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
       

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (carregandoEstados)
                return;  // Ignorar enquanto carrega

            if (comboBox2.SelectedItem == null)
                return;  // Nada selecionado, ignora

            DataRowView row = comboBox2.SelectedItem as DataRowView;
            if (row != null)
            {
                string selected = row["Nome"].ToString();

                if (selected == "Adicionar outro/Editar")
                {
                    EstadoEditorForm fe = new EstadoEditorForm();
                    fe.ShowDialog();

                    // Recarregar e resetar seleção
                    CarregarEstados();
                }
            }
        }
         private bool carregandoEstados = false;
        private void CarregarEstados()
        {
            carregandoEstados = true;

            DataTable dt = Estados.GetAll();

         DataRow novaLinha = dt.NewRow();
novaLinha["ID"] = -1; // Or another unique negative value
novaLinha["Nome"] = "Adicionar outro/Editar";
dt.Rows.InsertAt(novaLinha, 0); // Add to the top of the list


            comboBox2.DataSource = dt;
            comboBox2.DisplayMember = "Nome";
            comboBox2.ValueMember = "Nome";

            comboBox2.SelectedIndex = -1;  // Nenhum selecionado

            carregandoEstados = false;
        }
        bool carregandoCategorias = false;
        private void CarregarCategorias()
        {
            carregandoCategorias = true;

            DataTable dt = Categorias.GetAll();


            DataRow novaLinha = dt.NewRow();
            novaLinha["Nome"] = "Adicionar outro/Editar";
            dt.Rows.Add(novaLinha);

            comboBox1.DataSource = dt;
            comboBox1.DisplayMember = "Nome";
            comboBox1.ValueMember = "Nome";

            comboBox1.SelectedIndex = -1;  // Nenhum selecionado

            carregandoCategorias = false;
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (carregandoCategorias)
                return;  // Ignorar enquanto carrega

            if (comboBox1.SelectedIndex == -1 || comboBox1.SelectedItem == null)
                return;  // Nada selecionado, ignora

            DataRowView row = comboBox1.SelectedItem as DataRowView;
            if (row != null)
            {
                string selected = row["Nome"].ToString();

                if (selected == "Adicionar outro/Editar")
                {
                    CategoriaEditorForm fe = new CategoriaEditorForm();
                    fe.ShowDialog();

                    // Recarregar e resetar seleção
                    CarregarCategorias();
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Validar Nome
            if (string.IsNullOrWhiteSpace(textBox2.Text))
            {
                MessageBox.Show("Por favor, insira o nome.");
                return;
            }
            string nome = textBox2.Text.Trim();

            // Validar Categoria
            if (comboBox1.SelectedValue == null)
            {
                MessageBox.Show("Por favor, selecione uma categoria.");
                return;
            }
            int categoria = BLL.Categorias.GetIDByNome(comboBox1.SelectedValue.ToString());

            // Validar Valor
            if (!decimal.TryParse(textBox4.Text.Trim(), out decimal valor))
            {
                MessageBox.Show("Valor inválido.");
                return;
            }

            // Validar Quantidade
            if (!decimal.TryParse(textBox5.Text.Trim(), out decimal quantidade))
            {
                MessageBox.Show("Quantidade inválida.");
                return;
            }

            // Validar Estado
            if (comboBox2.SelectedValue == null || comboBox2.SelectedValue.ToString().Contains("Adicionar"))
            {
                MessageBox.Show("Por favor, selecione um estado válido.");
                return;
            }
            int estado = BLL.Estados.GetIDByNome(comboBox2.SelectedValue.ToString());

            // Validar Setor
            if (comboBox3.SelectedValue == null || comboBox3.SelectedValue.ToString().Contains("Adicionar"))
            {
                MessageBox.Show("Por favor, selecione um setor válido.");
                return;
            }
            int setor = BLL.Setores.GetIDByNome(comboBox3.SelectedValue.ToString());

            // Obter Data
            DateTime dataAquisicao = dateTimePicker1.Value;

            // Icone e Descrição
            string icon = string.IsNullOrWhiteSpace(textBox1.Text) ? null : textBox1.Text.Trim();
            string descricao = string.IsNullOrWhiteSpace(richTextBox1.Text) ? null : richTextBox1.Text;

            // Gravar no banco de dados
            bool sucesso = BLL.Bens.CriarBem(nome, descricao, categoria, valor, quantidade, dataAquisicao, estado, setor, icon);

            if (sucesso)
            {
                MessageBox.Show("Bem cadastrado com sucesso!");
            }
            else
            {
                MessageBox.Show("Erro ao cadastrar o bem.");
            }
        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox textBox = sender as TextBox;

            // Allow digits, control chars, and one dot (.)
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // Only allow one decimal point
            if ((e.KeyChar == '.') && textBox.Text.Contains("."))
            {
                e.Handled = true;
            }
        }

        private void textBox5_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox textBox = sender as TextBox;

            // Allow digits, control chars, and one dot (.)
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // Only allow one decimal point
            if ((e.KeyChar == '.') && textBox.Text.Contains("."))
            {
                e.Handled = true;
            }
        }
        bool carregandoSetor = false;
        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (carregandoSetor)
                return;  // Ignorar enquanto carrega

            if (comboBox3.SelectedIndex == -1 || comboBox3.SelectedItem == null)
                return;  // Nada selecionado, ignora

            DataRowView row = comboBox3.SelectedItem as DataRowView;
            if (row != null)
            {
                string selected = row["Nome"].ToString();

                if (selected == "Adicionar outro/Editar")
                {
                    SetoresEditarForm fe = new SetoresEditarForm();
                    fe.ShowDialog();

                    // Recarregar e resetar seleção
                    CarregarSetores();
                }
            }
        }
        private void CarregarSetores()
        {
            carregandoSetor = true;

            DataTable dt = Setores.GetAll();

            DataRow novaLinha = dt.NewRow();
            novaLinha["Nome"] = "Adicionar outro/Editar";
            dt.Rows.Add(novaLinha);

            comboBox3.DataSource = dt;
            comboBox3.DisplayMember = "Nome";
            comboBox3.ValueMember = "Nome";

            comboBox3.SelectedIndex = -1;  // Nenhum selecionado

            carregandoSetor = false;
        }
        private void textBox5_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.V)
            {
                e.SuppressKeyPress = true;
                e.Handled = true;
            }
        }

        private void textBox4_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.V)
            {
                e.SuppressKeyPress = true;
                e.Handled = true;
            }
        }
    }
}
