using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Notepad : MonoBehaviour
{
    

    [SerializeField] GameObject myNoteItemPrefab;
    [SerializeField] Transform myNoteParent;
    [SerializeField] TextMeshProUGUI myCurrentInput;
    [SerializeField] TMP_InputField myInputField;

    bool myNotePadIsOpen = false;
    bool myEditMode  = false;

    RectTransform myTransform;

    [SerializeField] Vector2 myHiddenYPos;
    [SerializeField] Vector2 myOpenYPos;

    List<Note> myNotes = new List<Note>();
    Note myNoteToEdit;
    //TODO
    List<string> myPreviousNotes;

    private void Awake()
    {
        myTransform = GetComponent<RectTransform>();
    }

    void Start()
    {
    }

    void Update()
    {
        if (myNotePadIsOpen && myCurrentInput.text.Length > 1)
        {
            if (Input.GetKeyDown(KeyCode.Return)) 
            {
                AddNewNote();
                myInputField.text = "";
                myCurrentInput.text = "";
            }
        }
    }

    void AddNewNote()
    {
        if(!myEditMode)
        {
            GameObject notePrefab = Instantiate(myNoteItemPrefab, myNoteParent);
            Note note = notePrefab.GetComponent<Note>();
            note.SetText(myCurrentInput.text);
            myNotes.Add(note);
        }
        else
        {
            myNoteToEdit.SetText(myCurrentInput.text);
            myNoteToEdit = null;
            myEditMode = false;
        }
    }

    public void EditNote(Note aNoteToEdit, string anOldText)
    {
        myEditMode = true;
        myNoteToEdit = aNoteToEdit;
        myInputField.text = anOldText;
        myCurrentInput.text = anOldText;
    }

    public void ToggleNotePad()
    {
        if (!myNotePadIsOpen) 
        {
            myTransform.anchoredPosition = myOpenYPos;
            myNotePadIsOpen = true;
        }
        else
        {
            myTransform.anchoredPosition = myHiddenYPos;
            myNotePadIsOpen = false;
        }
    }
}
