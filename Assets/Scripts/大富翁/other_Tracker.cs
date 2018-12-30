using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class other_Tracker : MonoBehaviour
{
    [SerializeField] private Text nameField, balanceField, healthField, gradeField, jobField, lectureField;


    void Awake()
    {
        initial();
    }
    public void initial()
    {

        nameField = transform.Find("Name").GetComponent<Text>();
        balanceField = transform.Find("Balance").GetComponent<Text>();
        healthField = transform.Find("Health").GetComponent<Text>();
        gradeField = transform.Find("Grade").GetComponent<Text>();
        jobField = transform.Find("Job").GetComponent<Text>();
        lectureField = transform.Find("LectureHaveDone").GetComponent<Text>();

    }


    //玩家名称
    public void updatename(string name)
    {
        this.nameField.text = name;
    }
    //玩家持有金额
    public void updatebalance(int balance)
    {

        this.balanceField.text = "金钱: " + balance;
    }
    //玩家健康值
    public void updatehealth(int health, int maxHealth)
    {
        this.healthField.text = "健康: " + health + "\\" + maxHealth;
    }
    //玩家年级
    public void updategrade(int grade)
    {
        this.gradeField.text = "年级: 大 " + grade;
    }
    //玩家打工熟练等级
    public void updatejobrank(string name, int rank)
    {

        this.jobField.text = "打工: " + name + "(" + rank + ")";
    }
    //玩家修读课程数
    public void updatelecturehavedone(int lecturehavedone)
    {
        this.lectureField.text = "已修课程数：" + lecturehavedone;
    }
  
}
