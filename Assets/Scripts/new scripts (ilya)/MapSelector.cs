using UnityEngine;

public class MapSelector : MonoBehaviour
{
    [SerializeField] private GameObject[] mapObjects;
    [SerializeField] private Material[] mapMaterials;
    [SerializeField] private MeshRenderer mapMeshRenderer;

    public void SelectMap(int mapIndex)
    {
        mapObjects[mapIndex].SetActive(true);
        mapMeshRenderer.material = mapMaterials[mapIndex];
    }
}
