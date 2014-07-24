namespace NPC_Sprite_Generator
{
    partial class Form1
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
            this.previewBox = new System.Windows.Forms.PictureBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.currentlyWorking = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.gfxWidth = new System.Windows.Forms.TextBox();
            this.gfxHeight = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.save = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.previewBox)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // previewBox
            // 
            this.previewBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.previewBox.Location = new System.Drawing.Point(3, 16);
            this.previewBox.Name = "previewBox";
            this.previewBox.Size = new System.Drawing.Size(272, 275);
            this.previewBox.TabIndex = 0;
            this.previewBox.TabStop = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.previewBox);
            this.groupBox1.Location = new System.Drawing.Point(215, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(278, 294);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Preview";
            // 
            // currentlyWorking
            // 
            this.currentlyWorking.AutoSize = true;
            this.currentlyWorking.Location = new System.Drawing.Point(12, 28);
            this.currentlyWorking.Name = "currentlyWorking";
            this.currentlyWorking.Size = new System.Drawing.Size(197, 13);
            this.currentlyWorking.TabIndex = 2;
            this.currentlyWorking.Text = "Currently Working with {0} which is a {1}";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 59);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "GFX Width";
            // 
            // gfxWidth
            // 
            this.gfxWidth.Location = new System.Drawing.Point(80, 56);
            this.gfxWidth.Name = "gfxWidth";
            this.gfxWidth.Size = new System.Drawing.Size(129, 20);
            this.gfxWidth.TabIndex = 4;
            // 
            // gfxHeight
            // 
            this.gfxHeight.Location = new System.Drawing.Point(80, 82);
            this.gfxHeight.Name = "gfxHeight";
            this.gfxHeight.Size = new System.Drawing.Size(129, 20);
            this.gfxHeight.TabIndex = 6;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 85);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(62, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "GFX Height";
            // 
            // save
            // 
            this.save.Location = new System.Drawing.Point(12, 108);
            this.save.Name = "save";
            this.save.Size = new System.Drawing.Size(197, 64);
            this.save.TabIndex = 7;
            this.save.Text = "Ok, Save, Next!";
            this.save.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(500, 318);
            this.Controls.Add(this.save);
            this.Controls.Add(this.gfxHeight);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.gfxWidth);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.currentlyWorking);
            this.Controls.Add(this.groupBox1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.previewBox)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox previewBox;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label currentlyWorking;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox gfxWidth;
        private System.Windows.Forms.TextBox gfxHeight;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button save;
    }
}