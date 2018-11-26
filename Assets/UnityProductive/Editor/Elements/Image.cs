using UnityEngine;
using UnityEditor;

namespace UnityProductive
{
	public enum WrapMode : int
	{
		Native,
		Stretch,
		Repeat
	}

	public class Image : RenderObject
	{
		public Texture Texture { get; set; }
		public WrapMode WrapMode { get; set; }
		public Vector2 UVOffsetXY { get; set; } = new Vector2(0.0f, 0.0f);
		public Vector2 UVOffsetZW { get; set; } = new Vector2(0.0f, 0.0f);

		public float NativeWidth
		{
			get
			{
				if (Texture != null)
				{
					return Texture.width;
				}

				return 0.0f;
			}
		}

		public float NativeHeight
		{
			get
			{
				if(Texture != null)
				{
					return Texture.height;
				}

				return 0.0f;
			}
		}

		public Vector2 NativeSize
		{
			get
			{
				if(Texture != null)
				{
					return new Vector2(Texture.width, Texture.height);
				}

				return Vector2.zero;
			}
		}

		protected override void RenderElements(Rect drawArea)
		{
			if (Texture != null)
			{
				switch(WrapMode)
				{
					case WrapMode.Native:
						GUILayout.Box(Texture, StyleHelper.GetEmptyStyle(), GUILayout.Width(drawArea.width), GUILayout.Height(drawArea.height));
						break;

					case WrapMode.Stretch:
						GUI.DrawTextureWithTexCoords(drawArea, Texture, new Rect(UVOffsetXY.x, UVOffsetXY.y, 1.0f + UVOffsetZW.x, 1.0f + UVOffsetZW.y), true);
						break;

					case WrapMode.Repeat:
						GUI.DrawTextureWithTexCoords(drawArea, Texture, new Rect(UVOffsetXY.x, UVOffsetXY.y, drawArea.width / Texture.width + UVOffsetZW.x, drawArea.height / Texture.height + UVOffsetZW.y), true);
						break;
				}
			}
		}
	}
}
