using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class char_carmra : MonoBehaviour {

    //public Transform targetp1;//p1
    //public Transform targetp2;//p2
    public Transform target;
    public float distanceUp = 13f;
    public float distanceAway = 12f;
    public float smooth = 4f;//位置平滑移动值
    public float camDepthSmooth = 5f;
    // Use this for initialization
    void Start()
    {
        Debug.Log("char carmra start");
    }

    // Update is called once per frame
    void Update()
    {
        // 鼠标轴控制相机的远近
        //if ((Input.mouseScrollDelta.y < 0 && Camera.main.fieldOfView >= 3) || Input.mouseScrollDelta.y > 0 && Camera.main.fieldOfView <= 80)
        //{
        //    Camera.main.fieldOfView += Input.mouseScrollDelta.y * camDepthSmooth * Time.deltaTime;
        //}
    }

    void LateUpdate()
    {

        //target = GamePlay.playerList[play.count].transform;
        if (target != null)
        {
            // 相机的位置
            Vector3 disPos = target.position + Vector3.up * distanceUp - target.forward * distanceAway;
            transform.position = Vector3.Lerp(transform.position, disPos, Time.deltaTime * smooth);
            //相机的角度
            transform.LookAt(target.position);
        }
          

            
        
     
    }
}
