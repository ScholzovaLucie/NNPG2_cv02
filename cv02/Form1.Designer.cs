namespace cv02
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            panel1 = new Panel();
            toolStrip1 = new ToolStrip();
            toolStripSeparator1 = new ToolStripSeparator();
            Add = new ToolStripButton();
            Unite = new ToolStripButton();
            toolStripSeparator2 = new ToolStripSeparator();
            Move = new ToolStripButton();
            toolStripButton4 = new ToolStripButton();
            Delete = new ToolStripButton();
            toolStripSeparator3 = new ToolStripSeparator();
            toolStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackgroundImage = Properties.Resources.Snímek_obrazovky_2024_03_04_102451;
            panel1.BackgroundImageLayout = ImageLayout.Stretch;
            panel1.Dock = DockStyle.Fill;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(800, 450);
            panel1.TabIndex = 0;
            panel1.MouseDown += MouseDown;
            panel1.MouseMove += MouseMove;
            panel1.MouseUp += MouseUp;
            panel1.MouseWheel += MouseWheel;
            panel1.Paint += Form1_Paint;
            // 
            // toolStrip1
            // 
            toolStrip1.Items.AddRange(new ToolStripItem[] { toolStripSeparator1, Add, Unite, toolStripSeparator2, Move, toolStripButton4, Delete, toolStripSeparator3 });
            toolStrip1.Location = new Point(0, 0);
            toolStrip1.Name = "toolStrip1";
            toolStrip1.Size = new Size(800, 25);
            toolStrip1.TabIndex = 1;
            toolStrip1.Text = "toolStrip1";
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new Size(6, 25);
            // 
            // Add
            // 
            Add.BackColor = SystemColors.ControlLight;
            Add.DisplayStyle = ToolStripItemDisplayStyle.Image;
            Add.Image = (Image)resources.GetObject("Add.Image");
            Add.ImageTransparentColor = Color.Magenta;
            Add.Name = "Add";
            Add.Size = new Size(23, 22);
            Add.Text = "toolStripButton1";
            // 
            // Unite
            // 
            Unite.DisplayStyle = ToolStripItemDisplayStyle.Image;
            Unite.Image = (Image)resources.GetObject("Unite.Image");
            Unite.ImageTransparentColor = Color.Magenta;
            Unite.Name = "Unite";
            Unite.Size = new Size(23, 22);
            Unite.Text = "toolStripButton2";
            // 
            // toolStripSeparator2
            // 
            toolStripSeparator2.Name = "toolStripSeparator2";
            toolStripSeparator2.Size = new Size(6, 25);
            // 
            // Move
            // 
            Move.DisplayStyle = ToolStripItemDisplayStyle.Image;
            Move.Image = (Image)resources.GetObject("Move.Image");
            Move.ImageTransparentColor = Color.Magenta;
            Move.Name = "Move";
            Move.Size = new Size(23, 22);
            Move.Text = "toolStripButton3";
            // 
            // toolStripButton4
            // 
            toolStripButton4.DisplayStyle = ToolStripItemDisplayStyle.Image;
            toolStripButton4.Image = (Image)resources.GetObject("toolStripButton4.Image");
            toolStripButton4.ImageTransparentColor = Color.Magenta;
            toolStripButton4.Name = "toolStripButton4";
            toolStripButton4.Size = new Size(23, 22);
            toolStripButton4.Text = "toolStripButton4";
            // 
            // Delete
            // 
            Delete.DisplayStyle = ToolStripItemDisplayStyle.Image;
            Delete.Image = (Image)resources.GetObject("Delete.Image");
            Delete.ImageTransparentColor = Color.Magenta;
            Delete.Name = "Delete";
            Delete.Size = new Size(23, 22);
            Delete.Text = "toolStripButton5";
            // 
            // toolStripSeparator3
            // 
            toolStripSeparator3.Name = "toolStripSeparator3";
            toolStripSeparator3.Size = new Size(6, 25);
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(toolStrip1);
            Controls.Add(panel1);
            Name = "Form1";
            Text = "Form1";
            toolStrip1.ResumeLayout(false);
            toolStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel panel1;
        private ToolStrip toolStrip1;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripButton Add;
        private ToolStripButton Unite;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripButton Move;
        private ToolStripButton toolStripButton4;
        private ToolStripButton Delete;
        private ToolStripSeparator toolStripSeparator3;
    }
}