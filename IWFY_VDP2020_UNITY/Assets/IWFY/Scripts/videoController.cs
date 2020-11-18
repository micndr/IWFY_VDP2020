using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class videoController : MonoBehaviour
{

    [SerializeField] private GameObject _videoPlayer;
    [SerializeField] private int _timeToStop;
    
    // Start is called before the first frame update
    void Start()
    {
        _videoPlayer.SetActive(false);
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("prova"))
        {
            _videoPlayer.SetActive(true);
            Destroy(_videoPlayer, _timeToStop);
        }
    }
}
