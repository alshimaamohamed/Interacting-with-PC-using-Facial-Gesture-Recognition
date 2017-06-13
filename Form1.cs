using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Speech.Synthesis;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public static string filename = "";
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            string fn = openFileDialog1.FileName;
            filename = fn;
            Bitmap B = PGMUtil.ToBitmap(fn);
            this.Size = B.Size;
            pictureBox1.Image = (Image)B;
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            int s = fn.LastIndexOf('\\') + 1;
            this.Text = fn.Substring(s) + " (W: " + B.Width.ToString() + ", H: " + B.Height.ToString() + ")";
            this.Size = new Size(624, 391);

            //==================================================================

        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Read training Data From .pts Files (PTSUtil)
            PTSUtil.ReadPtsFiles("train");   //Input String Could Be "train" || "test"

            // Extract Features From ReadData (FeatureExtraction_Class) 
            FeatureExtraction_Class.Extract_Feature("train");

            PTSUtil.ReadPtsFiles("test");
            FeatureExtraction_Class.Extract_Feature("test");

            FeatureExtraction_Class.ShufflingData();
            MessageBox.Show("Process Done.");
            //EX: To Use:
            // MessageBox.Show(PTSUtil.DataSetOfAllClassesInTesting[0][0].Item1);
            //MessageBox.Show(FeatureExtraction_Class.FeaturesExtractedForTrainning[1][2].Item1);

            ///////////////////////////////////////////////////////////////////
        }

        private void button3_Click(object sender, EventArgs e)
        {
            GET_BP_Data BP = new GET_BP_Data();
            BP.Show();
        }
        private const int SW_RESTORE = 9;
        private const int SW_MINIMIZE = 6;
        [DllImport("User32")]
        private static extern int ShowWindow(int hwnd, int nCmdShow);
        private void button4_Click(object sender, EventArgs e)
        {
            //==================================================================
            //Test And Do an Action
            if (filename != "")
            {
                List<Tuple<double, double>> points = new List<Tuple<double, double>>();
                filename = filename.Replace(".pgm", ".pts");
                points = PTSUtil.ReadImagePTSfile(filename);

                List<double> features = FeatureExtraction_Class.ExtractImgFeature(points);
                int classNum = Backpropagation.testoneImg(features);
                SpeechSynthesizer synth = new SpeechSynthesizer();

                // Configure the audio output. 
                synth.SetOutputToDefaultAudioDevice();
                synth.Rate = 5;
                ProcessStartInfo startInfo = new ProcessStartInfo("notepad.exe");
                if (classNum == 0)
                {
                    synth.Speak("This person is looking down");
                    //Process.Start("notepad.exe", @"E:\fcis-4\term2\Neural Network\Labs\trainData.txt");
                    int hWnd;
                    Process[] processRunning = Process.GetProcesses();
                    foreach (Process pr in processRunning)
                    {
                        if (pr.ProcessName == "notepad")
                        {
                            hWnd = pr.MainWindowHandle.ToInt32();
                            ShowWindow(hWnd, SW_MINIMIZE);
                        }
                    }
                    MessageBox.Show("Minimized");
                }
                else if (classNum == 1)
                {
                    synth.Speak("This person is looking left");
                    int hWnd;
                    Process[] processRunning = Process.GetProcesses();
                    foreach (Process pr in processRunning)
                    {
                        if (pr.ProcessName == "notepad")
                        {
                            hWnd = pr.MainWindowHandle.ToInt32();
                            ShowWindow(hWnd, SW_RESTORE);
                        }
                    }
                    MessageBox.Show("Dialog has been restored");
                }
                else if (classNum == 2)
                {
                    synth.Speak("This person is looking front");
                    //System.Diagnostics.Process.Start("notepad.exe");
                    Process.Start(startInfo);
                    MessageBox.Show("open");
                }
                else if (classNum == 3)
                {
                    synth.Speak("This person is closing his eyes");
                    Process[] proc = Process.GetProcessesByName("notepad");
                    proc[0].Kill();
                    MessageBox.Show("close");
                }
            }


        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        private void RadialBasisbutton_Click(object sender, EventArgs e)
        {
            RadialBasisForm f = new RadialBasisForm();
            f.Show();
        }

        private void buttonTestImageRadial_Click(object sender, EventArgs e)
        {
             //Test And Do an Action
            if (File.Exists(filename))
            {
                List<Tuple<double, double>> points = new List<Tuple<double, double>>();
                filename = filename.Replace(".pgm", ".pts");
                points = PTSUtil.ReadImagePTSfile(filename);

                List<double> features = FeatureExtraction_Class.ExtractImgFeature(points);
                int classNum = RadialBasis.testoneImg(features);
                SpeechSynthesizer synth = new SpeechSynthesizer();

                // Configure the audio output. 
                synth.SetOutputToDefaultAudioDevice();
                synth.Rate = 5;

                ProcessStartInfo startInfo = new ProcessStartInfo("notepad.exe");
                if (classNum == 0)
                {
                    synth.Speak("This person is closing his eyes");
                    Process[] proc = Process.GetProcessesByName("notepad");
                    foreach (Process p in proc)
                        p.Kill();
                    MessageBox.Show("close"); 
                }
                else if (classNum == 1)
                {
                    synth.Speak("This person is looking down");
                    //Process.Start("notepad.exe", @"E:\fcis-4\term2\Neural Network\Labs\trainData.txt");
                    int hWnd;
                    Process[] processRunning = Process.GetProcesses();
                    foreach (Process pr in processRunning)
                    {
                        if (pr.ProcessName == "notepad")
                        {
                            hWnd = pr.MainWindowHandle.ToInt32();
                            ShowWindow(hWnd, SW_MINIMIZE);
                        }
                    }
                    MessageBox.Show("Minimized");
                    
                }
                else if (classNum == 2)
                {
                    synth.Speak("This person is looking front");
                    //System.Diagnostics.Process.Start("notepad.exe");
                    Process.Start(startInfo);
                    MessageBox.Show("open");
                }
                else if (classNum == 3)
                {
                    synth.Speak("This person is looking left");
                    int hWnd;
                    Process[] processRunning = Process.GetProcesses();
                    foreach (Process pr in processRunning)
                    {
                        if (pr.ProcessName == "notepad")
                        {
                            hWnd = pr.MainWindowHandle.ToInt32();
                            ShowWindow(hWnd, SW_RESTORE);
                        }
                    }
                    MessageBox.Show("Dialog has been restored");
                }
            
            }
            
        }
      
    }
}
