using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TapeSelectManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadTape(int aCharacterIndexToLoad)
    {
        GameManager.Instance.LoadTape(aCharacterIndexToLoad);
        SceneManager.LoadScene(1);
    }
}
