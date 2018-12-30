using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera_controller : MonoBehaviour {

    [SerializeField] private Camera map, character;
	// Use this for initialization
	void Start () {
        this.map =transform.Find("Map").GetComponent<Camera>();
        this.character =transform.Find("Character").GetComponent<Camera>();
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.X)) //如果敲击键盘的"X" 
        {
            map.enabled = true; //camera1激活 
            character.enabled = false; //camera0停止
        }
        if (Input.GetKey(KeyCode.Z)) //如果敲击键盘的"Z" 
        {
            map.enabled = false; //camera1激活 
            character.enabled = true; //camera0停止
        }
    }
}
