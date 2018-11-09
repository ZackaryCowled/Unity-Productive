using System.Text;

namespace UnityProductive
{
	public class CSV
	{
		StringBuilder data;

		public CSV(string value = "")
		{
			data = new StringBuilder();
			data.Append(value);
		}

		public void AppendValue(string value)
		{
			data.Append(value + ',');
		}

		public void EndLine()
		{
			data.AppendLine();
		}

		public override string ToString()
		{
			return data.ToString();
		}
	}
}