using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace WindowsFormsApplication1
{
    public class Backpropagation
    {
        public static float learningRate;
        public static int epoch;
        public static float errorRate;
        public static int layers;
        public static List<int> nodes;
        public static List<List<Tuple<Tuple<int, int>, double>>> weights;
        public static bool bais;
       public static Random RanNum = new Random();
        public static float a = 1;
        public static List<List<double>> output;
        public static List<List<List<double>>> outputOfClassPatterns;
        public static List<List<List<List<double>>>> outputOfClasses;
        public static List<List<List<double>>> errorInEachPatternForEachClass;
        public static List<List<double>> errorSignalsInPattern = new List<List<double>>();
        public static List<List<double>> avgErrorForEachPattern = new List<List<double>>();
        public static List<List<double>> S = new List<List<double>>();
        public static List<List<List<int>>> resultOfTheTest = new List<List<List<int>>>();

        public static List<List<double>> Dash_Results = new List<List<double>>();
        public  void readData(float learn, int ep, float error, int w, List<int> n, bool b)
        {
            // training code data
            avgErrorForEachPattern.Add(new List<double>());
            avgErrorForEachPattern.Add(new List<double>());
            avgErrorForEachPattern.Add(new List<double>());
            avgErrorForEachPattern.Add(new List<double>());
            ////////////////////////


            weights = new List<List<Tuple<Tuple<int, int>, double>>>();
            learningRate = learn;
            epoch = ep;
            errorRate = error;
            layers = w + 2;
            nodes = new List<int>();
            output = new List<List<double>>();
            nodes = n;
            bais = b;
        }
        public static void NormalizeData(List<Tuple<int, List<double>>> ShuffledTrainingFeatures)
        {
            double Max = 0;
            double temp = 0;
            double Min = double.MaxValue;

            for (int j = 0; j < ShuffledTrainingFeatures.Count; j++)
            {
                temp = ShuffledTrainingFeatures[j].Item2.Max();
                if (temp > Max)
                {
                    Max = temp;
                }
                temp = ShuffledTrainingFeatures[j].Item2.Min();
                if (temp < Min)
                {
                    Min = temp;
                }

            }

            //====================================================

            for (int j = 0; j < ShuffledTrainingFeatures.Count; j++)
            {
                //Min = FeaturesExtractedForTrainning[i][j].Item2.Min();
                //Max = FeaturesExtractedForTrainning[i][j].Item2.Max();
                for (int k = 0; k < ShuffledTrainingFeatures[j].Item2.Count; k++)
                {
                    ShuffledTrainingFeatures[j].Item2[k] = ((2 * (ShuffledTrainingFeatures[j].Item2[k] - Min)) / (Max - Min)) - 1;
                }
            }


        }
        public void train(List<List<Tuple<string, List<double>>>> FeaturesExtractedForTrainning)
        {
            outputOfClassPatterns = new List<List<List<double>>>();
            outputOfClasses = new List<List<List<List<double>>>>();
            errorInEachPatternForEachClass = new List<List<List<double>>>();
            List<List<double>> tempdata;
            List<List<List<double>>> tempclassdata;
            List<double> temp;
            List<List<double>> ErrorInPatternsForOneClass = new List<List<double>>();
            List<double> currentPatternError = new List<double>();
            List<double> input_Layer = new List<double>();

            generateWeigths();
            NormalizeData(FeatureExtraction_Class.ShuffledTrainingFeatures);
            for (int i = 0; i < epoch; i++)
            {
                avgErrorForEachPattern[0].Clear();
                avgErrorForEachPattern[1].Clear();
                avgErrorForEachPattern[2].Clear();
                avgErrorForEachPattern[3].Clear();
                output = new List<List<double>>();

                   for (int j = 0; j < FeatureExtraction_Class.ShuffledTrainingFeatures.Count; j++)
                   {
                        for (int k = 1; k < layers; k++)
                        {
                            if (k == 1)
                            {
                                temp = new List<double>(FeatureExtraction_Class.ShuffledTrainingFeatures[j].Item2);
                                input_Layer = new List<double>(FeatureExtraction_Class.ShuffledTrainingFeatures[j].Item2);
                                if (bais)
                                {
                                    temp.Insert(0, 1.0);
                                    input_Layer.Insert(0, 1.0);
                                }
                                fNet(temp, k - 1);

                            }
                            else
                            {
                                temp = new List<double>(output[output.Count - 1]);
                                if (bais)
                                    temp.Insert(0, 1);
                                fNet(temp, k - 1);
                            }
                        }
                        tempdata = new List<List<double>>(output);
                        #region calculate error for each pattern to use it in backward step
                        if (FeatureExtraction_Class.ShuffledTrainingFeatures[j].Item1 == 0)
                        {
                            currentPatternError.Add((1 - tempdata[layers - 2][0]));
                            currentPatternError.Add((0 - tempdata[layers - 2][1]));
                            currentPatternError.Add((0 - tempdata[layers - 2][2]));
                            currentPatternError.Add((0 - tempdata[layers - 2][3]));
                        }
                        else if (FeatureExtraction_Class.ShuffledTrainingFeatures[j].Item1 == 1)
                        {
                            currentPatternError.Add((0 - tempdata[layers - 2][0]));
                            currentPatternError.Add((1 - tempdata[layers - 2][1]));
                            currentPatternError.Add((0 - tempdata[layers - 2][2]));
                            currentPatternError.Add((0 - tempdata[layers - 2][3]));
                        }
                        else if (FeatureExtraction_Class.ShuffledTrainingFeatures[j].Item1 == 2)
                        {
                            currentPatternError.Add((0 - tempdata[layers - 2][0]));
                            currentPatternError.Add((0 - tempdata[layers - 2][1]));
                            currentPatternError.Add((1 - tempdata[layers - 2][2]));
                            currentPatternError.Add((0 - tempdata[layers - 2][3]));
                        }
                        else if (FeatureExtraction_Class.ShuffledTrainingFeatures[j].Item1 == 3)
                        {
                            currentPatternError.Add((0 - tempdata[layers - 2][0]));
                            currentPatternError.Add((0 - tempdata[layers - 2][1]));
                            currentPatternError.Add((0 - tempdata[layers - 2][2]));
                            currentPatternError.Add((1 - tempdata[layers - 2][3]));
                        }


                        #endregion

                        #region backward computation

                        backwardComputation(Dash_Results, currentPatternError);

                        #endregion
                        #region update weights

                        update_weights(output, input_Layer);

                        #endregion

                        output.Clear();
                        tempdata.Clear();
                        currentPatternError.Clear();
                        Dash_Results.Clear();
                    


                }

                #region claculate the error and apply LMS
                //apply termnation using least mean square
                for (int j = 0; j < 15; j++)
                {
                    for (int c = 0; c < 4; c++)
                    {
                        for (int k = 1; k < layers; k++)
                        {
                            if (k == 1)
                            {
                                temp = new List<double>(FeaturesExtractedForTrainning[c][j].Item2);
                                if (bais)
                                {
                                    temp.Insert(0, 1.0);
                                }
                                fNet(temp, k - 1);
                            }
                            else
                            {
                                temp = new List<double>(output[output.Count - 1]);
                                if (bais)
                                    temp.Insert(0, 1);
                                fNet(temp, k - 1);
                            }
                        }
                        tempdata = new List<List<double>>(output);
                        #region calculate error for each pattern
                        if (c == 0)
                        {
                            currentPatternError.Add((1 - tempdata[layers - 2][0]));
                            currentPatternError.Add((0 - tempdata[layers - 2][1]));
                            currentPatternError.Add((0 - tempdata[layers - 2][2]));
                            currentPatternError.Add((0 - tempdata[layers - 2][3]));
                        }
                        else if (c == 1)
                        {
                            currentPatternError.Add((0 - tempdata[layers - 2][0]));
                            currentPatternError.Add((1 - tempdata[layers - 2][1]));
                            currentPatternError.Add((0 - tempdata[layers - 2][2]));
                            currentPatternError.Add((0 - tempdata[layers - 2][3]));
                        }
                        else if (c == 2)
                        {
                            currentPatternError.Add((0 - tempdata[layers - 2][0]));
                            currentPatternError.Add((0 - tempdata[layers - 2][1]));
                            currentPatternError.Add((1 - tempdata[layers - 2][2]));
                            currentPatternError.Add((0 - tempdata[layers - 2][3]));
                        }
                        else if (c == 3)
                        {
                            currentPatternError.Add((0 - tempdata[layers - 2][0]));
                            currentPatternError.Add((0 - tempdata[layers - 2][1]));
                            currentPatternError.Add((0 - tempdata[layers - 2][2]));
                            currentPatternError.Add((1 - tempdata[layers - 2][3]));
                        }
                        double count = 0;
                        for (int er = 0; er < currentPatternError.Count; er++)
                        {
                            count += currentPatternError[er];
                        }
                        count = count / 4;
                        avgErrorForEachPattern[c].Add(count);

                        #endregion
                        output.Clear();
                        tempdata.Clear();
                        currentPatternError.Clear();
                    }
                }
                double relativeError = 0;
                for (int m = 0; m < avgErrorForEachPattern.Count; m++)
                {
                    for (int n = 0; n < avgErrorForEachPattern[m].Count; n++)
                    {
                        relativeError += Math.Pow(avgErrorForEachPattern[m][n], 2);
                    }
                }
                double lastError = 0.5 * ((relativeError) / (avgErrorForEachPattern.Count * avgErrorForEachPattern[0].Count));
                if (lastError <= errorRate)
                {

                    break;
                }

                #endregion

            }
            saveWeightsOnFile();
        }
        public static void backwardComputation(List<List<double>> Dash_Results, List<double> Error)
        {

            S.Clear();

            //hidden to output
            int index_for_Dash_Results = Dash_Results.Count - 1;


            int number_of_layers = layers - 1;

            List<double> temp_of_S = new List<double>();

            for (int i = 0; i < 4; i++)
            {

                temp_of_S.Add(Error[i] * Dash_Results[number_of_layers - 1][i]);
            }
            S.Add(temp_of_S);
            //hidden to hidden & input to hidden
            List<List<double>> S_of_nodes = new List<List<double>>();

            int number_Of_current_layer = layers - 2;
            List<Tuple<Tuple<int, int>, double>> weights_of_current_layer = new List<Tuple<Tuple<int,int>,double>>( weights[number_Of_current_layer]);
            int index_of_S = 0;
            while (number_Of_current_layer != 0)
            {

                int Num_nodes_of_current_layer = nodes[number_Of_current_layer];
                int Num_nodes_of_next_layer = nodes[number_Of_current_layer + 1];

                List<double> temp = new List<double>();

                for (int j = 0; j < Num_nodes_of_current_layer; j++)
                {
                    double sum = 0;
                    int index = 0;
                    //    Tuple<Tuple<int,int>,double> Weights_From_current_Node=weights_of_current_layer.
                    for (int i = 0; i < Num_nodes_of_next_layer; i++)
                    {
                        index = (i * Num_nodes_of_current_layer) + j;
                        sum += (S[index_of_S][i] * (weights_of_current_layer[index].Item2));
                    }
                    temp.Add(sum * Dash_Results[number_Of_current_layer - 1][j]);
                }
                S.Add(temp);
                index_of_S++;
                number_Of_current_layer--;
                weights_of_current_layer = new List<Tuple<Tuple<int,int>,double>>( weights[number_Of_current_layer]);
            }
        }
        
        public static void fNet(List<double> x, int layerNum)
        {
            List<double> op = new List<double>();
            double net = 0;

            int numofnudes = nodes[layerNum];
            if (bais)
                numofnudes++;
            op.Clear();
            List<double> temp_Dash_Result = new List<double>();
            for (int k = 0; k < nodes[layerNum + 1]; k++)
            {
                net = 0;
                for (int j = 0; j < x.Count; j++)
                {
                    net += x[j] * weights[layerNum][j + ((k) * numofnudes)].Item2;
                }
                //v value after activation
                op.Add(sigmoid(net));
                temp_Dash_Result.Add(sigmoidDash(net));
            }
            output.Add(new List<double>(op));
            Dash_Results.Add(temp_Dash_Result);

            //  return sigmoid(net);
        }
        //        Layer->weights [FromNode , ToNode], value
        //public  List<List<Tuple<Tuple<int, int>, double>>> weights;
        public static void generateWeigths()
        {
            int bais_integer = 0;
            if (bais)
                bais_integer = 1;
            for (int i = 1; i < layers; i++)
            {
                List<Tuple<Tuple<int, int>, double>> LayerWeight = new List<Tuple<Tuple<int, int>, double>>();
                for (int j = 0; j < nodes[i]; j++)
                {
                    for (int k = 0; k < nodes[i - 1] + bais_integer; k++)
                    {
                        Tuple<int, int> nodetonode = new Tuple<int, int>(k, j);
                        double Weight = RanNum.NextDouble();

                        LayerWeight.Add(new Tuple<Tuple<int, int>, double>(nodetonode, Weight));
                    }

                }
                weights.Add(LayerWeight);
            }
        }
        public static double sigmoid(double v)
        {
          //  return (1 / (1 + Math.Exp(-a * v)));
            return ((Math.Exp(v/2) - Math.Exp(-v/2)) / (Math.Exp(v/2) + Math.Exp(-v/2)));
        }
        public static double sigmoidDash(double v)
        {
          //  return (a*sigmoid(v) / (1 - sigmoid(v)));
            return (a*Math.Exp(-a*v) / Math.Pow(1 + Math.Exp(-a*v),2));
        }
        public static void update_weights(List<List<double>> fNet_Result, List<double> input_Layer)
        {
            int S_Count = S.Count();
            if (bais == true)
            {

                for (int j = 0; j < fNet_Result.Count; j++)
                    fNet_Result[j].Insert(0, 1.0);

            }
            for (int i = 0; i < layers - 1; i++)
            {
                int Number_Of_nodes_in_current_Layer = nodes[i];
                int Number_of_nodes_in_next_Layer = nodes[i + 1];
                if (bais == true)
                {
                    Number_Of_nodes_in_current_Layer++;
                }
                List<Tuple<Tuple<int, int>, double>> weights_of_current_layer =new List<Tuple<Tuple<int,int>,double>>(weights[i]);
                List<Tuple<Tuple<int, int>, double>> New_weights = new List<Tuple<Tuple<int, int>, double>>();
                Tuple<int, int> indexing;
                double new_weight;

                for (int c = 0; c < Number_Of_nodes_in_current_Layer; c++)
                {

                    for (int n = 0; n < Number_of_nodes_in_next_Layer; n++)
                    {

                        if (i == 0)
                        {

                            //     weights_of_current_layer[(n * Number_of_nodes_in_next_Layer) + c].Item2 += (learningRate * S[S_Count-1][n] * x[c]);
                            indexing = weights_of_current_layer[(n * Number_Of_nodes_in_current_Layer) + c].Item1;
                            new_weight = weights_of_current_layer[(n * Number_Of_nodes_in_current_Layer) + c].Item2 + (learningRate * S[S_Count - 1][n] * input_Layer[c]);
                        }
                        else
                        {
                            indexing = weights_of_current_layer[(n * Number_Of_nodes_in_current_Layer) + c].Item1;
                            new_weight = weights_of_current_layer[(n * Number_Of_nodes_in_current_Layer) + c].Item2 + (learningRate * S[S_Count - 1][n] * fNet_Result[i - 1][c]);

                        }
                        weights[i][(n * Number_Of_nodes_in_current_Layer) + c] = new Tuple<Tuple<int, int>, double>(indexing, new_weight);

                    }
                }

                S_Count--;

            }


        }
        public static void saveWeightsOnFile()
        {
            FileStream weightFile = new FileStream("BP_Data.txt", FileMode.Create, FileAccess.ReadWrite);
            StreamWriter file = new StreamWriter(weightFile);
            file.Write("layers=");
            file.WriteLine(layers);
            if (bais)
            {
                file.WriteLine("baise=1");
            }
            else if (!bais)
            {
                file.WriteLine("baise=0");
            }
            for (int i = 0; i < nodes.Count; i++)
            {
                file.WriteLine(nodes[i]);
            }
            file.WriteLine("+");
            for (int i = 0; i < weights.Count; i++)
            {
                if (i != 0)
                {
                    file.WriteLine("-");
                }
                for (int j = 0; j < weights[i].Count; j++)
                {
                    file.Write(weights[i][j].Item1);
                    file.Write("/");
                    file.WriteLine(weights[i][j].Item2);
                }

            }
            file.Close();
            weightFile.Close();
        }
        public static void getWeightsFormFile()
        {
            FileStream weightFile = new FileStream("BP_Data.txt", FileMode.Open, FileAccess.ReadWrite);
            StreamReader file = new StreamReader(weightFile);
            string line;
            line=file.ReadLine();
            string[] list = line.Split('=');
            layers = int.Parse(list[1]);
            line = file.ReadLine();
            list = line.Split('=');
            if (list[1] == "1")
            {
                bais = true;
            }
            else
            {
                bais = false;
            }
            nodes = new List<int>();
            for (int i = 0; i < layers; i++)
            {
                line = file.ReadLine();
                nodes.Add(int.Parse(line));
            }
           line= file.ReadLine();
           weights = new List<List<Tuple<Tuple<int, int>, double>>>();
           int bais_integer = 0;
           if (bais)
               bais_integer = 1;
           for (int i = 1; i < layers; i++)
           {
               List<Tuple<Tuple<int, int>, double>> LayerWeight = new List<Tuple<Tuple<int, int>, double>>();
               for (int j = 0; j < nodes[i]; j++)
               {
                   for (int k = 0; k < nodes[i - 1] + bais_integer; k++)
                   {
                       line = file.ReadLine();
                       list = line.Split('(',',',')','/');
                       Tuple<int, int> nodetonode = new Tuple<int, int>(int.Parse(list[1]), int.Parse(list[2]));
                       double Weight = double.Parse(list[4]);

                       LayerWeight.Add(new Tuple<Tuple<int, int>, double>(nodetonode, Weight));
                   }

               }
               weights.Add(LayerWeight);
               line = file.ReadLine();
           }
            file.Close();
            weightFile.Close();
        }
        public void testData(List<List<Tuple<string, List<double>>>> FeaturesExtractedForTesting)
        {
            getWeightsFormFile();
            List<double> op = new List<double>();
            output = new List<List<double>>();
           // output.Clear();
            resultOfTheTest = new List<List<List<int>>>();
            //resultOfTheTest.Clear();
            //test code data
            resultOfTheTest.Add(new List<List<int>>());
            resultOfTheTest.Add(new List<List<int>>());
            resultOfTheTest.Add(new List<List<int>>());
            resultOfTheTest.Add(new List<List<int>>());
            resultOfTheTest[0].Add(new List<int>());
            resultOfTheTest[0].Add(new List<int>());
            resultOfTheTest[0].Add(new List<int>());
            resultOfTheTest[0].Add(new List<int>());
            resultOfTheTest[1].Add(new List<int>());
            resultOfTheTest[1].Add(new List<int>());
            resultOfTheTest[1].Add(new List<int>());
            resultOfTheTest[1].Add(new List<int>());
            resultOfTheTest[2].Add(new List<int>());
            resultOfTheTest[2].Add(new List<int>());
            resultOfTheTest[2].Add(new List<int>());
            resultOfTheTest[2].Add(new List<int>());
            resultOfTheTest[3].Add(new List<int>());
            resultOfTheTest[3].Add(new List<int>());
            resultOfTheTest[3].Add(new List<int>());
            resultOfTheTest[3].Add(new List<int>());
            //////////////////////
            List<double> temp;
            getWeightsFormFile();
            NormalizeTestingData(FeatureExtraction_Class.FeaturesExtractedForTesting);
            for (int c = 0; c < 4; c++)
            {
                for (int j = 0; j < FeaturesExtractedForTesting[c].Count; j++)
                {
                    output.Clear();
                    for (int k = 1; k < layers; k++)
                    {
                        if (k == 1)
                        {
                            temp = new List<double>(FeaturesExtractedForTesting[c][j].Item2);
                            if (bais)
                            {
                                temp.Insert(0, 1.0);
                            }
                            fNet(temp, k - 1);
                        }
                        else
                        {
                            temp = new List<double>(output[output.Count - 1]);
                            if (bais)
                                temp.Insert(0, 1);
                            fNet(temp, k - 1);
                        }
                    }
                    #region fill data for confusion matrix
                    double maxValue = output[layers - 2].Max();
                    //class 0
                    if (c == 0 && output[layers - 2][0] == maxValue)
                    {
                        resultOfTheTest[0][0].Add(1);
                    }
                    else if (c == 0 && output[layers - 2][1] == maxValue)
                    {
                        resultOfTheTest[1][0].Add(1);
                    }
                    else if (c == 0 && output[layers - 2][2] == maxValue)
                    {
                        resultOfTheTest[2][0].Add(1);
                    }
                    else if (c == 0 && output[layers - 2][3] == maxValue)
                    {
                        resultOfTheTest[3][0].Add(1);
                    }

                    //class 1
                    else if (c == 1 && output[layers - 2][0] == maxValue)
                    {
                        resultOfTheTest[0][1].Add(1);
                    }
                    else if (c == 1 && output[layers - 2][1] == maxValue)
                    {
                        resultOfTheTest[1][1].Add(1);
                    }
                    else if (c == 1 && output[layers - 2][2] == maxValue)
                    {
                        resultOfTheTest[2][1].Add(1);
                    }
                    else if (c == 1 && output[layers - 2][3] == maxValue)
                    {
                        resultOfTheTest[3][1].Add(1);
                    }
                    //class 2
                    else if (c == 2 && output[layers - 2][0] == maxValue)
                    {
                        resultOfTheTest[0][2].Add(1);
                    }
                    else if (c == 2 && output[layers - 2][1] == maxValue)
                    {
                        resultOfTheTest[1][2].Add(1);
                    }
                    else if (c == 2 && output[layers - 2][2] == maxValue)
                    {
                        resultOfTheTest[2][2].Add(1);
                    }
                    else if (c == 2 && output[layers - 2][3] == maxValue)
                    {
                        resultOfTheTest[3][2].Add(1);
                    }

                    //class 3
                    else if (c == 3 && output[layers - 2][0] == maxValue)
                    {
                        resultOfTheTest[0][3].Add(1);
                    }
                    else if (c == 3 && output[layers - 2][1] == maxValue)
                    {
                        resultOfTheTest[1][3].Add(1);
                    }
                    else if (c == 3 && output[layers - 2][2] == maxValue)
                    {
                        resultOfTheTest[2][3].Add(1);
                    }
                    else if (c == 3 && output[layers - 2][3] == maxValue)
                    {
                        resultOfTheTest[3][3].Add(1);
                    }
                    #endregion
                }
            }
        }


        public static void NormalizeTestingData(List<List<Tuple<string, List<double>>>> FeaturesExtractedForTesting)
        {
            double Max = 0;
            double temp = 0;
            double Min = double.MaxValue;
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < FeaturesExtractedForTesting[i].Count; j++)
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
                for (int j = 0; j < FeaturesExtractedForTesting[i].Count; j++)
                {
                    for (int k = 0; k < FeaturesExtractedForTesting[i][j].Item2.Count; k++)
                    {
                        FeaturesExtractedForTesting[i][j].Item2[k] = (2*((FeaturesExtractedForTesting[i][j].Item2[k] - Min)) / (Max - Min))-1;
                    }
                }
            }

        }
               
        public static void NormalizeTestingImage(List<double> FeaturesForTesting)
        {
            double Max = FeaturesForTesting.Max();
            double Min = FeaturesForTesting.Min();
            //====================================================
            for (int j = 0; j < FeaturesForTesting.Count; j++)
            {
               FeaturesForTesting[j] = (2 * ((FeaturesForTesting[j] - Min)) / (Max - Min)) - 1;
            }
        }

        public static int testoneImg(List<double> features)
        {
            List<double> op = new List<double>();
            output = new List<List<double>>();

            List<double> temp;
            getWeightsFormFile();
            NormalizeTestingImage(features);
            output.Clear();
            for (int k = 1; k < layers; k++)
            {
                if (k == 1)
                {
                    temp = new List<double>(features);
                    if (bais)
                    {
                        temp.Insert(0, 1.0);
                    }
                    fNet(temp, k - 1);
                }
                else
                {
                    temp = new List<double>(output[output.Count - 1]);
                    if (bais)
                        temp.Insert(0, 1);
                    fNet(temp, k - 1);
                }
            }
            double maxValue = output[layers - 2].Max();
            if (output[layers - 2][0] == maxValue)
            {
                return 0;
            }
            else if (output[layers - 2][1] == maxValue)
            {
                return 1;
            }
            else if ( output[layers - 2][2] == maxValue)
            {
                return 2;
            }
            else if ( output[layers - 2][3] == maxValue)
            {
                return 3;
            }

            return -1;
        }
    

    }
}
