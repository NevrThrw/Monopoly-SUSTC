using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class teachingbuilding : board
{
    private string[] turnforone = { "C/C++", "离散数学" };
    private string[] turnfortwo = { "Java", "高等数学", "概率论与数理统计", "数据库原理" };
    private string[] turnforthree = { "数据结构", "人工智能", "面向对象分析", "计算机组成原理", "嵌入式系统", "计算机网络", "操作系统" };
    //private string[] turnforfour = { "软件工程" };
    public static teachingbuilding instance;
   
    public override void Pass(player player)
    {

    }

    public override IEnumerator LandOn(player player)
    {
        Debug.Log(player.getPlayerName()+ "停在教学楼");
        if (player.getLectureHaveDone()<14)//判断是否有课程可以修读
        {
            string[] lectureList = player.getLectureList();//获得当前年级的课程表
            string lesson = lectureList[player.getCurLecture()];//获得下一个修读的课程
            int need = getToWait(player, lesson);
            if (player.getHealth() >= need)
            {
                yield return MyChoiceAlert.instance.CreateChoiceAlert(player.getPlayerName() + "是否学习\n" + lesson, "是", "否");
               
                if (MyChoiceAlert.instance.resultingDecision)//选择修读
                {
                    player.CmdSystem_Info(player.getPlayerName(), "修读" + lesson);
                   
                    player.setOuttoShow("学习中");
                    if (((IList)turnforone).Contains(lesson))
                    {
                        player.setToWait(1);
                        
                    }
                    else if (((IList)turnfortwo).Contains(lesson))
                    {
                        player.setToWait(2);
                        
                    }
                    else if (((IList)turnforthree).Contains(lesson))
                    {
                        player.setToWait(3);
                       
                    }
                    else
                    {
                        player.setToWait(4);
                       
                    }
                    player.setdisapper();
                    yield return GamePlay.instance.useBufferCard(player,"学习");
                    player.setCurLecture((player.getCurLecture() + 1) % lectureList.Length);
                    //player.AdjustLectureDone(1);
                }
            }
            else
            {
                player.CmdSystem_Info(player.getPlayerName(), "健康值不够!");
           
            }
        }

    }
    public int getToWait(player player,string lesson)
    {
        int timetowait = 0;

        if (((IList)turnforone).Contains(lesson))
        {
            timetowait = 1;
        }
        else if (((IList)turnfortwo).Contains(lesson))
        {
            timetowait = 2;
        }
        else if (((IList)turnforthree).Contains(lesson))
        {
            timetowait = 3;
        }
        else
        {
            timetowait = 4;
        }
        return timetowait;
    }
    // Use this for initialization
    void Start () {
        instance = this;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
