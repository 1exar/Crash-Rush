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

    private void Start()
    {
        StartCoroutine(ShowTutorialPanel(1f));
        StartCoroutine(ShowFinger(1f));
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Destroy(_fingerImage);
            StartCoroutine(HideTutorialPanel(0f));
        }

        else if (Input.GetMouseButtonUp(0))
        {
            _tutorialText.ChangeText("Превосходно! Добей оставшихся противников.");
            StartCoroutine(ShowTutorialPanel(5f));
            StartCoroutine(HideTutorialPanel(8f));

            this.enabled = false;
        }
    }

    private IEnumerator HideTutorialPanel(float delay)
    {
        yield return new WaitForSeconds(delay);
        _tutorialTextPanel.SetActive(false);
    }

    private IEnumerator ShowTutorialPanel(float delay)
    {
        yield return new WaitForSeconds(delay);

        _tutorialTextPanel.SetActive(true);
        _tutorialTextPanel.transform.localScale = Vector3.one * 0.5f;
        _tutorialTextPanel.transform.DOScale(1, 0.5f).SetEase(Ease.OutBack);
    }

    private IEnumerator ShowFinger(float delay)
    {
        yield return new WaitForSeconds(delay);
        _fingerImage.SetActive(true);
    }
}
