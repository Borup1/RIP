using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.SceneManagement;

public class PlayerMovementController : NetworkBehaviour
{
	public float Speed = 0.1f;
	public float MouseSensitivity = 2.0f;
	public GameObject PlayerModel;
	
	private void Start()
	{
		PlayerModel.SetActive(false);
		
	}
	
	private void Update()
	{
		if(SceneManager.GetActiveScene().name != "Lobby")
		{
			if(PlayerModel.activeSelf == false)
			{
				SetPositiion();
				PlayerModel.SetActive(true);
			}
			
			if (hasAuthority)
			{
				Movement();
				MouseLook();
			}						
		}
	}
	
	
	public void Movement()
	{
		float xDirection = Input.GetAxis("Horizontal");
		float zDirection = Input.GetAxis("Vertical");
		
		Vector3 MoveDirection = new Vector3(xDirection, 0.0f, zDirection);
		
		transform.position += MoveDirection * Speed;
	}
	
	public void SetPositiion()
	{
        transform.position = new Vector3(Random.Range(-5,5), (float)0.8, Random.Range(-15,7));
	}
	
	public void MouseLook()
	{
		float mouseX = Input.GetAxis("Mouse X") * MouseSensitivity;
		float mouseY = Input.GetAxis("Mouse Y") * MouseSensitivity;

		transform.Rotate(Vector3.up * mouseX);
		// Optionally, you can limit the vertical rotation to prevent flipping
		// transform.Rotate(Vector3.left * mouseY);
	}
}

