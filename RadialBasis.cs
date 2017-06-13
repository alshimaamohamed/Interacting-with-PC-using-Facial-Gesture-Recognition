using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
namespace WindowsFormsApplication1
{
    public class RadialBasis
    {
        #region initlization
       public static  bool bias;
      public static   FileStream f;
        public static int HiddenNeurons, Epochs;
        public List<List<int>> ConfusionMatrix = new List<List<int>>();
       public static  List<List<Tuple<string, List<double>>>> FeaturesExtractedForTrainning = FeatureExtraction_Class.FeaturesExtractedForTrainning;
        public static List<List<Tuple<string, List<double>>>> FeaturesExtractedForTesting = FeatureExtraction_Class.FeaturesExtractedForTesting;
        public static List<List<Tuple<int, int>>> CentroidsSamples = new List<List<Tuple<int, int>>>();
       public static  List<List<Tuple<int, int>>> CentroidsSamplesOld = new List<List<Tuple<int, int>>>();
        public static List<List<double>> Centroids = new List<List<double>>();
        public static List<double> Variance = new List<double>();
        public static List<double> GaussianTraining = new List<double>();
        public static List<double> GaussianTesting = new List<double>();
        public static List<List<double>> Weigths = new List<List<double>>();
        public static double LMS_threshold, eta;
        public static List<List<int>> d = new List<List<int>>();
        #endregion

        public static void NormalizeTrainingData()
        {
            double Max = 0;
            double temp = 0;
            double Min = double.MaxValue;
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 15; j++)
                {
                    temp = FeaturesExtractedForTrainning[i][j].Item2.Max();
                    if (temp > Max)
                    {
                        Max = temp;
                    }
                    temp = FeaturesExtractedForTrainning[i][j].Item2.Min();
                    if (temp < Min)
                    {
                        Min = temp;
                    }

                }
            }
            //====================================================
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 15; j++)
                {
                    for (int k = 0; k < FeaturesExtractedForTrainning[i][j].Item2.Count; k++)
                    {
                        FeaturesExtractedForTrainning[i][j].Item2[k] = (FeaturesExtractedForTrainning[i][j].Item2[k] - Min) / (Max - Min);
                    }
                }
            }

        }
        public static void NormalizeTestingData()
        {
            double Max = 0;
            double temp = 0;
            double Min = double.MaxValue;
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    temp = FeaturesExtractedForTesting[i][j].Item2.Max();
                    if (temp > Max)
                    {
                        Max = temp;
                    }
                    temp = FeaturesExtractedForTesting[i][j].Item2.Min();
                    if (temp < Min)
                    {
                        Min = temp;
                    }

                }
            }
            //====================================================
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    for (int k = 0; k < FeaturesExtractedForTesting[i][j].Item2.Count; k++)
                    {
                        FeaturesExtractedForTesting[i][j].Item2[k] = (FeaturesExtractedForTesting[i][j].Item2[k] - Min) / (Max - Min);
                    }
                }
            }

        }
        public static void SetInput(int HiddenNeuron, double threshold, int Epoch, double e, bool b)
        {
            Epochs = Epoch;
            HiddenNeurons = HiddenNeuron;
            LMS_threshold = threshold;
            eta = e;
            bias = b;

        }
        public void desiredOutput()
        {

            for (int i = 0; i < 4; i++)
            {

                List<int> temp = new List<int>();
                for (int j = 0; j < 4; j++)
                {
                    if (i == j)
                        temp.Add(1);
                    else
                        temp.Add(0);
                }
                d.Add(temp);
            }
        }

        public void Kmean()
        {
            NormalizeTrainingData();
            desiredOutput();
            #region FristCentriod
            Random rank = new Random();
            List<double> cList = new List<double>();
            for (int i = 0; i < HiddenNeurons; i++)
            {
                int SampleFile = rank.Next(0, 14);
                int classRandom = rank.Next(0, 3);
                if (!(cList.Contains(SampleFile)))
                {
                    cList.Add(SampleFile);
                    List<double> features = FeaturesExtractedForTrainning[classRandom][SampleFile].Item2;
                    Centroids.Add(features);

                }
                else
                    i--;


            }
            #endregion FristCentriod

            #region UpdateCentriods
            bool update = true;
            for (int i = 0; i < HiddenNeurons; i++)
            {

                List<Tuple<int, int>> Tt2 = new List<Tuple<int, int>>();

                CentroidsSamplesOld.Add(Tt2);
            }
            while (update)
            {
                CentroidsSamples.Clear();
                for (int i = 0; i < HiddenNeurons; i++)
                {
                    List<Tuple<int, int>> Tt1 = new List<Tuple<int, int>>();

                    CentroidsSamples.Add(Tt1);

                }

                for (int classNum = 0; classNum < 4; classNum++)
                {
                    for (int SampleNum = 0; SampleNum < 15; SampleNum++)
                    {
                        List<double> dist = new List<double>();

                        for (int i = 0; i < HiddenNeurons; i++)
                        {
                            List<double> sample = FeaturesExtractedForTrainning[classNum][SampleNum].Item2;
                            List<double> Centroid = Centroids[i];
                            double distance = 0;
                            //eclidian distance 
                            for (int FeatureNum = 0; FeatureNum < 19; FeatureNum++)
                            {
                                distance += Math.Pow((sample[i] - Centroid[i]), 2);
                            }
                            distance = Math.Sqrt(distance);
                            dist.Add(distance);
                        }
                        double min = dist[0];
                        int index = 0;
                        for (int i = 0; i < HiddenNeurons; i++)
                        {
                            if (dist[i] < min)
                            {
                                index = i;
                                min = dist[i];

                            }
                        }

                        Tuple<int, int> t = new Tuple<int, int>(classNum, SampleNum);
                        CentroidsSamples[index].Add(t);


                    }

                }
                //Calculate Mean for Centroids
                List<List<double>> MeanCentroid = new List<List<double>>();
                for (int i = 0; i < HiddenNeurons; i++)
                {
                    List<Tuple<int, int>> Samples = new List<Tuple<int, int>>();
                    Samples = CentroidsSamples[i]; // s1 , s2 , s3 
                    List<double> MeanTemp = new List<double>();
                    for (int featureNum = 0; featureNum < 19; featureNum++)
                    {
                        double FeaturesSum = 0;
                        for (int index = 0; index < Samples.Count(); index++) // samples count 
                            FeaturesSum += FeaturesExtractedForTrainning[Samples[index].Item1][Samples[index].Item2].Item2.ElementAt(featureNum);

                        //for each cluster there exist 19 mean 
                        MeanTemp.Add(FeaturesSum / Samples.Count());
                    }
                    MeanCentroid.Add(MeanTemp);

                    Centroids[i] = MeanTemp;
                }
                //===============================================================================================================================
                for (int i = 0; i < HiddenNeurons; i++)
                {
                    if (CentroidsSamples[i].Count() == CentroidsSamplesOld[i].Count())
                    {
                        for (int j = 0; j < CentroidsSamples[i].Count(); j++)
                        {
                            if (!CentroidsSamples[i].Contains(CentroidsSamplesOld[i][j]))
                            {
                                update = true;
                                break;

                            }
                            else
                                update = false;
                        }
                    }
                    else
                    {

                        break;
                    }
                }
                //   CentroidsSamplesOld = CentroidsSamples;
                //copy 
                CentroidsSamplesOld.Clear();
                for (int i = 0; i < HiddenNeurons; i++)
                {
                    List<Tuple<int, int>> Tt1 = new List<Tuple<int, int>>();

                    CentroidsSamplesOld.Add(Tt1);

                }
                for (int i = 0; i < HiddenNeurons; i++)
                {

                    for (int j = 0; j < CentroidsSamples[i].Count(); j++)
                    {
                        CentroidsSamplesOld[i].Add(CentroidsSamples[i][j]);
                    }


                }
            }
            #endregion UpdateCentriods

            #region GetVariance
            for (int i = 0; i < HiddenNeurons; i++)
            {
                double sum = 0;
                for (int samplesindex = 0; samplesindex < CentroidsSamples[i].Count(); samplesindex++)
                {
                    double distance = 0;
                    //eclidian distance 
                    List<double> sample = FeaturesExtractedForTrainning[CentroidsSamples[i][samplesindex].Item1][CentroidsSamples[i][samplesindex].Item2].Item2;
                    for (int FeatureNum = 0; FeatureNum < 19; FeatureNum++)
                    {
                        distance += Math.Pow((sample[FeatureNum] - Centroids[i][FeatureNum]), 2);
                    }
                    sum += Math.Sqrt(distance);

                }
                Variance.Add(sum / CentroidsSamples[i].Count());
            }
            #endregion GetVariance


        }



        public static List<double> GetGaussian(List<double> FeaturesExtracted)
        {
            List<double> gaussian = new List<double>();
            List<double> R = new List<double>();
            for (int h = 0; h < HiddenNeurons; h++)
            {
                double r = 0;
                for (int FeatureNum = 0; FeatureNum < 19; FeatureNum++)
                {
                    r += Math.Pow(Centroids[h][FeatureNum] - FeaturesExtracted[FeatureNum], 2);
                }
                r = Math.Sqrt(r);
                R.Add(r);
            }
            for (int h = 0; h < HiddenNeurons; h++)
            {
                gaussian.Add(Math.Exp(-1 * (Math.Pow(R[h], 2) / (2 * Math.Pow(Variance[h], 2)))));
            }
            return gaussian;
        }

        public void RadialBasisTraining()
        {
            double biasValue;

            #region randomWeights
            Random r = new Random();

            for (int classNum = 0; classNum < 4; classNum++)
            {
                List<double> l = new List<double>();

                for (int h = 0; h < HiddenNeurons; h++)
                {

                    l.Add(r.NextDouble());

                }
                Weigths.Add(l);
            }
            if (bias)

                biasValue = r.NextDouble();

            else
                biasValue = 0;

            #endregion


            for (int epoch = 0; epoch < Epochs; epoch++)
            {

                List<double> thisEpocError = new List<double>();
                Random r1 = new Random();
                for (int sampleNum = 0; sampleNum < 15; sampleNum++)
                {
                    List<int> rand = new List<int>();
                    for (int clas = 0; clas < 4; clas++)
                    {

                        int c = r1.Next(0, 4);
                        while (rand.Contains(c))
                        {
                            c = r1.Next(0, 4);
                        }
                        rand.Add(c);
                        GaussianTraining = GetGaussian(FeaturesExtractedForTrainning[c][sampleNum].Item2);

                        List<double> y = new List<double>();
                        List<int> d1 = new List<int>();
                        List<double> errors = new List<double>();
                        for (int classNum = 0; classNum < 4; classNum++)
                        {
                            double o = 0;

                            for (int h = 0; h < HiddenNeurons; h++)
                            {
                                o += Weigths[classNum][h] * GaussianTraining[h];

                            }
                            o += biasValue;
                            y.Add(o);
                        }
                        d1 = d[c];
                        double avgError = 0;
                        for (int i = 0; i < 4; i++)
                        {
                            errors.Add(d1[i] - y[i]);
                            avgError += errors[i];

                        }


                        thisEpocError.Add(avgError / 4);
                        //update weights
                        for (int classNum = 0; classNum < 4; classNum++)
                        {
                            for (int h = 0; h < HiddenNeurons; h++)
                                Weigths[classNum][h] = Weigths[classNum][h] + (eta * errors[classNum] * GaussianTraining[h]);

                        }
                        if (bias)
                        {
                            biasValue = biasValue + (eta * (avgError / 4) * 1);

                        }


                    }
                }//LMS

                double sum_error = 0;

                for (int j = 0; j < thisEpocError.Count; j++)
                {
                    sum_error += Math.Pow(thisEpocError[j], 2);
                }
                double MeanSquareError = sum_error / (2 * thisEpocError.Count);
                if (MeanSquareError < LMS_threshold)
                {
                    break;
                }
            }



        }


        public void saveData()
        {
            string fileName = HiddenNeurons.ToString() + " " + Epochs.ToString() + " " + LMS_threshold.ToString() + " " + eta.ToString() + bias + ".txt";
            if (!File.Exists(fileName))
            {
                f = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.Write);
                StreamWriter sw = new StreamWriter(f);
                //save centroids
                for (int i = 0; i < HiddenNeurons; i++)
                {
                    for (int j = 0; j < 19; j++)
                    {
                        sw.WriteLine(Centroids[i][j]);
                    }
                    sw.WriteLine();

                }

                //   save variance
                for (int i = 0; i < HiddenNeurons; i++)
                    sw.WriteLine(Variance[i]);
                sw.WriteLine();
                //save weights
                for (int c = 0; c < 4; c++)
                {
                    for (int h = 0; h < HiddenNeurons; h++)
                    {
                        sw.WriteLine(Weigths[c][h]);
                    }

                    sw.WriteLine();
                }

                sw.Close();
                f.Close();
            }

        }
        public void RadialBasisTesting()
        {

            NormalizeTestingData();
            //inilize confuion matrix
            for (int i = 0; i < 4; i++)
            {
                List<int> c = new List<int>();
                for (int j = 0; j < 4; j++)
                    c.Add(0);
                ConfusionMatrix.Add(c);
            }
            for (int sampleNum = 0; sampleNum < 5; sampleNum++)
            {
                for (int classNum = 0; classNum < 4; classNum++)
                {

                    GaussianTesting = GetGaussian(FeaturesExtractedForTesting[classNum][sampleNum].Item2);
                    List<double> output = new List<double>();
                    for (int c = 0; c < 4; c++)
                    {
                        double o = 0;

                        for (int h = 0; h < HiddenNeurons; h++)
                        {
                            o += Weigths[c][h] * GaussianTesting[h];

                        }
                        output.Add(o);
                    }
                    double max = output[0];
                    int actual = 0;
                    int d = classNum;
                    for (int c = 1; c < 4; c++)
                    {
                        if (max < output[c])
                        {
                            max = output[c];
                            actual = c;
                        }
                    }

                    ConfusionMatrix[d][actual]++;
                }
            }
        }
        public static bool readFromFile(bool best)
        {
            string fileName = "";
            if (!best)
                fileName = HiddenNeurons.ToString() + " " + Epochs.ToString() + " " + LMS_threshold.ToString() + " " + eta.ToString() + bias + ".txt";
            else
            {
                fileName = "10 20000 0.001 0.15False.txt";
                HiddenNeurons = 10;
            }

            if (File.Exists(fileName))
            {
                f = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                StreamReader sr = new StreamReader(f);
                //save centroids
                for (int i = 0; i < HiddenNeurons; i++)
                {
                    List<double> c = new List<double>();
                    for (int j = 0; j < 19; j++)
                    {
                        c.Add(double.Parse(sr.ReadLine()));
                    }
                    Centroids.Add(c);
                    sr.ReadLine();

                }

                //   save variance
                for (int i = 0; i < HiddenNeurons; i++)
                    Variance.Add(double.Parse(sr.ReadLine()));
                sr.ReadLine();
                //save weights
                for (int c = 0; c < 4; c++)
                {
                    List<double> w = new List<double>();
                    for (int h = 0; h < HiddenNeurons; h++)
                    {

                        w.Add(double.Parse(sr.ReadLine()));
                    }
                    Weigths.Add(w);
                    sr.ReadLine();
                }

                sr.Close();
                f.Close();
                return true;
            }
            else
                return false;

        }
        public static void NormalizeTestingImage(List<double> FeaturesForTesting)
        {
            double Max = FeaturesForTesting.Max();
            double Min = FeaturesForTesting.Min();
            //====================================================
            for (int j = 0; j < FeaturesForTesting.Count; j++)
            {

                //FeaturesForTesting[j] = (2 * ((FeaturesForTesting[j] - Min)) / (Max - Min)) - 1;
                FeaturesForTesting[j] = ((FeaturesForTesting[j] - Min)) / (Max - Min);
            }
        }
        public static int testoneImg(List<double> features)
        {
            if (readFromFile(true))
            {
                NormalizeTestingImage(features);
                    GaussianTesting = GetGaussian(features);
                    List<double> output = new List<double>();
                    for (int c = 0; c < 4; c++)
                    {
                        double o = 0;

                        for (int h = 0; h < HiddenNeurons; h++)
                        {
                            o += Weigths[c][h] * GaussianTesting[h];

                        }
                        output.Add(o);
                    }
                    double max = output[0];
                    int actual = 0;
                    for (int c = 1; c < 4; c++)
                    {
                        if (max < output[c])
                        {
                            max = output[c];
                            actual = c;
                        }
                    }
                    return actual;
                }
            else
                return 0;
            }
        
    }

}
