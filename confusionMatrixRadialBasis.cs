using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class confusionMatrixRadialBasis : Form
    {
        public confusionMatrixRadialBasis()
        {
            InitializeComponent();
        }

        private void confusionMatrixRadialBasis_Load(object sender, EventArgs e)
        {

            double digonal = 0, finalTotal = 0;
            double[] total1 = new double[4];
            DataGridViewRow data;
            for (int j = 1; j <= 4; j++)
            {

                data = (DataGridViewRow)confusionMatrixGridView.Rows[j - 1].Clone();

                data.Cells[0].Value = "classs " + (j);
                int total = 0;
                for (int i = 0; i < 4; i++)
                {
                    if (i == (j - 1))
                        digonal += RadialBasisForm.RadialBasis.ConfusionMatrix[j - 1][i];
                    total += RadialBasisForm.RadialBasis.ConfusionMatrix[j - 1][i];
                    total1[i] += RadialBasisForm.RadialBasis.ConfusionMatrix[j - 1][i];
                    data.Cells[i + 1].Value = RadialBasisForm.RadialBasis.ConfusionMatrix[j - 1][i];
                }
                finalTotal += total;
                data.Cells[5].Value = total;
                confusionMatrixGridView.Rows.Add(data);
            }
            data = (DataGridViewRow)confusionMatrixGridView.Rows[4].Clone();
            data.Cells[0].Value = "total";
            for (int l = 1; l <= 4; l++)
                data.Cells[l].Value = total1[l - 1];
            data.Cells[5].Value = finalTotal;
            confusionMatrixGridView.Rows.Add(data);

            //====================================================================
            double overallAcc = Math.Round((digonal / finalTotal) * 100);
            label1.Text = "Overall Accuracy= " + overallAcc.ToString() + " % ";
        }
    }
}
