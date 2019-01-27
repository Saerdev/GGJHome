using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance = null;

    public GameObject darkness;

    [System.Serializable]
    public struct DayItemMap
    {
        public int day;
        public ItemPosMap[] itemsToAdd;
    }

    [System.Serializable]
    public struct ItemPosMap
    {
        public GameObject item;
        public Transform spawnPos;
    }

    public DayItemMap[] dayItemMap;

    public int day = 1;

    public delegate void OnDayChange(bool didCompleteTasks);
    public OnDayChange eOnDayChange;

    bool didLeaveHouse;
    int leaveHouseCount;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        eOnDayChange = null;
        eOnDayChange += SceneItemManagement;
        eOnDayChange += DeactivateDarkness;
    }

    void SceneItemManagement(bool didCompleteTasks)
    {
        if (!didCompleteTasks)
            return;

        for (int i = 0; i < dayItemMap[day - 1].itemsToAdd.Length; i++)
        {
            ItemPosMap newItem = dayItemMap[day - 1].itemsToAdd[i];
            Instantiate(newItem.item, newItem.spawnPos.position, newItem.item.transform.rotation);
        }
    }

    public void ActivateDarkness()
    {
        darkness.gameObject.SetActive(true);
    }

    public void DeactivateDarkness(bool didCompleteTasks)
    {
        darkness.GetComponent<FlockingManager>().Init();
    }

    public void LeaveHouse()
    {
        didLeaveHouse = true;
        leaveHouseCount++;
        eOnDayChange(false);
    }

    private void OnDestroy()
    {
        eOnDayChange -= SceneItemManagement;
        eOnDayChange -= DeactivateDarkness;
    }
}
