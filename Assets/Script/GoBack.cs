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
        exitPanel.SetActive (true);
    }
}