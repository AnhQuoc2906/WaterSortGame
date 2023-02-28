using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HardButton : MonoBehaviour
{
    void OnMouseDown()
    {
        SceneManager.LoadScene("LevelHard");
    }
}