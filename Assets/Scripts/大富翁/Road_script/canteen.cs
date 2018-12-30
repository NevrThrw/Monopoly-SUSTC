using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class canteen : board
{
    public override void Pass(player player)
    {
        //Debug.Log("经过食堂");
       
        //player.AdjustBalance(10000);
    }

    public override IEnumerator LandOn(player player)
    {
        Debug.Log("停在食堂");
        if (player.getHealth() >= 2)
        {
           
            yield return MyChoiceAlert.instance.CreateChoiceAlert(player.getPlayerName() + ":是否进入食堂打工", "是", "否");
            Debug.Log(player.IsAI());
            //if (player.IsAI())
            //{
            //    Debug.Log("打工");
            //    AI.instance.makeDesicision(player, "打工");
            //}
            if (MyChoiceAlert.instance.resultingDecision) //是否打工
            {
                player.CmdSystem_Info(player.getPlayerName(), "进入食堂打工");
                player.setToWait(2);
                player.setOuttoShow("打工中");
                player.setdisapper();
                yield return GamePlay.instance.useBufferCard(player, "打工");
            }
        }
        else
        {
            player.CmdSystem_Info(player.getPlayerName(), "健康值不够");
        }
       
    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
