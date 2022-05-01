using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlanaceTrigger : MonoBehaviour
{
    [SerializeField] Transform Start;
    [SerializeField] Transform End;
    [SerializeField] AudioSource WindAudio;
    [SerializeField] GameObject NotFallingText;
    [SerializeField] float DeactiveText;
    void OnTriggerEnter(Collider other)
    {
        if (other is CharacterController) return;

        if (other.gameObject.CompareTag("Player") /*&& KillerAI.FinishedHouseArea*/)
        {
            GameEvents.BalanceMode?.Invoke(new GameEvents.BalanceData { IsBalance = true, Start = Start, End = End });
            StartCoroutine(DontFallText());
        
             WindAudio.Play();
          //  Debug.Log("Blanace Mode On");
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other is CharacterController) return;
        if (other.gameObject.CompareTag("Player"))
        {
            GameEvents.BalanceMode?.Invoke(new GameEvents.BalanceData { IsBalance = false, Start = Start, End = End });
            WindAudio.Stop();
            //Debug.Log("Blanace Mode OFF "+ other.GetType());
        }
    }
    private IEnumerator DontFallText()
    {
        NotFallingText.SetActive(true);
        yield return new WaitForSeconds(DeactiveText);
        NotFallingText.SetActive(false);

    }
}
