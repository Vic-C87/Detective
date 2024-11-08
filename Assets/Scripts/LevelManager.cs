using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    [SerializeField] GameObject myQuestionContainer;
    [SerializeField] GameObject myQuestionPrefab;
    [SerializeField] GameObject myFollowUpQuestionPrefab;
    [SerializeField] TextMeshProUGUI myAnswerText;
    [SerializeField] GameObject myFollowUpContainer;

    [SerializeField] GameObject myCharacter;
    [SerializeField] Questions myQuestions;

    AudioSource myAudioSource;

    Dictionary<Button, List<SQuestion>> myMainQuestionButtons = new Dictionary<Button, List<SQuestion>>();
    Button myFollowUpQuestionButton;

    [SerializeField] float myTypewriterDelay;

    SQuestion myCurrentQuestion;
    Button myCurrentButton;
    List<SQuestion> myCurrentFollowUps = new List<SQuestion>();
    int myCurrentFollowUpIndex;

    bool myIsAnswering = false;
    int myLetterIndex = 0;
    float myTypewriterTimeStamp;

    //DEBUG
    [SerializeField] bool playSound;

    private void Awake()
    {
        myAudioSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        LoadInNewCharacter(GameManager.Instance.GetCharacter());
    }

    private void Update()
    {
        if (myIsAnswering && Time.time - myTypewriterTimeStamp > myTypewriterDelay)
        {
            TypeMessage();
        }

        if (myIsAnswering && (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Return)))
        {
            myIsAnswering = false;
            myAnswerText.text = myCurrentQuestion.TheAnswer;
            myAudioSource.Stop();
            ContinueDialogue(myCurrentQuestion, myCurrentButton);
        }
    }

    void TypeMessage()
    {
        if (myLetterIndex < myCurrentQuestion.TheAnswer.Length)
        {
            myAnswerText.text += myCurrentQuestion.TheAnswer[myLetterIndex];
            myLetterIndex++;
            myTypewriterTimeStamp = Time.time;
        }
        else
        {
            myLetterIndex = 0;
            myIsAnswering = false;
            myTypewriterTimeStamp = 0;
            ContinueDialogue(myCurrentQuestion, myCurrentButton);
        }
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
        GameObject prefab = anIsMainQuestion ? myQuestionPrefab : myFollowUpQuestionPrefab;
        GameObject questionButton = Instantiate(prefab, aParentTransform);
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
        if (GameManager.Instance.IsQuestionAsked(aQuestion)) 
        {
            ReplayDialogue(aQuestion, aButton, someFollowUps);
        }
        else
        {
            GameManager.Instance.AddNEWQuestion(aQuestion);
            StartCoroutine(PlayDialogue(aQuestion, aButton, someFollowUps));
        }
    }

    void ReplayDialogue(SQuestion aQuestion, Button aButtonPressed, List<SQuestion> someFollowUps)
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

        aButtonPressed.interactable = false;
        foreach (var button in myMainQuestionButtons)
        {
            button.Key.interactable = false;
        }
        if (aQuestion.TheAnswerAudio != null)
        {
            myAudioSource.clip = aQuestion.TheAnswerAudio;
            myAudioSource.Play();
        }
        myQuestionContainer.SetActive(false);
        myLetterIndex = 0;
        myCurrentQuestion = aQuestion;
        myIsAnswering = true;
        myTypewriterTimeStamp = 0;
        myCurrentButton = aButtonPressed;
    }

    void ContinueDialogue(SQuestion aQuestion, Button aButtonPressed)
    {
        myQuestionContainer.SetActive(true);
        aButtonPressed.interactable = true;
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
        
        aButtonPressed.interactable = false;
        foreach (var button in myMainQuestionButtons)
        {
            button.Key.interactable = false;
        } 
        if (aQuestion.TheAnswerAudio != null)
        {
            myAudioSource.clip = aQuestion.TheAnswerAudio;
            myAudioSource.Play();
        }
        myQuestionContainer.SetActive(false);
        for (int  i = 0; i < aQuestion.TheAnswer.Length; i++) 
        {
            myAnswerText.text += aQuestion.TheAnswer[i];
            yield return new WaitForSeconds(myTypewriterDelay);
        }

        ContinueDialogue(aQuestion, aButtonPressed);
    }

    public void ReturnToTapeSelect()
    {
        GameManager.Instance.ReturnToTapeSelect();
    }
}
