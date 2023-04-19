using UnityEngine;

public class MapSelector : MonoBehaviour
{
    [SerializeField] private GameObject[] mapObjects;

    public void SelectMap(int mapIndex)
    {
        mapObjects[mapIndex].SetActive(true);
    }
}
