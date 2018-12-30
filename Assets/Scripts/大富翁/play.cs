using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.SceneManagement;

public class play : MonoBehaviour
{


    public static int count = 0;//获得当前的玩家的下标
    public int turncount = 0; //统计进行的回合数
    public static bool someoneWin = false;
    public static string winname = "";
    public int outshowcount = count;
    private bool press = false;//判断投骰子按钮是否被按下过
   
    public static play instance;
   
    // Use this for initialization
    private void Awake()
    {
        count = 0;
        turncount = 0;
        someoneWin = false;
        winname = "";
        instance = this;
        press = false;
    }

    public void setoutshowcount(int c)
    {
        this.outshowcount = c;
    }

    void OnDestroy()
    {
        count = 0;
    }



    void Start()
    {
        
        Button btn = this.GetComponent<Button>();
        btn.onClick.AddListener(OnClick);

    }
    // Update is called once per frame
    void Update()
    {
        outshowcount = count;
        // count_count = count;
    }
    public bool getPress()
    {
        return this.press;
    }
    public void setPress(bool press)
    {
        this.press = press;
    }
    public void OnClick()
    {
        if (GamePlay.playerList[count].isLocalPlayer)
        {
            if (press == false)
            {
                Debug.Log("Roll the dice");
                press = true;
                turncount += 1;
                StartCoroutine(GamePlay.instance.run());
            }
        }
       
        
    }

   //IEnumerator run(int conut)
   // {
   //     player nowP =playerList[count]; //获得当前的玩家角色
   //     nowP.SetPlayerName(nameList[count]);
   //     nowP.Initialize();
   //     if (nowP.getRoundToWait() == 0)//动作结束的操作
   //     {

   //         if (nowP.getoutToShow().Equals("打工"))
   //         {
   //             //yield return MyMessageAlert.instance.DisplayAlert(nowP.getPlayerName() + ": 打工结束你出来了");
   //             yield return System_Info.instance.Display(nowP.getPlayerName(), "打工结束");
   //             nowP.gameObject.SetActive(true);
   //             yield return nowP.updateWorkMoney();
   //             nowP.resetOuttoShow();
   //         }
   //         else if (nowP.getoutToShow().Equals("宿舍"))
   //         {
   //             //yield return MyMessageAlert.instance.DisplayAlert(nowP.getPlayerName() + ": 宿舍休息结束你出来了");
   //             yield return System_Info.instance.Display(nowP.getPlayerName(), "休息结束");
   //             nowP.gameObject.SetActive(true);
   //             nowP.resetOuttoShow();
   //             int recover = nowP.getMaxHealth() - nowP.getHealth() < 10 ? nowP.getMaxHealth() - nowP.getHealth() : 10;
   //             nowP.AdjustHealth(recover);//恢复所有的健康值
   //         }
   //         else if (nowP.getoutToShow().Equals("学习"))
   //         {
   //             yield return System_Info.instance.Display(nowP.getPlayerName(), "学习完成");
   //             nowP.gameObject.SetActive(true);
   //             nowP.resetOuttoShow();
   //             nowP.AdjustLectureDone(1);//更新已修课程数
   //         }
   //         else if (nowP.getoutToShow().Equals("离校"))
   //         {
   //             yield return System_Info.instance.Display(nowP.getPlayerName(), "回校");
   //             nowP.gameObject.SetActive(true);
   //             nowP.resetOuttoShow();
   //             nowP.setMaxHealth(nowP.getMaxHealth() + 2);
   //             nowP.AdjustHealth(1);
   //         }else if (nowP.getoutToShow().Equals("科研"))
   //         {
   //             yield return System_Info.instance.Display(nowP.getPlayerName(), "科研完成");
   //             nowP.gameObject.SetActive(true);
   //             nowP.resetOuttoShow();
   //             nowP.AdjustResearch(1);
   //             if (nowP.getProcess()==3)
   //             {
   //                 yield return System_Info.instance.Display(nowP.getPlayerName(), "发表一篇论文");
   //             }
   //         }
   //         Debug.Log("Move");
   //         yield return dice_controller.instance.roll();
   //         int[] result = dice_controller.instance.getRes();
   //         //yield return System_Info.instance.Display(nowP.getPlayerName(), "掷出" + result.Sum() + "点");
   //         yield return nowP.MoveSpaces(result.Sum());
   //         //result.Sum()

   //     } else//还在动作进行中的操作
   //     {
   //         if (nowP.getoutToShow() == "打工中")
   //         {
   //             nowP.updateToWork();
   //             nowP.AdjustHealth(-1);
   //             yield return System_Info.instance.Display(nowP.getPlayerName(), "打工中");
   //         }
   //         else if (nowP.getoutToShow() == "休息中")
   //         {
   //             nowP.updateToRelax();
   //             yield return System_Info.instance.Display(nowP.getPlayerName(), "休息中");
   //         }
   //         else if (nowP.getoutToShow() == "学习中")
   //         {
   //             nowP.updateToStudy();
   //             nowP.AdjustHealth(-1);
   //             yield return System_Info.instance.Display(nowP.getPlayerName(), "学习中");
   //         }
   //         else if (nowP.getoutToShow() == "离校中")
   //         {
   //             nowP.updateToOut();
   //             yield return System_Info.instance.Display(nowP.getPlayerName(), "离校中");
   //         }else if (nowP.getoutToShow() == "科研中")
   //         {
   //             nowP.updateToRes();
   //             nowP.AdjustHealth(-1);
   //             yield return System_Info.instance.Display(nowP.getPlayerName(), "科研中");
   //         }

   //         // yield return MyMessageAlert.instance.DisplayAlert(nowP.getPlayerName() + ": 你在打工中，跳过这个回合");
            
   //     }
   //     bool end = nowP.checkWin();
   //     if (end)
   //     {
   //         SceneManager.LoadScene("结束游戏");
   //     }

   //     yield return endTurn.instance.endturn();
        
   //     press = false;
   //     count = (count + 1) % 4;
   //     //更新角色的位置，解决碰撞移位问题
   //     if (turncount / 4 > 0)//所有角色均进行过一轮游戏
   //     {
   //         for (int i = 0; i < 4; i++)//更新所有角色的位置
   //         {
   //             player child = playerList[i];
   //             child.transform.position = child.current.transform.position;
   //         }
   //     }
   //     else//还有角色未完成第一轮的游戏
   //     {
   //         for(int i = 0; i < turncount; i++)//更新已经进行过游戏的角色的位置
   //         {
   //             player child = playerList[i];
   //             child.transform.position = child.current.transform.position;
   //         }
   //     }
   // }

}

