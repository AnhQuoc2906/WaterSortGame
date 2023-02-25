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
    // Start is called before the first frame update
    void Start()
    {
        UpdateColorsOnShader();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.P))
        {
            StartCoroutine(RotateBottle());
        }
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

        while(t<timeToRotate)
        {
            lerpValue = t / timeToRotate;
            angleValue = Mathf.Lerp(0.0f, 90.0f, lerpValue);

            transform.eulerAngles = new Vector3(0,0,angleValue);
            bottleMaskScaleRotation.material.SetFloat("_ScaleAndRotationMultiplier",ScaleAndRotationMultiplierCurve.Evaluate(angleValue));
            bottleMaskScaleRotation.material.SetFloat("_FillAmount",FillAmountCurve.Evaluate(angleValue));

            t += Time.deltaTime*RotationSpeedMultiplier.Evaluate(angleValue);

            yield return new WaitForEndOfFrame();
        }
        angleValue = 90.0f;
        transform.eulerAngles = new Vector3(0, 0, angleValue);
        bottleMaskScaleRotation.material.SetFloat("_ScaleAndRotationMultiplier", ScaleAndRotationMultiplierCurve.Evaluate(angleValue));
        bottleMaskScaleRotation.material.SetFloat("_FillAmount", FillAmountCurve.Evaluate(angleValue));

        StartCoroutine(RotateBottleBack());
    }

    IEnumerator RotateBottleBack()
    {
        float t = 0;
        float lerpValue, angleValue;

        while (t < timeToRotate)
        {
            lerpValue = t / timeToRotate;
            angleValue = Mathf.Lerp(90.0f, 0.0f, lerpValue);

            transform.eulerAngles = new Vector3(0, 0, angleValue);
            bottleMaskScaleRotation.material.SetFloat("_ScaleAndRotationMultiplier", ScaleAndRotationMultiplierCurve.Evaluate(angleValue));

            t += Time.deltaTime;

            yield return new WaitForEndOfFrame();
        }
        angleValue = 0.0f;
        transform.eulerAngles = new Vector3(0, 0, angleValue);
        bottleMaskScaleRotation.material.SetFloat("_ScaleAndRotationMultiplier", ScaleAndRotationMultiplierCurve.Evaluate(angleValue));
    }
}
