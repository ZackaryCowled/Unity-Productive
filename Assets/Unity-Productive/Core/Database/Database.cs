using System;
using System.Collections.Generic;
using System.Linq;

public class Database
{
	List<List<object>> data;
	Dictionary<string, Tuple<int, object>> dataMap;
	List<string> fieldNames;

	public Database()
	{
		data = new List<List<object>>();
		dataMap = new Dictionary<string, Tuple<int, object>>();
		fieldNames = new List<string>();
	}

	public void AddField<T>(string fieldName, T initialValue)
	{
		int columnCount = fieldNames.Count;

		dataMap[fieldName] = new Tuple<int, object>(columnCount, initialValue);
		fieldNames.Add(fieldName);
	}

	public void AddElement(params object[] fieldValues)
	{
		data.Add(new List<object>());

		int rowIndex = data.Count - 1;
		int columnsCount = fieldNames.Count;
		int fieldValuesCount = fieldValues.Count();

		for (int columnIndex = 0; columnIndex < columnsCount; columnIndex++)
		{
			if (columnIndex < fieldValuesCount)
			{
				data[rowIndex].Add(fieldValues[columnIndex]);
			}
			else
			{
				data[rowIndex].Add(dataMap[GetFieldName(columnIndex)].Item2);
			}
		}
	}

	public void RemoveField(string fieldName)
	{
		int index = dataMap[fieldName].Item1;
		int objectCount = data.Count;

		for (int i = 0; i < objectCount; i++)
		{
			data[index][i] = null;
		}
	}

	public string GetFieldName(int columnIndex)
	{
		return fieldNames[columnIndex];
	}

	public object GetValue(string fieldName, int id)
	{
		return data[dataMap[fieldName].Item1][id];
	}

	public void SetValue<T>(string fieldName, int id, T value)
	{
		data[dataMap[fieldName].Item1][id] = value;
	}

	public void LoadFromCSV(CSV csv)
	{
		Clear();

		char[] filters = Environment.NewLine.ToCharArray();
		List<string> lines = csv.ToString().Split(filters).ToList();

		if (lines.Count == 0)
		{
			return;
		}

		for (int lineIndex = 0; lineIndex < lines.Count;)
		{
			if (lines[lineIndex] == "" || lines[lineIndex] == ",")
			{
				lines.RemoveAt(lineIndex);
				continue;
			}
			else if (!string.IsNullOrEmpty(lines[lineIndex]))
			{
				while (lines[lineIndex][0] == ',')
				{
					lines[lineIndex] = lines[lineIndex].Substring(1, lines[lineIndex].Length - 2);
				}

				while (lines[lineIndex].Last() == ',')
				{
					lines[lineIndex] = lines[lineIndex].Substring(0, lines[lineIndex].Length - 2);
				}
			}

			lineIndex++;
		}

		if(lines.Count == 0)
		{
			return;
		}

		string[] fieldNames = lines[0].Split(',');

		for (int fieldNameIndex = 0; fieldNameIndex < fieldNames.Length; fieldNameIndex++)
		{
			//TODO: Read header and get default field value (Need to implement header in csv generation)
			//IDEA: Use reflection for casting (Deserializing)
			/*Header:
				<number of columns>
				<foreach column>
					<type name>
					<initial value>
			*/
			AddField(fieldNames[fieldNameIndex], 0);
		}

		for (int lineIndex = 1; lineIndex < lines.Count; lineIndex++)
		{
			AddElement();

			string[] elements = lines[lineIndex].Split(',');

			for (int elementIndex = 0; elementIndex < elements.Length; elementIndex++)
			{
				SetValue(fieldNames[elementIndex], lineIndex - 1, (object)elements[elementIndex]);
			}
		}
	}

	public void Clear()
	{
		data.Clear();
		dataMap.Clear();
		fieldNames.Clear();
	}

	public CSV GenerateCSV()
	{
		CSV csv = new CSV();

		KeyValuePair<string, Tuple<int, object>>[] dataArray = dataMap.ToArray();

		foreach (KeyValuePair<string, Tuple<int, object>> element in dataArray)
		{
			csv.AppendValue(element.Key);
		}

		csv.EndLine();

		int columnCount = fieldNames.Count;
		int rowCount = data.Count;

		for (int columnIndex = 0; columnIndex < columnCount; columnIndex++)
		{
			for (int rowIndex = 0; rowIndex < rowCount; rowIndex++)
			{
				csv.AppendValue(data[rowIndex][columnIndex].ToString());
			}
		}

		return csv;
	}
}

