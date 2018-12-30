using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class normal : board {
    //经过
    public override void Pass(player player)
    {
       // player.AdjustBalance(100);
    }
    //停下
    public override IEnumerator LandOn(player player)
    {
        yield return null;
    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
