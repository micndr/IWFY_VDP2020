using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using UnityEngine;

// Handles the dim_4 puzzle behaviour.

public class LightsOutController : MonoBehaviour
{
    public Triggerer triggererOutput;
    [SerializeField] private Animator[] animatorOfLayer; // array of length 4 which contains the 4 animators that animate the 4 moving layers
    
    private bool[,] _gameState; // Grid containing the exact current state of the game (true if flower is ON, false if OFF)
    private bool[] _rowsCompletelyOn; // array of length 5 containing true if corresponding layer has all flowers ON, false otherwise
    private bool[] _rowsCompletelyOff; // array of length 5 containing true if corresponding layer has all flowers OFF, false otherwise
    private bool _gameOn; // All flowers ON
    private bool _gameOff; // All flowers OFF
    private bool _gameOnSimplified; // All flowers on moving layers ON
    private bool _gameOffSimplified; // All flowers on moving layers OFF

    private bool[] _isRecessed; // array of length 5 containing true if level is recessed with respect to the outer one
    private bool[] _isElevated; // array of length 5 containing true if level is elevated with respect to the outer one
    
    // Global modifiers
    private bool _layersGoUp; // If true layers go UP when ON, if false DOWN when ON 
    private bool _simplified; // TODO: Evaluate if this shall be true or false. If true only 4 layers have to be completed in order to win the game.  
    
    private void Start()
    {
        _layersGoUp = false;
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
        _isElevated = new bool[] { false, false, false, false, false };
    }

    // TO BE CALLED AT THE LOAD UP OF THE DIM_4 THE SECOND TIME (IF THE SECOND TIME IS THE ONE WITH LAYERS GOING UP)
    public void SetLayersGoUp(bool newLayersGoUp)
    {
        _layersGoUp = newLayersGoUp;
    }

    public void setSimplified(bool newSimplified)
    {
        _simplified = newSimplified;
    }
    
    // This is the message sent by the single flowers to this controller when it changes state, it notifies the controller to update the model of the game mirroring the actual state of the flower.
    public void UpdateFlower(object[] state)
    {
        int[] pos = (int[])state[0]; // pos = [row, col] of the flower (row is the layer number, col is the flower number within the layer)
        bool isOn = (bool)state[1]; // isOn contains true if the flower is ON, false if OFF
        _gameState[pos[0], pos[1]] = isOn;
        //Debug.Log($"Flower ({pos[0]}, {pos[1]}) is now {(isOn?"ON":"OFF")}");
    }

    // Sent by the actual flower which is being clicked. It updates the game model and triggers the layer movements, if any.
    // NB: the flower which is clicked sends a message to its neighbours to notify them to change state. Then sends OnFlowerClick to this controller
    //     Each individual flower which changes state, the one clicked included, then send the UpdateFlower above message to the controller.
    public void OnFlowerClick()
    {
        UpdateGameState(_rowsCompletelyOn, _rowsCompletelyOff, _gameOn, _gameOff,_gameOnSimplified, _gameOffSimplified,_isRecessed, _isElevated);
    }

    // Where the actual logic happens.
    private void UpdateGameState(bool[] oldRowsCompletelyOn, bool[] oldRowsCompletelyOff, bool oldGameOn, bool oldGameOff, bool oldGameOnSimplified, bool oldGameOffSimplified, bool[] oldIsRecessed, bool[] oldIsElevated)
    {
        _rowsCompletelyOn = UniformRows(true);
        _rowsCompletelyOff = UniformRows(false);

        _gameOnSimplified = _rowsCompletelyOn[0] && _rowsCompletelyOn[1] && _rowsCompletelyOn[2] && _rowsCompletelyOn[3];
        _gameOffSimplified = _rowsCompletelyOff[0] && _rowsCompletelyOff[1] && _rowsCompletelyOff[2] && _rowsCompletelyOff[3];

        _gameOn = _gameOnSimplified && _rowsCompletelyOn[4];
        _gameOff = _gameOffSimplified && _rowsCompletelyOff[4];

        // Loop that updates either isRecessed or isElevated which are the indicators for the animation controllers
        for (int i = 0; i < 4; i++) // Level 4 does not move, so it loops layers 0 to 3.
        {
            // TODO: this code works but might be redundant. Look into it. Is it not necessary just to check each row separately?
            if (_layersGoUp)
            {
                if (!oldIsElevated[i] && _rowsCompletelyOn[i] && (_rowsCompletelyOn[i] != oldRowsCompletelyOn[i]))
                {
                    Debug.Log("Row "+i+" elevated");
                    _isElevated[i] = true;
                }
                else if (oldIsElevated[i] && _rowsCompletelyOff[i] && (_rowsCompletelyOff[i] != oldRowsCompletelyOff[i]))
                {
                    Debug.Log("Row "+i+" no longer elevated");
                    _isElevated[i] = false;
                }
            }
            else
            {
                if (!oldIsRecessed[i] && _rowsCompletelyOn[i] && (_rowsCompletelyOn[i] != oldRowsCompletelyOn[i]))
                {
                    Debug.Log("Row "+i+" recessed");
                    _isRecessed[i] = true;
                }
                else if (oldIsRecessed[i] && _rowsCompletelyOff[i] && (_rowsCompletelyOff[i] != oldRowsCompletelyOff[i]))
                {
                    Debug.Log("Row "+i+" no longer recessed");
                    _isRecessed[i] = false;
                }
            }
        }

        UpdateMaterialPositions();
        
        // They must be different from the old version in order to only send the message once.

        if (_simplified)
        {
            if (_gameOnSimplified && (_gameOnSimplified != oldGameOnSimplified)) GameOnSimplified();
            else if (_gameOffSimplified && (_gameOffSimplified != oldGameOffSimplified)) GameOffSimplified();
        }
        else
        {
            if (_gameOn && (_gameOn != oldGameOn)) GameOn();
            else if (_gameOff && (_gameOff != oldGameOff)) GameOff();
        }
    }

    // Checks which rows are uniformly either ON or OFF depending on the value of the passed argument 'on'
    private bool[] UniformRows(bool on)
    {
        bool[] uniformRow = new bool[5];
        for (int i=0;i<5;i++)
        {
            uniformRow[i] = All(i, on);
        }

        return uniformRow;
    }

    // Checks if a given 'row' is all either ON or OFF depending on the value of the passed argument 'on'
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

    // Scans the indicators isRecessed and isElevated in order to update the animators
    // TODO: see if this code can be semplified only using one indicator _isMoved
    private void UpdateMaterialPositions()
    {
        for (int i = 0; i < 4; i++)
        {
            if (_isRecessed[i]) animatorOfLayer[i].SetBool("isDown", true);
            else animatorOfLayer[i].SetBool("isDown", false);
            if (_isElevated[i]) animatorOfLayer[i].SetBool("isUp", true);
            else animatorOfLayer[i].SetBool("isUp", false);
        }
    }
    
    // Triggered when the game becomes fully on.
    private void GameOn()
    {
        // TODO: All the code to execute when the game is fully switched on, now subsituted by console prints
        triggererOutput.Trigger();
        Debug.Log("The game is fully on.");
    }
    
    // Triggered when the game becomes fully off
    private void GameOff()
    {
        // TODO: All the code to execute when the game is fully switched off, now subsituted by console prints
        Debug.Log("The game is fully off.");
    }
    
    private void GameOnSimplified()
    {
        // TODO: All the code to execute when the game is fully switched on, now subsituted by console prints
        triggererOutput.Trigger();
        Debug.Log("The Simplified game is fully on.");
    }
    
    private void GameOffSimplified()
    {
        // TODO: All the code to execute when the game is fully switched off, now subsituted by console prints
        Debug.Log("The Simplified game is fully off.");
    }
    
    // DEBUG FUNCTIONS - in order to print to console the state of the game and inner values
    private void PrintGrid()
    {
        Debug.Log($"{(_gameState[0, 0] ? 1 : 0)}, {(_gameState[0, 1] ? 1 : 0)}, {(_gameState[0, 2] ? 1 : 0)}, {(_gameState[0, 3] ? 1 : 0)}, {(_gameState[0, 4] ? 1 : 0)}, \n" +
                  $"{(_gameState[1, 0] ? 1 : 0)}, {(_gameState[1, 1] ? 1 : 0)}, {(_gameState[1, 2] ? 1 : 0)}, {(_gameState[1, 3] ? 1 : 0)}, {(_gameState[1, 4] ? 1 : 0)}, \n" +
                  $"{(_gameState[2, 0] ? 1 : 0)}, {(_gameState[2, 1] ? 1 : 0)}, {(_gameState[2, 2] ? 1 : 0)}, {(_gameState[2, 3] ? 1 : 0)}, {(_gameState[2, 4] ? 1 : 0)}, \n" +
                  $"{(_gameState[3, 0] ? 1 : 0)}, {(_gameState[3, 1] ? 1 : 0)}, {(_gameState[3, 2] ? 1 : 0)}, {(_gameState[3, 3] ? 1 : 0)}, {(_gameState[3, 4] ? 1 : 0)}, \n" +
                  $"{(_gameState[4, 0] ? 1 : 0)}, {(_gameState[4, 1] ? 1 : 0)}, {(_gameState[4, 2] ? 1 : 0)}, {(_gameState[4, 3] ? 1 : 0)}, {(_gameState[4, 4] ? 1 : 0)}");
    }

    private void PrintArray(bool [] a)
    {
        Debug.Log($"{a[0]}" + $"{a[1]}" + $"{a[2]}" + $"{a[3]}" + $"{a[4]}");
    }

}
