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
    public partial class RadialBasisForm : Form
    {
        public static RadialBasis RadialBasis = new RadialBasis();
        public RadialBasisForm()
        {
            InitializeComponent();
        }

        private void RadialBasis_Btn_Click(object sender, EventArgs e)
        {
            RadialBasis.SetInput(int.Parse(txt_HiddenNeurons.Text), double.Parse(txt_ThresholdLMS.Text), int.Parse(txt_Epochs.Text), double.Parse(txt_Eta.Text), chBoxBias.Checked);
            if (chBox_trainOrTest.Checked)
            {
                RadialBasis.Kmean();
                RadialBasis.RadialBasisTraining();
                RadialBasis.RadialBasisTesting();
                RadialBasis.saveData();
                confusionMatrixRadialBasis cm = new confusionMatrixRadialBasis();
                cm.Show();
            }

            else
            {
                if (RadialBasis.readFromFile(false))
                {
                    RadialBasis.RadialBasisTesting();
                    confusionMatrixRadialBasis cm = new confusionMatrixRadialBasis();
                    cm.Show();
                }
                else
                    MessageBox.Show("file does not exist ");
            }
        }

    }
}
