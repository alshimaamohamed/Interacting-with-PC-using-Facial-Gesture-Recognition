using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;
//using MathNet.Numerics.LinearAlgebra;

namespace WindowsFormsApplication1
{
    class PTSUtil
    {
        // Format of Stored List which store Data From .pts Files (which have Points of each LandMark)
        // ==== List[4 class] each class have a List[N(#samples "(sample = a file .pts)" )] ===
        // each Sample: have:: File Name & List[20 Features] :D                            <<==  
        //Ex: List[Class Looking Left][sample File 1].item1 (Name Of File1 in Class 1)     <<==

        public static List<List<Tuple<string, List<Tuple<double, double>>>>> DataSetOfAllClassesInTrainnig = new List<List<Tuple<string, List<Tuple<double, double>>>>>();
        public static List<List<Tuple<string, List<Tuple<double, double>>>>> DataSetOfAllClassesInTesting = new List<List<Tuple<string, List<Tuple<double, double>>>>>();
       //=====================================================================================================================================================================


        // Read Features For Trainning & Testing And Store It
        public static void ReadPtsFiles(string TrainOrTest)
        {
            DataSetOfAllClassesInTrainnig.Clear();
            DataSetOfAllClassesInTesting.Clear();
            List<string> TrainfilesName = new List<string>();
            TrainfilesName.Add(@"\Dataset\Training Dataset\Closing Eyes");
            TrainfilesName.Add(@"\Dataset\Training Dataset\Looking Down");
            TrainfilesName.Add(@"\Dataset\Training Dataset\Looking Front");
            TrainfilesName.Add(@"\Dataset\Training Dataset\Looking Left");
            List<string> TestfilesName = new List<string>();
            TestfilesName.Add(@"\Dataset\Testing Dataset\Closing Eyes");
            TestfilesName.Add(@"\Dataset\Testing Dataset\Looking Down");
            TestfilesName.Add(@"\Dataset\Testing Dataset\Looking Front");
            TestfilesName.Add(@"\Dataset\Testing Dataset\Looking Left");
            string folder = Path.GetDirectoryName(Application.ExecutablePath);

            #region Read Training Files
            if (TrainOrTest == "train")
            {
                for (int path = 0; path < 4; path++)
                {
                    List<Tuple<string, List<Tuple<double, double>>>> FeatureOfClass_i = new List<Tuple<string, List<Tuple<double, double>>>>();
                    DirectoryInfo di = new DirectoryInfo(folder + TrainfilesName[path]);
                    FileInfo[] files = di.GetFiles("*.pts");
                    foreach (FileInfo f in files)
                    {
                        FileStream fileStream = f.OpenRead();
                        string NameofFile = f.Name.Replace(".pts","");
                        List<Tuple<double,double>> points = new List<Tuple<double,double>>();
                        using (StreamReader reader = new StreamReader(fileStream))
                        {
                            string line = null;
                            //Ignore 3Lines From File .pts
                            for (int i = 0; i < 3; ++i)
                            {
                                line = reader.ReadLine();
                            }
                            //Begin Reading Points in File
                            for (int j = 0; j < 20; j++)
                            { 
                                string[] temp = reader.ReadLine().Split(' ');
                                Tuple<double, double> point_j = new Tuple<double, double>(Convert.ToDouble(temp[0]), Convert.ToDouble(temp[1]));
                                points.Add(point_j);
                            }
                            reader.Dispose();
                        }
                        Tuple<string, List<Tuple<double, double>>> Samples_i = new Tuple<string, List<Tuple<double, double>>>(NameofFile,points);
                        FeatureOfClass_i.Add(Samples_i);
                        // MessageBox.Show(f.Name);
                    }
                    DataSetOfAllClassesInTrainnig.Add(FeatureOfClass_i);
                }
            }
            #endregion

            #region Read Test Files
            else
            {
                for (int path = 0; path < 4; path++)
                {
                    List<Tuple<string, List<Tuple<double, double>>>> FeatureOfClass_i = new List<Tuple<string, List<Tuple<double, double>>>>();
                    DirectoryInfo di = new DirectoryInfo(folder + TestfilesName[path]);
                    FileInfo[] files = di.GetFiles("*.pts");
                    foreach (FileInfo f in files)
                    {
                        FileStream fileStream = f.OpenRead();
                        string NameofFile = f.Name.Replace(".pts", "");
                        List<Tuple<double, double>> points = new List<Tuple<double, double>>();
                        using (StreamReader reader = new StreamReader(fileStream))
                        {
                            string line = null;
                            //Ignore 3Lines From File .pts
                            for (int i = 0; i < 3; ++i)
                            {
                                line = reader.ReadLine();
                            }
                            //Begin Reading Points in File
                            for (int j = 0; j < 20; j++)
                            {
                                string[] temp = reader.ReadLine().Split(' ');
                                Tuple<double, double> point_j = new Tuple<double, double>(Convert.ToDouble(temp[0]), Convert.ToDouble(temp[1]));
                                points.Add(point_j);
                            }
                            reader.Dispose();
                        }
                        Tuple<string, List<Tuple<double, double>>> Samples_i = new Tuple<string, List<Tuple<double, double>>>(NameofFile, points);
                        FeatureOfClass_i.Add(Samples_i);
                        // MessageBox.Show(f.Name);
                    }
                    DataSetOfAllClassesInTesting.Add(FeatureOfClass_i);
                }
            }
            #endregion
        }

        public static List<Tuple<double, double>> ReadImagePTSfile(string path)
        {
            List<Tuple<double, double>> points = new List<Tuple<double, double>>();
            int ind = path.LastIndexOf('\\');
            string[] names = path.Split('\\');
            path = path.Remove( path.LastIndexOf('\\'));
            DirectoryInfo di = new DirectoryInfo(path);
            FileInfo[] files = di.GetFiles("*.pts");
            foreach (FileInfo f in files)
             { 
             FileStream fileStream = f.OpenRead();
             string NameofFile = f.Name;
             if (NameofFile.ToLower() == names[names.Length - 1].ToLower())
             {
                 FileStream fStream = f.OpenRead();
                 using (StreamReader reader = new StreamReader(fStream))
                 {
                     string line = null;
                     //Ignore 3Lines From File .pts
                     for (int i = 0; i < 3; ++i)
                     {
                         line = reader.ReadLine();
                     }
                     //Begin Reading Points in File
                     for (int j = 0; j < 20; j++)
                     {
                         string[] temp = reader.ReadLine().Split(' ');
                         Tuple<double, double> point_j = new Tuple<double, double>(Convert.ToDouble(temp[0]), Convert.ToDouble(temp[1]));
                         points.Add(point_j);
                     }
                     reader.Dispose();
                 }
             }
            }
            return points;
        }


    }
}
