using UnityEngine;

namespace UnityProductive
{
	public class Area
	{
		Vector2 position;
		Vector2 scale;
		Margin margin;

		public Area(Vector2 position, Vector2 scale, Margin margin)
		{
			this.position = position;
			this.scale = scale;
			this.margin = margin;
		}

		public Vector2 GetPosition()
		{
			return position;
		}

		public Vector2 GetScale()
		{
			return scale;
		}

		public Margin GetMargin()
		{
			return margin;
		}

		public void SetPosition(Vector2 newPosition)
		{
			position = newPosition;
		}

		public void SetScale(Vector2 newScale)
		{
			scale = newScale;
		}

		public void SetMargin(Margin newMargin)
		{
			margin = newMargin;
		}

		public Rect ToRect()
		{
			return new Rect(position.x + margin.Left, position.y + margin.Top, scale.x - margin.Left - margin.Right, scale.y - margin.Top - margin.Bottom);
		}
	}
}