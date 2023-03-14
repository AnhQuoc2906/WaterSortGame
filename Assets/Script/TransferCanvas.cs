using UnityEngine;
using UnityEngine.UI;
using Unity.VisualScripting;

public class TransferCanvas : MonoBehaviour
{
    public BottleController FirstBottle;
    public BottleController SecondBottle;
    public GameObject bottlePrefab;
    public GameObject bottlePrefab1;

    public GameObject canvas1, canvas2, canvas3, canvas4;


    public void Setup()
    {
        FirstBottle = Instantiate(bottlePrefab, Vector3.zero, Quaternion.identity).GetComponent<BottleController>();

        // Instantiate the second bottle prefab at position (1, 0, 0) with no rotation
        SecondBottle = Instantiate(bottlePrefab1, new Vector3(1, 0, 0), Quaternion.identity).GetComponent<BottleController>();

        if (FirstBottle.numberOfColorsInBottle == FirstBottle.numberOfTopColorLayers&&SecondBottle.numberOfColorsInBottle==SecondBottle.numberOfTopColorLayers)
        {
            gameObject.SetActive(true);
            canvas1.SetActive(false);
            canvas2.SetActive(false);
            canvas3.SetActive(false);
            canvas4.SetActive(false);
            FirstBottle.gameObject.SetActive(false);
            SecondBottle.gameObject.SetActive(false);
        }


    }
    }
