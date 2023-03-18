using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottleSelect : MonoBehaviour
{
    public GameObject Select;
    void Start()
    {
        Select.SetActive(false);
    }

    private void OnMouseEnter()
    {
        Select.SetActive(true);
    }

    private void OnMouseExit()
    {
        Select.SetActive(false);
    }
}
