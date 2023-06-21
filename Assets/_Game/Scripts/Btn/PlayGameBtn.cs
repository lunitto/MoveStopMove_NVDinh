using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayGameBtn : BaseBtn
{
    protected override void OnClick()
    {
        GameManager.instance.PlayGame();
    }
}