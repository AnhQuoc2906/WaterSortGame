using UnityEngine;
using UnityEngine.UI;

public class TransferCanvas : MonoBehaviour
{
    public BottleController bottleController;
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
        if (bottleController.numberOfColorsToTransfer == bottleController.numberOfColorsInBottle)
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
