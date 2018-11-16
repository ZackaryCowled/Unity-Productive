using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityProductive;

public class MachineLearningXORTest : MonoBehaviour
{
	public bool shouldMutate = true;
	public float biasMutationChance = 0.3f;
	public float weightMutationChance = 0.7f;
	public float maxLearningRate = 0.03f;
	public Text outputText;

	NeuralNetwork neuralNetwork;
	NeuralNetwork bestNeuralNetwork;
	List<List<float>> testInputs;
	List<float> testOutputs;

	float error = float.MaxValue;
	float lowestError = float.MaxValue;

	void Awake()
	{
		bestNeuralNetwork = new NeuralNetwork(new int[] { 2, 4, 1 });

		testInputs = new List<List<float>>();

		testInputs.Add(new List<float>());
		testInputs[testInputs.Count - 1].Add(0.0f);
		testInputs[testInputs.Count - 1].Add(0.0f);

		testInputs.Add(new List<float>());
		testInputs[testInputs.Count - 1].Add(0.0f);
		testInputs[testInputs.Count - 1].Add(1.0f);

		testInputs.Add(new List<float>());
		testInputs[testInputs.Count - 1].Add(1.0f);
		testInputs[testInputs.Count - 1].Add(0.0f);

		testInputs.Add(new List<float>());
		testInputs[testInputs.Count - 1].Add(1.0f);
		testInputs[testInputs.Count - 1].Add(1.0f);

		testOutputs = new List<float>();

		testOutputs.Add(0.0f);

		testOutputs.Add(1.0f);

		testOutputs.Add(1.0f);

		testOutputs.Add(0.0f);
	}

	void Update()
	{
		if(testInputs.Count != testOutputs.Count)
		{
			Debug.Log("Skipping test, please ensure that there are the same number of test input sets as test output sets");
			return;
		}

		neuralNetwork = new NeuralNetwork(bestNeuralNetwork);

		if(shouldMutate)
		{
			neuralNetwork.MutateBiases(biasMutationChance, -maxLearningRate, maxLearningRate);
			neuralNetwork.MutateWeights(weightMutationChance, -maxLearningRate, maxLearningRate);
		}

		error = 0;
		outputText.text = "";

		for(int i = 0; i < testInputs.Count && testInputs[i].Count > 1; i++)
		{
			neuralNetwork.Execute(testInputs[i][0], testInputs[i][1]);

			NeuronLayer outputLayer = neuralNetwork.OutputLayer;

			if (outputLayer != null && outputLayer.Neurons.Count > 0)
			{
				outputText.text += outputLayer.Neurons[0].Output + System.Environment.NewLine;
				error += Mathf.Abs(testOutputs[i] - outputLayer.Neurons[0].Output);
			}
		}

		if(error < lowestError)
		{
			lowestError = error;
			bestNeuralNetwork = new NeuralNetwork(neuralNetwork);
		}
	}
}
