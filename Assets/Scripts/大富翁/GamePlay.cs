using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class GamePlay : NetworkBehaviour
{
    public static GamePlay instance;
    public static List<player> playerList = new List<player>();
    public static string[] nameList = { "蓝十一", "绿十一", "黄十一", "红十一" };
    private Text numOfPlayers;
    public bool end;

    private void Awake()
    {
        instance = this;
      
        Debug.Log("GamePlay Awake" );
        numOfPlayers = GameObject.Find("玩家轮换").transform.Find("玩家数").GetComponent<Text>();
        end = false;

    }
    void Start()
    {
        Debug.Log("GamePlay start");
        //最开始的时候
        //playerList[0].Initialize();

    }

     void OnDestroy()
    {
        Debug.Log("销毁 GamePlay");
        playerList = new List<player>();
    }


    // Start is called before the first frame update


    // Update is called once per frame
    void Update()
    {
    
        numOfPlayers.text = "轮到玩家: " + nameList[play.count]+"\n掷骰子";
      
        player temp = playerList[play.count];
            if (temp.IsAI() && !play.instance.getPress())
            {
                new WaitForSeconds(2f);
                AI.instance.pressthebutton();//投骰子
            }
        
       
    }

    

    public IEnumerator run()
    {
        
        player nowP = playerList[play.count]; //获得当前的玩家角色
        // nowP.SetPlayerName(nameList[play.count]);

        if (nowP.getRoundToWait() == 0)//动作结束的操作
        {

            if (nowP.getoutToShow().Equals("打工"))
            {
                //yield return MyMessageAlert.instance.DisplayAlert(nowP.getPlayerName() + ": 打工结束你出来了");
                nowP.CmdSystem_Info(nowP.getPlayerName(), "打工结束");
                nowP.setrecover();
                yield return nowP.updateWorkMoney();
                nowP.resetOuttoShow();
                if (nowP.use == true)//使用过卡牌
                {
                    if (nowP.next != 0)//使用的是熟练卡
                    {
                        Debug.Log("恢复");
                        nowP.setRankName(nowP.jobRankName[nowP.next - 1]);//重新恢复原来的熟练度
                        nowP.next = 0;//将标志重新归零
                    }
                    nowP.use = false;
                    nowP.SetBuffer();
                }
            }
            else if (nowP.getoutToShow().Equals("宿舍"))
            {
                //yield return MyMessageAlert.instance.DisplayAlert(nowP.getPlayerName() + ": 宿舍休息结束你出来了");
                nowP.CmdSystem_Info(nowP.getPlayerName(), "休息结束");
               
                nowP.setrecover();
                nowP.resetOuttoShow();
                int recover = nowP.getMaxHealth() - nowP.getHealth() < 5 ? nowP.getMaxHealth() - nowP.getHealth() : 5;
                nowP.AdjustHealth(recover);//恢复所有的健康值
            }
            else if (nowP.getoutToShow().Equals("学习"))
            {
                nowP.CmdSystem_Info(nowP.getPlayerName(), "学习完成");
                nowP.setrecover();
                nowP.resetOuttoShow();
                nowP.AdjustLectureDone(1);//更新已修课程数
                if (nowP.use == true)
                {
                    nowP.use = false;
                    nowP.SetBuffer();
                }
            }
            else if (nowP.getoutToShow().Equals("离校"))
            {
                nowP.CmdSystem_Info(nowP.getPlayerName(), "回校");
           
                nowP.setrecover();
                nowP.resetOuttoShow();
                nowP.setMaxHealth(nowP.getMaxHealth() + 2);
                nowP.AdjustHealth(1);
            }
            else if (nowP.getoutToShow().Equals("科研"))
            {
                nowP.CmdSystem_Info(nowP.getPlayerName(), "科研完成");
             
                nowP.setrecover();
                nowP.resetOuttoShow();
                nowP.AdjustResearch(1);
                if (nowP.use == true)
                {
                    nowP.use = false;
                    nowP.SetBuffer();
                }
                if (nowP.getProcess() == 3)
                {
                    nowP.CmdSystem_Info(nowP.getPlayerName(), "发表一篇论文");
                
                }
            }
            Debug.Log("Move");
            yield return dice_controller.instance.roll();
            int[] result = dice_controller.instance.getRes();
            //yield return System_Info.instance.Display(nowP.getPlayerName(), "掷出" + result.Sum() + "点");
            yield return nowP.MoveSpaces(result.Sum());
            //result.Sum()

        }
        else//还在动作进行中的操作
        {
            if (nowP.getoutToShow() == "打工中")
            {
                nowP.updateToWork();
                nowP.AdjustHealth(-1);
                nowP.CmdSystem_Info(nowP.getPlayerName(), "打工中");
               
            }
            else if (nowP.getoutToShow() == "休息中")
            {
                nowP.updateToRelax();
                nowP.CmdSystem_Info(nowP.getPlayerName(), "休息中");
            }
            else if (nowP.getoutToShow() == "学习中")
            {
                nowP.updateToStudy();
                nowP.AdjustHealth(-1);
                nowP.CmdSystem_Info(nowP.getPlayerName(), "学习中");
            }
            else if (nowP.getoutToShow() == "离校中")
            {
                nowP.updateToOut();
                nowP.CmdSystem_Info(nowP.getPlayerName(), "离校中");
            }
            else if (nowP.getoutToShow() == "科研中")
            {
                nowP.updateToRes();
                nowP.AdjustHealth(-1);
                nowP.CmdSystem_Info(nowP.getPlayerName(), "科研中");
            }

            // yield return MyMessageAlert.instance.DisplayAlert(nowP.getPlayerName() + ": 你在打工中，跳过这个回合");

        }

        end = nowP.checkWin();
        Debug.Log("是否结束" + end);
        
       
        
       

     

    yield return endTurn.instance.endturn(nowP);
        
        //更新角色的位置，解决碰撞移位问题
        if (play.instance.turncount / playerList.Count > 0)//所有角色均进行过一轮游戏
        {
            for (int i = 0; i < playerList.Count; i++)//更新所有角色的位置
            {
                player child = playerList[i];
                child.transform.position = child.current.transform.position;
            }
        }
        else//还有角色未完成第一轮的游戏
        {
            for (int i = 0; i < play.instance.turncount; i++)//更新已经进行过游戏的角色的位置
            {
                player child = playerList[i];
                child.transform.position = child.current.transform.position;
            }
        }

        playerList[play.count].updateall();
        //调整count 必须从客户端变化，并通知全部
        Debug.Log("当前有count:" + play.count);
        int c = (play.count + 1) % playerList.Count;//服务器变化了count
        playerList[play.count].updatecount(c);
        playerList[play.count].CmdEnd(end);

        yield return new WaitForSeconds(2);
        play.instance.setPress(false);



    }


    //[Command]
    //void CmdEnd(bool isend)
    //{
    //    RpcEnd(isend);
    //}

    //[ClientRpc]
    //void RpcEnd(bool isend)
    //{
    //    end = isend;
    //    Gameover();
    //}

    //public void Gameover()
    //{
    //    if (end)
    //    {
    //        SceneManager.LoadScene("结束游戏");
    //    }


    //}



   



    // 使用buffer牌
    public IEnumerator useBufferCard(player player,string type)
    {

        switch (player.getBufferCard())
        {
            case "学习卡":
                if (type.Equals("学习"))
                {
                    player.AdjustBuffer("学习加速");
                    player.updateToStudy();
                    player.use = true;
                    player.CmdSystem_Info(player.getPlayerName(), "使用了学习卡进行加速");
                    
                }
                break;
            case "打工卡":
                if (type.Equals("打工"))
                {
                    player.AdjustBuffer("打工加速");
                    player.updateToWork();
                    player.use = true;
                    player.CmdSystem_Info(player.getPlayerName(), "使用了打工卡进行加速");
                    
                }
                break;
            case "科研卡":
                if (type.Equals("科研"))
                {
                    player.AdjustBuffer("科研加速");
                    player.updateToRes();
                    player.use = true;
                    player.CmdSystem_Info(player.getPlayerName(), "使用了科研卡进行加速");
                   
                }
                break;
            case "熟练卡":
                player.next = 0;//用于判断是否暂时提升了熟练度
                if (type.Equals("打工"))
                {
                    player.AdjustBuffer("打工熟练增加");
                    for (int i = 0; i < player.jobRankName.Length; i++)//如果打工等级不是最高的，暂时将等级升为下一级
                    {
                        if (player.getRankName().Equals(player.jobRankName[i]) && i < 3)
                        {
                            player.next = i + 1;
                            player.setRankName(player.jobRankName[player.next]);//提升打工等级
                            player.use = true;
                            player.CmdSystem_Info(player.getPlayerName(), "使用了熟练卡增加熟练度");
                            
                            break;
                        }
                    }
                }
                break;
            case "爆肝卡":
                
                if (type.Equals("打工"))
                {
                    player.AdjustBuffer("爆肝");
                    player.AdjustHealth(-4);//健康值额外下降2点
                    player.setMaxHealth(player.getMaxHealth() - 2);
                    player.updateToWork();
                    player.updateToWork();
                    player.use = true;
                    player.CmdSystem_Info(player.getPlayerName(), "使用了爆肝卡爆肝");
                   
                }
                else if (type.Equals("科研"))
                {
                    player.AdjustBuffer("爆肝");
                    player.AdjustHealth(-4);//健康值额外下降2点
                    player.setMaxHealth(player.getMaxHealth() - 2);
                    player.updateToRes();
                    player.updateToRes();
                    player.use = true;
                    player.CmdSystem_Info(player.getPlayerName(), "使用了爆肝卡爆肝");
                }
                else if (type.Equals("学习"))
                {
                    player.AdjustBuffer("爆肝");
                    player.AdjustHealth(-4);//健康值额外下降2点
                    player.setMaxHealth(player.getMaxHealth() - 2);
                    player.updateToStudy();
                    player.updateToStudy();
                    player.use = true;
                    player.CmdSystem_Info(player.getPlayerName(), "使用了爆肝卡爆肝");
                }
                //player.setToWait(player.getRoundToWait() - 2 >= 0 ? player.getRoundToWait() - 2 : 0)；
                break;
        }
        if (player.use == true)
        {
            player.AdjustBufferCard("无");//角色的buff牌清空
        }

        yield return null;
    }

}
