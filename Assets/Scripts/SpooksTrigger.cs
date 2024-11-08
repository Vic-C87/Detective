using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpooksTrigger : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject spookyThing;
    [SerializeField] float spooksDuration;
    [SerializeField] float minTime;
    [SerializeField] float maxTime;
    
    

    void Start()
    {
        startNewTimer(minTime, maxTime);
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator waitForSpooks(float spooksDuration)
    {
        spookyThing.gameObject.SetActive(true);
        yield return new WaitForSeconds(spooksDuration);
        spookyThing.gameObject.SetActive(false);
    }

    IEnumerator startNewTimer(float minTime, float maxTime)
    {
        float timerLength = Random.Range(minTime, maxTime);
        yield return new WaitForSeconds(timerLength);
        waitForSpooks(spooksDuration);
    }
}
