using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Mirror;


public class Exit : MonoBehaviour
{
	public void ExitGame()
	{
		Application.Quit();
		Debug.Log("Exit Game");
	}		
}
