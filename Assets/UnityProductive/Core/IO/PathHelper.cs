﻿using System.IO;

namespace UnityProductive
{
	public static class PathHelper
	{
		public static string EnsurePath(string path)
		{
			if (!Directory.Exists(path))
			{
				DirectoryInfo directoryInfo = Directory.CreateDirectory(path);
				return directoryInfo.FullName;
			}

			return path;
		}
	}
}
