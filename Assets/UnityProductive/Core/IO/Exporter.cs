using System.IO;

namespace UnityProductive
{
	public static class Exporter
	{
		public static void ExportTextToFile(string filepath, string text)
		{
			StreamWriter streamWriter = new StreamWriter(filepath);

			streamWriter.Write(text);

			streamWriter.Close();
		}
	}
}
