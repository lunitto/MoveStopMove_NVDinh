using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundBtn : BaseBtn
{
    [SerializeField] private GameObject mute;

    private void Start()
    {
        ChangeCross(SoundManager.instance.isMute);
    }

    protected override void OnClick()
    {
        SoundManager.instance.isMute = !SoundManager.instance.isMute;
        DataManager.ins.playerData.isMute = SoundManager.instance.isMute;
        ChangeCross(SoundManager.instance.isMute);
    }

    public void ChangeCross(bool isMute)
    {
        if (isMute == false)
        {
            mute.SetActive(false);
        }
        else
        {
            mute.SetActive(true);
        }
    }
}
