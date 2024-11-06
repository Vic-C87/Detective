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

    [SerializeField] GameObject myCharacter;
    [SerializeField] Questions myQuestions;

    AudioSource myAudioSource;

    Dictionary<Button, List<SQuestion>> myMainQuestionButtons = new Dictionary<Button, List<SQuestion>>();
    Button myFollowUpQuestionButton;

    [SerializeField] float myTypewriterDelay;

    List<SQuestion> myCurrentFollowUps = new List<SQuestion>();
    int myCurrentFollowUpIndex;

    private void Awake()
    {
        myAudioSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        LoadInNewCharacter(GameManager.Instance.GetCharacter());
    }

    public void LoadInNewCharacter(GameObject aCharacter)
    {
        if (myMainQuestionButtons.Count > 0) 
        {
            foreach (var button in myMainQuestionButtons)
            {
                Destroy(button.Key.gameObject);
            }
        }
        myMainQuestionButtons = new Dictionary<Button, List<SQuestion>>();

        GameObject character = Instantiate(aCharacter);

        myCharacter = character;
        myQuestions = myCharacter.GetComponent<Character>().GetQuestions();

        foreach (Question question in myQuestions.myQuestions)
        {
            SetQuestionButton(question, myQuestionContainer.transform);
        }
    }

    void SetQuestionButton(SQuestion aQuestion, Transform aParentTransform, List<SQuestion> someFollowUps,bool anIsMainQuestion = true)
    {
        GameObject questionButton = Instantiate(myQuestionPrefab, aParentTransform);
        questionButton.GetComponentInChildren<TextMeshProUGUI>().text = aQuestion.TheQuestion;
        Button button = questionButton.GetComponent<Button>();
        button.onClick.AddListener(() => SelectQuestion(aQuestion, button, myCurrentFollowUps));
        if (anIsMainQuestion)  
            myMainQuestionButtons.Add(button, someFollowUps);
        else
            myFollowUpQuestionButton = button;
    }

    void SetQuestionButton(Question aQuestion, Transform aParentTransform, bool anIsMainQuestion = true)
    {
        SQuestion question = new SQuestion();
        question.TheQuestion = aQuestion.TheQuestion;
        question.TheAnswer = aQuestion.TheAnswer;
        question.TheAnswerAudio = aQuestion.TheAnswerAudio;
        question.HasFollowUp = aQuestion.HasFollowUp;

        if (question.HasFollowUp)
        {
            myCurrentFollowUpIndex = 0;
        }
        SetQuestionButton(question, aParentTransform, aQuestion.FollowUps, anIsMainQuestion);
    }

    void SelectQuestion(SQuestion aQuestion, Button aButton, List<SQuestion> someFollowUps)
    {
        StartCoroutine(PlayDialogue(aQuestion, aButton, someFollowUps));
    }

    IEnumerator PlayDialogue(SQuestion aQuestion, Button aButtonPressed, List<SQuestion> someFollowUps)
    {
        if (myMainQuestionButtons.ContainsKey(aButtonPressed)) 
        {
            if (myFollowUpQuestionButton != null)
            {
                Destroy(myFollowUpQuestionButton.gameObject);
                myFollowUpQuestionButton = null;
                myCurrentFollowUpIndex = 0;
            }
            myCurrentFollowUps = someFollowUps;
        }
        myAnswerText.text = "";
        aButtonPressed.GetComponentInChildren<TextMeshProUGUI>().fontStyle = FontStyles.Bold;
        foreach (var button in myMainQuestionButtons)
        {
            button.Key.interactable = false;
        }        
        if (aQuestion.TheAnswerAudio != null)
        {
            myAudioSource.clip = aQuestion.TheAnswerAudio;
            myAudioSource.Play();
        }
        for (int  i = 0; i < aQuestion.TheAnswer.Length; i++) 
        {
            myAnswerText.text += aQuestion.TheAnswer[i];
            yield return new WaitForSeconds(myTypewriterDelay);
        }
        aButtonPressed.GetComponentInChildren<TextMeshProUGUI>().fontStyle = FontStyles.Normal;
        foreach (var button in myMainQuestionButtons)
        {
            button.Key.interactable = true;
        }

        if (aQuestion.HasFollowUp) 
        {
            if (myMainQuestionButtons.ContainsKey(aButtonPressed))
            {
                myCurrentFollowUps = myMainQuestionButtons[aButtonPressed];
            }
            if (myFollowUpQuestionButton != null)
            {
                Destroy(myFollowUpQuestionButton.gameObject);
                myFollowUpQuestionButton = null;
            }
            SetQuestionButton(myCurrentFollowUps[myCurrentFollowUpIndex], myFollowUpContainer.transform, myCurrentFollowUps, false);
            myCurrentFollowUpIndex++;
        }
    }

    public void ReturnToTapeSelect()
    {
        GameManager.Instance.ReturnToTapeSelect();
    }
}
