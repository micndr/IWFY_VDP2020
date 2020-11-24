using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerController : IwfyClickableObjectNoPopup
{
    // Position of the current flower: [row, column]
    [SerializeField] private int[] _pos;
    [SerializeField] private Light light;

    // UP -> Towards the well
    // DOWN -> Away from the well
    // RIGHT -> On the right looking at the well
    // LEFT -> On the left looking at the well
    [SerializeField] private GameObject _flowerRight;
    [SerializeField] private GameObject _flowerLeft;
    [SerializeField] private GameObject _flowerUp;
    [SerializeField] private GameObject _flowerDown;
    
    private bool isOn = false;

    private GameObject _controllerFlower;
    public override void Start()
    {
        base.Start();
        _controllerFlower = GameObject.FindGameObjectWithTag("LightsOutController"); 
    }

    // Update is called once per frame
    void OnPointerClick()
    {
        Debug.Log($"Flower ({_pos[0]}, {_pos[1]}) clicked.");

        // Change color.
        SwitchColor();
        
        // Notify neighbours to change color.
        if(_flowerDown) _flowerDown?.SendMessage("SwitchColor");
        if(_flowerUp) _flowerUp?.SendMessage("SwitchColor");
        if(_flowerRight) _flowerRight?.SendMessage("SwitchColor");
        if(_flowerLeft) _flowerLeft?.SendMessage("SwitchColor");
        
        // Notify the controller to update the state
        _controllerFlower?.SendMessage("OnFlowerClick");
    }

    public void SwitchColor()
    {
        if (isOn)
        {
            isOn = false;
            //gameObject.GetComponent<Renderer>().material.color = Color.blue;
            light.intensity = 0;
        }
        else
        {
            isOn = true;
            //gameObject.GetComponent<Renderer>().material.color = Color.cyan;
            light.intensity = 1;
        }

        // Update LightsOutController
        object[] state = {_pos, isOn};
        _controllerFlower?.SendMessage("UpdateFlower", state);
    }
}
