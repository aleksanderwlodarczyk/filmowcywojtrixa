using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameManager {
	private static bool isPaused = false;
	public static bool IsPaused { get { return isPaused; }  }

	public static void Pause()
	{
		isPaused = true;
	}
	public static void Unpause()
	{
		isPaused = false;
	}
}
