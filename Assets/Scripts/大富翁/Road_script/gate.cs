using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gate : board
{
    public override void Pass(player player)
    {

    }

    public override IEnumerator LandOn(player player)
    {
        Debug.Log("停在校門");
        yield return MyChoiceAlert.instance.CreateChoiceAlert(
            player.getPlayerName() + ":是否离校去玩？", "是", "否"
         );
     
        if (MyChoiceAlert.instance.resultingDecision)
        {
            player.CmdSystem_Info(player.getPlayerName(), "离校");
            player.setToWait(1);
            player.setOuttoShow("离校中");
            player.setdisapper();
        }
    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
