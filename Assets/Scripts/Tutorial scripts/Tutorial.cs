using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Tutorial : MonoBehaviour
{
    [SerializeField]
    private GameObject _fingerImage;
    [SerializeField]
    private Transform _tutorialTextPanel;
    [SerializeField]
    private TutorialText _tutorialText;

    private int _currentStage = 0;

    private void Start()
    {
        Invoke(nameof(ShowTutorialPanel), 1f);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (_currentStage == 0)
            {
                _fingerImage.SetActive(false);
                HideTutorialPanel();
            }
        }

        else if (Input.GetMouseButtonUp(0))
        {
            if (_currentStage == 0)
            {
                _tutorialText.ChangeText("Excelent work!");
                Invoke(nameof(ShowTutorialPanel), 5f);
                Invoke(nameof(HideTutorialPanel), 8f);
            }
        }
    }

    private void ShowTutorialPanel()
    {
        _tutorialTextPanel.gameObject.SetActive(true);
        _tutorialTextPanel.localScale = Vector3.one * 0.5f;
        _tutorialTextPanel.DOScale(1, 0.5f).SetEase(Ease.OutBack);
    }

    private void HideTutorialPanel()
    {
        _tutorialTextPanel.gameObject.SetActive(false);
    }
}
