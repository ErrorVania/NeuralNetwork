using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNeuralNetwork
{
    class Neuron
    {
        private Random random;
        public float[] weights;

        public static float bias = 0f;
        public Neuron(int inputs,bool isInputNeuron)
        {
            random = new Random(DateTime.Now.Millisecond);

            weights = new float[inputs + 1];

            for (int i = 0; i < inputs + 1; i++)
            {
                if (isInputNeuron)
                {
                    weights[i] = 1f;
                    continue;
                }
                weights[i] = (float)random.NextDouble();
            }
        }
        public Neuron(Neuron other)
        {
            for (int i = 0; i < weights.Length; i++)
            {
                weights[i] = other.weights[i];
            } 
        }
        public float Feed(float[] data)
        {
            //this.inputs = data;


            float result = 0;
            for (int i = 0; i < data.Length; i++)
            {
                result += data[i] * weights[i];
            }


            result += bias * weights[weights.Length - 1]; //last weight is for bias
            result += weights.Sum();


            return 1.0f / (1.0f + (float)Math.Exp(-result));//sigmoid
        }


    }
    class Layer
    {
        public int NeuronCount;
        public int inputCount;
        public Neuron[] Neurons;

        public Layer(int size, int InputsPerNeuron,bool isInputLayer)
        {
            NeuronCount = size;
            inputCount = InputsPerNeuron;


            Neurons = new Neuron[size];
            for (int i = 0; i < NeuronCount; i++)
            {
                if (isInputLayer)
                {
                    Neurons[i] = new Neuron(InputsPerNeuron, true);
                    continue;
                }
                Neurons[i] = new Neuron(InputsPerNeuron, false);
            }
        }

        public Layer(Layer other)
        {
            NeuronCount = other.NeuronCount;
            inputCount = other.inputCount;
            for (int i = 0; i < Neurons.Length; i++)
            {
                Neurons[i] = other.Neurons[i];
            }
        }




        public float[] FeedForward(float[] inputs)
        {
            float[] result = new float[NeuronCount];
            for (int i = 0; i < NeuronCount; i++)
            {
                result[i] = Neurons[i].Feed(inputs);
            }
            return result;
        }

    }
    class NeuralNetwork
    {
        private Layer[] NeuronLayers;
        public NeuralNetwork(int[] layerSizes)
        {
            NeuronLayers = new Layer[layerSizes.Length];
            bool firstlayer = true;

            for (int i = 0; i < layerSizes.Length; i++)
            {
                if (!firstlayer)
                {
                    NeuronLayers[i] = new Layer(layerSizes[i], layerSizes[i - 1],false);
                } else
                {
                    NeuronLayers[i] = new Layer(layerSizes[i], layerSizes[i],true);
                    firstlayer = false;
                }
            }
        }

        public NeuralNetwork(NeuralNetwork other)
        {
            for (int i = 0; i < NeuronLayers.Length; i++)
            {
                NeuronLayers[i] = other.NeuronLayers[i];
            }
        }

        public float[] FeedForward(float[] data)
        {
            float[] t = data;
            for (int i = 1; i < NeuronLayers.Length; i++)
            {
                t = NeuronLayers[i].FeedForward(t);
            }
            return t;
        }


        public void Mutate()
        {
            Random r = new Random(DateTime.Now.Millisecond);
            int choice = r.Next(1,3);

            for (int curLayer = 0; curLayer < this.NeuronLayers.Length; curLayer++)
            {
                for (int curNeuron = 0; curNeuron < this.NeuronLayers[curLayer].Neurons.Length; curNeuron++)
                {
                    for (int curWeight = 0; curWeight < this.NeuronLayers[curLayer].Neurons[curNeuron].weights.Length; curWeight++)
                    {
                        if (choice == 1)
                        {
                            NeuronLayers[curLayer].Neurons[curNeuron].weights[curWeight] *= -1f;
                        } else if (choice == 2)
                        {
                            NeuronLayers[curLayer].Neurons[curNeuron].weights[curWeight] = (float)r.NextDouble();
                        }
                    }
                }
            }



        }
    }

}
