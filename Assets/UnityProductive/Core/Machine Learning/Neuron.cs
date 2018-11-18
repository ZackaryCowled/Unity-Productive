using System.Collections.Generic;
using UnityEngine;

namespace UnityProductive
{
	public class Neuron
	{
		public float Output { get; set; }
		public float Weight { get; set; }
		public float[] Weights { get; set; }

		int index;

		public Neuron(int index, int weights)
		{
			this.index = index;

			Weight = Random.Range(-1.0f, 1.0f);
			Weights = new float[weights];
			
			for(int weightIndex = 0; weightIndex < weights; weightIndex++)
			{
				Weights[weightIndex] = Random.Range(-1.0f, 1.0f);
			}
		}

		public void Update(NeuronLayer inputLayer)
		{
			Output = 1.0f;

			foreach(Neuron neuron in inputLayer.Neurons)
			{
				Output *= neuron.Weights[index] * neuron.Output + inputLayer.Bias;
			}

			Output = 1.0f / (1.0f + Mathf.Exp(-Output));
		}

		public void MutateWeights(float chance = 0.5f, float min = -0.1f, float max = 0.1f)
		{
			for(int weightIndex = 0; weightIndex < Weights.Length; weightIndex++)
			{
				if (Random.Range(0.0f, 1.0f) <= chance)
				{
					Weights[weightIndex] += Random.Range(min, max);
				}
			}
		}
	}
}
