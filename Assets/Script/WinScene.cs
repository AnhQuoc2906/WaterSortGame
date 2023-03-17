using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WinScene : MonoBehaviour
{
    public BottleController Bottle1;
    public BottleController Bottle2;
    [SerializeField]
    public GameObject winScene;
    BottleController[] bottles = new BottleController[2];
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
        foreach (var bottle in bottles)
        {
            if (bottle.numberOfColorsInBottle == bottle.numberOfTopColorLayers)
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
        }
    }
}
