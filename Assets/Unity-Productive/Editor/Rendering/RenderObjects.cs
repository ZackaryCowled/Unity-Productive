using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public abstract class RenderObject
{
	public delegate void ResizeEvent(EditorWindow window);
	public event ResizeEvent OnResize;

	public bool IsActive;
	public Area RenderArea;
	public Color RenderColor;

	Vector2 windowScale;

	public RenderObject()
	{
		IsActive = true;
		RenderArea = new Area(Vector2.zero, Vector2.zero, new Margin(0.0f, 0.0f, 0.0f, 0.0f));
		RenderColor = GUI.color;
		windowScale = Vector2.zero;
	}

	~RenderObject()
	{
		OnResize = null;
	}

	public void PollEvents(EditorWindow window)
	{
		HandleResizeEvent(window);
	}

	void HandleResizeEvent(EditorWindow window)
	{
		Vector2 currentWindowScale = new Vector2(window.position.width, window.position.height);

		if(currentWindowScale != windowScale)
		{
			windowScale = currentWindowScale;

			OnResize?.Invoke(window);
		}
	}

	public void Render(EditorWindow window)
	{
		Rect drawArea = RenderArea.ToRect();

		GUILayout.BeginArea(drawArea);

		Color originalColor = GUI.color;

		GUI.color = RenderColor;

		RenderElements(drawArea);

		GUI.color = originalColor;

		GUILayout.EndArea();
	}

	protected abstract void RenderElements(Rect drawArea);
}
