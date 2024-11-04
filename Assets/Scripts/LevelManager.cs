using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    [SerializeField] GameObject myQuestionContainer;
    [SerializeField] GameObject myQuestionPrefab;
    [SerializeField] TextMeshProUGUI myAnswerText;
    [SerializeField] GameObject myFollowUpContainer;

    [SerializeField] Questions myQuestions;

    AudioSource myAudioSource;

    List<Button> myMainQuestionButtons = new List<Button>();
    public List<Button> myFollowUpQuestionButtons = new List<Button>();

    [SerializeField] float myDelayBetweenQnA;
    [SerializeField] float myTypewriterDelay;

    Color halfAlpha;
    Color fullAlpha;

    private void Awake()
    {
        myAudioSource = GetComponent<AudioSource>();
        halfAlpha = new Color(1, 1, 1, .5f);
        fullAlpha = new Color(1, 1, 1, 2);
    }

    // Start is called before the first frame update
    void Start()
    {
        foreach (Question question in myQuestions.myQuestions)
        {
            GameObject questionButton = Instantiate(myQuestionPrefab, myQuestionContainer.transform);
            questionButton.GetComponentInChildren<TextMeshProUGUI>().text = question.TheQuestion;
            Button button = questionButton.GetComponent<Button>();
            button.onClick.AddListener(() => SelectQuestion(question, button));
            myMainQuestionButtons.Add(button);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SelectQuestion(Question aQuestion, Button aButton)
    {
        //StopCoroutine(PlayDialogue(aQuestion, aButton));
        StartCoroutine(PlayDialogue(aQuestion, aButton));
    }

    IEnumerator PlayDialogue(Question aQuestion, Button aButtonPressed)
    {
        if (myMainQuestionButtons.Contains(aButtonPressed) && myFollowUpQuestionButtons.Count > 0) 
        {
            Destroy(myFollowUpQuestionButtons[0].gameObject);
        }
        myAnswerText.text = "";
        myAudioSource.clip = aQuestion.TheQuestionAudio;
        float clipLength = myAudioSource.clip.length;
        myAudioSource.Play();
        aButtonPressed.GetComponentInChildren<TextMeshProUGUI>().fontStyle = FontStyles.Bold;
        foreach (var button in myMainQuestionButtons)
        {
            button.interactable = false;
        }
        yield return new WaitForSeconds(clipLength + myDelayBetweenQnA);
        

        myAudioSource.clip = aQuestion.TheAnswerAudio;
        myAudioSource.Play();
        for (int  i = 0; i < aQuestion.TheAnswer.Length; i++) 
        {
            myAnswerText.text += aQuestion.TheAnswer[i];
            yield return new WaitForSeconds(myTypewriterDelay);
        }
        aButtonPressed.GetComponentInChildren<TextMeshProUGUI>().fontStyle = FontStyles.Normal;
        foreach (var button in myMainQuestionButtons)
        {
            button.interactable = true;
        }

        if (aQuestion.HasFollowUp) 
        {
            myFollowUpQuestionButtons = new List<Button>();
            GameObject followUpQuestionBtn = Instantiate(myQuestionPrefab, myFollowUpContainer.transform);
            followUpQuestionBtn.GetComponentInChildren<TextMeshProUGUI>().text = aQuestion.FollowUps[0].TheQuestion;
            Button button = followUpQuestionBtn.GetComponent<Button>();
            button.onClick.AddListener(() => SelectQuestion(aQuestion.FollowUps[0], button));
            myFollowUpQuestionButtons.Add(button);
        }
    }
}
