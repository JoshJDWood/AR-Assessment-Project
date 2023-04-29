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

        int i = Random.Range(0, remainingNumbers.Count);
        currentNumberVal = remainingNumbers[i];
        remainingNumbers.RemoveAt(i);

        currentNumberObj = Instantiate(numberPrefabs[currentNumberVal % 10], new Vector3(0,-0.1f,0.5f), Quaternion.identity);
        AssignButtonVals();
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
            SpawnNextRandomNumber();
            uIManager.CorrectAnswerResponse();
        }
        else
        {
            uIManager.IncorrectAnswerResponse();
        }
    }

    public void ResumeGame()
    {
        if (currentNumberObj == null)
        {
            SpawnNextRandomNumber();
        }
    }
}
