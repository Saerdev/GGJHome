using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlickeringLight : MonoBehaviour
{
    Light TestLight;
    //public float min;
    //public float max;
    // Start is called before the first frame update
    void Start()
    {
        TestLight = GetComponent<Light>();
        StartCoroutine(Flashing());
    }

    IEnumerator Flashing()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(0,2));
            TestLight.enabled = !TestLight.enabled;
            //System.Threading.Thread.Sleep(5000);

        }

    }


}
