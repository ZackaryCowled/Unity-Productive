using System.IO;

namespace UnityProductive
{
	public static class Importer
	{
		public static string ImportFileAsText(string filepath)
		{
			StreamReader streamReader = new StreamReader(filepath);

			string text = streamReader.ReadToEnd();

			streamReader.Close();

			return text;
		}
	}
}
