using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class BottleController : MonoBehaviour
{
    public Color[] bottleColors;
    public SpriteRenderer bottleMaskScaleRotation;
    public float timeToRotate = 1.0f;

    public AnimationCurve ScaleAndRotationMultiplierCurve;
    public AnimationCurve FillAmountCurve;

    public AnimationCurve RotationSpeedMultiplier;

    public float[] fillAmounts, rotationValues;

    private int rotationIndex = 0;

    [Range(0,4)]
    public int numberOfColorsInBottle = 4;

    public Color topColor;
    public int numberOfTopColorLayers=1;

    public BottleController bottleControllerRef;
    public bool justThisBottle = false;
    private int numberOfColorsToTransfer = 0;

    public Transform leftRotationPoint,rightRotationPoint, chosenRotationPoint;

    private float directionMultiplier = 1.0f;

    Vector3 originalPosition, startPosition, endPosition;
    public LineRenderer lineRenderer;

    // Start is called before the first frame update
    void Start()
    {
        bottleMaskScaleRotation.material.SetFloat("_FillAmount", fillAmounts[numberOfColorsInBottle]);

        originalPosition = transform.position;

        UpdateColorsOnShader();
        
        UpdateTopColorValues();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P) && justThisBottle==true)
        {
            UpdateTopColorValues();

            if (bottleControllerRef.FillBottleCheck(topColor))
            {
                ChosenRotationPointAndDirection();

                numberOfColorsToTransfer = Mathf.Min(numberOfTopColorLayers, 4 - bottleControllerRef.numberOfColorsInBottle);

                for(int i = 0; i < numberOfColorsToTransfer; i++)
                {
                    bottleControllerRef.bottleColors[bottleControllerRef.numberOfColorsInBottle + i] = topColor;
                }
                bottleControllerRef.UpdateColorsOnShader();
            }
            CalculateRotationIndex(4 - bottleControllerRef.numberOfColorsInBottle);
            StartCoroutine(RotateBottle());
        }
    }

    public void StartColorTransfer()
    {
        ChosenRotationPointAndDirection();

        numberOfColorsToTransfer = Mathf.Min(numberOfTopColorLayers, 4 - bottleControllerRef.numberOfColorsInBottle);

        for (int i = 0; i < numberOfColorsToTransfer; i++)
        {
            bottleControllerRef.bottleColors[bottleControllerRef.numberOfColorsInBottle + i] = topColor;
        }
        bottleControllerRef.UpdateColorsOnShader();

        CalculateRotationIndex(4 - bottleControllerRef.numberOfColorsInBottle);

        transform.GetComponent<SpriteRenderer>().sortingOrder += 2;
        bottleMaskScaleRotation.sortingOrder += 2;

        StartCoroutine(MoveBottle());
    }

    IEnumerator MoveBottle()
    {
        startPosition = transform.position;
        if(chosenRotationPoint == leftRotationPoint)
        {
            endPosition = bottleControllerRef.rightRotationPoint.position;
        } else
        {
            endPosition = bottleControllerRef.leftRotationPoint.position;
        }

        float t = 0;
        while (t <= 1)
        {
            transform.position = Vector3.Lerp(startPosition, endPosition, t);
            t += Time.deltaTime * 2;

            yield return new WaitForEndOfFrame();
        }

        transform.position = endPosition;

        StartCoroutine(RotateBottle());
    }

    IEnumerator MoveBottleBack()
    {
        startPosition = transform.position;
        endPosition = originalPosition;

        float t = 0;
        while (t <= 1)
        {
            transform.position = Vector3.Lerp(startPosition, endPosition, t);
            t += Time.deltaTime * 2;

            yield return new WaitForEndOfFrame();
        }

        transform.position = endPosition;

        transform.GetComponent<SpriteRenderer>().sortingOrder -= 2;
        bottleMaskScaleRotation.sortingOrder -= 2;
    }

    void UpdateColorsOnShader()
    {
        bottleMaskScaleRotation.material.SetColor("_ColorRedMain", bottleColors[0]);
        bottleMaskScaleRotation.material.SetColor("_ColorYellow", bottleColors[1]);
        bottleMaskScaleRotation.material.SetColor("_ColorGreen", bottleColors[2]);
        bottleMaskScaleRotation.material.SetColor("_ColorPink", bottleColors[3]);
    }

    IEnumerator RotateBottle()
    {
        float t = 0;
        float lerpValue, angleValue;

        float lastAngleValue = 0;

        while(t<timeToRotate)
        {
            lerpValue = t / timeToRotate;
            angleValue = Mathf.Lerp(0.0f, directionMultiplier * rotationValues[rotationIndex], lerpValue);

            //transform.eulerAngles = new Vector3(0,0,angleValue);

            transform.RotateAround(chosenRotationPoint.position, Vector3.forward, lastAngleValue - angleValue);

            bottleMaskScaleRotation.material.SetFloat("_ScaleAndRotationMultiplier",ScaleAndRotationMultiplierCurve.Evaluate(angleValue));

            if (fillAmounts[numberOfColorsInBottle]>FillAmountCurve.Evaluate(angleValue)+0.005f)
            {
                if (lineRenderer.enabled == false)
                {
                    lineRenderer.startColor = topColor;
                    lineRenderer.endColor = topColor;

                    lineRenderer.SetPosition(0, chosenRotationPoint.position);
                    lineRenderer.SetPosition(1, chosenRotationPoint.position - Vector3.up * 1.45f);

                    lineRenderer.enabled = true;
                }
                bottleMaskScaleRotation.material.SetFloat("_FillAmount", FillAmountCurve.Evaluate(angleValue));

                bottleControllerRef.FillUp(FillAmountCurve.Evaluate(lastAngleValue) - FillAmountCurve.Evaluate(angleValue));
            }

            t += Time.deltaTime*RotationSpeedMultiplier.Evaluate(angleValue);
            lastAngleValue = angleValue;
            yield return new WaitForEndOfFrame();
        }
        angleValue = directionMultiplier * rotationValues[rotationIndex];
        //transform.eulerAngles = new Vector3(0, 0, angleValue);
        bottleMaskScaleRotation.material.SetFloat("_ScaleAndRotationMultiplier", ScaleAndRotationMultiplierCurve.Evaluate(angleValue));
        bottleMaskScaleRotation.material.SetFloat("_FillAmount", FillAmountCurve.Evaluate(angleValue));

        numberOfColorsInBottle -= numberOfColorsToTransfer;
        bottleControllerRef.numberOfColorsInBottle += numberOfColorsToTransfer;

        lineRenderer.enabled = false;

        StartCoroutine(RotateBottleBack());
    }

    IEnumerator RotateBottleBack()
    {
        float t = 0;
        float lerpValue, angleValue;

        float lastAngleValue = directionMultiplier * rotationValues[rotationIndex];

        while (t < timeToRotate)
        {
            lerpValue = t / timeToRotate;
            angleValue = Mathf.Lerp(directionMultiplier * rotationValues[rotationIndex], 0.0f, lerpValue);

            //transform.eulerAngles = new Vector3(0, 0, angleValue);

            transform.RotateAround(chosenRotationPoint.position, Vector3.forward, lastAngleValue - angleValue);

            bottleMaskScaleRotation.material.SetFloat("_ScaleAndRotationMultiplier", ScaleAndRotationMultiplierCurve.Evaluate(angleValue));

            lastAngleValue = angleValue;

            t += Time.deltaTime;

            yield return new WaitForEndOfFrame();
        }
        UpdateTopColorValues();
        angleValue = 0.0f;
        transform.eulerAngles = new Vector3(0, 0, angleValue);
        bottleMaskScaleRotation.material.SetFloat("_ScaleAndRotationMultiplier", ScaleAndRotationMultiplierCurve.Evaluate(angleValue));

        StartCoroutine(MoveBottleBack());
    }

    public void UpdateTopColorValues()
    {
        if(numberOfColorsInBottle != 0)
        {
            numberOfTopColorLayers = 1;

            topColor = bottleColors[numberOfColorsInBottle - 1];

            if(numberOfColorsInBottle== 4)
            {
                if (bottleColors[3].Equals(bottleColors[2]))
                {
                    numberOfTopColorLayers = 2;
                    if (bottleColors[2].Equals(bottleColors[1]))
                    {
                        numberOfTopColorLayers = 3;
                        if (bottleColors[1].Equals(bottleColors[0]))
                        {
                            numberOfTopColorLayers = 4;
                        }
                    }
                }
            } else if (numberOfColorsInBottle == 3)
            {
                if (bottleColors[2].Equals(bottleColors[1]))
                {
                    numberOfTopColorLayers = 2;
                    if (bottleColors[1].Equals(bottleColors[0]))
                    {
                        numberOfTopColorLayers = 3;
                    }
                }
            } else if (numberOfColorsInBottle == 2)
            {
                if (bottleColors[1].Equals(bottleColors[0]))
                {
                    numberOfTopColorLayers = 2;
                }
            }

            rotationIndex = 3 - (numberOfColorsInBottle - numberOfTopColorLayers);
        }
    }

    public bool FillBottleCheck(Color colorToCheck)
    {
        if (numberOfColorsInBottle == 0)
        {
            return true;
        } else
        {
            if (numberOfColorsInBottle == 4)
            {
                return false;
            }
            else
            {
                if (topColor.Equals(colorToCheck))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
    }

    private void CalculateRotationIndex(int numberOfEmptySpacesInSecondBottle)
    {
        rotationIndex = 3 - (numberOfColorsInBottle - Mathf.Min(numberOfEmptySpacesInSecondBottle, numberOfTopColorLayers));
    }

    private void FillUp(float fillAmountToAdd)
    {
        bottleMaskScaleRotation.material.SetFloat("_FillAmount", bottleMaskScaleRotation.material.GetFloat("_FillAmount") + fillAmountToAdd);
    }

    private void ChosenRotationPointAndDirection()
    {
        if(transform.position.x > bottleControllerRef.transform.position.x)
        {
            chosenRotationPoint = leftRotationPoint;
            directionMultiplier = -1.0f;
        }
        else
        {
            chosenRotationPoint = rightRotationPoint;
            directionMultiplier = 1.0f;
        }
    }
}
