using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UINotification : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _myTurn, _enemyTurn;
    
    public void ShowTurn(bool isMine)
    {
        if (isMine)
        {
            StartCoroutine(ShowText(_myTurn.gameObject));
        }
        else
        {
            StartCoroutine(ShowText(_enemyTurn.gameObject));
        }
    }

    private IEnumerator ShowText(GameObject text)
    {
        text.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        text.SetActive(false);
    }
    
}
