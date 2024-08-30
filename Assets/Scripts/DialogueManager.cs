using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] TMP_Text text;
    [SerializeField] List<Dialogue> dialogues;
    [SerializeField] float talkSpeed;
    [SerializeField] GameObject speechBubble;
    bool isPlayerInTalkingRange = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player")) isPlayerInTalkingRange = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player")) isPlayerInTalkingRange = false;
    }

    private void Update()
    {
        if (isPlayerInTalkingRange)
            if (Input.GetKeyDown(KeyCode.Space))
            {
                StopAllCoroutines();
                StartCoroutine(Yap(dialogues[Random.Range(0, dialogues.Count)])); //yapps a random dialogue from the start
            }
    }

        IEnumerator Yap(Dialogue dialogue)
    {
        speechBubble.SetActive(true);
        ///play start animation
        text.text = "";
        for(int i = 0; i < dialogue.phrases.Count; i++) 
        {
            string phrase = dialogue.phrases[i];
            //Writes phrase
            foreach(char c in phrase)
            {
                text.text += c;
                ///play letter sound
                yield return new WaitForSeconds(talkSpeed);
            }
            yield return new WaitForSeconds(phrase.Count() / 5);
            //Deletes phrase
            foreach (char c in phrase)
            {
                text.text = text.text.Remove(text.text.Count() - 1);
                yield return new WaitForSeconds(talkSpeed / 5);
            }
        }
        ///play end animation
        speechBubble.SetActive(false);
        StopAllCoroutines();
    }
}
