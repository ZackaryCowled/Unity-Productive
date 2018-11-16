using System.Collections.Generic;
using UnityEngine;

namespace UnityProductive
{
	public class Neuron
	{
		public float Bias { get; set; }
		public float Output { get; set; }
		public List<NeuronConnection> Connections { get; set; }

		public Neuron(float bias)
		{
			Bias = bias;
			Connections = new List<NeuronConnection>();
		}

		public void Initialize()
		{
			Output = Bias;
		}

		public void ConnectToNeuron(Neuron neuron, float weight = 1.0f)
		{
			Connections.Add(new NeuronConnection(neuron, weight));
		}

		public void DisconnectFromNeuron(Neuron neuron)
		{
			for (int i = 0; i < Connections.Count; i++)
			{
				if (Connections[i].Neuron == neuron)
				{
					Connections.RemoveAt(i);
					return;
				}
			}
		}

		public void Execute()
		{
			foreach (NeuronConnection connection in Connections)
			{
				connection.Neuron.Output += Output * connection.Weight;
			}
		}

		public void MutateBias(float chance = 1.0f, float min = -0.01f, float max = 0.01f)
		{
			if (Random.Range(0.0f, 1.0f) <= chance)
			{
				Bias += Random.Range(min, max);
			}
		}

		public void MutateWeights(float chance = 1.0f, float min = -0.01f, float max = 0.01f)
		{
			foreach(NeuronConnection neuronConnection in Connections)
			{
				if(Random.Range(0.0f, 1.0f) <= chance)
				{
					neuronConnection.Weight += Random.Range(min, max);
				}
			}
		}
	}
}
