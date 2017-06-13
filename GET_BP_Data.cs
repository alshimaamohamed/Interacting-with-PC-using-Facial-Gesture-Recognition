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
    public partial class GET_BP_Data : Form
    {
        public static Backpropagation backprop = new Backpropagation();

        public GET_BP_Data()
        {
            InitializeComponent();
        }

        private void GET_BP_Data_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            System.IO.File.WriteAllText("BP_Data.txt", string.Empty);   
            string[] numNodes = textBox5.Text.ToString().Split(',');
            List<int> num=new List<int>();
            num.Add(19);
            for(int i=0;i<numNodes.Length;i++)
            {
                num.Add(int.Parse(numNodes[i]));
            }
            num.Add(4);
            bool b;
            if (checkBox1.Checked)
            {
                b = true;
            }
            else
            {
                b = false;
            }
            if (num.Count == int.Parse(textBox4.Text.ToString())+2)
            {
                backprop.readData(float.Parse(textBox1.Text.ToString()), int.Parse(textBox2.Text.ToString()),
                    float.Parse(textBox3.Text.ToString()), int.Parse(textBox4.Text.ToString()),
                   num, b);
                backprop.train(FeatureExtraction_Class.FeaturesExtractedForTrainning);
               // backprop.generateWeigths();
                
            }
            else
            {
                MessageBox.Show("length of nodes is not equal weights numbers");
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
        }

        private void button2_Click(object sender, EventArgs e)
        {
            backprop.testData(FeatureExtraction_Class.FeaturesExtractedForTesting);
            confusionMatrixBP conf = new confusionMatrixBP();
            conf.Show();
        }
    }
}
