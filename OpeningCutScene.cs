using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
public class OpeningCutScene : MonoBehaviour
{
    [SerializeField] PlayableDirector playable;
    [SerializeField] GameObject CloseEyesAnim;
    [SerializeField] GameObject WakeUpAnim;
    [SerializeField] GameObject OpeningCutSceneGM;
    [SerializeField] GameObject StartIndications;
    [SerializeField] GameObject player;
    [SerializeField] GameObject CarWerak;

    private bool IsStopped;
    void Update()
    {
       StartCoroutine(TimeLineFinish());
    }
    private IEnumerator TimeLineFinish()
    {
        if (playable.state == PlayState.Paused && !IsStopped)
        {
            IsStopped = true;
            CloseEyesAnim.SetActive(true);
            yield return new WaitForSeconds(1);
            ActiveStartGame();
        }
    }

    private void ActiveStartGame()
    {
        OpeningCutSceneGM.SetActive(false);
        CloseEyesAnim.SetActive(false);
        StartIndications.SetActive(true);
        CarWerak.SetActive(true);
        player.SetActive(true);
        WakeUpAnim.SetActive(true);
        
    }
}
