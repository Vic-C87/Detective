using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Note : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI myNote;
    Notepad myNotepad;

    // Start is called before the first frame update
    void Start()
    {
        myNotepad = GetComponentInParent<Image>().GetComponentInParent<Notepad>();
    }

    // Update is called once per frame
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
