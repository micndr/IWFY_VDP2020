using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerController : IwfyClickableObjectNoPopup
{
    // Position of the current flower: [row, column]
    [SerializeField] private int[] _pos;
    [SerializeField] private Animator _animator;
    [SerializeField] private AudioSource _audio;

    // UP -> Towards the well
    // DOWN -> Away from the well
    // RIGHT -> On the right looking at the well
    // LEFT -> On the left looking at the well
    [SerializeField] private GameObject _flowerRight;
    [SerializeField] private GameObject _flowerLeft;
    [SerializeField] private GameObject _flowerUp;
    [SerializeField] private GameObject _flowerDown;

    [SerializeField] private Material matOn;
    [SerializeField] private Material matOff;

    private bool isOn = false;
    public float amplitude = 0.001f;
    public float speed = 1f;

    private GameObject _controllerFlower;
    private Renderer graphicRenderer0;
    private Renderer graphicRenderer1;
    public GameObject fxFlashPrefab;
    public override void Start()
    {
        base.Start();
        _controllerFlower = GameObject.FindGameObjectWithTag("LightsOutController");
        graphicRenderer0 = transform.Find("pPipe19").GetComponent<Renderer>();
        graphicRenderer1 = transform.Find("pCylinder3").GetComponent<Renderer>();
    }

    // Update is called once per frame
    void FixedUpdate() {
        
        //if (isOn == false) transform.position += transform.position.normalized * Mathf.Sin(Time.time * speed) * amplitude;
        
    }
    
    public override void OnPointerClick()
    {
        Debug.Log($"Flower ({_pos[0]}, {_pos[1]}) clicked.");

        // Change color.
        SwitchColor();
        
        // Notify neighbours to change color.
        if(_flowerDown) _flowerDown?.SendMessage("SwitchColor");
        if(_flowerUp) _flowerUp?.SendMessage("SwitchColor");
        if(_flowerRight) _flowerRight?.SendMessage("SwitchColor");
        if(_flowerLeft) _flowerLeft?.SendMessage("SwitchColor");

        // Light effect.
        if (_flowerDown) FxFlash(_flowerDown.transform);
        if (_flowerUp) FxFlash(_flowerUp.transform);
        if (_flowerRight) FxFlash(_flowerRight.transform);
        if (_flowerLeft) FxFlash(_flowerLeft.transform);

        // Notify the controller to update the state
        _controllerFlower?.SendMessage("OnFlowerClick");
    }

    public void SwitchColor()
    {
        if (isOn)
        {
            isOn = false;
            graphicRenderer0.material = matOff;
            graphicRenderer1.material = matOff;
        }
        else
        {
            isOn = true;
            graphicRenderer0.material = matOn;
            graphicRenderer1.material = matOn;
        }
        
        // Update the animator
        if(_animator) _animator?.SetBool("isOn", isOn);
        
        // Play sound
        if (isOn) if (_audio) _audio.Play();

        // Update LightsOutController
        object[] state = {_pos, isOn};
        _controllerFlower?.SendMessage("UpdateFlower", state);
    }

    public void FxFlash (Transform target) {
        GameObject fxline = Instantiate(fxFlashPrefab, transform.position, transform.rotation);
        Destroy(fxline, 0.25f);
        var line = fxFlashPrefab.GetComponent<LineRenderer>();
        Vector3[] poss = new Vector3[2];
        poss[0] = transform.position;
        //arc commented out
        //Vector3 midpoint = (transform.position + target.position) / 2f;
        //poss[1] = midpoint + midpoint.normalized;
        poss[1] = target.position;
        line.SetPositions(poss);
        line.positionCount = 2;
    }
}
