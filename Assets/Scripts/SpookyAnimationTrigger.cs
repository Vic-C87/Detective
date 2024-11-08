using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpookyAnimationTrigger : MonoBehaviour
{
    [SerializeField] Animator tvStaticAnimator;
    [SerializeField] float spooksDuration;
    [SerializeField] float minCooldown;
    [SerializeField] float maxCooldown;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(startNewTimer(minCooldown, maxCooldown));
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator waitForSpooks(float spooksDuration)
    {
        tvStaticAnimator.SetBool("Static Toggle", true);
        yield return new WaitForSeconds(spooksDuration);
        tvStaticAnimator.SetBool("Static Toggle", false);
        StartCoroutine(startNewTimer(minCooldown, maxCooldown));
    }

    IEnumerator startNewTimer(float minTime, float maxTime)
    {
        float timerLength = Random.Range(minTime, maxTime);
        yield return new WaitForSeconds(timerLength);
        StartCoroutine(waitForSpooks(spooksDuration));
    }
}
