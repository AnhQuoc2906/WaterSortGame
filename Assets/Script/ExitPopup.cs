using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitPopup : MonoBehaviour
{
    [SerializeField] GameObject exitPanel;

    // Update is called once per frame
    public void BackToDifficulty()
    {
        SceneManager.LoadScene("DifficultySelect");
    }

    public void Cancel()
    {
        exitPanel.SetActive(false);
    }
}
