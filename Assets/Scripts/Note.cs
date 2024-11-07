using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Note : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI myNote;
    Notepad myNotepad;

    void Start()
    {
        myNotepad = GetComponentInParent<Image>().GetComponentInParent<Notepad>();
    }

    void Update()
    {
        
    }

    public void SetText(string someText)
    {
        myNote.text = someText;
    }

    public void EditNote()
    {
        myNotepad.EditNote(this, myNote.text);
    }

    public void DeleteNote() 
    {
        Destroy(this.gameObject);
    }
}
