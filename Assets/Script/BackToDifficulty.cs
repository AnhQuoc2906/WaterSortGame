using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackToDifficulty : MonoBehaviour
{
    void OnMouseDown()
    {
        SceneManager.LoadScene("DifficultySelect");
    }
}