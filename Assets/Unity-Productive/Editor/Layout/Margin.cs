using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Margin
{
	Vector4 margin;

	public Margin()
	{
		Left = 0.0f;
		Top = 0.0f;
		Right = 0.0f;
		Bottom = 0.0f;
	}

	public Margin(float value)
	{
		Left = value;
		Top = value;
		Right = value;
		Bottom = value;
	}

	public Margin(float marginLeft, float marginTop, float marginRight, float marginBottom)
	{
		Left = marginLeft;
		Top = marginTop;
		Right = marginRight;
		Bottom = marginBottom;
	}

	public float Left
	{
		get { return margin.x; }
		set { margin.x = value; }
	}

	public float Top
	{
		get { return margin.y; }
		set { margin.y = value; }
	}

	public float Right
	{
		get	{ return margin.z; }
		set { margin.z = value; }
	}

	public float Bottom
	{
		get { return margin.w; }
		set { margin.w = value; }
	}
}
