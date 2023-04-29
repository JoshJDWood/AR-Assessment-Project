using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AnswerButton : MonoBehaviour
{
    [SerializeField] GameManager gameManager;
    private TMP_Text buttonText;
    private int currentInt;

    private void Awake()
    {
        buttonText = GetComponentInChildren<TMP_Text>();
    }

    public void SetNewInt(int i)
    {
        currentInt = i;
        buttonText.text = "" + i;
    }

    public void CheckMyAnswer()
    {
        gameManager.CheckAnswer(currentInt);
    }
}
