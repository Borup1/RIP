using UnityEngine;
using UnityEngine.UI;
using Steamworks;
using TMPro;

public class Friendlist : MonoBehaviour
{
    public GameObject friendButtonPrefab; // Assign the friend button prefab in the inspector
    public Transform friendsListContainer; // Assign the container (like a ScrollView's content) in the inspector

   void Start()
   {
       if (SteamManager.Initialized)
       {
           PrintFriendsList();
       }
       else
       {
           Debug.LogError("Steamworks is not initialized.");
       }
   }
    
    void PrintFriendsList()
    {
	    // Debug.Log("Attempting to print friends list.");
    
        int friendCount = SteamFriends.GetFriendCount(EFriendFlags.k_EFriendFlagImmediate);
	    // Debug.Log("Number of Friends: " + friendCount);
    
        for (int i = 0; i < friendCount; i++)
        {
            CSteamID friendSteamID = SteamFriends.GetFriendByIndex(i, EFriendFlags.k_EFriendFlagImmediate);
            string friendName = SteamFriends.GetFriendPersonaName(friendSteamID);
	        // Debug.Log("Friend " + (i + 1) + ": " + friendName);
    
            // Instantiate a new button for each friend
            GameObject friendButton = Instantiate(friendButtonPrefab, friendsListContainer);
            friendButton.GetComponentInChildren<TMP_Text>().text = friendName;
    
            // Add a listener to the button to handle inviting the friend
            CSteamID capturedFriendSteamID = friendSteamID; // Capture the current value in the loop
            friendButton.GetComponent<Button>().onClick.AddListener(() => InviteFriend(capturedFriendSteamID));
        }
    }
  
	void InviteFriend(CSteamID friendSteamID)
	{
		Debug.Log("Inviting friend: " + SteamFriends.GetFriendPersonaName(friendSteamID));
		SteamLobby steamLobby = GameObject.Find("NetworkManager").GetComponent<SteamLobby>();
		if (steamLobby != null)
		{
			if (steamLobby.CurrentLobbyID != 0)
			{
				steamLobby.InviteUserToLobby(friendSteamID);
			}
			else
			{
				steamLobby.InviteUserToGame(friendSteamID);
			}
		}
		else
		{
			Debug.LogError("SteamLobby component not found on NetworkManager.");
		}
	}

}