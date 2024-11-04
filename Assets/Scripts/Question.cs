using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct Question
{
    public string TheQuestion;
    public AudioClip TheQuestionAudio;
    public string TheAnswer;
    public AudioClip TheAnswerAudio;
    public bool HasFollowUp;
    public List<Question> FollowUps;
}
