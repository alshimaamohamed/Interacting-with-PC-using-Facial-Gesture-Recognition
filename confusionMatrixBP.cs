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
    public partial class confusionMatrixBP : Form
    {
        public confusionMatrixBP()
        {
            InitializeComponent();
        }

        private void confusionMatrixBP_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < 4; i++)
            {
                dataGridView1.Rows.Add("Class " + (i + 1));
            }
            dataGridView1[1, 0].Value = Backpropagation.resultOfTheTest[0][0].Count.ToString();
            dataGridView1[1, 1].Value = Backpropagation.resultOfTheTest[0][1].Count.ToString();
            dataGridView1[1, 2].Value = Backpropagation.resultOfTheTest[0][2].Count.ToString();
            dataGridView1[1, 3].Value = Backpropagation.resultOfTheTest[0][3].Count.ToString();

            dataGridView1[1, 4].Value = Backpropagation.resultOfTheTest[0][0].Count + Backpropagation.resultOfTheTest[0][1].Count +
                                     Backpropagation.resultOfTheTest[0][2].Count + Backpropagation.resultOfTheTest[0][3].Count;


            dataGridView1[2, 0].Value = Backpropagation.resultOfTheTest[1][0].Count.ToString();
            dataGridView1[2, 1].Value = Backpropagation.resultOfTheTest[1][1].Count.ToString();
            dataGridView1[2, 2].Value = Backpropagation.resultOfTheTest[1][2].Count.ToString();
            dataGridView1[2, 3].Value = Backpropagation.resultOfTheTest[1][3].Count.ToString();

            dataGridView1[2, 4].Value = Backpropagation.resultOfTheTest[1][0].Count + Backpropagation.resultOfTheTest[1][1].Count +
                                     Backpropagation.resultOfTheTest[1][2].Count + Backpropagation.resultOfTheTest[1][3].Count;


            dataGridView1[3, 0].Value = Backpropagation.resultOfTheTest[2][0].Count.ToString();
            dataGridView1[3, 1].Value = Backpropagation.resultOfTheTest[2][1].Count.ToString();
            dataGridView1[3, 2].Value = Backpropagation.resultOfTheTest[2][2].Count.ToString();
            dataGridView1[3, 3].Value = Backpropagation.resultOfTheTest[2][3].Count.ToString();

            dataGridView1[3, 4].Value = Backpropagation.resultOfTheTest[2][0].Count + Backpropagation.resultOfTheTest[2][1].Count +
                                     Backpropagation.resultOfTheTest[2][2].Count + Backpropagation.resultOfTheTest[2][3].Count;

            dataGridView1[4, 0].Value = Backpropagation.resultOfTheTest[3][0].Count.ToString();
            dataGridView1[4, 1].Value = Backpropagation.resultOfTheTest[3][1].Count.ToString();
            dataGridView1[4, 2].Value = Backpropagation.resultOfTheTest[3][2].Count.ToString();
            dataGridView1[4, 3].Value = Backpropagation.resultOfTheTest[3][3].Count.ToString();

            dataGridView1[4, 4].Value = Backpropagation.resultOfTheTest[3][0].Count + Backpropagation.resultOfTheTest[3][1].Count +
                                     Backpropagation.resultOfTheTest[3][2].Count + Backpropagation.resultOfTheTest[3][3].Count;


            dataGridView1[5, 0].Value = Backpropagation.resultOfTheTest[0][0].Count + Backpropagation.resultOfTheTest[1][0].Count +
                                     Backpropagation.resultOfTheTest[2][0].Count + Backpropagation.resultOfTheTest[3][0].Count;

            dataGridView1[5, 1].Value = Backpropagation.resultOfTheTest[0][1].Count + Backpropagation.resultOfTheTest[1][1].Count +
                                    Backpropagation.resultOfTheTest[2][1].Count + Backpropagation.resultOfTheTest[3][1].Count;

            dataGridView1[5, 2].Value = Backpropagation.resultOfTheTest[0][2].Count + Backpropagation.resultOfTheTest[1][2].Count +
                                    Backpropagation.resultOfTheTest[2][2].Count + Backpropagation.resultOfTheTest[3][2].Count;

            dataGridView1[5, 3].Value = Backpropagation.resultOfTheTest[0][3].Count + Backpropagation.resultOfTheTest[1][3].Count +
                                    Backpropagation.resultOfTheTest[2][3].Count + Backpropagation.resultOfTheTest[3][3].Count;

            double countAll = (Backpropagation.resultOfTheTest[0][0].Count + Backpropagation.resultOfTheTest[1][1].Count +
                                    Backpropagation.resultOfTheTest[2][2].Count + Backpropagation.resultOfTheTest[3][3].Count);
            double div = (countAll / 20) * 100;
            label2.Text = div + " %";
        }
    }
}
