using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Timer : MonoBehaviour
{
    [SerializeField] GameObject exitPanel;
    [SerializeField] GameObject congratulationPanel;
    [SerializeField] GameObject restartPanel;
    float currentTime;
    public TextMeshProUGUI currentTimeText;
    // Start is called before the first frame update
    void Start()
    {
        currentTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (!exitPanel.activeSelf && !congratulationPanel.activeSelf && !restartPanel.activeSelf) 
        {
            currentTime += Time.deltaTime;
        } 
        TimeSpan time = TimeSpan.FromSeconds(currentTime);
        currentTimeText.text = time.ToString(@"mm\:ss");
    }
}
