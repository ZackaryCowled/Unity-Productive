using System.Collections.Generic;
using UnityEngine;

namespace UnityProductive
{
	public class Neuron
	{
		public float Output { get; set; }
		public float[] Weights { get; set; }
		public float[] DeltaWeights { get; set; }
		public float Gradient { get; set; }

		int index;

		public Neuron(int index, int weights)
		{
			this.index = index;

			Weights = new float[weights];
			DeltaWeights = new float[weights];

			for(int i = 0; i < weights; i++)
			{
				Weights[i] = Random.Range(-1.0f, 1.0f);
			}
		}

		public void CalculateOutputGradients(float target)
		{
			float delta = target - Output;
			Gradient = delta * TransferDerivative(Output);
		}

		public void CalculateHiddenGradients(NeuronLayer nextLayer)
		{
			float dow = SumDOW(nextLayer);
			Gradient = dow * TransferDerivative(Output);
		}

		float SumDOW(NeuronLayer nextLayer)
		{
			float sum = 0.0f;

			for(int i = 0; i < nextLayer.Neurons.Count; i++)
			{
				sum += Weights[i] * nextLayer.Neurons[i].Gradient;
			}

			return sum;
		}

		public void FeedForward(NeuronLayer inputLayer)
		{
			//SIGMOID
			//Output = 1.0f;

			//foreach(Neuron neuron in inputLayer.Neurons)
			//{
			//	Output *= neuron.Weights[index] * neuron.Output + inputLayer.Bias;
			//}

			//Output = 1.0f / (1.0f + Mathf.Exp(-Output));

			//HYPERBOLIC TANGENT
			float sum = 0.0f;

			foreach (Neuron neuron in inputLayer.Neurons)
			{
				sum += neuron.Weights[index] * neuron.Output;
			}

			sum += inputLayer.Bias;

			Output = Transfer(sum);
		}

		float Transfer(float value)
		{
			return (float)System.Math.Tanh(value);
		}

		float TransferDerivative(float value)
		{
			return 1.0f - value * value;
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

		public void UpdateWeights(NeuronLayer previousLayer)
		{
			for(int i = 0; i < previousLayer.Neurons.Count; i++)
			{
				Neuron neuron = previousLayer.Neurons[i];

				float oldDeltaWeight = neuron.DeltaWeights[index];

				float newDeltaWeight = 0.1f /*Learning rate*/ * neuron.Output * Gradient + 0.5f /*Momentum*/ * oldDeltaWeight;

				neuron.DeltaWeights[index] = newDeltaWeight;
				neuron.Weights[index] += newDeltaWeight;
			}
		}
	}
}
