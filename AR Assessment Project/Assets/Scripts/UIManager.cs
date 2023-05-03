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
    [SerializeField] private GameObject progressTickPrefab;
    [SerializeField] private GameObject progressBar;
    [SerializeField] private Color[] numberColors;
    private List<GameObject> progressBarTicks = new List<GameObject>();
    private Coroutine fadeText;

    private void Awake()
    {
        SpawnProgressBar();
    }

    public void PlayButton()
    {
        playButton.SetActive(false);
        gameUI.SetActive(true);
        gameManager.ResumeGame();
    }

    public void Pause()
    {
        playButton.SetActive(true);
        gameUI.SetActive(false);
    }

    public void AnswerResponse(string response, Color color)
    {
        checkAnswerResponse.text = response;
        checkAnswerResponse.color = color;
        fadeText = StartCoroutine(FadeResponse(1.5f, color));
    }

    private void SpawnProgressBar()
    {
        for (int i = 1; i < 11; i++)
        {
            GameObject newProgressTick = Instantiate(progressTickPrefab);
            newProgressTick.transform.SetParent(progressBar.transform);
            newProgressTick.GetComponent<TMP_Text>().text = "" + i;
            int spacing = 95;
            newProgressTick.GetComponent<RectTransform>().anchoredPosition = new Vector3((-5.5f * spacing) + (i * spacing), 0);
            progressBarTicks.Add(newProgressTick);
        }
    }

    public void AddProgressTick(int i)
    {
        int shiftI = i - 1;
        TMP_Text tickText = progressBarTicks[shiftI].GetComponent<TMP_Text>();
        tickText.color = numberColors[shiftI];
    }

    public void ResetTickColors()
    {
        foreach (GameObject tick in progressBarTicks)
        {
            tick.GetComponent<TMP_Text>().color = Color.grey;
        }
    }

    IEnumerator FadeResponse(float duration, Color originalColor)
    {
        yield return new WaitForFixedUpdate();
        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / duration)
        {
            float aValue = Mathf.Lerp(1f, 0, t);
            Color newColor = new Color(originalColor.r, originalColor.g, originalColor.b, aValue);
            checkAnswerResponse.color = newColor;
            yield return null;
        }
    }
}
