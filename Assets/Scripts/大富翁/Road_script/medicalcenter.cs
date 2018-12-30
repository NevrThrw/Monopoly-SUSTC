using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class medicalcenter : board
{
    public override void Pass(player player)
    {

    }

    public override IEnumerator LandOn(player player)
    {
            Debug.Log("停在社康中心");
      
            yield return MyChoiceAlert.instance.CreateChoiceAlert(
            player.getPlayerName() + ":是否花费1000金钱回复5点健康度", "是", "否"
         );
        //if (player.IsAI())
        //{
        //    AI.instance.makeDesicision(player, "治疗");
        //}
        if (MyChoiceAlert.instance.resultingDecision)
            {
                if (player.getBalance() >= 1000)
                {
                    player.AdjustBalance(-1000);//扣钱
                    player.AdjustHealth(5);                                          //加健康度
                }
                else
                {
                player.CmdSystem_Info(player.getPlayerName(), "金钱不够");
              

                }
            }
           
        Debug.Log("離開社康中心");
    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
