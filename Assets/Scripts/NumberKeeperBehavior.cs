using System.Collections;
using TMPro;
using UnityEngine;

public class NumberKeeperBehavior : MonoBehaviour
{
    [Header("Game Manager")]
    [SerializeField] GameManager gameManager;
    [Header("Minutes")]
    [SerializeField] Animator animatorMinutes;
    [SerializeField]TMP_Text minutesFrontText;
    [SerializeField] TMP_Text minutesBackText;
    [Header("Decimals")]
    [SerializeField] Animator animatorSecondsD;
    [SerializeField] TMP_Text secondsDFrontText;
    [SerializeField] TMP_Text secondsDBackText;
    [Header("Units")]
    [SerializeField] Animator animatorSecondsU;
    [SerializeField] TMP_Text secondsUFrontText;
    [SerializeField] TMP_Text secondsUBackText;

    int minutes, secondsD, secondsU;

    //Changes the time on the tree
    private void Update()
    {
        int timeLeft = (int)gameManager.dayTimeLeft;

        minutes =  timeLeft / 60;
        timeLeft -= 60 * minutes;
        secondsD = timeLeft / 10;
        timeLeft -= secondsD * 10;
        secondsU = timeLeft;
        //Debug.Log($"{minutes} : {secondsD} {secondsU}");

        if (minutesBackText.text != minutes.ToString())
        {
            animatorMinutes.SetTrigger("count");
        }
        if (secondsDBackText.text != secondsD.ToString())
        {
            animatorSecondsD.SetTrigger("count");
        }
        if (secondsUBackText.text != secondsU.ToString())
        {
            animatorSecondsU.SetTrigger("count");
        }

        minutesBackText.text = minutes.ToString();
        secondsDBackText.text = secondsD.ToString();
        secondsUBackText.text = secondsU.ToString();

        StartCoroutine(UpdateTimeBehind());
    }

    IEnumerator UpdateTimeBehind()
    {
        yield return new WaitForSeconds(.6f);
        minutesFrontText.text = minutes.ToString();
        secondsDFrontText.text = secondsD.ToString();
        secondsUFrontText.text = secondsU.ToString();
        StopAllCoroutines();
    }
}
