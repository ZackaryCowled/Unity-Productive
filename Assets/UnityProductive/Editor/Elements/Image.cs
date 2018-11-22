using UnityEngine;

namespace UnityProductive
{
	public class Image : RenderObject
	{
		public Texture Texture { get; set; }
		public bool HasBorder { get; set; } = false;

		protected override void RenderElements(Rect drawArea)
		{
			if (Texture != null)
			{
				if (HasBorder)
				{
					GUILayout.Box(Texture, GUILayout.Width(drawArea.width), GUILayout.Height(drawArea.height));
				}
				else
				{
					GUILayout.Box(Texture, StyleHelper.GetEmptyStyle(), GUILayout.Width(drawArea.width), GUILayout.Height(drawArea.height));
				}
			}
		}
	}
}
