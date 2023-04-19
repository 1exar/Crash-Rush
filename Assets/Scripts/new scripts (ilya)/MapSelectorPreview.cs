using UnityEngine;
using DG.Tweening;
using System.Collections;
using System;
using Random = UnityEngine.Random;
using TMPro;

public class MapSelectorPreview : MonoBehaviour
{
    [SerializeField] private MapDatasScriptableObject mapDatasScriptableObject;

    [SerializeField] private TextMeshProUGUI mapPreviewName;
    [SerializeField] private TextMeshProUGUI mapPreviewInfo;

    [SerializeField] private Animation anim;
    [SerializeField] private RectTransform spinner;
    [SerializeField] private RectTransform mapListObject;
    [SerializeField] private GameObject mapPreviewObjectPrefab;

    [SerializeField] private float height;
    [SerializeField] private int amount;
    [SerializeField] private float duration;

    private MapSelector _mapSelector;
    private MapData[] _mapDatas;
    private int _selectedMapIndex;

    private void Start()
    {
        _mapSelector = FindObjectOfType<MapSelector>();
        _mapDatas = mapDatasScriptableObject.MapDatas;
        GenerateMapSelector();
    }

    private void GenerateMapSelector()
    {
        for (int i = 0; i < amount; i++)
        {
            int randomMapIndex = Random.Range(0, _mapDatas.Length);

            MapPreviewObject newMapPreviewObj = Instantiate(mapPreviewObjectPrefab, mapListObject).GetComponent<MapPreviewObject>();
            newMapPreviewObj.SetMapPreviewImage(_mapDatas[randomMapIndex].MapPreviewSprite);

            if (i == amount - 1)
            {
                _selectedMapIndex = randomMapIndex;
            }
        }

        Spin();
    }

    private void Spin()
    {
        float pathLength = (amount - 1) * height;
        float finalCoord = spinner.localPosition.y - pathLength;

        Coroutine mapPreviewSpinCoroutine = StartCoroutine(MapPreviewSpinCoroutine(finalCoord));
        spinner.DOLocalMoveY(finalCoord, duration).SetEase(Ease.OutCubic).OnComplete(() =>
        {
            StopCoroutine(mapPreviewSpinCoroutine);
            mapPreviewSpinCoroutine = null;

            SelectMap();
        });
    }

    private IEnumerator MapPreviewSpinCoroutine(float finalCoord)
    {
        while (true)
        {
            float currentPosition = (finalCoord - spinner.localPosition.y) / height;
            int newClampedPosition = Convert.ToInt32(currentPosition);
            float newPosition = finalCoord - (newClampedPosition * height);

            mapListObject.localPosition = new Vector2(0, newPosition);
            
            yield return new WaitForEndOfFrame();
        }
    }

    private void SelectMap()
    {
        mapListObject.GetChild(amount - 1).SetParent(mapListObject.parent);

        for (int i = 0; i < mapListObject.childCount - 1; i++) //says index is outside of the bounds of the array but idk why
        {
            Destroy(mapListObject.GetChild(i).gameObject);
        }

        mapPreviewName.text = _mapDatas[_selectedMapIndex].MapName;
        mapPreviewInfo.text = _mapDatas[_selectedMapIndex].MapInfo;

        anim.Play();
        _mapSelector.SelectMap(_selectedMapIndex);
    }
}
