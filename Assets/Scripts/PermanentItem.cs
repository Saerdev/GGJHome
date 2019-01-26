using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PermanentItem : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //click mouse
        ClickObject();
        StickToMouse();
    }
    void ClickObject()
    {
        //you click the mouse
        if (Input.GetMouseButton(0))
        {
            transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);


        }

    }
    private void StickToMouse()
        //so it stays with mouse
    {
        transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }
}
