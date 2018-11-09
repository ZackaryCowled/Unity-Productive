using System.Collections.Generic;
using System.Linq;
using UnityEditor;

namespace UnityProductive
{
	public class RenderGroup
	{
		HashPool<string> renderObjects;
		EditorWindow parentWindow;

		public RenderGroup()
		{
			renderObjects = new HashPool<string>();
		}

		public T CreateRenderObject<T>(string hash) where T : RenderObject, new()
		{
			return renderObjects.CreateObject<T>(hash);
		}

		public T GetRenderObject<T>(string hash) where T : RenderObject
		{
			return renderObjects.GetObject<T>(hash);
		}

		public void RemoveRenderObject(string hash)
		{
			renderObjects.DestroyObject(hash);
		}

		public void PollEvents(EditorWindow window)
		{
			parentWindow = window;

			renderObjects.ForEach<RenderObject>(PollEvents);
		}

		void PollEvents(RenderObject renderObject)
		{
			if(renderObject != null && renderObject.IsActive)
			{
				renderObject.PollEvents(parentWindow);
			}
		}

		public void Render(EditorWindow window)
		{
			parentWindow = window;

			renderObjects.ForEach<RenderObject>(Render);
		}

		void Render(RenderObject renderObject)
		{
			if(renderObject != null && renderObject.IsActive)
			{
				renderObject.Render(parentWindow);
			}
		}
	}
}
