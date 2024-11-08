using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] string myName;
    [SerializeField] string myDescription;
    [SerializeField] GameObject myModel;
    Questions myQuestions;


    private void Awake()
    {
        myQuestions = GetComponent<Questions>();    
    }

    private void Start()
    {
        Instantiate(myModel);
    }

    public Questions GetQuestions()
    {
        return myQuestions;
    }
}
