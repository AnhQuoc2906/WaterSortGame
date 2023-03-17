using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GoBack : MonoBehaviour
{
    [SerializeField] GameObject exitPanel;

    private void Start()
    {
        exitPanel.SetActive(false);
    }
    public void ShowPanel () 
    {
        if(exitPanel.activeSelf) {
            exitPanel.SetActive(false);
        } else exitPanel.SetActive (true);
    }
}