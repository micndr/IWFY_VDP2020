using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GraphicsSender : MonoBehaviour
{
    public Slider volume;
    public GlobalState globalState;
    
    // Start is called before the first frame update
    void Start()
    {
        volume = GetComponent<Slider>();
        globalState = FindObjectOfType<GlobalState>();
    }
    
    public void OnValueChanged()
    {
        globalState.OnGraphicsChanged((int)volume.value);    
    }
}
