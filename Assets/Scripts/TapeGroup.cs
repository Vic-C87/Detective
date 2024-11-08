using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapeGroup : MonoBehaviour
{
    [SerializeField] int myID;
    [SerializeField] TapeSelectManager myTapeSelectManager;
    [SerializeField] Material myStandardMaterial;
    [SerializeField] Material myOutlineMaterial;

    MeshRenderer[] myTapeMeshes;

    List<Material> myOulinedList = new List<Material>();
    List<Material> myStandardList = new List<Material>();

    void Start()
    {
        myTapeMeshes = GetComponentsInChildren<MeshRenderer>();
        myOulinedList.Add(myStandardMaterial);
        myOulinedList.Add(myOutlineMaterial);
        myStandardList.Add(myStandardMaterial);
        myStandardList.Add(myStandardMaterial);
    }

    void Update()
    {
        
    }

    public void Hover()
    {
        foreach (var item in myTapeMeshes)
        {
            item.materials = myOulinedList.ToArray();
        }
    }

    public void NoHover()
    {
        foreach (var item in myTapeMeshes)
        {
            item.materials = myStandardList.ToArray();
        }
    }

    public void Press()
    {
        myTapeSelectManager.LoadTape(myID);
    }
    
}
