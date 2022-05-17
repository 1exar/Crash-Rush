using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Tutorial : MonoBehaviour
{
    [SerializeField]
    private GameObject _fingerImage;
    [SerializeField]
    private GameObject _tutorialTextPanel;
    [SerializeField]
    private TutorialText _tutorialText;

    private int _currentStage = 0;

    private void Start()
    {
        Invoke(nameof(ShowTutorialPanel), 1f);
        Invoke(nameof(ShowFinger), 1f);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (_currentStage == 0)
            {
                Destroy(_fingerImage);
                HideTutorialPanel();
            }
        }

        else if (Input.GetMouseButtonUp(0))
        {
            if (_currentStage == 0)
            {
                _tutorialText.ChangeText("Превосходно! Добей оставшихся противников.");
                Invoke(nameof(ShowTutorialPanel), 5f);
                Invoke(nameof(HideTutorialPanel), 8f);

                _currentStage += 1;
            }
        }
    }

    private void ShowFinger()
    {
        _fingerImage.SetActive(true);
    }

    private void ShowTutorialPanel()
    {
        _tutorialTextPanel.SetActive(true);
        _tutorialTextPanel.transform.localScale = Vector3.one * 0.5f;
        _tutorialTextPanel.transform.DOScale(1, 0.5f).SetEase(Ease.OutBack);
    }

    private void HideTutorialPanel()
    {
        _tutorialTextPanel.SetActive(false);
    }
}
