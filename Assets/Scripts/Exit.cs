using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Mirror;


public class Exit : MonoBehaviour
{
	public void ExitGame()
	{
#if UNITY_EDITOR
		Debug.Log("Stopping play mode in editor");
		UnityEditor.EditorApplication.isPlaying = false; // Stop play mode in editor
#else
		Debug.Log("Exiting game");
		Application.Quit(); // Quit the game in a built application
#endif
	}		
}
  
