using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvestNode : MonoBehaviour
{
    //X Should never change, however X WILL, so we should plan for such
    public float maxY = 0;
    public float minY = 0;
    public void UpdateY(float value, float max, float min)
    {
        if (value == float.NaN) { value = 0; }
        //Find where the value falls on a scale from Max to Min
        float temp = (value - min) / (max - min) * (maxY - minY) + minY;
        gameObject.transform.position = new Vector3(transform.position.x, temp, transform.position.z);
    }
}
