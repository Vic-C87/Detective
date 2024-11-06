using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Question", menuName = "ScriptableObject/Questions", order = 1)]
public class Question : ScriptableObject
{
    public string TheQuestion;
    public string TheAnswer;
    public AudioClip TheAnswerAudio;
    public bool HasFollowUp;
    public List<SQuestion> FollowUps;
}
