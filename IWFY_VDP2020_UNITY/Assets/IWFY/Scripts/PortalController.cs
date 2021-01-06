using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalController : MonoBehaviour
{
    public GameObject Eia;
    
    // [SerializeField] private float _xcoord;
    // [SerializeField] private float _ycoord;
    // [SerializeField] private float _zcoord;
    
    
    [SerializeField] public string _newscene;

    public void TriggerMsg() {
        SceneManager.LoadScene(_newscene);
    }

    public void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Player")) {
            AudioManager am = FindObjectOfType<AudioManager>();
            am.StopOstForSceneChange();
            StartCoroutine(teleport());
            MoveFlat move = col.GetComponent<MoveFlat>();
            move.lockUserInput = true;
        }
    }

    IEnumerator teleport()
    {
        yield return new WaitForSeconds(0.25f);
        SceneManager.LoadScene(_newscene);
    }
}
