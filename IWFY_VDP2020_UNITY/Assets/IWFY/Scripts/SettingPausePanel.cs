using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingPausePanel : MonoBehaviour
{
    [SerializeField] private GameObject beforePanel;
    [SerializeField] private GameObject thisPanel;

    public void goBack()
    {
        beforePanel.SetActive(true);
        thisPanel.SetActive(false);
    }
}
