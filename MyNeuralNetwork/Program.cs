using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;


namespace MyNeuralNetwork
{

    class Program
    {
        static void Main(string[] args)
        {
            int[] proportions = { 2, 25, 25, 1 };

            NeuralNetwork n = new NeuralNetwork(proportions);
            foreach (float a in n.FeedForward(new float[] { 1f, 0f }))
            {
                Console.WriteLine(a);
            }

            



            Console.ReadLine();
        }
    }
}
