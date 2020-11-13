using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class mouse_enter : MonoBehaviour
{

	//Create the object whose material will be modified
	public GameObject myObject;

	//Create the boolean variable (true/false)
	//Through this boolean variable we can use a single button to give two different commands (in our case the On / Off)
	//Initialize the value to true.

	void Start()
	{
		
	}

	//This function allows you to perform two different tasks depending on the current Boolean variable.
	//This function is activated after clicking the object on which it was attached this script.
	void OnMouseEnter()
	{
		myObject.SetActive(true);
	}

	void OnMouseExit()
    {
		myObject.SetActive(false);
    }

}