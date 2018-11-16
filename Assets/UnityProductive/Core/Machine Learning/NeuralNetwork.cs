using System.Collections.Generic;

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

		public NeuralNetwork(params int[] layers)
		{
			NeuronLayers = new List<NeuronLayer>();

			foreach (int layerSize in layers)
			{
				NeuronLayers.Add(new NeuronLayer(layerSize));
			}

			for (int i = 0; i < NeuronLayers.Count - 1; i++)
			{
				NeuronLayers[i].ConnectToLayer(NeuronLayers[i + 1]);
			}
		}

		public NeuralNetwork(NeuralNetwork copy)
		{
			NeuronLayers = new List<NeuronLayer>();

			foreach (NeuronLayer neuronLayer in copy.NeuronLayers)
			{
				NeuronLayers.Add(new NeuronLayer(neuronLayer.Neurons.Count));
			}

			for (int i = 0; i < NeuronLayers.Count - 1; i++)
			{
				NeuronLayers[i].ConnectToLayer(NeuronLayers[i + 1]);
			}

			for (int i = 0; i < NeuronLayers.Count; i++)
			{
				for (int j = 0; j < NeuronLayers[i].Neurons.Count; j++)
				{
					NeuronLayers[i].Neurons[j].Bias = copy.NeuronLayers[i].Neurons[j].Bias;
					NeuronLayers[i].Neurons[j].Output = copy.NeuronLayers[i].Neurons[j].Output;

					for (int k = 0; k < NeuronLayers[i].Neurons[j].Connections.Count; k++)
					{
						NeuronLayers[i].Neurons[j].Connections[k].Weight = copy.NeuronLayers[i].Neurons[j].Connections[k].Weight;
					}
				}
			}
		}

		public void Execute(params float[] inputs)
		{
			if (NeuronLayers.Count > 0)
			{
				for (int i = 0; i < inputs.Length && i < NeuronLayers[0].Neurons.Count; i++)
				{
					NeuronLayers[0].Neurons[i].Bias = inputs[i];
				}
			}

			foreach (NeuronLayer neuronLayer in NeuronLayers)
			{
				foreach (Neuron neuron in neuronLayer.Neurons)
				{
					neuron.Initialize();
				}
			}

			foreach (NeuronLayer neuronLayer in NeuronLayers)
			{
				foreach (Neuron neuron in neuronLayer.Neurons)
				{
					neuron.Execute();
				}
			}
		}

		public void MutateBiases(float chance = 1.0f, float min = -0.01f, float max = 0.01f)
		{
			foreach (NeuronLayer neuronLayer in NeuronLayers)
			{
				neuronLayer.MutateBiases(chance, min, max);
			}
		}

		public void MutateWeights(float chance = 1.0f, float min = -0.01f, float max = 0.01f)
		{
			foreach (NeuronLayer neuronLayer in NeuronLayers)
			{
				neuronLayer.MutateWeights(chance, min, max);
			}
		}
	}
}
