using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PolaroidChooseScript : MonoBehaviour
{
    [SerializeField] string myName;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        Debug.Log(myName);
        GameManager.Instance.BackToMainMenu(myName);
        SceneManager.LoadScene(0);//Change to main menu
    }
}
