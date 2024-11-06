using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] string myName;
    [SerializeField] string myDescription;
    Questions myQuestions;


    private void Awake()
    {
        myQuestions = GetComponent<Questions>();    
    }

    public Questions GetQuestions()
    {
        return myQuestions;
    }
}
