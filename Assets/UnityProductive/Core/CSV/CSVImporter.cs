namespace UnityProductive
{
	public static class CSVImporter
	{
		public static CSV Import(string filepath)
		{
			CSV csv = new CSV();

			csv.AppendValue(Importer.ImportFileAsText(filepath));

			return csv;
		}
	}
}
