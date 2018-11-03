using UnityEngine;
using UnityEditor;
using System;

public class Button : RenderObject
{
	public delegate void ClickedEvent();
	public event ClickedEvent OnClicked;

	public string Text;

	public Button(string text, bool isActive = true)
	{
		Text = text;
		IsActive = isActive;
	}

	~Button()
	{
		OnClicked = null;
	}

	protected override void RenderElements(Rect drawArea)
	{
		if(GUILayout.Button(Text, GUILayout.Width(drawArea.width), GUILayout.Height(drawArea.height)))
		{
			OnClicked?.Invoke();
		}
	}
}
