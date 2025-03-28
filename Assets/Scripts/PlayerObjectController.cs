﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using Steamworks;
using UnityEngine.SceneManagement;

public class PlayerObjectController : NetworkBehaviour
{
    //Player Data
    [SyncVar] public int ConnectionID;
    [SyncVar] public int PlayerIdNumber;
    [SyncVar] public ulong PlayerSteamID;
    [SyncVar(hook = nameof(PlayerNameUpdate))] public string PlayerName;

    private CustomNetworkManager manager;


    private CustomNetworkManager Manager
    {
        get
        {
            if(manager != null)
            {
                return manager;
            }
            return manager = CustomNetworkManager.singleton as CustomNetworkManager;
        }
    }
    
	private void Start()
	{
		DontDestroyOnLoad(this.gameObject);
	}

    public override void OnStartAuthority()
    {
        CmdSetPlayerName(SteamFriends.GetPersonaName().ToString());
        gameObject.name = "LocalGamePlayer";
        LobbyController.Instance.FindLocalPlayer();
        LobbyController.Instance.UpdateLobbyName();
    }


    public override void OnStartClient()
    {
        Manager.GamePlayers.Add(this);
        LobbyController.Instance.UpdateLobbyName();
        LobbyController.Instance.UpdatePlayerList();
    }


    public override void OnStopClient()
    {
        Manager.GamePlayers.Remove(this);
        LobbyController.Instance.UpdatePlayerList();
    }


    [Command]
    private void CmdSetPlayerName(string playerName)
    {
        this.PlayerNameUpdate(this.PlayerName, playerName);
    }


    public void PlayerNameUpdate(string OldValue, string NewValue)
    {
        if(isServer) //Host
        {
            this.PlayerName = NewValue;
        }
        if(isClient) //Client
        {
            LobbyController.Instance.UpdatePlayerList();
        }
    }

	//Start Game
	
	public void CanStartGame(string SceneName)
	{
		if (hasAuthority)
		{
			CmdCanStartGame(SceneName);
		}
	}
    
	[Command]
	public void CmdCanStartGame(string SceneName)
	{
		manager.StartGame(SceneName);
	}
	
	
	
	
	
    public void Quit()
	{
		
		//Set the offline scene to null
		manager.offlineScene = "";

		//Make the active scene the offline scene
		SceneManager.LoadScene("MainMenu");

		//Leave Steam Lobby
		SteamLobby.Instance.LeaveLobby();

		if (hasAuthority)
		{
			if (isServer)
			{
				manager.StopHost();
			}
			else
			{
				manager.StopClient();
			}
		}
	}
}
