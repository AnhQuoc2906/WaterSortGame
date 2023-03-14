using UnityEngine;
using System.Collections;
using Unity.VisualScripting;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;

public class GameController : MonoBehaviour
{
    public BottleController FirstBottle;
    public BottleController SecondBottle;
    public TransferCanvas transferCanvas;
    private bool isFinished;
    public GameObject canvas1,canvas2,canvas3,canvas4;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);

            if (hit.collider != null)
            {
                if (hit.collider.GetComponent<BottleController>() != null)
                {
                    if (FirstBottle == null)
                    {
                        FirstBottle = hit.collider.GetComponent<BottleController>();
                    }
                    else
                    {
                        if (FirstBottle == hit.collider.GetComponent<BottleController>())
                        {
                            FirstBottle = null;
                        }
                        else
                        {
                            SecondBottle = hit.collider.GetComponent<BottleController>();
                            FirstBottle.bottleControllerRef = SecondBottle;

                            FirstBottle.UpdateTopColorValues();
                            SecondBottle.UpdateTopColorValues();

                            if (SecondBottle.FillBottleCheck(FirstBottle.topColor) == true)
                            {
                                FirstBottle.StartColorTransfer();
                                FirstBottle = null;
                                SecondBottle = null;
                            }
                            else
                            {
                                FirstBottle = null;
                                SecondBottle = null;
                                Congratulation();


                            }

                        }


                    }

                }

            }
          
            
        }
     
    }
    public void Congratulation()
    {
       
            canvas1.SetActive(false);
            canvas2.SetActive(false);
            canvas3.SetActive(false);
            canvas4.SetActive(false);

            transferCanvas.Setup();
        
    }
}


