using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gym : board
{
    public override void Pass(player player)
    {

    }

    public sealed override IEnumerator LandOn(player player)
    {
        Debug.Log("停在运动场");
        if (player.getHealth() >= 2)
        {
           
            yield return MyChoiceAlert.instance.CreateChoiceAlert(player.getPlayerName() + ": 是否进入运动场打工？", "是", "否");
            //if (player.IsAI())
            //{
            //    AI.instance.makeDesicision(player, "打工");
            //}
            if (MyChoiceAlert.instance.resultingDecision) //是否打工
            {
                player.CmdSystem_Info(player.getPlayerName(), "进入运动场打工");
      
                player.setToWait(2);
                player.setOuttoShow("打工中");
                player.setdisapper();
               
                yield return GamePlay.instance.useBufferCard(player, "打工");
            }
        }
        else
        {
            player.CmdSystem_Info(player.getPlayerName(), "健康值不够!");
          
        }
        //yield return null;
    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
