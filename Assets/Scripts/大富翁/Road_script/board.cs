using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class board : MonoBehaviour {
    public board preceding, next;
    private board[] lis;
    private void Awake()
    {
        string currentSpace = gameObject.name;
        Transform parent = gameObject.transform.parent;
        lis = parent.GetComponentsInChildren<board>();
        int num = -1;
        for(int i = 0; i < lis.Length; i++)
        {
            if (parent.GetChild(i).transform.name == currentSpace)
            {
                num = i;
            }
        }
        next = currentSpace == "66" ? lis[0] : lis[num + 1];
        preceding = currentSpace == "一号门" ? lis[lis.Length - 1] : lis[num - 1];
        AdditionalInit();
    }
    //返回地块列表
    public board[] getlist()
    {
        return lis;
    }

    protected virtual void AdditionalInit() { }
    public abstract void Pass(player player); //经过时触发的代码
    public abstract IEnumerator LandOn(player player);//停下时触发的代码
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    
}
