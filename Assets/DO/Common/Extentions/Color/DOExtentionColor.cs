﻿using UnityEngine;
using System.Collections;

public static class DOExtentionColor {

	public static Color SetAlpha(this Color color, float alpha)
	{
		color.a = alpha;
		return color;
	}

}
