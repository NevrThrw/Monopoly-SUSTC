using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text.RegularExpressions;

public class MyChoiceAlert : MonoBehaviour
{

    public static MyChoiceAlert instance;
    Regex study = new Regex("学习");
    Regex research = new Regex("科研");
    Regex goout = new Regex("离校");
    Regex relax = new Regex("休息");
    Regex job = new Regex("打工");
    Regex cure = new Regex("回复");
    void Awake()
    {
        instance = this;
        Button ok = transform.Find("Panel").Find("Gotowork").GetComponent<Button>();
        ok.onClick.AddListener(Affirmative);
        Button notok = transform.Find("Panel").Find("Cancel").GetComponent<Button>();
        notok.onClick.AddListener(Negative);

    }

    [SerializeField] private Text choiceDialog;
    [SerializeField] private Button affirmative, negative;
    [SerializeField] private Text affirmativeText, negativeText;
    [HideInInspector] public bool decisionMade = false, resultingDecision = false;

    public IEnumerator CreateChoiceAlert(string message, string affirmativeText, string negativeText)//玩家做出选择
    {
        decisionMade = false;
        resultingDecision = false;

        // Initialize UI elements.  
        choiceDialog.text = message;
        this.affirmativeText.text = affirmativeText;
        this.negativeText.text = negativeText;

        transform.GetChild(0).gameObject.SetActive(true);

        while (!decisionMade)//做了决定没
        {
            if (GamePlay.playerList[play.count].IsAI())
            {
                yield return new WaitForSeconds(1);
                if (study.IsMatch(message))
                {
                    yield return AI.instance.makeDesicision(GamePlay.playerList[play.count], "学习");
                    //if (resultingDecision == true)
                    //{
                    //    yield return GamePlay.instance.useBufferCard(GamePlay.playerList[play.count],"学习");//使用buff卡
                    //}
                }
                else if (research.IsMatch(message))
                {
                    yield return AI.instance.makeDesicision(GamePlay.playerList[play.count], "科研");
                    //if (resultingDecision == true)
                    //{
                    //    yield return GamePlay.instance.useBufferCard(GamePlay.playerList[play.count], "科研");//使用buff卡
                    //}
                }
                else if (goout.IsMatch(message))
                {
                    yield return AI.instance.makeDesicision(GamePlay.playerList[play.count], "离校");

                }
                else if (relax.IsMatch(message))
                {
                    yield return AI.instance.makeDesicision(GamePlay.playerList[play.count], "休息");

                }
                else if (job.IsMatch(message))
                {
                    yield return AI.instance.makeDesicision(GamePlay.playerList[play.count], "打工");
                    //if (resultingDecision == true)
                    //{
                    //    yield return GamePlay.instance.useBufferCard(GamePlay.playerList[play.count], "打工"); ;//使用buff卡
                    //}
                }
                else if (cure.IsMatch(message))
                {
                    yield return AI.instance.makeDesicision(GamePlay.playerList[play.count], "治疗");
                }
            }
            yield return null;
        }
        transform.GetChild(0).gameObject.SetActive(false);
    }

    public void Affirmative()//点击按钮时触发的函数
    {
        resultingDecision = true;
        decisionMade = true;
        Debug.Log("肯定");
    }

    public void Negative()
    {
        resultingDecision = false;
        decisionMade = true;
        Debug.Log("否定");
    }

}
