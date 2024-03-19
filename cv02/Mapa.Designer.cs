namespace cv02
{
    partial class Mapa
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Mapa));
            toolStrip1 = new ToolStrip();
            toolStripSeparator1 = new ToolStripSeparator();
            pridani_uzlu = new ToolStripButton();
            vymazani_uzlu = new ToolStripButton();
            posun_uzlu = new ToolStripButton();
            toolStripSeparator2 = new ToolStripSeparator();
            vytvoreni_useku = new ToolStripButton();
            vymazani_useku = new ToolStripButton();
            toolStripSeparator3 = new ToolStripSeparator();
            Paths = new ToolStripLabel();
            count_paths = new ToolStripLabel();
            toolStripSeparator4 = new ToolStripSeparator();
            DisjunktPaths = new ToolStripLabel();
            DisjunktPathsCount = new ToolStripLabel();
            Panel = new Panel();
            panel1 = new Panel();
            SeznamDisjunktnichCest = new ListBox();
            SeznamCest = new ListBox();
            label2 = new Label();
            label1 = new Label();
            PaintPanel = new Panel();
            contextMenuStrip1 = new ContextMenuStrip(components);
            toolStrip1.SuspendLayout();
            Panel.SuspendLayout();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // toolStrip1
            // 
            toolStrip1.Items.AddRange(new ToolStripItem[] { toolStripSeparator1, pridani_uzlu, vymazani_uzlu, posun_uzlu, toolStripSeparator2, vytvoreni_useku, vymazani_useku, toolStripSeparator3, Paths, count_paths, toolStripSeparator4, DisjunktPaths, DisjunktPathsCount });
            toolStrip1.Location = new Point(0, 0);
            toolStrip1.Name = "toolStrip1";
            toolStrip1.Size = new Size(1484, 25);
            toolStrip1.TabIndex = 1;
            toolStrip1.Text = "toolStrip1";
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new Size(6, 25);
            // 
            // pridani_uzlu
            // 
            pridani_uzlu.BackColor = Color.White;
            pridani_uzlu.DisplayStyle = ToolStripItemDisplayStyle.Text;
            pridani_uzlu.ImageTransparentColor = Color.Magenta;
            pridani_uzlu.Name = "pridani_uzlu";
            pridani_uzlu.Size = new Size(33, 22);
            pridani_uzlu.Text = "Add";
            pridani_uzlu.Click += pridani_uzlu_Click;
            // 
            // vymazani_uzlu
            // 
            vymazani_uzlu.CheckOnClick = true;
            vymazani_uzlu.DisplayStyle = ToolStripItemDisplayStyle.Text;
            vymazani_uzlu.Image = (Image)resources.GetObject("vymazani_uzlu.Image");
            vymazani_uzlu.ImageTransparentColor = Color.Magenta;
            vymazani_uzlu.Name = "vymazani_uzlu";
            vymazani_uzlu.Size = new Size(44, 22);
            vymazani_uzlu.Text = "Delete";
            vymazani_uzlu.Click += vymazani_uzlu_Click;
            // 
            // posun_uzlu
            // 
            posun_uzlu.CheckOnClick = true;
            posun_uzlu.DisplayStyle = ToolStripItemDisplayStyle.Text;
            posun_uzlu.Image = (Image)resources.GetObject("posun_uzlu.Image");
            posun_uzlu.ImageTransparentColor = Color.Magenta;
            posun_uzlu.Name = "posun_uzlu";
            posun_uzlu.Size = new Size(41, 22);
            posun_uzlu.Text = "Move";
            posun_uzlu.Click += posun_uzlu_Click;
            // 
            // toolStripSeparator2
            // 
            toolStripSeparator2.Name = "toolStripSeparator2";
            toolStripSeparator2.Size = new Size(6, 25);
            // 
            // vytvoreni_useku
            // 
            vytvoreni_useku.CheckOnClick = true;
            vytvoreni_useku.DisplayStyle = ToolStripItemDisplayStyle.Text;
            vytvoreni_useku.Image = (Image)resources.GetObject("vytvoreni_useku.Image");
            vytvoreni_useku.ImageTransparentColor = Color.Magenta;
            vytvoreni_useku.Name = "vytvoreni_useku";
            vytvoreni_useku.Size = new Size(72, 22);
            vytvoreni_useku.Text = "Create path";
            vytvoreni_useku.Click += vytvoreni_useku_Click;
            // 
            // vymazani_useku
            // 
            vymazani_useku.CheckOnClick = true;
            vymazani_useku.DisplayStyle = ToolStripItemDisplayStyle.Text;
            vymazani_useku.Image = (Image)resources.GetObject("vymazani_useku.Image");
            vymazani_useku.ImageTransparentColor = Color.Magenta;
            vymazani_useku.Name = "vymazani_useku";
            vymazani_useku.Size = new Size(81, 22);
            vymazani_useku.Text = "Remove path";
            vymazani_useku.Click += vymazani_useku_Click;
            // 
            // toolStripSeparator3
            // 
            toolStripSeparator3.Name = "toolStripSeparator3";
            toolStripSeparator3.Size = new Size(6, 25);
            // 
            // Paths
            // 
            Paths.Name = "Paths";
            Paths.Size = new Size(39, 22);
            Paths.Text = "Paths:";
            // 
            // count_paths
            // 
            count_paths.Name = "count_paths";
            count_paths.Size = new Size(13, 22);
            count_paths.Text = "0";
            // 
            // toolStripSeparator4
            // 
            toolStripSeparator4.Name = "toolStripSeparator4";
            toolStripSeparator4.Size = new Size(6, 25);
            // 
            // DisjunktPaths
            // 
            DisjunktPaths.Name = "DisjunktPaths";
            DisjunktPaths.Size = new Size(85, 22);
            DisjunktPaths.Text = "Disjunkt Paths:";
            // 
            // DisjunktPathsCount
            // 
            DisjunktPathsCount.Name = "DisjunktPathsCount";
            DisjunktPathsCount.Size = new Size(13, 22);
            DisjunktPathsCount.Text = "0";
            // 
            // Panel
            // 
            Panel.Controls.Add(panel1);
            Panel.Controls.Add(PaintPanel);
            Panel.Dock = DockStyle.Fill;
            Panel.Location = new Point(0, 25);
            Panel.Name = "Panel";
            Panel.Size = new Size(1484, 736);
            Panel.TabIndex = 2;
            // 
            // panel1
            // 
            panel1.Controls.Add(SeznamDisjunktnichCest);
            panel1.Controls.Add(SeznamCest);
            panel1.Controls.Add(label2);
            panel1.Controls.Add(label1);
            panel1.Dock = DockStyle.Right;
            panel1.Location = new Point(1244, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(240, 736);
            panel1.TabIndex = 1;
            // 
            // SeznamDisjunktnichCest
            // 
            SeznamDisjunktnichCest.FormattingEnabled = true;
            SeznamDisjunktnichCest.ItemHeight = 15;
            SeznamDisjunktnichCest.Location = new Point(1, 297);
            SeznamDisjunktnichCest.Name = "SeznamDisjunktnichCest";
            SeznamDisjunktnichCest.Size = new Size(240, 439);
            SeznamDisjunktnichCest.TabIndex = 4;
            // 
            // SeznamCest
            // 
            SeznamCest.FormattingEnabled = true;
            SeznamCest.ItemHeight = 15;
            SeznamCest.Location = new Point(1, 22);
            SeznamCest.Name = "SeznamCest";
            SeznamCest.Size = new Size(238, 244);
            SeznamCest.TabIndex = 3;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(5, 275);
            label2.Name = "label2";
            label2.Size = new Size(140, 15);
            label2.TabIndex = 2;
            label2.Text = "Seznam disjunktních cest";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(2, 4);
            label1.Name = "label1";
            label1.Size = new Size(72, 15);
            label1.TabIndex = 1;
            label1.Text = "Seznam cest";
            // 
            // PaintPanel
            // 
            PaintPanel.AutoScroll = true;
            PaintPanel.BackgroundImage = Properties.Resources.mapa;
            PaintPanel.BackgroundImageLayout = ImageLayout.Stretch;
            PaintPanel.Location = new Point(0, 0);
            PaintPanel.Name = "PaintPanel";
            PaintPanel.Size = new Size(1248, 736);
            PaintPanel.TabIndex = 0;
            PaintPanel.MouseWheel += PaintPanel_MouseWheel;
            // 
            // contextMenuStrip1
            // 
            contextMenuStrip1.Name = "contextMenuStrip1";
            contextMenuStrip1.Size = new Size(61, 4);
            // 
            // Mapa
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1484, 761);
            Controls.Add(Panel);
            Controls.Add(toolStrip1);
            Name = "Mapa";
            Text = "Form1";
            toolStrip1.ResumeLayout(false);
            toolStrip1.PerformLayout();
            Panel.ResumeLayout(false);
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private ToolStrip toolStrip1;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripButton pridani_uzlu;
        private Panel Panel;
        private Panel PaintPanel;
        private ToolStripButton vymazani_uzlu;
        private ToolStripButton posun_uzlu;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripButton vytvoreni_useku;
        private ToolStripButton vymazani_useku;
        private ToolStripSeparator toolStripSeparator3;
        private ContextMenuStrip contextMenuStrip1;
        private ToolStripLabel Paths;
        private ToolStripLabel count_paths;
        private ToolStripSeparator toolStripSeparator4;
        private ToolStripLabel DisjunktPaths;
        private ToolStripLabel DisjunktPathsCount;
        private Panel panel1;
        private Label label2;
        private Label label1;
        private ListBox SeznamDisjunktnichCest;
        private ListBox SeznamCest;
    }
}