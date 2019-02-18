namespace UdpFinishing.UIControls
{
    partial class UCcontacts
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.labelStatus = new System.Windows.Forms.Label();
            this.labelName = new System.Windows.Forms.Label();
            this.ovlProfile = new UdpFinishing.UIControls.OvalPictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.ovlProfile)).BeginInit();
            this.SuspendLayout();
            // 
            // labelStatus
            // 
            this.labelStatus.AutoEllipsis = true;
            this.labelStatus.AutoSize = true;
            this.labelStatus.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelStatus.ForeColor = System.Drawing.SystemColors.Highlight;
            this.labelStatus.Location = new System.Drawing.Point(68, 36);
            this.labelStatus.Name = "labelStatus";
            this.labelStatus.Size = new System.Drawing.Size(45, 17);
            this.labelStatus.TabIndex = 6;
            this.labelStatus.Text = "label2";
            // 
            // labelName
            // 
            this.labelName.AutoEllipsis = true;
            this.labelName.AutoSize = true;
            this.labelName.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelName.ForeColor = System.Drawing.Color.White;
            this.labelName.Location = new System.Drawing.Point(64, 14);
            this.labelName.Name = "labelName";
            this.labelName.Size = new System.Drawing.Size(58, 19);
            this.labelName.TabIndex = 5;
            this.labelName.Text = "label1";
            // 
            // ovlProfile
            // 
            this.ovlProfile.BackColor = System.Drawing.Color.DarkGray;
            this.ovlProfile.Location = new System.Drawing.Point(0, 0);
            this.ovlProfile.Name = "ovlProfile";
            this.ovlProfile.Size = new System.Drawing.Size(60, 61);
            this.ovlProfile.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.ovlProfile.TabIndex = 7;
            this.ovlProfile.TabStop = false;
            // 
            // UCcontacts
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.Controls.Add(this.ovlProfile);
            this.Controls.Add(this.labelStatus);
            this.Controls.Add(this.labelName);
            this.Name = "UCcontacts";
            this.Size = new System.Drawing.Size(144, 66);
            ((System.ComponentModel.ISupportInitialize)(this.ovlProfile)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelStatus;
        private System.Windows.Forms.Label labelName;
        private OvalPictureBox ovlProfile;
    }
}
