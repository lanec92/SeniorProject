
namespace frmReservation
{
    partial class frmReservation
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtPassName = new System.Windows.Forms.TextBox();
            this.cmbSeatRow = new System.Windows.Forms.ComboBox();
            this.radA = new System.Windows.Forms.RadioButton();
            this.grpSeatsColumn = new System.Windows.Forms.GroupBox();
            this.radD = new System.Windows.Forms.RadioButton();
            this.radC = new System.Windows.Forms.RadioButton();
            this.radB = new System.Windows.Forms.RadioButton();
            this.btnAddPass = new System.Windows.Forms.Button();
            this.btnShowPass = new System.Windows.Forms.Button();
            this.btnSearchPass = new System.Windows.Forms.Button();
            this.lstOutput = new System.Windows.Forms.ListBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnLogOut = new System.Windows.Forms.Button();
            this.grpSeatsColumn.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Name:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(20, 59);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 20);
            this.label2.TabIndex = 1;
            this.label2.Text = "Seat:";
            // 
            // txtPassName
            // 
            this.txtPassName.Location = new System.Drawing.Point(73, 21);
            this.txtPassName.Name = "txtPassName";
            this.txtPassName.Size = new System.Drawing.Size(138, 20);
            this.txtPassName.TabIndex = 2;
            // 
            // cmbSeatRow
            // 
            this.cmbSeatRow.FormattingEnabled = true;
            this.cmbSeatRow.Location = new System.Drawing.Point(73, 61);
            this.cmbSeatRow.Name = "cmbSeatRow";
            this.cmbSeatRow.Size = new System.Drawing.Size(121, 21);
            this.cmbSeatRow.TabIndex = 3;
            // 
            // radA
            // 
            this.radA.AutoSize = true;
            this.radA.Location = new System.Drawing.Point(6, 16);
            this.radA.Name = "radA";
            this.radA.Size = new System.Drawing.Size(38, 24);
            this.radA.TabIndex = 4;
            this.radA.TabStop = true;
            this.radA.Text = "A";
            this.radA.UseVisualStyleBackColor = true;
            // 
            // grpSeatsColumn
            // 
            this.grpSeatsColumn.Controls.Add(this.radD);
            this.grpSeatsColumn.Controls.Add(this.radC);
            this.grpSeatsColumn.Controls.Add(this.radB);
            this.grpSeatsColumn.Controls.Add(this.radA);
            this.grpSeatsColumn.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpSeatsColumn.Location = new System.Drawing.Point(73, 88);
            this.grpSeatsColumn.Name = "grpSeatsColumn";
            this.grpSeatsColumn.Size = new System.Drawing.Size(97, 87);
            this.grpSeatsColumn.TabIndex = 5;
            this.grpSeatsColumn.TabStop = false;
            // 
            // radD
            // 
            this.radD.AutoSize = true;
            this.radD.Location = new System.Drawing.Point(50, 46);
            this.radD.Name = "radD";
            this.radD.Size = new System.Drawing.Size(39, 24);
            this.radD.TabIndex = 7;
            this.radD.TabStop = true;
            this.radD.Text = "D";
            this.radD.UseVisualStyleBackColor = true;
            // 
            // radC
            // 
            this.radC.AutoSize = true;
            this.radC.Location = new System.Drawing.Point(6, 46);
            this.radC.Name = "radC";
            this.radC.Size = new System.Drawing.Size(38, 24);
            this.radC.TabIndex = 6;
            this.radC.TabStop = true;
            this.radC.Text = "C";
            this.radC.UseVisualStyleBackColor = true;
            // 
            // radB
            // 
            this.radB.AutoSize = true;
            this.radB.Location = new System.Drawing.Point(50, 16);
            this.radB.Name = "radB";
            this.radB.Size = new System.Drawing.Size(38, 24);
            this.radB.TabIndex = 5;
            this.radB.TabStop = true;
            this.radB.Text = "B";
            this.radB.UseVisualStyleBackColor = true;
            // 
            // btnAddPass
            // 
            this.btnAddPass.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddPass.Location = new System.Drawing.Point(73, 201);
            this.btnAddPass.Name = "btnAddPass";
            this.btnAddPass.Size = new System.Drawing.Size(157, 28);
            this.btnAddPass.TabIndex = 6;
            this.btnAddPass.Text = "Add Passenger";
            this.btnAddPass.UseVisualStyleBackColor = true;
            this.btnAddPass.Click += new System.EventHandler(this.btnAddPass_Click);
            // 
            // btnShowPass
            // 
            this.btnShowPass.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnShowPass.Location = new System.Drawing.Point(73, 262);
            this.btnShowPass.Name = "btnShowPass";
            this.btnShowPass.Size = new System.Drawing.Size(157, 28);
            this.btnShowPass.TabIndex = 7;
            this.btnShowPass.Text = "Show Passengers";
            this.btnShowPass.UseVisualStyleBackColor = true;
            this.btnShowPass.Click += new System.EventHandler(this.btnShowPass_Click);
            // 
            // btnSearchPass
            // 
            this.btnSearchPass.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSearchPass.Location = new System.Drawing.Point(73, 319);
            this.btnSearchPass.Name = "btnSearchPass";
            this.btnSearchPass.Size = new System.Drawing.Size(157, 28);
            this.btnSearchPass.TabIndex = 8;
            this.btnSearchPass.Text = "Search Passengers";
            this.btnSearchPass.UseVisualStyleBackColor = true;
            this.btnSearchPass.Click += new System.EventHandler(this.btnSearchPass_Click);
            // 
            // lstOutput
            // 
            this.lstOutput.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstOutput.FormattingEnabled = true;
            this.lstOutput.ItemHeight = 24;
            this.lstOutput.Location = new System.Drawing.Point(271, 41);
            this.lstOutput.Name = "lstOutput";
            this.lstOutput.Size = new System.Drawing.Size(257, 340);
            this.lstOutput.TabIndex = 10;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(267, 18);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(174, 20);
            this.label3.TabIndex = 11;
            this.label3.Text = "(1A, 1B, 1C, 1D, ...10D)";
            // 
            // btnLogOut
            // 
            this.btnLogOut.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLogOut.Location = new System.Drawing.Point(73, 379);
            this.btnLogOut.Name = "btnLogOut";
            this.btnLogOut.Size = new System.Drawing.Size(157, 28);
            this.btnLogOut.TabIndex = 12;
            this.btnLogOut.Text = "Log Out";
            this.btnLogOut.UseVisualStyleBackColor = true;
            this.btnLogOut.Click += new System.EventHandler(this.btnLogOut_Click);
            // 
            // frmReservation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(548, 463);
            this.Controls.Add(this.btnLogOut);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lstOutput);
            this.Controls.Add(this.btnSearchPass);
            this.Controls.Add(this.btnShowPass);
            this.Controls.Add(this.btnAddPass);
            this.Controls.Add(this.grpSeatsColumn);
            this.Controls.Add(this.cmbSeatRow);
            this.Controls.Add(this.txtPassName);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "frmReservation";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Airline Reservation";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmReservation_Closing);
            this.Load += new System.EventHandler(this.frmReservation_Load);
            this.grpSeatsColumn.ResumeLayout(false);
            this.grpSeatsColumn.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtPassName;
        private System.Windows.Forms.ComboBox cmbSeatRow;
        private System.Windows.Forms.RadioButton radA;
        private System.Windows.Forms.GroupBox grpSeatsColumn;
        private System.Windows.Forms.RadioButton radD;
        private System.Windows.Forms.RadioButton radC;
        private System.Windows.Forms.RadioButton radB;
        private System.Windows.Forms.Button btnAddPass;
        private System.Windows.Forms.Button btnShowPass;
        private System.Windows.Forms.Button btnSearchPass;
        private System.Windows.Forms.ListBox lstOutput;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnLogOut;
    }
}

