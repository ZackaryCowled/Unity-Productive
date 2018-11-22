using UnityEngine;

namespace UnityProductive
{
	public class Panel : RenderObject
	{
		protected override void RenderElements(Rect drawArea)
		{
			GUILayout.Box("", GUILayout.Width(drawArea.width), GUILayout.Height(drawArea.height));
		}
	}
}
