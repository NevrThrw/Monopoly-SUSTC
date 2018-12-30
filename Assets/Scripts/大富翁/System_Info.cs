using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class System_Info : MonoBehaviour {

    private Text info;
    private ScrollRect scrollrect;
    public static System_Info instance;

	// Use this for initialization
	void Awake () {
        instance = this;
        this.info = transform.Find("Panel").Find("Info").GetComponent<Text>();
        this.scrollrect = transform.Find("Panel").GetComponent<ScrollRect>();
        //instance.Display("", "初始化完成");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void mydisplay(string playerName, string inforamtion)
    {
        string addText = "\n  " + "<color=red>" + playerName + "</color>: " + inforamtion;
        info.text += addText;
        if (info.text.Length >= 40000)//防止系统信息溢出
        {
            info.text = info.text.Substring(20000);
        }
        Canvas.ForceUpdateCanvases();
        scrollrect.verticalNormalizedPosition = 0f;
        Canvas.ForceUpdateCanvases();
    }




    //public IEnumerator Display(string playerName, string inforamtion)
    //{
    //    string addText = "\n  " + "<color=red>" + playerName + "</color>: " + inforamtion;
    //    info.text += addText;
    //    if (info.text.Length >= 40000)//防止系统信息溢出
    //    {
    //        info.text = info.text.Substring(20000);
    //    }
    //    Canvas.ForceUpdateCanvases();
    //    scrollrect.verticalNormalizedPosition = 0f;
    //    Canvas.ForceUpdateCanvases();
    //    yield return null;
    //}
}
