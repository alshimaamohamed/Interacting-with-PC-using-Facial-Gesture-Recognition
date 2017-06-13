namespace WindowsFormsApplication1
{
    partial class RadialBasisForm
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
            this.chBoxBias = new System.Windows.Forms.CheckBox();
            this.chBox_trainOrTest = new System.Windows.Forms.CheckBox();
            this.txt_Eta = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.RadialBasis_Btn = new System.Windows.Forms.Button();
            this.t = new System.Windows.Forms.Label();
            this.e = new System.Windows.Forms.Label();
            this.h = new System.Windows.Forms.Label();
            this.txt_ThresholdLMS = new System.Windows.Forms.TextBox();
            this.txt_Epochs = new System.Windows.Forms.TextBox();
            this.txt_HiddenNeurons = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // chBoxBias
            // 
            this.chBoxBias.AutoSize = true;
            this.chBoxBias.Location = new System.Drawing.Point(15, 180);
            this.chBoxBias.Name = "chBoxBias";
            this.chBoxBias.Size = new System.Drawing.Size(45, 17);
            this.chBoxBias.TabIndex = 21;
            this.chBoxBias.Text = "bias";
            this.chBoxBias.UseVisualStyleBackColor = true;
            // 
            // chBox_trainOrTest
            // 
            this.chBox_trainOrTest.AutoSize = true;
            this.chBox_trainOrTest.Location = new System.Drawing.Point(15, 203);
            this.chBox_trainOrTest.Name = "chBox_trainOrTest";
            this.chBox_trainOrTest.Size = new System.Drawing.Size(48, 17);
            this.chBox_trainOrTest.TabIndex = 20;
            this.chBox_trainOrTest.Text = "train";
            this.chBox_trainOrTest.UseVisualStyleBackColor = true;
            // 
            // txt_Eta
            // 
            this.txt_Eta.Location = new System.Drawing.Point(123, 124);
            this.txt_Eta.Name = "txt_Eta";
            this.txt_Eta.Size = new System.Drawing.Size(100, 20);
            this.txt_Eta.TabIndex = 19;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 124);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(23, 13);
            this.label1.TabIndex = 18;
            this.label1.Text = "Eta";
            // 
            // RadialBasis_Btn
            // 
            this.RadialBasis_Btn.Location = new System.Drawing.Point(112, 191);
            this.RadialBasis_Btn.Name = "RadialBasis_Btn";
            this.RadialBasis_Btn.Size = new System.Drawing.Size(111, 39);
            this.RadialBasis_Btn.TabIndex = 17;
            this.RadialBasis_Btn.Text = "RadialBasis";
            this.RadialBasis_Btn.UseVisualStyleBackColor = true;
            this.RadialBasis_Btn.Click += new System.EventHandler(this.RadialBasis_Btn_Click);
            // 
            // t
            // 
            this.t.AutoSize = true;
            this.t.Location = new System.Drawing.Point(12, 85);
            this.t.Name = "t";
            this.t.Size = new System.Drawing.Size(76, 13);
            this.t.TabIndex = 16;
            this.t.Text = "Threshold LMS";
            // 
            // e
            // 
            this.e.AutoSize = true;
            this.e.Location = new System.Drawing.Point(12, 56);
            this.e.Name = "e";
            this.e.Size = new System.Drawing.Size(41, 13);
            this.e.TabIndex = 15;
            this.e.Text = "Epochs";
            // 
            // h
            // 
            this.h.AutoSize = true;
            this.h.Location = new System.Drawing.Point(12, 30);
            this.h.Name = "h";
            this.h.Size = new System.Drawing.Size(83, 13);
            this.h.TabIndex = 14;
            this.h.Text = "Hidden Neurons";
            // 
            // txt_ThresholdLMS
            // 
            this.txt_ThresholdLMS.Location = new System.Drawing.Point(123, 85);
            this.txt_ThresholdLMS.Name = "txt_ThresholdLMS";
            this.txt_ThresholdLMS.Size = new System.Drawing.Size(100, 20);
            this.txt_ThresholdLMS.TabIndex = 13;
            // 
            // txt_Epochs
            // 
            this.txt_Epochs.Location = new System.Drawing.Point(123, 49);
            this.txt_Epochs.Name = "txt_Epochs";
            this.txt_Epochs.Size = new System.Drawing.Size(100, 20);
            this.txt_Epochs.TabIndex = 12;
            // 
            // txt_HiddenNeurons
            // 
            this.txt_HiddenNeurons.Location = new System.Drawing.Point(123, 23);
            this.txt_HiddenNeurons.Name = "txt_HiddenNeurons";
            this.txt_HiddenNeurons.Size = new System.Drawing.Size(100, 20);
            this.txt_HiddenNeurons.TabIndex = 11;
            // 
            // RadialBasisForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(290, 262);
            this.Controls.Add(this.chBoxBias);
            this.Controls.Add(this.chBox_trainOrTest);
            this.Controls.Add(this.txt_Eta);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.RadialBasis_Btn);
            this.Controls.Add(this.t);
            this.Controls.Add(this.e);
            this.Controls.Add(this.h);
            this.Controls.Add(this.txt_ThresholdLMS);
            this.Controls.Add(this.txt_Epochs);
            this.Controls.Add(this.txt_HiddenNeurons);
            this.Name = "RadialBasisForm";
            this.Text = "RadialBasisForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox chBoxBias;
        private System.Windows.Forms.CheckBox chBox_trainOrTest;
        private System.Windows.Forms.TextBox txt_Eta;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button RadialBasis_Btn;
        private System.Windows.Forms.Label t;
        private System.Windows.Forms.Label e;
        private System.Windows.Forms.Label h;
        private System.Windows.Forms.TextBox txt_ThresholdLMS;
        private System.Windows.Forms.TextBox txt_Epochs;
        private System.Windows.Forms.TextBox txt_HiddenNeurons;
    }
}