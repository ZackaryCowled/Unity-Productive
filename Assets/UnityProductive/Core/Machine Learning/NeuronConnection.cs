namespace UnityProductive
{
	public class NeuronConnection
	{
		public Neuron Neuron { get; private set; }
		public float Weight { get; set; }

		public NeuronConnection(Neuron neuron, float weight)
		{
			Neuron = neuron;
			Weight = weight;
		}
	}
}
