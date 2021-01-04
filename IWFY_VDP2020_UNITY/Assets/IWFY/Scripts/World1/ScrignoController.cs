using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

// This handles the opening of the scrignos around the world(s)
public class ScrignoController : MonoBehaviour
{
    // 0->2, 1->3, 2->4... is the mapping of the indexes of the script to those of the model
    [SerializeField] private GameObject[] _ghiera;
    [SerializeField] private GameObject _chiave;
    [SerializeField] private Animator[] _animator;
    
    private int[] _angle;
    private bool _occupied;
    private GameObject _audioManager;

    // At the start all the private variables are initialized, and the game's initial state is created
    private void Start()
    {
        _angle = new int[_ghiera.Length];
        _occupied = false;
        _audioManager = GameObject.FindGameObjectWithTag("AudioManager");
        Initialize();
    }

    // This handles the initial position of the ghiere. Each ghiera of index i is moved i times.
    // NB: Ghiere are numbered outwards starting from 0
    private void Initialize()
    {
        int seconds = 0;
        for (int i = 0; i < _ghiera.Length; i++)
        {
            for (int j = 0; j < i; j++)
            {
                seconds++;
                StartCoroutine(DelayedGhieraClicked(i, seconds));
            }
        }
    }

    // Event called when a ghiera is clicked
    public void GhieraClicked(int ghieraNumber)
    {
        if (!_occupied)
        {
            // Input to the ghiera is only accepted once every 0.25 seconds, which is the time of the animation.
            _occupied = true;
            StartCoroutine(Occupied());
            
            // Play ghiera sound
            _audioManager.SendMessage("PlayAmbient", 11);
            
            // Manipulate the internal model
            _angle[ghieraNumber] += 30;
            if (_angle[ghieraNumber] >= 360) _angle[ghieraNumber] -= 360;
            
            // Notify the animator to animate one step of the ghiera
            _animator[ghieraNumber].SetTrigger("GoOn");
            
            // Check if all ghiere are aligned (if the scrigno must be opened)
            CheckWin();
        }
    }
    
    // Used to only accept one input every 0.25 seconds
    private IEnumerator Occupied()
    {
        yield return new WaitForSeconds(0.25f);
        _occupied = false;
    }
    
    // Used to advance ghiera position from script.
    private IEnumerator DelayedGhieraClicked(int ghieraNumber, int delay)
    {
        yield return new WaitForSeconds(delay);
        GhieraClicked(ghieraNumber);
    }

    // Checks if scrigno has to be opened
    private void CheckWin()
    {
        // Debug, prints model.
        PrintAll();
        
        // Scrigno is opened when all the angles of rotation are 0
        if (AllZeros())
        {
            // Coroutine that animates the opening of coperchio and chiave
            StartCoroutine(Apertura()); 
            
            // Disable interactivity of ghiere
            for (int i = 0; i < _ghiera.Length; i++)
            {
                _ghiera[i].GetComponent<MeshCollider>().enabled = false;
            }

            // Enable interactivity of chiave
            _chiave.GetComponent<Triggerer>().enabled = true;
            _chiave.GetComponent<ItemPickup>().enabled = true;
            _chiave.GetComponent<IwfyClickableObjectNoPopup>().enabled = true;
        }
    }

    // Opens coperchio and moves chiave
    private IEnumerator Apertura()
    {
        yield return new WaitForSeconds(0.25f);
        _audioManager.SendMessage("PlayAmbient", 12);
        _animator[_animator.Length-2].SetBool("Aperto", true);
        _animator[_animator.Length-1].SetBool("Aperto", true);
    }

    // True if all rotation angles are zero.
    private bool AllZeros()
    {
        bool foundNotZero = false;
        for (int i = 0; i < _angle.Length; i++)
        {
            if (_angle[i] != 0) foundNotZero = true;
        }

        return !foundNotZero;
    }

    // Prints the model to console.
    private void PrintAll()
    {
        for (int i = 0; i < _ghiera.Length; i++)
        {
            Debug.Log("Angolo Ghiera "+i+": "+ _angle[i]);
        }
    }
}
