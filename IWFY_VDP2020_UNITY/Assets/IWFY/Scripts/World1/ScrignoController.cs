using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrignoController : MonoBehaviour
{
    // 0->2, 1->3, 2->4
    [SerializeField] private GameObject[] _ghiera;
    private int[] _angle;
    [SerializeField] private Animator[] _animator;

    private void Start()
    {
        _angle = new int[_ghiera.Length];
        _angle[0] = 30;
        _animator[0].SetInteger("Angle", _angle[0]);
        _angle[1] = 60;
        _animator[1].SetInteger("Angle", _angle[1]);
        _angle[2] = 90;
        _animator[2].SetInteger("Angle", _angle[2]);
    }

    public void GhieraClicked(int ghieraNumber)
    {
        Debug.Log("Ghiera Clicked!");
        _angle[ghieraNumber] += 30;
        if (_angle[ghieraNumber] >= 360) _angle[ghieraNumber] -= 360;
        _animator[ghieraNumber].SetInteger("Angle", _angle[ghieraNumber]);
        CheckWin();
    }

    private void CheckWin()
    {
        if (AllZeros()) Debug.Log("GHIERA COMPLETED"); // Send ghiera completed message;
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
}
