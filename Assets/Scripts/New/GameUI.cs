using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUI : MonoBehaviour
{
    [SerializeField] private GameObject losePanel;
    [SerializeField] private GameObject winPanel;

    public void ShowLosePanel()
    {
        losePanel.SetActive(true);
    }

    public void ShowWinPanel()
    {
        winPanel.SetActive(true);
    }
}
