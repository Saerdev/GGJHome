using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Darkness"))
        {
            GameManager.Instance.day++;
            GameManager.Instance.eOnDayChange(true);
        }

        if (other.CompareTag("FrontDoor"))
        {
            GameManager.Instance.day++;
            GameManager.Instance.eOnDayChange(false);
        }
    }
}
