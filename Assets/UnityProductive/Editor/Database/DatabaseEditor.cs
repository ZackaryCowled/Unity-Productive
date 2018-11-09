using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEditor;

namespace UnityProductive
{
	public class DatabaseEditor : EditorWindow
	{
		static List<RenderGroup> renderGroups;
		static Database database;

		[MenuItem("Productive/Database Editor")]
		static void CreateWindow()
		{
			EditorWindow window = GetWindow(typeof(DatabaseEditor));

			window.titleContent = new GUIContent("Database");
			window.minSize = new Vector2(300, 300);

			window.Show();
		}

		static void InitializeElements(EditorWindow window)
		{
			renderGroups = new List<RenderGroup>();

			string databasesPath = Application.dataPath + "/Resources/Databases/";

			if (!Directory.Exists(databasesPath))
			{
				Directory.CreateDirectory(databasesPath);
			}

			DatabaseManager databaseManager = new DatabaseManager(window);

			renderGroups.Add(databaseManager);
		}

		void OnGUI()
		{
			if (renderGroups == null)
			{
				InitializeElements(this);
			}

			foreach (RenderGroup renderGroup in renderGroups)
			{
				renderGroup.PollEvents(this);
				renderGroup.Render(this);
			}
		}
	}
}
