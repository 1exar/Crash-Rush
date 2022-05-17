using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class TutorialText : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _text;

    public void ChangeText(string newText)
    {
        _text.text = newText;
    }
}
