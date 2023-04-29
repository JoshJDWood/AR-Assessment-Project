using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private TMP_Text checkAnswerResponse;
    [SerializeField] private GameObject playButton;
    [SerializeField] private GameObject gameUI;
    private Coroutine fadeText;

    public void PlayButton()
    {
        playButton.SetActive(false);
        gameUI.SetActive(true);
        gameManager.ResumeGame();
    }

    public void CorrectAnswerResponse()
    {
        checkAnswerResponse.text = "Well Done!";
        checkAnswerResponse.color = Color.green;
        fadeText = StartCoroutine(FadeResponse(1.5f, Color.green));
    }

    public void IncorrectAnswerResponse()
    {
        checkAnswerResponse.text = "Try Again";
        checkAnswerResponse.color = Color.grey;
        fadeText = StartCoroutine(FadeResponse(1.5f, Color.grey));
    }

    IEnumerator FadeResponse(float duration, Color originalColor)
    {
        yield return new WaitForFixedUpdate();
        float alpha = checkAnswerResponse.color.a;
        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / duration)
        {
            float aValue = Mathf.Lerp(1f, 0, t);
            Color newColor = new Color(originalColor.r, originalColor.g, originalColor.b, aValue);
            checkAnswerResponse.color = newColor;
            yield return null;
        }
    }
}
