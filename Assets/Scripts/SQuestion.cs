using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct SQuestion
{
    public string TheQuestion;
    public string TheAnswer;
    public AudioClip TheAnswerAudio;
    public bool HasFollowUp;
}
