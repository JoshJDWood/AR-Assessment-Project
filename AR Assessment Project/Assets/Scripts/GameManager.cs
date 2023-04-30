using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject[] numberPrefabs;
    [SerializeField] private AnswerButton[] answerButtons;
    [SerializeField] private UIManager uIManager;
    private GameObject currentNumberObj = null;
    private int currentNumberVal;
    private List<int> remainingNumbers = new List<int>() {1, 2, 3, 4, 5, 6, 7, 8, 9, 10};

    // Start is called before the first frame update
    void Start()
    {

    }

    public void SpawnNextRandomNumber()
    {
        if (currentNumberObj != null)
        {
            Destroy(currentNumberObj);
        }

        if (remainingNumbers.Count != 0)
        {
            int i = Random.Range(0, remainingNumbers.Count);
            currentNumberVal = remainingNumbers[i];
            remainingNumbers.RemoveAt(i);

            currentNumberObj = Instantiate(numberPrefabs[currentNumberVal % 10], new Vector3(0, -0.1f, 0.5f), Quaternion.identity);
            AssignButtonVals();
        }
    }

    private void AssignButtonVals()
    {
        int randomI = Random.Range(0, 3);
        answerButtons[randomI].SetNewInt(currentNumberVal); //set one answer button to the correct value

        //set other two buttons to random possible answers
        int wrongVal = 11;//impossible answer to start
        int otherWrongVal = 11;
        for (int i = 1; i < answerButtons.Length; i++)
        {
            do
            {
                wrongVal = Random.Range(1, 11);
            } while (wrongVal == currentNumberVal || wrongVal == otherWrongVal);
            answerButtons[(randomI + i) % 3].SetNewInt(wrongVal);
            otherWrongVal = wrongVal;
        }
    }

    public void CheckAnswer(int answer)
    {
        if (answer == currentNumberVal)
        {
            if (remainingNumbers.Count != 0)
            {
                SpawnNextRandomNumber();
                uIManager.AnswerResponse("Well Done!", Color.green);
            }
            else
            {
                uIManager.AnswerResponse("Well Done, you found all the numbers!", Color.green);
                PrepareRestart();
            }
        }
        else
        {
            uIManager.AnswerResponse( "Try Again", Color.grey);
        }
    }

    private void PrepareRestart()
    {
        Destroy(currentNumberObj);
        remainingNumbers = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
        StartCoroutine(DelayedPause(2));
    }

    IEnumerator DelayedPause(float delay)
    {
        yield return new WaitForSeconds(delay);
        uIManager.Pause();
    }

    public void ResumeGame()
    {
        if (currentNumberObj == null)
        {
            SpawnNextRandomNumber();
        }
    }
}
