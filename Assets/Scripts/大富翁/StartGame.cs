using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class StartGame : MonoBehaviour
{
    //string sceneName = "大富翁游戏界面_打工";
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnClick()
    {
        SceneManager.LoadScene("大富翁游戏界面_打工");
    }
}
