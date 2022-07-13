// This script moves your player automatically in the direction he is looking at. You can 
// activate the autowalk function by pull the cardboard trigger, by define a threshold angle 
// or combine both by selecting both of these options.
// The threshold is an value in degree between 0� and 90�. So for example the threshold is 
// 30�, the player will move when he is looking 31� down to the bottom and he will not move 
// when the player is looking 29� down to the bottom. This script can easally be configured
// in the Unity Inspector.Attach this Script to your CardboardMain-GameObject. If you 
// haven't the Cardboard Unity SDK, download it from https://developers.google.com/cardboard/unity/download

using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour 
{
    new Camera camera; 
	
	//This is the variable for the player speed
	[Tooltip("With this speed the player will move.")]
	public float speed;
	PlayerMovement control;
	float strafeForward;
	float strafeRight;

	void Awake(){
		control = new PlayerMovement();
		control.Gameplay.StrafeForward.performed += context => strafeForward = context.ReadValue<float>(); 
		control.Gameplay.StrafeRight.performed += context => strafeRight = context.ReadValue<float>(); 
		control.Gameplay.StrafeForward.canceled += context => strafeForward = 0f; 
		control.Gameplay.StrafeRight.canceled += context => strafeRight = 0f; 
	}

	void OnEnable(){
		Debug.Log("I'm enabled");
		control.Gameplay.Enable();
	}
	void OnDisable(){
		control.Gameplay.Disable();
	}

	void Start () 
	{
		camera = GetComponentInChildren<Camera>();
	}
	
	void Update () 
	{
		transform.position += new Vector3(camera.transform.forward.x, 0 , camera.transform.forward.z).normalized * strafeForward * speed * Time.deltaTime;
		transform.position += new Vector3(camera.transform.right.x, 0 , camera.transform.right.z).normalized * strafeRight * speed * Time.deltaTime;
	}
}