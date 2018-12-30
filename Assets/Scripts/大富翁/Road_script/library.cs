using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class library : board
{
    public override void Pass(player player)//每次经过，随机给角色一张buff卡，替换已有的
    {
        int i= Random.Range(1,11);
        if (i < 3)//3
        {
            player.AdjustBufferCard("学习卡");
        }
        else if(i<6)//6
        {
            player.AdjustBufferCard("打工卡");
        }else if (i < 7)//7
        {
            player.AdjustBufferCard("科研卡");
        }
        else if(i<9)//9
        {
            player.AdjustBufferCard("熟练卡");
        }
        else
        {
            player.AdjustBufferCard("爆肝卡");

        }
       // player.AdjustBufferCard("爆肝卡");
        player.AdjustBufferCard(player.getBufferCard());
        player.CmdSystem_Info(player.getPlayerName(), "获得" + player.getBufferCard());

    }
    

    public override IEnumerator LandOn(player player)
    {
        yield return null;
    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
