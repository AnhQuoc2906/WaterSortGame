using UnityEngine;
using UnityEngine.UI;

public class TransferCanvas : MonoBehaviour
{
    public BottleController bottleController1;
    public BottleController bottleController2;

    public GameObject canvas;

    // Start is called before the first frame update
    void Start()
    {
        // Hide the canvas at the beginning
        canvas.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // Check if numberOfColorsToTransfer is zero
        if (bottleController1.numberOfColorsInBottle == 4 && bottleController2.numberOfColorsInBottle==2)
        {
            // Show the canvas if it is hidden
            if (!canvas.activeSelf)
            {
                canvas.SetActive(true);
            }
        }
        else
        {
            // Hide the canvas if it is shown
            if (canvas.activeSelf)
            {
                canvas.SetActive(false);
            }
        }
    }
}
