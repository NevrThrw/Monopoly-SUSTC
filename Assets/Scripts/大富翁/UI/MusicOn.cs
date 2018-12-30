using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MusicOn : MonoBehaviour
{
    public Image _message1;
    public Image _message2;
    public void onBtp()
    {
        _message1.gameObject.SetActive(false);
        _message2.gameObject.SetActive(true);
    }
}
