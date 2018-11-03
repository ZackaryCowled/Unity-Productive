using System.Collections.Generic;
using UnityEditor;

public class RenderGroup
{
	Dictionary<string, int> renderObjectsMap;
	List<RenderObject> renderObjects;

	public RenderGroup()
	{
		renderObjectsMap = new Dictionary<string, int>();
		renderObjects = new List<RenderObject>();
	}

	public RenderObject AddRenderObject(string uniqueIdentifier, RenderObject renderObject)
	{
		renderObjectsMap[uniqueIdentifier] = renderObjects.Count;
		renderObjects.Add(renderObject);
		return renderObjects[renderObjects.Count - 1];
	}

	public RenderObject GetRenderObject(string uniqueIdentifier)
	{
		int renderObjectID;

		if (renderObjectsMap.TryGetValue(uniqueIdentifier, out renderObjectID))
		{
			return renderObjects[renderObjectID];
		}

		return null;
	}

	public void RemoveRenderObject(string uniqueIdentifier)
	{
		int renderObjectID;

		if(renderObjectsMap.TryGetValue(uniqueIdentifier, out renderObjectID))
		{
			renderObjects[renderObjectID] = null;
		}
	}

	public void PollEvents(EditorWindow window)
	{
		foreach (RenderObject renderObject in renderObjects)
		{
			if (renderObject != null && renderObject.IsActive)
			{
				renderObject.PollEvents(window);
			}
		}
	}

	public void Render(EditorWindow window)
	{
		foreach (RenderObject renderObject in renderObjects)
		{
			if(renderObject != null && renderObject.IsActive)
			{
				renderObject.Render(window);
			}
		}
	}
}
