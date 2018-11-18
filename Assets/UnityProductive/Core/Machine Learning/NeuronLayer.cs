using System.Collections.Generic;

namespace UnityProductive
{
	public class NeuronLayer
	{
		public List<Neuron> Neurons;
		public float Bias { get; set; }

		public NeuronLayer(int layerSize, int nextLayerSize)
		{
			Neurons = new List<Neuron>();

			for (int i = 0; i < layerSize; i++)
			{
				Neurons.Add(new Neuron(i, nextLayerSize));
			}

			Bias = 1.0f;
		}

		public void MutateWeights(float chance = 0.5f, float min = -0.1f, float max = 0.1f)
		{
			foreach(Neuron neuron in Neurons)
			{
				neuron.MutateWeights(chance, min, max);
			}
		}
	}
}
