using System.Collections.Generic;
using UnityEngine;

namespace UnityProductive
{
	public enum TransferFunction
	{
		HyperbolicTangent,
		Sigmoid
	}

	public class Neuron
	{
		public float Output { get; set; }
		public float[] Weights { get; set; }
		public float[] DeltaWeights { get; set; }
		public float Gradient { get; set; }
		public TransferFunction TransferFunction { get; set; } = TransferFunction.Sigmoid;

		int index;

		public Neuron(int index, int weights, TransferFunction transferFunction)
		{
			this.index = index;

			Weights = new float[weights];
			DeltaWeights = new float[weights];

			for(int i = 0; i < weights; i++)
			{
				Weights[i] = Random.Range(-1.0f, 1.0f);
			}

			TransferFunction = transferFunction;
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
			switch(TransferFunction)
			{
				case TransferFunction.HyperbolicTangent:
					return (float)System.Math.Tanh(value);

				case TransferFunction.Sigmoid:
					return 1.0f / (1.0f + Mathf.Exp(-value));

				default:
					return 0.0f;
			}
		}

		float TransferDerivative(float value)
		{
			switch (TransferFunction)
			{
				case TransferFunction.HyperbolicTangent:
					return 1.0f - Mathf.Pow(value, 2.0f);

				case TransferFunction.Sigmoid:
					return value * (1.0f - value);

				default:
					return 0.0f;
			}
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

		public void UpdateWeights(NeuronLayer previousLayer, float learningRate = 0.3f, float momentum = 0.5f)
		{
			for(int i = 0; i < previousLayer.Neurons.Count; i++)
			{
				Neuron neuron = previousLayer.Neurons[i];

				float oldDeltaWeight = neuron.DeltaWeights[index];

				float newDeltaWeight = learningRate * neuron.Output * Gradient + momentum * oldDeltaWeight;

				neuron.DeltaWeights[index] = newDeltaWeight;
				neuron.Weights[index] += newDeltaWeight;
			}
		}
	}
}
