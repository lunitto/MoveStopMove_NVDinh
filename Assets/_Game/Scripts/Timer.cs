using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [SerializeField] private float duration;
    [SerializeField] private Player player;
    public Text timerText;

    public void OnEnable()
    {
        StartCoroutine(TimerCount());
    }

    public IEnumerator TimerCount()
    {
        float t = duration;
        while (t > 0)
        {
            t -= Time.deltaTime;
            timerText.text = ((int)t).ToString();
            yield return null;
        }
        UIManager.instance.HideRevivePanel();
        UIManager.instance.ShowLosePanel();
        GameManager.instance.DeleteThisElementInEnemyLists(player);
        GameManager.instance.currentAlive--;
        GameManager.instance.characterList.Remove(player);
        UIManager.instance.ShowLosePanel();
        if (SoundManager.instance != null)
        {
            SoundManager.instance.Play(SoundType.Lose);
        }
        yield return null;
    }
}
