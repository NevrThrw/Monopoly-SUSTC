using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MyMessageAlert : MonoBehaviour
{
    public static MyMessageAlert instance;

    void Awake()
    {
        instance = this;
        Button ok = transform.Find("Panel").Find("Button").GetComponent<Button>();
        ok.onClick.AddListener(UserOK);
    }

    [SerializeField] private Text displayText;
    [SerializeField] private Button okButton;
    private bool userSaidOK = false;

    public IEnumerator DisplayAlert(string alert)//消息通知
    {
        displayText.text = alert;
        transform.GetChild(0).gameObject.SetActive(true);

        while (!userSaidOK)
            yield return null;

        transform.GetChild(0).gameObject.SetActive(false);
        userSaidOK = false;
    }

    public void UserOK()
    {
        userSaidOK = true;
    }
}