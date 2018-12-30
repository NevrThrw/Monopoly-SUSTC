using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class END : MonoBehaviour
{
    // Start is called before the first frame update
    private Text winplayer;

    void Start()
    {
        winplayer = GameObject.Find("胜利").transform.Find("人").GetComponent<Text>();
        winplayer.text = "游戏结束!!!"+"\n"+ play.winname+"在南科大富翁中获得了最终胜利";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void quitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();  
#endif
    }
}
