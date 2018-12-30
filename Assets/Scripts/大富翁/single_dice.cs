using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class single_dice : MonoBehaviour {

    
	

    public int getCount()
    {
        Transform a = transform.GetChild(0);

        for(int i = 0; i < transform.childCount; i++)
        {
            if(transform.GetChild(i).transform.position.y> a.transform.position.y)
            {
                a = transform.GetChild(i);
            }
        }
        return Int32.Parse(a.gameObject.name);
    }
}
