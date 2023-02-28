using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EasyButton : MonoBehaviour
{
    void OnMouseDown()
    {
        SceneManager.LoadScene("LevelEasy");
    }
}