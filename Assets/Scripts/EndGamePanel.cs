using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EndGamePanel : MonoBehaviour
{
    [SerializeField]
    private GameObject _winPanel;
    [SerializeField]
    private GameObject _failPanel;

    public void ShowWinPanel()
    {
        _winPanel.SetActive(true);
        _winPanel.transform.localScale = Vector3.one * 0.7f;
        _winPanel.transform.DOScale(1, 0.2f);
    }

    public void ShowFailPanel()
    {
        _failPanel.SetActive(true);
        _failPanel.transform.localScale = Vector3.one * 0.7f;
        _failPanel.transform.DOScale(1, 0.2f);
    }
}
