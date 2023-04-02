using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleLineRenderer : MonoBehaviour
{
    public static CircleLineRenderer _instance;

    void Start()
    {
        _instance= this;
        //DrawCircle(100, 1);
    }

    public void DrawCircle(int steps, float radius, LineRenderer _circleRenderer)
    {
        _circleRenderer.positionCount = steps;

        for(int currentStep = 0; currentStep < steps; currentStep++)
        {
            float circumferenceProgress = (float)currentStep / steps;

            float currentRadian = circumferenceProgress * 2 * Mathf.PI;

            float xScaled = Mathf.Cos(currentRadian);
            float yScaled = Mathf.Sin(currentRadian);

            float x = xScaled * radius;
            float y = yScaled * radius;

            Vector3 currentPosition = new Vector3(x, y, 0);

            _circleRenderer.SetPosition(currentStep, currentPosition);
        }
    }
}
