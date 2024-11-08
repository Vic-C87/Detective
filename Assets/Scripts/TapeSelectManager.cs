using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TapeSelectManager : MonoBehaviour
{
    [SerializeField] Animator myTapeAnimator;
    [SerializeField] GameObject myStaticTVPlane;
    [SerializeField] AudioClip myStaticSound;
    AudioSource myAudioSource;

    [SerializeField] GameObject[] myTapes;

    private void Awake()
    {
        myAudioSource = GetComponent<AudioSource>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void InsertTape()
    {
        myTapeAnimator.SetBool("Play", true);
        myStaticTVPlane.SetActive(true);
        myAudioSource.PlayOneShot(myStaticSound);
    }

    public void LoadTape(int aCharacterIndexToLoad)
    {
        InsertTape();
        GameManager.Instance.LoadTape(aCharacterIndexToLoad);
    }
}
