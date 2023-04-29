using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject[] numberPrefabs;
    [SerializeField] private AnswerButton[] answerButtons;
    private GameObject currentNumber = null;
    // Start is called before the first frame update
    void Start()
    {
        SpawnNextRandomNumber();
        int i = 1;
        foreach (AnswerButton answerButton in answerButtons)
        {
            answerButton.SetNewInt(i);
            i++;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnNextRandomNumber()
    {
        if (currentNumber != null)
        {
            Destroy(currentNumber);
        }

        int nextNumber = Random.Range(0, 9);
        currentNumber = Instantiate(numberPrefabs[nextNumber]);
    }
}
