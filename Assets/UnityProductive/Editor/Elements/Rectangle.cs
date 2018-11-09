using UnityEngine;

namespace UnityProductive
{
	public class Rectangle : RenderObject
	{
		protected override void RenderElements(Rect drawArea)
		{
			GUILayout.Box("", GUILayout.Width(drawArea.width), GUILayout.Height(drawArea.height));
		}
	}
}
