using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using UnityEngine;

public class LightsOutController : MonoBehaviour
{
    private bool[,] _gameState;
    private bool[] _rowsCompletelyOn;
    private bool[] _rowsCompletelyOff;
    private bool _gameOn;
    private bool _gameOff;
    private bool[] _isRecessed; // true if level is recessed with respect to the outer
    
    private int[] _levelsDepth;

    private void Start()
    {
        _gameState = new bool[,] {
            {false, false, false, false, false},
            {false, false, false, false, false},
            {false, false, false, false, false},
            {false, false, false, false, false},
            {false, false, false, false, false}
        };
        _rowsCompletelyOn = new bool[] { false, false, false, false, false };
        _rowsCompletelyOff = new bool[] { true, true, true, true, true };
        _gameOn = false;
        _gameOff = true;
        _isRecessed = new bool[] { false, false, false, false, false };
        
        _levelsDepth = new int[] {0, 0, 0, 0, 0};
    }

    private void PrintGrid()
    {
        Debug.Log($"{_gameState[0, 0]}, {_gameState[0, 1]}, {_gameState[0, 2]}, {_gameState[0, 3]}, {_gameState[0, 4]}, \n" +
                  $"{_gameState[1, 0]}, {_gameState[1, 1]}, {_gameState[1, 2]}, {_gameState[1, 3]}, {_gameState[1, 4]}, \n" +
                  $"{_gameState[2, 0]}, {_gameState[2, 1]}, {_gameState[2, 2]}, {_gameState[2, 3]}, {_gameState[2, 4]}, \n" +
                  $"{_gameState[3, 0]}, {_gameState[3, 1]}, {_gameState[3, 2]}, {_gameState[3, 3]}, {_gameState[3, 4]}, \n" +
                  $"{_gameState[4, 0]}, {_gameState[4, 1]}, {_gameState[4, 2]}, {_gameState[4, 3]}, {_gameState[4, 4]}");
    }

    private void PrintArray(bool [] a)
    {
        Debug.Log($"{a[0]}" + $"{a[1]}" + $"{a[2]}" + $"{a[3]}" + $"{a[4]}");
    }

    public void UpdateFlower(object[] state)
    {
        int[] pos = (int[])state[0];
        bool isOn = (bool)state[1];
        _gameState[pos[0], pos[1]] = isOn;
        Debug.Log($"Flower ({pos[0]}, {pos[1]}) is now {(isOn?"ON":"OFF")}");
        // PrintGrid();
    }

    public void OnFlowerClick()
    {
        Debug.Log("FLOWER HAS BEEN CLICKED");
        UpdateRestOfState(_rowsCompletelyOn, _rowsCompletelyOff, _gameOn, _gameOff, _isRecessed);
    }

    private void UpdateRestOfState(bool[] oldRowsCompletelyOn, bool[] oldRowsCompletelyOff, bool oldGameOn, bool oldGameOff, bool[] oldIsRecessed)
    {
        _rowsCompletelyOn = UniformRow(true);
        // PrintArray(_rowsCompletelyOn);
        _rowsCompletelyOff = UniformRow(false);
        // PrintArray(_rowsCompletelyOff);
        _gameOn = _rowsCompletelyOn.All(rowOn => rowOn==true);
        // Debug.Log(_gameOn);
        _gameOff = _rowsCompletelyOff.All(rowOff => rowOff==false);
        // Debug.Log(_gameOff);    
        
        for (int i = 0; i < 4; i++) // Level 4 does not move.
        {
            if (!oldIsRecessed[i] && _rowsCompletelyOn[i] && (_rowsCompletelyOn[i] != oldRowsCompletelyOn[i]))
            {
                _isRecessed[i] = true;
                RowDownOneStep(i);
            }
            else if (oldIsRecessed[i] && _rowsCompletelyOff[i] && (_rowsCompletelyOff[i] != oldRowsCompletelyOff[i]))
            {
                _isRecessed[i] = false;
                RowUpOneStep(i);
            } else Debug.Log($"Level {i} unchanged at depth {_levelsDepth[i]}.");
        }

        if (_gameOn && (_gameOn != oldGameOn)) GameOn();
        else if (_gameOff && (_gameOff != oldGameOff)) GameOff();
    }

    private bool[] UniformRow(bool on)
    {
        bool[] uniformRow = new bool[5];
        for (int i=0;i<5;i++)
        {
            uniformRow[i] = All(i, on);
        }

        return uniformRow;
    }

    private bool All(int row, bool on)
    {
        bool all;
        if (on)
        {
            return _gameState[row, 0] &&
                   _gameState[row, 1] &&
                   _gameState[row, 2] &&
                   _gameState[row, 3] &&
                   _gameState[row, 4];
        }
        else
        {
            return !_gameState[row, 0] &&
                   !_gameState[row, 1] &&
                   !_gameState[row, 2] &&
                   !_gameState[row, 3] &&
                   !_gameState[row, 4];       
        }
    }

    private void RowUpOneStep(int row)
    {
        // TODO: Subsitute all the logs for the corresponding animation of the ground.
        if(row>=4) Debug.Log("Grounds stays still");
        if (row >= 3)
        {
            _levelsDepth[3]++;
            Debug.Log($"Level 3 goes up a step. To depth {_levelsDepth[3]}"); // Close bridge plank.
        }

        if (row >= 2)
        {
            _levelsDepth[2]++;
            Debug.Log($"Level 2 goes up a step. To depth {_levelsDepth[2]}"); // Close bridge plank.
        }

        if (row >= 1)
        {
            _levelsDepth[1]++;
            Debug.Log($"Level 1 goes up a step. To depth {_levelsDepth[1]}"); // Close bridge plank.
        }

        if (row >= 0)
        {
            _levelsDepth[0]++;
            Debug.Log($"Level 0 goes up a step. To depth {_levelsDepth[0]}"); // Close bridge plank.
        }
    }

    private void RowDownOneStep(int row)
    {
        // TODO: Subsitute all the logs for the corresponding animation of the ground.
        if(row>=4) Debug.Log("Grounds stays still");
        if (row >= 3)
        {
            _levelsDepth[3]--;
            Debug.Log($"Level 3 goes down a step. To depth {_levelsDepth[3]}");
        }

        if (row >= 2)
        {
            _levelsDepth[2]--;
            Debug.Log($"Level 2 goes down a step. To depth {_levelsDepth[2]}");
        }

        if (row >= 1)
        {
            _levelsDepth[1]--;
            Debug.Log($"Level 1 goes down a step. To depth {_levelsDepth[1]}");
        }

        if (row >= 0)
        {
            _levelsDepth[0]--;
            Debug.Log($"Level 0 goes down a step. To depth {_levelsDepth[0]}");
        }
    }
    private void GameOn()
    {
        // TODO: All the code to execute when the game is fully switched on, now subsituted by console prints
        Debug.Log("The game is fully on."); // Open bridge
    }
    
    private void GameOff()
    {
        // TODO: All the code to execute when the game is fully switched off, now subsituted by console prints
        Debug.Log("The game is fully off.");
    }
}
