using System.Collections.Generic;
using UnityEngine;

namespace UnityProductive
{
	public class NeuralNetwork
	{
		public List<NeuronLayer> NeuronLayers { get; set; }

		public NeuronLayer InputLayer
		{
			get
			{
				if (NeuronLayers.Count > 0)
				{
					return NeuronLayers[0];
				}

				return null;
			}
		}

		public NeuronLayer OutputLayer
		{
			get
			{
				if (NeuronLayers.Count > 0)
				{
					return NeuronLayers[NeuronLayers.Count - 1];
				}

				return null;
			}
		}

		float error;

		public NeuralNetwork(int[] layers)
		{
			NeuronLayers = new List<NeuronLayer>();

			if (layers == null || layers.Length == 0)
			{
				Debug.LogWarning("Neural network created with zero layers");
				return;
			}

			for (int layerIndex = 0; layerIndex < layers.Length - 1; layerIndex++)
			{
				NeuronLayers.Add(new NeuronLayer(layers[layerIndex], layers[layerIndex + 1]));
			}

			NeuronLayers.Add(new NeuronLayer(layers[layers.Length - 1], 0));
		}

		public NeuralNetwork(NeuralNetwork copy)
		{
			NeuronLayers = new List<NeuronLayer>();

			if (copy == null || copy.NeuronLayers.Count == 0)
			{
				Debug.LogWarning("Neural network copied with zero layers");
				return;
			}

			if (copy.NeuronLayers.Count > 1)
			{
				for (int layerIndex = 0; layerIndex < copy.NeuronLayers.Count - 1; layerIndex++)
				{
					NeuronLayers.Add(new NeuronLayer(copy.NeuronLayers[layerIndex].Neurons.Count, copy.NeuronLayers[layerIndex + 1].Neurons.Count));
				}
			}

			NeuronLayers.Add(new NeuronLayer(copy.NeuronLayers[copy.NeuronLayers.Count - 1].Neurons.Count, 0));

			for (int layerIndex = 0; layerIndex < copy.NeuronLayers.Count; layerIndex++)
			{
				for (int neuronIndex = 0; neuronIndex < copy.NeuronLayers[layerIndex].Neurons.Count; neuronIndex++)
				{
					NeuronLayers[layerIndex].Neurons[neuronIndex].Output = copy.NeuronLayers[layerIndex].Neurons[neuronIndex].Output;

					for (int weightIndex = 0; weightIndex < copy.NeuronLayers[layerIndex].Neurons[neuronIndex].Weights.Length; weightIndex++)
					{
						NeuronLayers[layerIndex].Neurons[neuronIndex].Weights[weightIndex] = copy.NeuronLayers[layerIndex].Neurons[neuronIndex].Weights[weightIndex];
					}
				}

				NeuronLayers[layerIndex].Bias = copy.NeuronLayers[layerIndex].Bias;
			}
		}

		public void FeedForward(float[] inputValues)
		{
			if (NeuronLayers.Count == 0)
			{
				Debug.LogError("FeedForward failed because no input layer exists in the neural network");
				return;
			}

			if (inputValues.Length != NeuronLayers[0].Neurons.Count)
			{
				Debug.LogError("FeedForward passed " + inputValues.Length.ToString() + " elements but needed " + NeuronLayers[0].Neurons.Count.ToString());
				return;
			}

			for (int neuronIndex = 0; neuronIndex < inputValues.Length; neuronIndex++)
			{
				NeuronLayers[0].Neurons[neuronIndex].Output = inputValues[neuronIndex];
			}

			for (int layerIndex = 1; layerIndex < NeuronLayers.Count; layerIndex++)
			{
				for (int neuronIndex = 0; neuronIndex < NeuronLayers[layerIndex].Neurons.Count; neuronIndex++)
				{
					NeuronLayers[layerIndex].Neurons[neuronIndex].FeedForward(NeuronLayers[layerIndex - 1]);
				}
			}
		}

		public void BackPropagate(float[] targetValues)
		{
			if (NeuronLayers.Count == 0)
			{
				Debug.LogError("BackPropagate failed because no layers exist in the neural network");
				return;
			}

			NeuronLayer outputLayer = NeuronLayers[NeuronLayers.Count - 1];

			if (targetValues.Length != outputLayer.Neurons.Count)
			{
				Debug.Log("BackPropagate passed " + targetValues.Length.ToString() + " elements but needed " + outputLayer.Neurons.Count.ToString());
				return;
			}

			error = 0.0f;

			for (int i = 0; i < outputLayer.Neurons.Count; i++)
			{
				float delta = targetValues[i] - outputLayer.Neurons[i].Output;
				error += delta * delta;
			}

			error /= outputLayer.Neurons.Count;
			error = Mathf.Sqrt(error);

			for (int i = 0; i < outputLayer.Neurons.Count; i++)
			{
				outputLayer.Neurons[i].CalculateOutputGradients(targetValues[i]);
			}

			for (int i = NeuronLayers.Count - 2; i > 0; i--)
			{
				NeuronLayer hiddenLayer = NeuronLayers[i];
				NeuronLayer nextLayer = NeuronLayers[i + 1];

				for (int j = 0; j < hiddenLayer.Neurons.Count; j++)
				{
					hiddenLayer.Neurons[j].CalculateHiddenGradients(nextLayer);
				}
			}

			for (int i = NeuronLayers.Count - 1; i > 0; i--)
			{
				NeuronLayer layer = NeuronLayers[i];
				NeuronLayer previousLayer = NeuronLayers[i - 1];

				for (int j = 0; j < layer.Neurons.Count; j++)
				{
					layer.Neurons[j].UpdateWeights(previousLayer);
				}
			}
		}

		public void MutateWeights(float chance = 0.5f, float min = -0.1f, float max = 0.1f)
		{
			foreach (NeuronLayer neuronLayer in NeuronLayers)
			{
				neuronLayer.MutateWeights(chance, min, max);
			}
		}
	}
}
