using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowsFormsApplication1
{
    class FeatureExtraction_Class
    {
        // Formate Of Feature List Is:=> For Each Class (List [4 Classes]) For Each Class There is a List Of DataSamples (File/Image)
        // For Each Sample There is a Name And 19 dependent Feature (Calculated By Euclidean distance)
        //  Ex: List[Class1][Sample File 0].Item2 == List of 19 dependent Feature.... 
        public static List<List<Tuple<string, List<double>>>> FeaturesExtractedForTrainning = new List<List<Tuple<string, List<double>>>>();
        public static List<Tuple<int, List<double>>> ShuffledTrainingFeatures = new List<Tuple<int, List<double>>>();
        public static Random Number = new Random();
        public static List<List<Tuple<string, List<double>>>> FeaturesExtractedForTesting = new List<List<Tuple<string, List<double>>>>();
        //====================================================================================================================

        public static void Extract_Feature(string TrainOrTest)
        {
            
            #region Extract Feature From Trainning Data
            if (TrainOrTest == "train")
            {
                FeaturesExtractedForTrainning.Clear();
                for (int classNum = 0; classNum < 4; classNum++)
                {
                    List<Tuple<string, List<double>>> FeaturesOfFiles = new List<Tuple<string, List<double>>>();
                    for (int FileNum = 0; FileNum < 15; FileNum++)
                    {
                        List<double> distances = new List<double>();
                        for (int featureNum = 0; featureNum < 20; featureNum++)
                        {
                            if (featureNum != 14)
                            {
                                distances.Add(Math.Sqrt(Math.Pow((PTSUtil.DataSetOfAllClassesInTrainnig[classNum][FileNum].Item2[featureNum].Item1 - PTSUtil.DataSetOfAllClassesInTrainnig[classNum][FileNum].Item2[14].Item1), 2) +
                                                        Math.Pow((PTSUtil.DataSetOfAllClassesInTrainnig[classNum][FileNum].Item2[featureNum].Item2 - PTSUtil.DataSetOfAllClassesInTrainnig[classNum][FileNum].Item2[14].Item2), 2)));
                            }
                        }
                        Tuple<string, List<double>> FeaturesOFFile_i = new Tuple<string, List<double>>(PTSUtil.DataSetOfAllClassesInTrainnig[classNum][FileNum].Item1 + "/" + classNum , distances);
                        FeaturesOfFiles.Add(FeaturesOFFile_i);
                    }
                    FeaturesExtractedForTrainning.Add(FeaturesOfFiles);
                }
            }
            #endregion

            #region Extract Feature From Testing Data
            else
            {
                FeaturesExtractedForTesting.Clear();
                for (int classNum = 0; classNum < 4; classNum++)
                {
                    List<Tuple<string, List<double>>> FeaturesOfFiles = new List<Tuple<string, List<double>>>();
                    for (int FileNum = 0; FileNum < 5; FileNum++)
                    {
                        List<double> distances = new List<double>();
                        for (int featureNum = 0; featureNum < 20; featureNum++)
                        {
                            if (featureNum != 14)
                            {
                                distances.Add(Math.Sqrt(Math.Pow((PTSUtil.DataSetOfAllClassesInTesting[classNum][FileNum].Item2[featureNum].Item1 - PTSUtil.DataSetOfAllClassesInTesting[classNum][FileNum].Item2[14].Item1), 2) +
                                                        Math.Pow((PTSUtil.DataSetOfAllClassesInTesting[classNum][FileNum].Item2[featureNum].Item2 - PTSUtil.DataSetOfAllClassesInTesting[classNum][FileNum].Item2[14].Item2), 2)));
                            }
                        }
                        Tuple<string, List<double>> FeaturesOFFile_i = new Tuple<string, List<double>>(PTSUtil.DataSetOfAllClassesInTesting[classNum][FileNum].Item1 + "/"+ classNum, distances);
                        FeaturesOfFiles.Add(FeaturesOFFile_i);
                    }
                    FeaturesExtractedForTesting.Add(FeaturesOfFiles);
                }
            }
            #endregion


        }

        public static List<double> ExtractImgFeature( List<Tuple<double, double>>  points)
        {
            List<double> distances = new List<double>();
            for (int featureNum = 0; featureNum < 20; featureNum++)
            {
                if (featureNum != 14)
                {
                    distances.Add(Math.Sqrt(Math.Pow((points[featureNum].Item1 - points[14].Item1), 2) +
                                            Math.Pow((points[featureNum].Item2 - points[14].Item2), 2)));
                }
            }
            return distances;
        }
        public static void ShufflingData()
        {
            List<int> ClassCount = new List<int>();
            int MaxRandom = 15;
            ClassCount.Add(0);
            ClassCount.Add(0);
            ClassCount.Add(0);
            ClassCount.Add(0);
            int count = 1;
            while (count <= 60)
            {
                int classNum = Number.Next(0, 4);

                while (ClassCount[classNum] + 1 > MaxRandom)
                {
                    classNum = Number.Next(0, 4);
                }
                ClassCount[classNum]++;

                int FileNum = Number.Next(0, 15);
                List<double> Features = new List<double>();
                for (int featureNum = 0; featureNum < 19; featureNum++)
                {
                    Features.Add(FeaturesExtractedForTrainning[classNum][FileNum].Item2[featureNum]);
                }
                ShuffledTrainingFeatures.Add(new Tuple<int,List<double>>(classNum, Features));

                count++;
            }

        }

    }
}
