using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour
{

    public static AI instance;
    // Use this for initialization
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void pressthebutton()
    {
        play.instance.OnClick();//模拟点击投骰子按钮
    }
    public void endturn()
    {
        endTurn.instance.OnClick();
    }
    public IEnumerator makeDesicision(player player, string type)//Ai的选择策略----学习，打工，离校，休息, 治疗，科研
    {
        int nowHealth = player.getHealth();//目前的健康值
        int nowMaxHealth = player.getMaxHealth();//目前的健康值上限
        int nowBalance = player.getBalance();//目前的金钱
        int nowLecDone = player.getLectureHaveDone();//目前已修的课程数
        int winBalance = 50000;
        int winLecDone = 14;
        int winRes = 3;
        if (type.Equals("打工"))
        {
            if (nowHealth >= 2)//如果健康值足够
            {
                MyChoiceAlert.instance.resultingDecision = true;
                MyChoiceAlert.instance.decisionMade = true;
                // yield return GamePlay.instance.useBufferCard(player, "打工");
                yield return null;
            }
            else//否则
            {
                MyChoiceAlert.instance.resultingDecision = false;
                MyChoiceAlert.instance.decisionMade = true;
            }
        }
        else if (type.Equals("学习"))
        {
            string[] lectureList = player.getLectureList();//获得当前年级的课程表
            string lesson = lectureList[player.getCurLecture()];//获得下一个修读的课程
            if (nowHealth >= teachingbuilding.instance.getToWait(player, lesson))//如果健康值足够
            {
                MyChoiceAlert.instance.resultingDecision = true;
                MyChoiceAlert.instance.decisionMade = true;
                //yield return GamePlay.instance.useBufferCard(player, "学习");
                yield return null;
            }
            else//否则
            {
                MyChoiceAlert.instance.resultingDecision = false;
                MyChoiceAlert.instance.decisionMade = true;
            }
        }
        else if (type.Equals("离校"))
        {
            if (nowMaxHealth < 30)//如果健康值上限小于30，则选择离校
            {
                MyChoiceAlert.instance.resultingDecision = true;
                MyChoiceAlert.instance.decisionMade = true;
            }
            else//否则
            {
                MyChoiceAlert.instance.resultingDecision = false;
                MyChoiceAlert.instance.decisionMade = true;
            }
        }
        else if (type.Equals("休息"))
        {
            if (nowHealth < nowMaxHealth * 0.2)
            {
                MyChoiceAlert.instance.resultingDecision = true;
                MyChoiceAlert.instance.decisionMade = true;
            }
            else
            {
                MyChoiceAlert.instance.resultingDecision = false;
                MyChoiceAlert.instance.decisionMade = true;
            }
        }
        else if (type.Equals("科研"))
        {
            if (nowHealth >= 4)
            {
                MyChoiceAlert.instance.resultingDecision = true;
                MyChoiceAlert.instance.decisionMade = true;
                //yield return GamePlay.instance.useBufferCard(player, "科研");
                yield return null;
            }
            else
            {
                MyChoiceAlert.instance.resultingDecision = false;
                MyChoiceAlert.instance.decisionMade = true;
            }
        }
        else if (type.Equals("治疗"))
        {
            if (nowBalance >= 1000 && nowHealth < 0.2 * nowMaxHealth)
            {
                MyChoiceAlert.instance.resultingDecision = true;
                MyChoiceAlert.instance.decisionMade = true;
            }
            else
            {
                MyChoiceAlert.instance.resultingDecision = false;
                MyChoiceAlert.instance.decisionMade = true;

            }
        }
    }
}


