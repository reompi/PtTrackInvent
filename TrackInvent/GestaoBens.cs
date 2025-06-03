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
    public partial class GestaoBens : Form
    {
        public GestaoBens()
        {
            InitializeComponent();
            LoadBens(BLL.Bens.GetAll());
            CarregarFiltros();

        }
        private void LoadBens(DataTable dtBens)
        {
            panel1.Controls.Clear();

            int cardWidth = 120;
            int cardHeight = 180; // Aumenta para caber mais informações
            int margin = 10;
            int columns = 6;
            if (columns < 1) columns = 1;

            for (int i = 0; i < dtBens.Rows.Count; i++)
            {
                DataRow row = dtBens.Rows[i];
                string nome = row["Nome"].ToString();
                string icon = row["Icon"]?.ToString() ?? "";
                    string quantidade = row["Quantidade"].ToString();
                string valor = row["Valor"].ToString();

                int col = i % columns;
                int rowIndex = i / columns;

                int x = col * (cardWidth + margin);
                int y = rowIndex * (cardHeight + margin);

                // Botão com imagem
                Button btn = new Button();
                btn.Width = cardWidth;
                btn.Height = cardHeight-60;
                btn.Location = new Point(0, 0);
                btn.BackgroundImageLayout = ImageLayout.Zoom;
                btn.FlatStyle = FlatStyle.Flat;
                btn.FlatAppearance.BorderSize = 0;
                btn.Font = new Font("Calibri", 24F, FontStyle.Regular);
                btn.Text = icon;
                btn.Tag = row["ID"];
                btn.Click += BtnIcon_Click; 

                // Label nome
                Label lblNome = new Label();
                lblNome.Text = nome;
                lblNome.Width = cardWidth;
                lblNome.Height = 22;
                lblNome.TextAlign = ContentAlignment.MiddleCenter;
                lblNome.Location = new Point(0, btn.Bottom + 2);

                // Label quantidade
                Label lblQtd = new Label();
                lblQtd.Text = "Qtd: " + quantidade;
                lblQtd.Width = cardWidth;
                lblQtd.Height = 18;
                lblQtd.Location = new Point(0, lblNome.Bottom + 2);

                // Label valor
                Label lblValor = new Label();
                lblValor.Text = "Valor: " + valor;
                lblValor.Width = cardWidth;
                lblValor.Height = 18;
                lblValor.Location = new Point(0, lblQtd.Bottom + 2);

                // Painel do card
                Panel card = new Panel();
                card.Width = cardWidth;
                card.Height = cardHeight;
                card.Location = new Point(x + 10, y + 80);
                card.Controls.Add(btn);
                card.Controls.Add(lblNome);
                card.Controls.Add(lblQtd);
                card.Controls.Add(lblValor);
                card.BorderStyle = BorderStyle.FixedSingle;

                panel1.Controls.Add(card);
            }
        }
        private void BtnIcon_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            int bemID = (int)btn.Tag;

            // Exemplo: abrir formulário de detalhes
            var formDetalhes = new BemEditar(bemID);
            formDetalhes.ShowDialog();

            LoadBens(BLL.Bens.GetAll()); // Recarrega caso tenha havido alteração
        }

        private Panel panelFiltros;
        private TextBox txtPesquisa;
        private ComboBox cbCategoria, cbEstado, cbSetor;
        private bool isPlaceholderTxt = true;
        private bool isPlaceholderCbEstado = true;
        private bool isPlaceholderCbSetor = true;
        private bool isPlaceholderCbCategoria = true;
        private void CarregarFiltros()
        {
            if (panelFiltros != null) return; // Já carregado

            panelFiltros = new Panel();
            panelFiltros.Dock = DockStyle.Top;
            panelFiltros.Height = 60;
            this.Controls.Add(panelFiltros);
            this.Controls.SetChildIndex(panelFiltros, 0); // Garante que está no topo

            // TextBox de pesquisa
            txtPesquisa = new TextBox();
            txtPesquisa.Text = "Pesquisar nome...";
            txtPesquisa.ForeColor = Color.Gray;
            // Eventos para simular placeholder
            txtPesquisa.Enter += (s, e) =>
            {
                if (txtPesquisa.Text == "Pesquisar nome...")
                {
                    txtPesquisa.Text = "";
                    txtPesquisa.ForeColor = Color.Black;
                    isPlaceholderTxt = false;
                }
            };
            txtPesquisa.Leave += (s, e) =>
            {
                if (string.IsNullOrWhiteSpace(txtPesquisa.Text))
                {
                    isPlaceholderTxt = true;
                    txtPesquisa.Text = "Pesquisar nome...";
                    txtPesquisa.ForeColor = Color.Gray;
                }
            };
              txtPesquisa.TextChanged += (s, e) =>
              {
                if (isPlaceholderTxt == false)
                    AtualizarFiltros();
              };
            txtPesquisa.Location = new Point(10, 15);
            txtPesquisa.Width = 150;
            panelFiltros.Controls.Add(txtPesquisa);

            // ComboBox Categoria
            cbCategoria = CriarComboComVazio(BLL.Categorias.GetAll(), "Nome", new Point(180, 15),"Categoria");
            cbCategoria.SelectedIndexChanged += (s, e) => {
                if (cbCategoria.SelectedIndex == 0) // Se for o placeholder
                {
                    isPlaceholderCbCategoria = true;
                }
                else
                {
                    isPlaceholderCbCategoria = false;
                }
                AtualizarFiltros(); };
            panelFiltros.Controls.Add(cbCategoria);

            // ComboBox Estado
            cbEstado = CriarComboComVazio(BLL.Estados.GetAll(), "Nome", new Point(330, 15),"Estado");
            cbEstado.SelectedIndexChanged += (s, e) =>
            {
                if (cbEstado.SelectedIndex == 0) // Se for o placeholder
                {
                    isPlaceholderCbEstado = true;
                }
                else
                {
                    isPlaceholderCbEstado = false;
                }
                AtualizarFiltros();
            }; 
            panelFiltros.Controls.Add(cbEstado);

            // ComboBox Setor
            cbSetor = CriarComboComVazio(BLL.Setores.GetAll(), "Nome", new Point(480, 15), "Setor");
            cbSetor.SelectedIndexChanged += (s, e) => {
                if (cbSetor.SelectedIndex == 0) // Se for o placeholder
                {
                    isPlaceholderCbSetor = true;
                }
                else
                {
                    isPlaceholderCbSetor = false;
                }
                AtualizarFiltros(); };
            panelFiltros.Controls.Add(cbSetor);
        }

        private void GestaoBens_Load(object sender, EventArgs e)
        {

        }

        private ComboBox CriarComboComVazio(DataTable dt, string display, Point pos, string sender)
        {
            if (!dt.Columns.Contains(display))
                throw new ArgumentException($"Column '{display}' does not exist in the DataTable.", nameof(display));

            // Create a new table and add the placeholder row
            DataTable newDt = dt.Clone();
            DataRow emptyRow = newDt.NewRow();
            emptyRow[display] = "Sem filtro de " + sender;
            newDt.Rows.Add(emptyRow);

            foreach (DataRow row in dt.Rows)
            {
                newDt.ImportRow(row);
            }

            // Create and configure ComboBox
            ComboBox cb = new ComboBox
            {
                DropDownStyle = ComboBoxStyle.DropDownList,
                Location = pos,
                Width = 145,
                DataSource = newDt,
                DisplayMember = display,
                DrawMode = DrawMode.OwnerDrawFixed // Enable custom drawing
            };

            // Custom draw event for gray placeholder
            cb.DrawItem += (s, e) =>
            {
                if (e.Index < 0) return;

                ComboBox combo = (ComboBox)s;
                string text = ((DataRowView)combo.Items[e.Index])[display].ToString();

                // Set background and focus rectangle
                e.DrawBackground();
                e.DrawFocusRectangle();

                // Use gray for placeholder, otherwise normal color
                using (Brush brush = new SolidBrush(
                    e.Index == 0 ? Color.Gray : combo.ForeColor))
                {
                    e.Graphics.DrawString(text, e.Font, brush, e.Bounds);
                }
            };

            return cb;
        }


        private void AtualizarFiltros()
        {
            string pesquisa = isPlaceholderTxt ? "" : txtPesquisa.Text.Trim();
            int? categoriaId = isPlaceholderCbCategoria ? (int?)null : BLL.Categorias.GetIDByNome(cbCategoria.Text);
 ;

            int? estadoId = isPlaceholderCbEstado ? (int?)null : BLL.Estados.GetIDByNome(cbEstado.Text);
 ;
            int? setorId = isPlaceholderCbSetor ? (int?)null : BLL.Setores.GetIDByNome(cbSetor.Text); ;


            // Chamada ao BLL com filtros
            DataTable dtBens = BLL.Bens.GetByFiltro(pesquisa, categoriaId, estadoId, setorId);

            LoadBens(dtBens); // Este é o método que monta os cards
        }

    }
}
