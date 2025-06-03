namespace TrackInvent
{
    partial class TelaPrincipal
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TelaPrincipal));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.bensToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cadastrarBemToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.listarBensToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.movimentaçõesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.novaMovimentaçãoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.históricoDeMovimentaçõesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.manutençõesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.registrarManutençãoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.históricoDeManutençõesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.relatóriosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gerarRelatórioPDFExcelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.utilizadoresToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gerirUtilizadoresToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gerirUtilizadoresToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.contaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.GripMargin = new System.Windows.Forms.Padding(2, 2, 0, 2);
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.bensToolStripMenuItem,
            this.movimentaçõesToolStripMenuItem,
            this.manutençõesToolStripMenuItem,
            this.relatóriosToolStripMenuItem,
            this.utilizadoresToolStripMenuItem,
            this.contaToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1661, 33);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // bensToolStripMenuItem
            // 
            this.bensToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cadastrarBemToolStripMenuItem,
            this.listarBensToolStripMenuItem});
            this.bensToolStripMenuItem.Name = "bensToolStripMenuItem";
            this.bensToolStripMenuItem.Size = new System.Drawing.Size(90, 29);
            this.bensToolStripMenuItem.Text = "📦Bens";
            // 
            // cadastrarBemToolStripMenuItem
            // 
            this.cadastrarBemToolStripMenuItem.Name = "cadastrarBemToolStripMenuItem";
            this.cadastrarBemToolStripMenuItem.Size = new System.Drawing.Size(259, 34);
            this.cadastrarBemToolStripMenuItem.Text = "➕ Cadastrar Bem";
            this.cadastrarBemToolStripMenuItem.Click += new System.EventHandler(this.cadastrarBemToolStripMenuItem_Click);
            // 
            // listarBensToolStripMenuItem
            // 
            this.listarBensToolStripMenuItem.Name = "listarBensToolStripMenuItem";
            this.listarBensToolStripMenuItem.Size = new System.Drawing.Size(259, 34);
            this.listarBensToolStripMenuItem.Text = "🗃️ Gerir Bens";
            this.listarBensToolStripMenuItem.Click += new System.EventHandler(this.listarBensToolStripMenuItem_Click);
            // 
            // movimentaçõesToolStripMenuItem
            // 
            this.movimentaçõesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.novaMovimentaçãoToolStripMenuItem,
            this.históricoDeMovimentaçõesToolStripMenuItem});
            this.movimentaçõesToolStripMenuItem.Name = "movimentaçõesToolStripMenuItem";
            this.movimentaçõesToolStripMenuItem.Size = new System.Drawing.Size(184, 29);
            this.movimentaçõesToolStripMenuItem.Text = "🔄 Movimentações";
            // 
            // novaMovimentaçãoToolStripMenuItem
            // 
            this.novaMovimentaçãoToolStripMenuItem.Name = "novaMovimentaçãoToolStripMenuItem";
            this.novaMovimentaçãoToolStripMenuItem.Size = new System.Drawing.Size(371, 34);
            this.novaMovimentaçãoToolStripMenuItem.Text = "➕ Nova Movimentação";
            this.novaMovimentaçãoToolStripMenuItem.Click += new System.EventHandler(this.novaMovimentaçãoToolStripMenuItem_Click);
            // 
            // históricoDeMovimentaçõesToolStripMenuItem
            // 
            this.históricoDeMovimentaçõesToolStripMenuItem.Name = "históricoDeMovimentaçõesToolStripMenuItem";
            this.históricoDeMovimentaçõesToolStripMenuItem.Size = new System.Drawing.Size(371, 34);
            this.históricoDeMovimentaçõesToolStripMenuItem.Text = "📜 Histórico de Movimentações";
            this.históricoDeMovimentaçõesToolStripMenuItem.Click += new System.EventHandler(this.históricoDeMovimentaçõesToolStripMenuItem_Click);
            // 
            // manutençõesToolStripMenuItem
            // 
            this.manutençõesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.registrarManutençãoToolStripMenuItem,
            this.históricoDeManutençõesToolStripMenuItem});
            this.manutençõesToolStripMenuItem.Name = "manutençõesToolStripMenuItem";
            this.manutençõesToolStripMenuItem.Size = new System.Drawing.Size(164, 29);
            this.manutençõesToolStripMenuItem.Text = "🔧 Manutenções";
            // 
            // registrarManutençãoToolStripMenuItem
            // 
            this.registrarManutençãoToolStripMenuItem.Name = "registrarManutençãoToolStripMenuItem";
            this.registrarManutençãoToolStripMenuItem.Size = new System.Drawing.Size(351, 34);
            this.registrarManutençãoToolStripMenuItem.Text = "➕ Registrar Manutenção";
            // 
            // históricoDeManutençõesToolStripMenuItem
            // 
            this.históricoDeManutençõesToolStripMenuItem.Name = "históricoDeManutençõesToolStripMenuItem";
            this.históricoDeManutençõesToolStripMenuItem.Size = new System.Drawing.Size(351, 34);
            this.históricoDeManutençõesToolStripMenuItem.Text = "📋 Histórico de Manutenções";
            // 
            // relatóriosToolStripMenuItem
            // 
            this.relatóriosToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.gerarRelatórioPDFExcelToolStripMenuItem});
            this.relatóriosToolStripMenuItem.Name = "relatóriosToolStripMenuItem";
            this.relatóriosToolStripMenuItem.Size = new System.Drawing.Size(136, 29);
            this.relatóriosToolStripMenuItem.Text = "📑 Relatórios";
            // 
            // gerarRelatórioPDFExcelToolStripMenuItem
            // 
            this.gerarRelatórioPDFExcelToolStripMenuItem.Name = "gerarRelatórioPDFExcelToolStripMenuItem";
            this.gerarRelatórioPDFExcelToolStripMenuItem.Size = new System.Drawing.Size(343, 34);
            this.gerarRelatórioPDFExcelToolStripMenuItem.Text = "➕ Gerar Relatório PDF/Excel";
            this.gerarRelatórioPDFExcelToolStripMenuItem.Click += new System.EventHandler(this.gerarRelatórioPDFExcelToolStripMenuItem_Click);
            // 
            // utilizadoresToolStripMenuItem
            // 
            this.utilizadoresToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.gerirUtilizadoresToolStripMenuItem,
            this.gerirUtilizadoresToolStripMenuItem1});
            this.utilizadoresToolStripMenuItem.Name = "utilizadoresToolStripMenuItem";
            this.utilizadoresToolStripMenuItem.Size = new System.Drawing.Size(150, 29);
            this.utilizadoresToolStripMenuItem.Text = "👤 Utilizadores";
            // 
            // gerirUtilizadoresToolStripMenuItem
            // 
            this.gerirUtilizadoresToolStripMenuItem.Name = "gerirUtilizadoresToolStripMenuItem";
            this.gerirUtilizadoresToolStripMenuItem.Size = new System.Drawing.Size(299, 34);
            this.gerirUtilizadoresToolStripMenuItem.Text = "➕ Cadastrar Utilizador";
            this.gerirUtilizadoresToolStripMenuItem.Click += new System.EventHandler(this.gerirUtilizadoresToolStripMenuItem_Click);
            // 
            // gerirUtilizadoresToolStripMenuItem1
            // 
            this.gerirUtilizadoresToolStripMenuItem1.Name = "gerirUtilizadoresToolStripMenuItem1";
            this.gerirUtilizadoresToolStripMenuItem1.Size = new System.Drawing.Size(299, 34);
            this.gerirUtilizadoresToolStripMenuItem1.Text = "📜 Gerir Utilizadores";
            this.gerirUtilizadoresToolStripMenuItem1.Click += new System.EventHandler(this.gerirUtilizadoresToolStripMenuItem1_Click);
            // 
            // contaToolStripMenuItem
            // 
            this.contaToolStripMenuItem.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.contaToolStripMenuItem.Name = "contaToolStripMenuItem";
            this.contaToolStripMenuItem.Size = new System.Drawing.Size(161, 29);
            this.contaToolStripMenuItem.Text = "❌ Sair da conta";
            this.contaToolStripMenuItem.Click += new System.EventHandler(this.contaToolStripMenuItem_Click);
            // 
            // TelaPrincipal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1661, 450);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "TelaPrincipal";
            this.Text = " ";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.TelaPrincipal_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem bensToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cadastrarBemToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem listarBensToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem movimentaçõesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem novaMovimentaçãoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem históricoDeMovimentaçõesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem manutençõesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem registrarManutençãoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem históricoDeManutençõesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem relatóriosToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem gerarRelatórioPDFExcelToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem utilizadoresToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem gerirUtilizadoresToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem gerirUtilizadoresToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem contaToolStripMenuItem;
    }
}