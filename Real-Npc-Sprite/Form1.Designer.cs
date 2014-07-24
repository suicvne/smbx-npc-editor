namespace Real_Npc_Sprite
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
            this.save = new System.Windows.Forms.Button();
            this.gfxHeight = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.gfxWidth = new System.Windows.Forms.TextBox();
            this.previewBox = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.currentNpc = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.useOffset = new System.Windows.Forms.CheckBox();
            this.scaleButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.previewBox)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // save
            // 
            this.save.Location = new System.Drawing.Point(13, 104);
            this.save.Name = "save";
            this.save.Size = new System.Drawing.Size(197, 64);
            this.save.TabIndex = 14;
            this.save.Text = "Ok, Save, Next!";
            this.save.UseVisualStyleBackColor = true;
            this.save.Click += new System.EventHandler(this.save_Click);
            // 
            // gfxHeight
            // 
            this.gfxHeight.Location = new System.Drawing.Point(81, 78);
            this.gfxHeight.Name = "gfxHeight";
            this.gfxHeight.Size = new System.Drawing.Size(129, 20);
            this.gfxHeight.TabIndex = 13;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 81);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(62, 13);
            this.label2.TabIndex = 12;
            this.label2.Text = "GFX Height";
            // 
            // gfxWidth
            // 
            this.gfxWidth.Location = new System.Drawing.Point(81, 52);
            this.gfxWidth.Name = "gfxWidth";
            this.gfxWidth.Size = new System.Drawing.Size(129, 20);
            this.gfxWidth.TabIndex = 11;
            // 
            // previewBox
            // 
            this.previewBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.previewBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.previewBox.Location = new System.Drawing.Point(3, 16);
            this.previewBox.Name = "previewBox";
            this.previewBox.Size = new System.Drawing.Size(272, 275);
            this.previewBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.previewBox.TabIndex = 0;
            this.previewBox.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 55);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 13);
            this.label1.TabIndex = 10;
            this.label1.Text = "GFX Width";
            // 
            // currentNpc
            // 
            this.currentNpc.AutoSize = true;
            this.currentNpc.Location = new System.Drawing.Point(13, 24);
            this.currentNpc.Name = "currentNpc";
            this.currentNpc.Size = new System.Drawing.Size(21, 13);
            this.currentNpc.TabIndex = 9;
            this.currentNpc.Text = "{0}";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.previewBox);
            this.groupBox1.Location = new System.Drawing.Point(216, 8);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(278, 294);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Preview";
            // 
            // useOffset
            // 
            this.useOffset.AutoSize = true;
            this.useOffset.Location = new System.Drawing.Point(19, 203);
            this.useOffset.Name = "useOffset";
            this.useOffset.Size = new System.Drawing.Size(74, 17);
            this.useOffset.TabIndex = 15;
            this.useOffset.Text = "Use offset";
            this.useOffset.UseVisualStyleBackColor = true;
            this.useOffset.CheckedChanged += new System.EventHandler(this.useOffset_CheckedChanged);
            // 
            // scaleButton
            // 
            this.scaleButton.Location = new System.Drawing.Point(13, 174);
            this.scaleButton.Name = "scaleButton";
            this.scaleButton.Size = new System.Drawing.Size(197, 23);
            this.scaleButton.TabIndex = 16;
            this.scaleButton.Text = "Proportionally Size to 32 x 32";
            this.scaleButton.UseVisualStyleBackColor = true;
            this.scaleButton.Click += new System.EventHandler(this.scaleButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(505, 314);
            this.Controls.Add(this.scaleButton);
            this.Controls.Add(this.useOffset);
            this.Controls.Add(this.save);
            this.Controls.Add(this.gfxHeight);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.gfxWidth);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.currentNpc);
            this.Controls.Add(this.groupBox1);
            this.Name = "Form1";
            this.Text = "Sprite Maker";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.previewBox)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button save;
        private System.Windows.Forms.TextBox gfxHeight;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox gfxWidth;
        private System.Windows.Forms.PictureBox previewBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label currentNpc;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox useOffset;
        private System.Windows.Forms.Button scaleButton;
    }
}

