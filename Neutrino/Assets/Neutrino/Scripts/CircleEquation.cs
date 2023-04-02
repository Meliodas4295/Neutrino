using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleEquation : MonoBehaviour
{
    public static CircleEquation _instance;
    // Start is called before the first frame update
    void Start()
    {
        _instance= this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private bool OnCircle(float x, float y, float radius)
    {
        if(Mathf.Pow(x,2) + Mathf.Pow(y,2) == Mathf.Pow(radius, 2))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private int CircleAngle(int numberOfSection)
    {
        return 360 / numberOfSection;
    }


}
