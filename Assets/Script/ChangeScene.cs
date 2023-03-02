using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public void MovetoScene(int SceneId)
    {
        SceneManager.LoadScene(SceneId);
    }
}
