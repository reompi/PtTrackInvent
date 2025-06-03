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
    public partial class TelaPrincipal : Form
    {
        public TelaPrincipal()
        {
            InitializeComponent();
            this.IsMdiContainer = true;
            foreach (Control c in this.Controls)
            {
                if (c is MdiClient)
                {
                    c.BackColor = Color.White; 
                }
            }
        }

        private void cadastrarBemToolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

        private void gerirUtilizadoresToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Registro formRegistro = new Registro();
            formRegistro.MdiParent = this;
            formRegistro.Show();
        }

        private void contaToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void setoresToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void gerirSetoresToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void novaMovimentaçãoToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void gerirUtilizadoresToolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

        private void cadastrarBemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CadastroBens cadastroBens = new CadastroBens();
                cadastroBens.MdiParent = this;
            cadastroBens.Show();
        }

        private void listarBensToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GestaoBens gestaoBens = new GestaoBens();
            gestaoBens.MdiParent = this;
            gestaoBens.Show();
        }
    }
}
