using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    static GameManager myGameManager;

    public static GameManager Instance { get { return myGameManager; } }

    [SerializeField] List<GameObject> myCharacters;

    int mySelectedCharacterIndex = 0;

    List<SQuestion> myAskedQuestions = new List<SQuestion>();

    [SerializeField] AudioSource myTutorial;


    private void Awake()
    {
        if (myGameManager != null && myGameManager != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            myGameManager = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        myTutorial.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void LoadTape(int aCharacterToInterview)
    {
        mySelectedCharacterIndex = aCharacterToInterview;
    }

    public GameObject GetCharacter() 
    {
        return myCharacters[mySelectedCharacterIndex];
    }

    public void ReturnToTapeSelect()
    {
        SceneManager.LoadScene(0);

    }

    public void AddNEWQuestion(SQuestion aQuestion)
    {
        if (!IsQuestionAsked(aQuestion))
            myAskedQuestions.Add(aQuestion);
    }

    public bool IsQuestionAsked(SQuestion aQuestion)
    {
        return myAskedQuestions.Contains(aQuestion);
    }
}
