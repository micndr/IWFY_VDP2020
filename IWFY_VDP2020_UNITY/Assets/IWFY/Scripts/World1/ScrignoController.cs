using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ScrignoController : MonoBehaviour
{
    // 0->2, 1->3, 2->4
    [SerializeField] private GameObject[] _ghiera;
    [SerializeField] private GameObject _chiave;
    private int[] _angle;
    private bool occupied;
    [SerializeField] private Animator[] _animator;
    private GameObject _audioManager;

    private void Start()
    {
        _angle = new int[_ghiera.Length];
        Initialize();
        occupied = false;
        _audioManager = GameObject.FindGameObjectWithTag("AudioManager");
    }

    private void Initialize()
    {
        StartCoroutine(DelayedGhieraClicked(0, 1));
        StartCoroutine(DelayedGhieraClicked(1, 2));
        StartCoroutine(DelayedGhieraClicked(1, 3));
        StartCoroutine(DelayedGhieraClicked(2, 4));
        StartCoroutine(DelayedGhieraClicked(2, 5));
        StartCoroutine(DelayedGhieraClicked(2, 6));
    }

    public void GhieraClicked(int ghieraNumber)
    {
        if (!occupied)
        {
            occupied = true;
            StartCoroutine(Occupied());
            //Debug.Log("Ghiera Clicked!");
            _audioManager.SendMessage("PlayAmbient", 11);
            _angle[ghieraNumber] += 30;
            if (_angle[ghieraNumber] >= 360) _angle[ghieraNumber] -= 360;
            _animator[ghieraNumber].SetTrigger("GoOn");
            //_animator[ghieraNumber].SetInteger("Angle", _angle[ghieraNumber]);
            CheckWin();
        }
    }
    
    private IEnumerator Occupied()
    {
        yield return new WaitForSeconds(0.25f);
        occupied = false;
    }
    
    private IEnumerator DelayedGhieraClicked(int ghieraNumber, int delay)
    {
        yield return new WaitForSeconds(delay);
        GhieraClicked(ghieraNumber);
    }

    private void CheckWin()
    {
        PrintAll();
        if (AllZeros())
        {
            StartCoroutine(Apertura()); // Send ghiera completed message;
            for (int i = 0; i < _ghiera.Length; i++)
            {
                _ghiera[i].GetComponent<MeshCollider>().enabled = false;
            }

            _chiave.GetComponent<Triggerer>().enabled = true;
            _chiave.GetComponent<ItemPickup>().enabled = true;
            _chiave.GetComponent<IwfyClickableObjectNoPopup>().enabled = true;
        }
    }

    private IEnumerator Apertura()
    {
        yield return new WaitForSeconds(0.25f);
        _audioManager.SendMessage("PlayAmbient", 12);
        _animator[_animator.Length-2].SetBool("Aperto", true);
        _animator[_animator.Length-1].SetBool("Aperto", true);
    }

    private bool AllZeros()
    {
        bool foundNotZero = false;
        for (int i = 0; i < _angle.Length; i++)
        {
            if (_angle[i] != 0) foundNotZero = true;
        }

        return !foundNotZero;
    }

    private void PrintAll()
    {
        for (int i = 0; i < _ghiera.Length; i++)
        {
            Debug.Log("Angolo Ghiera "+i+": "+ _angle[i]);
        }
    }
}
