using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class researchbuilding : board
{
    public override void Pass(player player)
    {

    }

    public override IEnumerator LandOn(player player)
    {
        Debug.Log(player.getPlayerName() + "停在科研楼");
        if (player.getGrade()>=3 &&player.getLectureHaveDone()>10)//当玩家是大三及以上，并且修读了10门课程以上
        {
            yield return MyChoiceAlert.instance.CreateChoiceAlert(player.getPlayerName() + "是否进行科研\n", "是", "否");
            //if (player.IsAI())
            //{
            //    AI.instance.makeDesicision(player, "科研");
            //}
            if (MyChoiceAlert.instance.resultingDecision)//选择科研
            {
                player.CmdSystem_Info(player.getPlayerName(), "进行科研");
             
                player.setOuttoShow("科研中");
                player.setToWait(4);
                player.setdisapper();
                yield return GamePlay.instance.useBufferCard(player, "科研");
            }
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
