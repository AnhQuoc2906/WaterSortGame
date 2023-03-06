using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitPopup : MonoBehaviour
{
    [SerializeField] GameObject exitPanel;

    public void Cancel()
    {
        exitPanel.SetActive(false);
    }
}
