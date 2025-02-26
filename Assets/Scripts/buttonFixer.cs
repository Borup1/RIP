using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buttonFixer : MonoBehaviour
{
	void Start()
	{
		GetComponent<UnityEngine.UI.Button>().onClick.AddListener(GameObject.Find("NetworkManager").GetComponent<SteamLobby>().HostLobby);
	}
}
