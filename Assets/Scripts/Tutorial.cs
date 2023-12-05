using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    public GameObject[] objects;
    int selection = 0;
    public GameObject next;
    public void Start()
    {
        foreach(GameObject obj in objects)
        {
            obj.SetActive(false);
        }

        objects[0].SetActive(true);
    }

    public void Next()
    {
        objects[selection].SetActive(false);
        selection += 1;
        if(selection >= objects.Length)
        {
            //Run First Day
            GameManager.instance.startFirstDay();
            //Hide the Next Button
            next.SetActive(false);
            //Hide pause
            this.gameObject.SetActive(false);
            return;
        }
        objects[selection].SetActive(true);
    }
}
