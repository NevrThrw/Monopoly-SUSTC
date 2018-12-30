using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using System;
using System.Text.RegularExpressions;
using UnityEngine.SceneManagement;


[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(CapsuleCollider))]
[RequireComponent(typeof(Rigidbody))]
public class player : NetworkBehaviour
{

    public float animSpeed = 1.5f;              // a public setting for overall animator animation speed
    public float lookSmoother = 3f;             // a smoothing setting for camera motion
    private string[] lectureForGrade1 = { "Java", "C/C++", "高等数学" };
    private string[] lectureForGrade2 = { "数据结构","离散数学","概率论与数理统计","计算机组成原理","数据库原理"};
    private string[] lectureForGrade3 = {"计算机网络","嵌入式系统","面向对象分析","人工智能","操作系统" };
    private string[] lectureForGrade4 = { "软件工程"};
    private Color[] colorlist = { Color.blue, Color.green, Color.yellow, Color.red };

   
    //public bool iswin = false;

    private int haveDoneStudy=0; // 统计已经修读完的课程数
    [SerializeField] private Material[] playerColors;
    private Animator anim;                          // a reference to the animator on the character
    private CapsuleCollider col;                    // a reference to the capsule collider of the character
    private int myid;
    public string playerName;
    private Text numOfPlayers;
    public int nowpositionID;
    public Color mycolor;
    private Text tuoGuan;




    private void Awake()
    {
       
         current = GameObject.Find("1").GetComponent<board>();
         nowpositionID = 1;
         initialBoard = GameObject.Find("1").GetComponent<board>();
         Debug.Log("player awake");
         

        GameObject.Find("托管").GetComponent<Button>().onClick.AddListener(toBeAAI);
        tuoGuan = GameObject.Find("托管").transform.Find("Text").GetComponent<Text>();
    }


    public void toBeAAI()
    {
        if (isLocalPlayer)
        {
            if (isAI)  //当前是托管状态
            {
                SetIsAI(false);
                tuoGuan.text = "托管";
            }
            else
            {
                SetIsAI(true);
                tuoGuan.text = "取消托管";
            }
        }

    }








    [Command]
    public void CmdSystem_Info(string playname, string info)
    {
        Debug.Log("服务器收到指令");
        RpcSystem_Info(playname, info);
    }

    //通知每个客户端
    [ClientRpc]
    void RpcSystem_Info(string playname, string info)
    {
        Debug.Log("客户端更新信息栏 ");
        doSystem_Info(playname, info);
    }

    void doSystem_Info(string playname, string info)
    {
         System_Info.instance.mydisplay(playname, info);
    }



    public void updatecount(int count)
    {

        CmdUpdatecount(count);
    }

    [Command]
    void CmdUpdatecount(int count)
    {
        Debug.Log("共有玩家 " + GamePlay.playerList.Count);
        RpcUpdatecount(count);
    }

    //通知每个客户端
    [ClientRpc]
    void RpcUpdatecount(int c)
    {
        setCount(c);
    }

    public void setCount(int c)
    {
        Debug.Log("客户端更新 " + c);
        play.count = c;
    }


    [Command]
    public void CmdEnd(bool isend)
    {
        RpcEnd(isend);
    }

    [ClientRpc]
    void RpcEnd(bool isend)
    {
        Gameover(isend);
    }

    public void Gameover(bool isend)
    {
        if (isend)    //isend
        {
            play.someoneWin = true;
            play.winname = this.playerName;
           
            SceneManager.LoadScene("结束游戏");
            
        }
       

    }



    public void setdisapper()
    {
       
        CmdSetDisapper(play.count, false);
    }

    public void setrecover()
    {
        CmdSetDisapper(play.count, true);
      
    }


    [Command]
    public void CmdSetDisapper(int count,bool isend)
    {
        RpcSetDisapper(count,isend);
    }

    [ClientRpc]
    void RpcSetDisapper(int count,bool isend)
    {
        setPlayActive(count,isend);
    }

    void setPlayActive(int count,bool isend)
    {
        GamePlay.playerList[count].transform.gameObject.SetActive(isend);

    }






    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        col = gameObject.GetComponent<CapsuleCollider>();
        Debug.Log("player start");
       
        GamePlay.playerList.Add(this);

        myid = GamePlay.playerList.Count - 1;
        mycolor = colorlist[myid];
        transform.Find("MicroMale").GetComponent<SkinnedMeshRenderer>().material.color = mycolor;
        this.SetPlayerName(GamePlay.nameList[myid]);//初始化角色姓名


        if (isLocalPlayer)
        {
           
            this.SetInfoTracker(GameObject.Find("tracker").GetComponent<Info_Tracker>());
            Initialize();
            GameObject.Find("Character").GetComponent<char_carmra>().target = gameObject.transform;
        }
        else
        {
            if (GameObject.Find("otherTracker").transform.Find("otherTrackerA").gameObject.activeSelf == false)
            {
               
                GameObject.Find("otherTracker").transform.Find("otherTrackerA").gameObject.SetActive(true);
                this.SetInfoTracker(GameObject.Find("otherTracker").transform.Find("otherTrackerA").GetComponent<Info_Tracker>());
            }
            else if(GameObject.Find("otherTracker").transform.Find("otherTrackerB").gameObject.activeSelf == false)
            {
              
                GameObject.Find("otherTracker").transform.Find("otherTrackerB").gameObject.SetActive(true);
                this.SetInfoTracker(GameObject.Find("otherTracker").transform.Find("otherTrackerB").GetComponent<Info_Tracker>());
            }
            else
            {
                
                GameObject.Find("otherTracker").transform.Find("otherTrackerC").gameObject.SetActive(true);
                this.SetInfoTracker(GameObject.Find("otherTracker").transform.Find("otherTrackerC").GetComponent<Info_Tracker>());
            }



            Initialize();
        }


        // Debug.Log("abc"+transform.position);
    }

  
    void Update()
    {
        AdjustBalance(0);//初始化金钱
        AdjustLectureDone(0);//初始化修读课程数
        AdjustHealth(0);//初始化健康
        AdjustJobRank(0);//初始化打工熟练度
       // play.count = count;
        // numOfPlayers.text = "玩家数目: " + fuck;
       // Debug.Log(transform.position);
    }

    

    /**
     用户名区域
     */
    public void SetPlayerName(string playerName)
    {
        this.playerName = playerName;
    }
    public String getPlayerName()
    {
        return playerName;
    }
    //判断是否是AI
    private bool isAI = false;
    public void SetIsAI(bool isAI)
    {
        this.isAI = isAI;
    }
    public bool IsAI()
    {
        return this.isAI;
    }
    public board initialBoard;
    public board current; //当前所在的地块
    
    public void SetStart(board start)
    {
        this.current = start;
       
        Debug.Log(nowpositionID);
        transform.position = this.current.transform.position;
    }

    public void updateLocation(board start)
    {
        this.current = start;
        nowpositionID++;
        nowpositionID = nowpositionID % 84;
       // start.getlist();
        transform.position = this.current.transform.position;
    }

    public void setnowpositionID(int id)
    {
        this.nowpositionID = id;
        current = current.getlist()[id];
    }
    



    public  Info_Tracker tracker;
    //初始化信息追踪器和玩家名称
    public void SetInfoTracker(Info_Tracker tracker)
    {
        this.tracker = tracker;
        tracker.initial();
        this.tracker.updatename(playerName);
    }
     
    private int accountbalance = 1000;
    //修改化钱
    public void AdjustBalance(int balance)
    {
        this.accountbalance += balance;
        this.tracker.updatebalance(accountbalance);
    }


    
    //getter
    public int getBalance()
    {
        return accountbalance;
    }
    

    /**
    修改健康值区域
    */
    private int playerMaxhealth = 20;//体力上限
    //获得健康上限
    public int getMaxHealth()
    {
        return playerMaxhealth;
    }
    //修改健康上限
    public void setMaxHealth(int max)
    {
        if (max < 0)
        {
            this.playerMaxhealth = 0;
        }
        else
        {
            this.playerMaxhealth = max;
        }
       
    }
    private int playerhealth = 20;
    //修改健康值
    public void AdjustHealth(int health)
    {

        this.playerhealth += health;
        if (playerhealth > playerMaxhealth)
        {
            playerhealth = playerMaxhealth;
        }
        this.tracker.updatehealth(playerhealth, playerMaxhealth);

    }



    //getter
    public int getHealth()
    {
        return playerhealth;
    }

    private int playergrade = 1;
    
    //getter
    public int getGrade()
    {
        return playergrade;
    }
    private int curLecture = 0;//标记下一修读课程再课程数组中的坐标
    //获得下一修读课程的坐标
    public int getCurLecture()
    {
        return this.curLecture;
    }
    //设置下一修读课程的坐标
    public void setCurLecture(int cur)
    {
        this.curLecture = cur;
    }
    //获得已修读课程数
    
    public int getLectureHaveDone()
    {
        return this.haveDoneStudy;
    }
   


    
    //更新已学课程数和角色年级
    public void AdjustLectureDone(int lectureNum)
    {
        this.haveDoneStudy += lectureNum;
        this.tracker.updatelecturehavedone(this.haveDoneStudy);
        if (this.haveDoneStudy >= 0 && this.haveDoneStudy < 3)
        {
            this.playergrade = 1;
            this.tracker.updategrade(playergrade);
        }
        else if (this.haveDoneStudy >= 3 && this.haveDoneStudy < 8)
        {
            this.playergrade = 2;
            this.tracker.updategrade(playergrade);
        }
        else if (this.haveDoneStudy >= 8 && this.haveDoneStudy < 13)
        {
            this.playergrade = 3;
            this.tracker.updategrade(playergrade);
        }
        else if (this.haveDoneStudy >= 13 && this.haveDoneStudy < 14)
        {
            this.playergrade = 4;
            this.tracker.updategrade(playergrade);
        }
        else {
            this.playergrade = 4;
            this.tracker.updategrade(playergrade);
        }
        AdjustJobRank(0);
    }
    
    //获得当前年级的课程列表
    public string[] getLectureList()
    {
        if (this.playergrade == 1)
        {
            return lectureForGrade1;
        }else if (this.playergrade == 2)
        {
            return lectureForGrade2;
        }else if (this.playergrade == 3)
        {
            return lectureForGrade3;
        }
        else
        {
            return lectureForGrade4;
        }
    }

    private int jobrank = 0; //打工等级
    private string rankName = ""; //等级名字
    public void setRankName(string name)
    {
        this.rankName = name;
    }
    public string[]  jobRankName = { "菜鸟", "入门", "老手", "大老板" };
    //修改打工等级
    public void AdjustJobRank(int rank)
    {
        this.jobrank += rank;
        if (jobrank > 6 && playergrade >= 3)
        {
            rankName = "大老板";
        }
        else if (jobrank >= 5 && playergrade >= 2)
        {
            rankName = "老手";
        }
        else if (jobrank >= 3)
        {
            rankName = "入门";
        }
        else
        {
            rankName = "菜鸟";
        }
        this.tracker.updatejobrank(rankName,jobrank);
    }
    //最后修改钱数
    public IEnumerator updateWorkMoney()
    {
        Debug.Log(rankName);
        switch (rankName)
        {
            case "大老板":
                AdjustBalance(2000);
                CmdSystem_Info(this.playerName, "通过打工获得2000元");
                break;
            case "老手":
                AdjustBalance(1200);
                CmdSystem_Info(this.playerName, "通过打工获得1200元");
                break;
            case "入门":
                AdjustBalance(800);
                CmdSystem_Info(this.playerName, "通过打工获得800元");
                break;
            case "菜鸟":
                AdjustBalance(500);
                CmdSystem_Info(this.playerName, "通过打工获得500元");
          
                break;
        }
        AdjustJobRank(1);
        CmdSystem_Info(this.playerName, "当前熟练度等级为" + this.rankName);
        yield return null;
    }
    //科研部分
    private int researchProcess = 0;
    //更新科研进度
    public void AdjustResearch(int process)
    {
        this.researchProcess += process;
    }
    //获得当前科研进度
    public int getProcess()
    {
        return this.researchProcess;
    }

    //打工过程，每回合减一
    public void updateToWork()
    {
        roundToWait -= 1;
        if (roundToWait == 0)
        {
            outtoshow = "打工";
        }
    }
    //休息过程，每回合减一
    public void updateToRelax()
    {
        roundToWait -= 1;
        if (roundToWait == 0)
        {
            outtoshow = "宿舍";
        }
    }
    //学习过程，每回合减一
    public void updateToStudy()
    {
        roundToWait -= 1;
        if (roundToWait == 0)
        {
            outtoshow = "学习";
        }
    }
    //离校过程，每回合减一
    public void updateToOut()
    {
        roundToWait -= 1;
        if (roundToWait == 0)
        {
            outtoshow = "离校";
        }
    }
    //科研过程，每回合减一
    public void updateToRes()
    {
        roundToWait -= 1;
        if (roundToWait == 0)
        {
            outtoshow = "科研";
        }
    }
    //等级名字getter
    public string getRankName()
    {
        return this.rankName;
    }
    //buff卡
    private string bufferCard = "无";
    //buff状态
    private string playerBuffer = "";
    //修改buffer牌
    public void AdjustBufferCard(string bufferCard)
    {

        this.bufferCard = bufferCard;
        this.tracker.updatebuffercard(this.bufferCard);
    }
    //getter
    public string getBufferCard()
    {
        return bufferCard;
    }
    public int next=0;//用于判断是否暂时提升了熟练度
    public bool use = false;//用于判断是否使用了技能卡

    
    public void AdjustBuffer(string buffer)//修改buff状态
    {
        this.playerBuffer = buffer;//更新buff状态
        this.tracker.updatebuffercard("无");//使用之后显示buff牌清空
    }
    public void SetBuffer()
    {
        this.playerBuffer = "";
    }
    //等待回合操作
    private int roundToWait = 0;
    private string outtoshow = "";//play中用来判断，默认为空
    public void resetOuttoShow()
    {
        outtoshow = "";
    }
    public string getoutToShow()
    {
        return outtoshow;
    }
   public void setOuttoShow(string outtoshow)
    {
        this.outtoshow = outtoshow;
    }
    public void setToWait(int i)
    {
        roundToWait = i;
    }
    public int getRoundToWait()
    {
        return roundToWait;
    }


    //初始化角色属性显示
    public void Initialize()
    {

        //this.SetPlayerName("RPJ");


            this.SetPlayerName(this.playerName);//初始化角色姓名
            this.SetInfoTracker(this.tracker);//初始化追踪器
            
            this.AdjustBufferCard(this.bufferCard);
            this.AdjustBalance(0);//初始化金钱
            this.AdjustLectureDone(0);//初始化修读课程数
            this.AdjustHealth(0);//初始化健康
            this.AdjustJobRank(0);//初始化打工熟练度

    }

    //联网使用，同步课程数
    public void setAll(int haveDoneStudy,int accountbalance,int playerhealth, int playerMaxhealth,int jobrank)
    {
        this.haveDoneStudy = haveDoneStudy;
        this.accountbalance = accountbalance;
        this.playerhealth = playerhealth;
        this.playerMaxhealth = playerMaxhealth;
        this.jobrank = jobrank;
    }

   





    //初始化角色属性显示
    public void updateall()
    {

        Cmdupdateall(play.count,haveDoneStudy, accountbalance, playerhealth, playerMaxhealth, jobrank,nowpositionID);

    }

    [Command]
    void Cmdupdateall(int count,int haveDoneStudy, int accountbalance, int playerhealth, int playerMaxhealth, int jobrank,int nowpositionID)
    {
       // Debug.Log("服务器更新数据" + GamePlay.instance.getCount());
        Rpcupdateall(count, haveDoneStudy, accountbalance, playerhealth, playerMaxhealth,jobrank, nowpositionID);
    }
    //通知每个客户端
    [ClientRpc]
    void Rpcupdateall(int count, int haveDoneStudy, int accountbalance, int playerhealth, int playerMaxhealth, int jobrank,int nowpositionID)
    {
       // Debug.Log("客户端更新数据" + count);
        GamePlay.playerList[count].setAll(haveDoneStudy, accountbalance, playerhealth, playerMaxhealth, jobrank);
        GamePlay.playerList[count].setnowpositionID(nowpositionID);
       // GamePlay.playerList[count].SetStart(current);
    }






    //判断玩家是否获胜
    public bool checkWin()
    {
        if(this.haveDoneStudy==14 || this.researchProcess==3|| this.accountbalance >= 35000)  //50000
        {
            return true;
        }
        return false;
    }
    //角色旋转
    public IEnumerator RotateAdditionalDegrees(float degrees, float timeforRotate)
    {
        
        float coefficient = 0;
        float starttime = Time.time;
        float startAngle = transform.eulerAngles.y;
        
        while (coefficient < .98f)
        {
            coefficient = (Time.time - starttime) / timeforRotate;
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, startAngle + degrees * coefficient, transform.eulerAngles.z);
            
            yield return null;
        }
        
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, startAngle + degrees, transform.eulerAngles.z);
       
    }
    
    //移动动作的实现
    public IEnumerator MovetoSpace(board space, float timeformove)
    {
        //Debug.Log("地块的坐标"+space.transform.position);
        float starttime = Time.time;
        Vector3 startPostion = transform.position;
        Vector3 endPostion = space.gameObject.transform.position;
        Vector3 dispalcement = endPostion - startPostion;
        dispalcement.y = 0;

        float cofficient = 0;
        while (cofficient <= .98f)
        {
            cofficient = (Time.time - starttime) / timeformove;
            Vector3 newPosition = startPostion + dispalcement * cofficient;
            transform.position = newPosition;
            yield return null;
        }
        updateLocation(space);
       // this.current = space;
        //transform.position = this.current.transform.position;
        //Debug.Log("角色的坐标" + transform.position);
    }





    //根据骰子结果移动
    public IEnumerator MoveSpaces(int spaces)
    {
        //Debug.Log("移动时的坐标" + transform.position);
        bool movingForward = spaces > 0;
        spaces = Math.Abs(spaces);
        for(int i = 0; i < spaces; i++)
        {
            board target = movingForward ? this.current.next: this.current.preceding;
            this.current.Pass(this); //经过该地块时触发的函数
            float timeformove = .8f * (Mathf.Sqrt((i * 1.0f) / spaces + .8f) - .35f);
            yield return MovetoSpace(target, timeformove);
            Regex reg1 = new Regex("右转");
            Regex reg2 = new Regex("左转");
            if (reg1.IsMatch(this.current.name))//如果是右转
            {
                yield return RotateAdditionalDegrees(movingForward ? 90 : -90, .5f);
            }else if (reg2.IsMatch(this.current.name))//如果是左转
            {
                yield return RotateAdditionalDegrees(movingForward ? -90 : 90, 1f);
            }

        }
        yield return current.LandOn(this);
    }


   
}
