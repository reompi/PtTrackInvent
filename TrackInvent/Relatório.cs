using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ClosedXML.Excel;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;

namespace TrackInvent
{
    public partial class Relatório : Form
    {
        private bool isPlaceholderCbEstado = true;
        private bool isPlaceholderCbSetor = true;
        private bool isPlaceholderCbCategoria = true;
        public Relatório()
        {
            InitializeComponent();
            CarregarFiltros();
            btnFiltrar_Click(null, EventArgs.Empty);
            dtInicio.Format = DateTimePickerFormat.Custom;
            dtInicio.CustomFormat = "dd/MM/yyyy";

            dtFim.Format = DateTimePickerFormat.Custom;
            dtFim.CustomFormat = "dd/MM/yyyy";

        }

        private void Relatório_Load(object sender, EventArgs e)
        {

        }
        private void CarregarFiltros()
        {
            CriarComboComVazio(BLL.Categorias.GetAll(), cbCategoria, "Nome", "Categoria");
            cbCategoria.ValueMember = "ID";
            cbCategoria.SelectedIndexChanged += (s, e) =>
            {
                if (cbCategoria.SelectedIndex == 0) // Se for o placeholder
                {
                    isPlaceholderCbCategoria = true;
                }
                else
                {
                    isPlaceholderCbCategoria = false;
                }
            };
            CriarComboComVazio(BLL.Estados.GetAll(), cbEstado, "Nome", "Estado");
            cbEstado.ValueMember = "ID";
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
            };
            CriarComboComVazio(BLL.Setores.GetAll(),cbSetor, "Nome" ,"Setor");
            cbSetor.ValueMember = "ID";
            cbSetor.SelectedIndexChanged += (s, e) =>
            {
                if (cbSetor.SelectedIndex == 0) // Se for o placeholder
                {
                    isPlaceholderCbSetor = true;
                }
                else
                {
                    isPlaceholderCbSetor = false;
                }
            };
            dtInicio.Value = DateTime.Today.AddMonths(-6);
            dtFim.Value = DateTime.Today;
        }
        private ComboBox CriarComboComVazio(DataTable dt , ComboBox cb, string display, string sender)
        {
             if (!dt.Columns.Contains(display))
                throw new ArgumentException($"Column '{display}' does not exist in the DataTable.", nameof(display));

            // Create a new table and add the placeholder row
            DataTable newDt = dt.Clone();
            DataRow emptyRow = newDt.NewRow();
            emptyRow[display] = "Sem filtro de " + sender;

            newDt.Rows.Add(emptyRow);

            // Agora adiciona as outras linhas do dt original
            foreach (DataRow row in dt.Rows)
            {
                newDt.ImportRow(row);
            }
            cb.DisplayMember = display;

            cb.DataSource = newDt;
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
        private void btnFiltrar_Click(object sender, EventArgs e)
        {

            int? categoriaId = isPlaceholderCbCategoria ? (int?)null : BLL.Categorias.GetIDByNome(cbCategoria.Text);
            ;

            int? estadoId = isPlaceholderCbEstado ? (int?)null : BLL.Estados.GetIDByNome(cbEstado.Text);
            ;
            int? setorId = isPlaceholderCbSetor ? (int?)null : BLL.Setores.GetIDByNome(cbSetor.Text); ;
            DateTime dataInicio = dtInicio.Value.Date;
            DateTime dataFim = dtFim.Value.Date;

            DataTable resultado = BLL.Bens.Filtrar(categoriaId, estadoId, setorId, dataInicio, dataFim);

            dgvRelatorio.DataSource = resultado;

            CalcularTotais(resultado);
        }
        private void CalcularTotais(DataTable dt)
        {
            int totalBens = 0;
            decimal totalValor = 0;

            foreach (DataRow row in dt.Rows)
            {
                int qtd = Convert.ToInt32(row["Quantidade"]);
                decimal valor = Convert.ToDecimal(row["Valor"]);
                totalBens += qtd;
                totalValor += valor * qtd;
            }

            lblTotalBens.Text = $"Total de Bens: {totalBens}";
            lblValorTotal.Text = $"Valor Total: {totalValor:C}";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ExportarParaExcel((DataTable)dgvRelatorio.DataSource);
        }
        private void button2_Click(object sender, EventArgs e)
        {
            ExportarParaPDF((DataTable)dgvRelatorio.DataSource);
        }
        private void ExportarParaExcel(DataTable dt)
        {
            using (SaveFileDialog sfd = new SaveFileDialog()
            {
                Filter = "Excel Workbook|*.xlsx",
                Title = "Salvar Relatório em Excel"
            })
            {
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    using (XLWorkbook wb = new XLWorkbook())
                    {
                        wb.Worksheets.Add(dt, "Relatorio");
                        wb.SaveAs(sfd.FileName);
                        MessageBox.Show("Exportado com sucesso para Excel!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }
        private void ExportarParaPDF(DataTable dt)
        {
            using (SaveFileDialog sfd = new SaveFileDialog()
            {
                Filter = "PDF Files|*.pdf",
                Title = "Salvar Relatório em PDF"
            })
            {
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    using (FileStream stream = new FileStream(sfd.FileName, FileMode.Create))
                    {
                        Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 20f, 20f);
                        PdfWriter.GetInstance(pdfDoc, stream);
                        pdfDoc.Open();

                        PdfPTable pdfTable = new PdfPTable(dt.Columns.Count);
                        pdfTable.WidthPercentage = 100;

                        // Cabeçalhos
                        foreach (DataColumn column in dt.Columns)
                        {
                            pdfTable.AddCell(new Phrase(column.ColumnName, FontFactory.GetFont("Arial", "10", Font.Bold)));
                        }

                        // Dados
                        foreach (DataRow row in dt.Rows)
                        {
                            foreach (object cell in row.ItemArray)
                            {
                                pdfTable.AddCell(cell.ToString());
                            }
                        }

                        pdfDoc.Add(pdfTable);
                        pdfDoc.Close();
                        stream.Close();
                        MessageBox.Show("Exportado com sucesso para PDF!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }
    }
}
