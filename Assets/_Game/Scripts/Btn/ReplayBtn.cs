using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReplayBtn : BaseBtn
{
    protected override void OnClick()
    {
        GameManager.instance.Replay();
        
    }
}
