using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GameUI : MonoBehaviour
{
    [SerializeField] private GameObject losePanel;
    [SerializeField] private GameObject winPanel;
    [SerializeField] private GameObject crossSign;

    public void ShowLosePanel()
    {
        losePanel.SetActive(true);
        losePanel.transform.localScale = Vector3.one * 0.8f;
        losePanel.transform.DOScale(1, 0.5f);
        crossSign.SetActive(false);
    }

    public void ShowWinPanel()
    {
        winPanel.SetActive(true);
        winPanel.transform.localScale = Vector3.one * 0.8f;
        winPanel.transform.DOScale(1, 0.5f);
        crossSign.SetActive(false);
    }
}
