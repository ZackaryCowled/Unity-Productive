using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quad : RenderObject
{
	public Quad(bool isActive = true)
	{
		IsActive = isActive;
	}

	protected override void RenderElements(Rect drawArea)
	{
		GUILayout.Box("", GUILayout.Width(drawArea.width), GUILayout.Height(drawArea.height));
	}
}
