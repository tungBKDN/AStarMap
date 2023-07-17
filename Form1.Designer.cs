namespace DirectMap
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
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.addButton = new System.Windows.Forms.Button();
            this.connectButton = new System.Windows.Forms.Button();
            this.directButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.locX = new System.Windows.Forms.Label();
            this.locY = new System.Windows.Forms.Label();
            this.taskName = new System.Windows.Forms.Label();
            this.cost = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::DirectionMap.Properties.Resources.Ảnh_chụp_màn_hình_2023_07_12_152457;
            this.pictureBox1.Location = new System.Drawing.Point(5, 4);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(1212, 848);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.WaitOnLoad = true;
            this.pictureBox1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseClick);
            this.pictureBox1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseMove);
            // 
            // addButton
            // 
            this.addButton.Location = new System.Drawing.Point(1223, 12);
            this.addButton.Name = "addButton";
            this.addButton.Size = new System.Drawing.Size(69, 23);
            this.addButton.TabIndex = 1;
            this.addButton.Text = "Add";
            this.addButton.UseVisualStyleBackColor = true;
            this.addButton.Click += new System.EventHandler(this.addButton_Click);
            // 
            // connectButton
            // 
            this.connectButton.Location = new System.Drawing.Point(1223, 41);
            this.connectButton.Name = "connectButton";
            this.connectButton.Size = new System.Drawing.Size(69, 23);
            this.connectButton.TabIndex = 1;
            this.connectButton.Text = "Connect";
            this.connectButton.UseVisualStyleBackColor = true;
            this.connectButton.Click += new System.EventHandler(this.connectButton_Click);
            // 
            // directButton
            // 
            this.directButton.Location = new System.Drawing.Point(1223, 70);
            this.directButton.Name = "directButton";
            this.directButton.Size = new System.Drawing.Size(69, 23);
            this.directButton.TabIndex = 1;
            this.directButton.Text = "Direct";
            this.directButton.UseVisualStyleBackColor = true;
            this.directButton.Click += new System.EventHandler(this.directButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(1223, 99);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(69, 23);
            this.cancelButton.TabIndex = 1;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(1220, 206);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "LocationX";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(1220, 252);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "LocationY";
            // 
            // locX
            // 
            this.locX.AutoSize = true;
            this.locX.Location = new System.Drawing.Point(1220, 221);
            this.locX.Name = "locX";
            this.locX.Size = new System.Drawing.Size(55, 13);
            this.locX.TabIndex = 2;
            this.locX.Text = "LocationX";
            // 
            // locY
            // 
            this.locY.AutoSize = true;
            this.locY.Location = new System.Drawing.Point(1220, 267);
            this.locY.Name = "locY";
            this.locY.Size = new System.Drawing.Size(55, 13);
            this.locY.TabIndex = 2;
            this.locY.Text = "LocationY";
            // 
            // taskName
            // 
            this.taskName.AutoSize = true;
            this.taskName.Location = new System.Drawing.Point(1220, 143);
            this.taskName.Name = "taskName";
            this.taskName.Size = new System.Drawing.Size(55, 13);
            this.taskName.TabIndex = 2;
            this.taskName.Text = "LocationX";
            // 
            // cost
            // 
            this.cost.AutoSize = true;
            this.cost.Font = new System.Drawing.Font("Microsoft Sans Serif", 18.33962F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cost.Location = new System.Drawing.Point(1223, 352);
            this.cost.Name = "cost";
            this.cost.Size = new System.Drawing.Size(90, 31);
            this.cost.TabIndex = 3;
            this.cost.Text = "--- km";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(1220, 339);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(28, 13);
            this.label4.TabIndex = 2;
            this.label4.Text = "Cost";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1361, 861);
            this.Controls.Add(this.cost);
            this.Controls.Add(this.locY);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.locX);
            this.Controls.Add(this.taskName);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.directButton);
            this.Controls.Add(this.connectButton);
            this.Controls.Add(this.addButton);
            this.Controls.Add(this.pictureBox1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button addButton;
        private System.Windows.Forms.Button connectButton;
        private System.Windows.Forms.Button directButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label locX;
        private System.Windows.Forms.Label locY;
        private System.Windows.Forms.Label taskName;
        private System.Windows.Forms.Label cost;
        private System.Windows.Forms.Label label4;
    }
}

