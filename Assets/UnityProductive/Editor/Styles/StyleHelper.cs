using UnityEngine;

namespace UnityProductive
{
	public static class StyleHelper
	{
		static GUIStyle emptyStyle;

		public static GUIStyle GetEmptyStyle()
		{
			if (emptyStyle == null)
			{
				emptyStyle = new GUIStyle();
			}

			return emptyStyle;
		}
	}
}
