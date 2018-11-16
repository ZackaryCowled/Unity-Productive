using System.Collections.Generic;

namespace UnityProductive
{
	public class NeuronLayer
	{
		public List<Neuron> Neurons;

		public NeuronLayer(int size)
		{
			Neurons = new List<Neuron>();

			for (int i = 0; i < size; i++)
			{
				Neurons.Add(new Neuron(1.0f));
			}
		}

		public void ConnectToLayer(NeuronLayer neuronLayer, float weight = 1.0f)
		{
			foreach (Neuron neuron in Neurons)
			{
				foreach (Neuron otherNeuron in neuronLayer.Neurons)
				{
					neuron.ConnectToNeuron(otherNeuron, weight);
				}
			}
		}

		public void DisconnectFromLayer(NeuronLayer neuronLayer)
		{
			foreach (Neuron neuron in Neurons)
			{
				foreach (Neuron otherNeuron in neuronLayer.Neurons)
				{
					neuron.DisconnectFromNeuron(otherNeuron);
				}
			}
		}

		public void MutateBiases(float chance = 1.0f, float min = -0.01f, float max = 0.01f)
		{
			foreach(Neuron neuron in Neurons)
			{
				neuron.MutateBias(chance, min, max);
			}
		}

		public void MutateWeights(float chance = 1.0f, float min = -0.01f, float max = 0.01f)
		{
			foreach(Neuron neuron in Neurons)
			{
				neuron.MutateWeights(chance, min, max);
			}
		}
	}
}
