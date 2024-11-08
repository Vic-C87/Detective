using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpooksTrigger : MonoBehaviour
{
    [SerializeField] GameObject spookyThing;
    [SerializeField] float spooksDuration;
    [SerializeField] float minTime;
    [SerializeField] float maxTime; 

    void Start()
    {
        StartCoroutine(startNewTimer(minTime, maxTime));
    }

    void Update()
    {

    }

    IEnumerator waitForSpooks(float spooksDuration)
    {
        Debug.Log("hit");
        spookyThing.gameObject.SetActive(true);
        yield return new WaitForSeconds(spooksDuration);
        spookyThing.gameObject.SetActive(false);
        StartCoroutine(startNewTimer(minTime, maxTime));
    }

    IEnumerator startNewTimer(float minTime, float maxTime)
    {
        float timerLength = Random.Range(minTime, maxTime);
        Debug.Log("här");
        yield return new WaitForSeconds(timerLength);
        Debug.Log("nu");
        StartCoroutine(waitForSpooks(spooksDuration));
    }
}
