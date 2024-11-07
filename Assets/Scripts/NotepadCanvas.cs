using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotepadCanvas : MonoBehaviour
{
    static NotepadCanvas myInstance;
    public static NotepadCanvas Instance { get { return myInstance; } }

    private void Awake()
    {
        if (myInstance != null && myInstance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            myInstance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }
}
