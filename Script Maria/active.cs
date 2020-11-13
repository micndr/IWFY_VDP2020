using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class active : MonoBehaviour
{

	//Create the object whose material will be modified
	//public GameObject myObject;
	public GameObject[] myObjects;

	//Create the boolean variable (true/false)
	//Through this boolean variable we can use a single button to give two different commands (in our case the On / Off)
	//Initialize the value to true.
	private bool active_ = true;

	void Start()
	{
		Debug.Log("avviato");
	}

	//This function allows you to perform two different tasks depending on the current Boolean variable.
	//This function is activated after clicking the object on which it was attached this script.
	void OnMouseDown()
	{
		Debug.Log("ciao");

		//If the boolean variable is true (it is always true at the beginning), the object myObject (SpotLight) is activated 
		//and returns false to the boolean variable.
		//Note the myObject should be initially disabled.
		if (active_ == true)

		{
			foreach (GameObject i in myObjects)
			{
				i.SetActive(true);
			}

			active_ = false;
			return;
		}

		//If the boolean variable is false (for example after the first click), the object myObject (SpotLight) is deactivated 
		//and returns true to the boolean variable.
		if (active_ == false)

		{
			foreach (GameObject i in myObjects)
			{
				i.SetActive(false);
			}

			active_ = true;
			return;
		}
		return;
	}

}
