using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class Help : MonoBehaviour
{
    private bool set = true;
    private ScrollRect scrollrect;
    public Image _titleImage;
   

    private void Awake()
    {
        this.gameObject.SetActive(false);
        this.scrollrect = transform.Find("Image").GetComponent<ScrollRect>();
        GameObject.Find("Exit").GetComponent<Button>().onClick.AddListener(quitGame);
        GameObject.Find("Help").GetComponent<Button>().onClick.AddListener(showInstruction);

    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void showInstruction()
    {
        this.gameObject.SetActive(set);
        _titleImage.gameObject.SetActive(!set);
       

        if (set == true)
        {
            scrollrect.verticalNormalizedPosition = 1f;
        }
        set = !set;
    }

    public void loadScene()
    {
        SceneManager.LoadScene("大富翁游戏界面_打工");
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
