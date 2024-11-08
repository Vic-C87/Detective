using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tape : MonoBehaviour
{
    TapeGroup myParent;

    private void Awake()
    {
        myParent = GetComponentInParent<TapeGroup>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseOver()
    {
        myParent.Hover();
    }

    private void OnMouseExit()
    {
        myParent.NoHover();
    }

    private void OnMouseDown()
    {
        myParent.Press();
    }
}
