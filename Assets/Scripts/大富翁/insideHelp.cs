using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class insideHelp : MonoBehaviour
{
    private bool set = true;
    private ScrollRect scrollrect;
   


    private void Awake()
    {
        this.gameObject.SetActive(false);
        this.scrollrect = transform.Find("Image").GetComponent<ScrollRect>();
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
       
        if (set == true)
        {
            scrollrect.verticalNormalizedPosition = 1f;
        }
        set = !set;
    }

  
  
}
