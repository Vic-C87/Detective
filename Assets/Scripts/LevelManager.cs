using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    [SerializeField] GameObject myQuestionContainer;
    [SerializeField] GameObject myQuestionPrefab;

    [SerializeField] Questions myQuestions;

    AudioSource myAudioSource;

    List<Button> myMainQuestionButtons = new List<Button>();

    [SerializeField] float myDelayBetweenQnA;

    private void Awake()
    {
        myAudioSource = GetComponent<AudioSource>();
    }

    // Start is called before the first frame update
    void Start()
    {
        foreach (Question question in myQuestions.myQuestions)
        {
            GameObject questionButton = Instantiate(myQuestionPrefab, myQuestionContainer.transform);
            questionButton.GetComponentInChildren<TextMeshProUGUI>().text = question.TheQuestion;
            Button button = questionButton.GetComponent<Button>();
            button.onClick.AddListener(() => StartCoroutine(PlayDialogue(question, button)));
            myMainQuestionButtons.Add(button);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator PlayDialogue(Question aQuestion, Button aButtonPressed)
    {
        myAudioSource.clip = aQuestion.TheQuestionAudio;
        float clipLength = myAudioSource.clip.length;
        myAudioSource.Play();
        foreach (var button in myMainQuestionButtons)
        {
            button.interactable = false;
        }
        yield return new WaitForSeconds(clipLength + myDelayBetweenQnA);
        foreach (var button in myMainQuestionButtons)
        {
            button.interactable = true;
        }

        myAudioSource.clip = aQuestion.TheAnswerAudio;
        clipLength = myAudioSource.clip.length;
        myAudioSource.Play();
        yield return new WaitForSeconds(clipLength + myDelayBetweenQnA);

        if (aQuestion.HasFollowUp) 
        {
            Debug.Log("Add follow up Question button to screen");
        }
    }
}
