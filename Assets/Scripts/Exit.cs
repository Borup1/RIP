using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Mirror;


public class Exit : MonoBehaviour
{
	public void Exitapp()
	{
		Application.Quit();
		Debug.Log("Exit Game");
	}
	
	public void ExitGame()
	{   
		SteamLobby.Instance.LeaveLobby(); 
		// stops host 
		if (NetworkServer.active && NetworkClient.isConnected)
		{
			NetworkManager.singleton.StopHost();
		}
		//// stop client if client-only
		else if (NetworkClient.isConnected)
		{
			NetworkManager.singleton.StopClient();
		}
		//// stop server if server-only
		else if (NetworkServer.active)
		{
			NetworkManager.singleton.StopServer();
		}
	}
	
}
		

  
