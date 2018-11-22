using UnityEditor;
using UnityEngine;

namespace UnityProductive
{
	public class DatabaseManager : RenderGroup
	{
		Database database;

		Panel background;
		Image logoImage;
		Button openDatabaseButton;
		Button createDatabaseButton;

		public DatabaseManager(EditorWindow window)
		{
			database = new Database();

			background = CreateRenderObject<Panel>("Background");
			background.OnResize += OnBackgroundResize;

			logoImage = CreateRenderObject<Image>("Logo Image");
			logoImage.Texture = Resources.Load<Texture>("UnityProductiveLogo");
			logoImage.OnResize += OnLogoImageResize;

			openDatabaseButton = CreateRenderObject<Button>("Open Database Button");
			openDatabaseButton.Text = "Open Database";
			openDatabaseButton.OnResize += OnOpenDatabaseButtonResize;
			openDatabaseButton.OnClicked += OnOpenDatabaseButtonClicked;

			createDatabaseButton = CreateRenderObject<Button>("Create Database Button");
			createDatabaseButton.Text = "Create Database";
			createDatabaseButton.OnResize += OnCreateDatabaseButtonResize;
			createDatabaseButton.OnClicked += OnCreateDatabaseButtonClicked;
		}

		Database GetDatabase()
		{
			return database;
		}

		void OnBackgroundResize(EditorWindow window)
		{
			background.RenderArea.SetPosition(new Vector2(0.0f, window.position.height - 50.0f));
			background.RenderArea.SetScale(new Vector2(window.position.width, 50.0f));
		}

		void OnLogoImageResize(EditorWindow window)
		{
			logoImage.RenderArea.SetPosition(new Vector2(window.position.width * 0.5f - 100.0f, window.position.height * 0.5f - 125.0f));
			logoImage.RenderArea.SetScale(new Vector2(200.0f, 200.0f));
		}

		void OnOpenDatabaseButtonResize(EditorWindow window)
		{
			openDatabaseButton.RenderArea.SetPosition(new Vector2(window.position.width * 0.5f - 150.0f, window.position.height - 50.0f));
			openDatabaseButton.RenderArea.SetScale(new Vector2(300.0f, 25.0f));
			openDatabaseButton.RenderArea.SetMargin(new Margin(5.0f, 5.0f, 5.0f, 2.0f));
		}

		void OnCreateDatabaseButtonResize(EditorWindow window)
		{
			createDatabaseButton.RenderArea.SetPosition(new Vector2(window.position.width * 0.5f - 150.0f, window.position.height - 25.0f));
			createDatabaseButton.RenderArea.SetScale(new Vector2(300.0f, 25.0f));
			createDatabaseButton.RenderArea.SetMargin(new Margin(5.0f, 2.0f, 5.0f, 5.0f));
		}

		void OnOpenDatabaseButtonClicked()
		{
			string databaseDirectory = PathHelper.EnsurePath(Application.dataPath + "/Resources/Databases/");

			string filepath = EditorUtility.OpenFilePanel("Open Database", databaseDirectory, "csv");

			if (string.IsNullOrEmpty(filepath))
			{
				return;
			}

			OpenDatabase(filepath);
		}

		void OpenDatabase(string filepath)
		{
			database = new Database();

			database.LoadFromCSV(new CSV(Importer.ImportFileAsText(filepath)));
		}

		void OnCreateDatabaseButtonClicked()
		{
			string databaseDirectory = PathHelper.EnsurePath(Application.dataPath + "/Resources/Databases/");

			string filepath = EditorUtility.SaveFilePanel("Create Database", databaseDirectory, "New Database", "csv");

			if (string.IsNullOrEmpty(filepath))
			{
				return;
			}

			CreateDatabase(filepath);
		}

		void CreateDatabase(string filepath)
		{
			database = new Database();

			Exporter.ExportTextToFile(filepath, database.GenerateCSV().ToString());

			AssetDatabase.Refresh();
		}
	}
}
