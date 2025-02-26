using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Mirror;

public class CameraController : NetworkBehaviour
{
	public GameObject CameraHolder;
	public Vector3 offset;
	
	public override void OnStartAuthority()
	{
		CameraHolder.SetActive(true);
	}
	
	public void Update()
	{
		if(SceneManager.GetActiveScene().name != "Lobby")
		{
			CameraHolder.transform.position = transform.position + offset;
		}
	}
	
}
