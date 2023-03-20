using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WinScene4Bottles : MonoBehaviour
{
    public BottleController Bottle1, Bottle2, Bottle3, Bottle4;
    public GameObject winScene;
    public GameObject backButton;
    BottleController[] bottles = new BottleController[4];
    float currentTime;
    public TextMeshProUGUI currentTimeText;
    bool check = false;

    private GameController gameController;

    // Start is called before the first frame update
    void Start()
    {
        winScene.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        bottles[0] = Bottle1;
        bottles[1] = Bottle2;
        bottles[2] = Bottle3;
        bottles[3] = Bottle4;
        foreach (var bottle in bottles)
        {
            if(bottle.numberOfColorsInBottle== 0)
            {
                check = true;
            }
            else if (bottle.numberOfColorsInBottle == bottle.numberOfTopColorLayers)
            {
                check = true;
            }
            else
            {
                check = false;
                break;
            }
        }
        if (check == true)
        {
            winScene.SetActive(true);
            backButton.SetActive(false);
        }
    }
}
