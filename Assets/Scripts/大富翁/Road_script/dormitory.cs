using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dormitory : board
{
    public override void Pass(player player)
    {

    }

    public override IEnumerator LandOn(player player)
    {
        Debug.Log("停在宿舍");
        yield return MyChoiceAlert.instance.CreateChoiceAlert(player.getPlayerName() + ":是否进入宿舍休息", "是", "否");
        //if(player.IsAI())
        //    {
        //    AI.instance.makeDesicision(player, "休息");
        //}
        if (MyChoiceAlert.instance.resultingDecision) //是否休息
        {
            player.CmdSystem_Info(player.getPlayerName(), "进入宿舍休息");
            player.setToWait(1);
            player.setOuttoShow("休息中");
            player.setdisapper();
        }
        yield return null;
    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
