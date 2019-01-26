using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance = null;

    public int day;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }
}
