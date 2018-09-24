using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ShopButtonColors {

	private static Color _defaultColor = new Color(0f, 0f, 0f, 0.39f);
	private static Color _boughtColor = new Color(0f, 0.33f, 0f, 0.39f);
	private static Color _disabledColor = new Color(0.5f, 0f, 0f, 0.39f);

	public static Color Default {
		get
		{
			return _defaultColor;
		}

		set { }
	}

	public static Color Bought
	{
		get
		{
			return _boughtColor;
		}

		set { }
	}

	public static Color Disabled
	{
		get
		{
			return _disabledColor;
		}

		set { }
	}
}
