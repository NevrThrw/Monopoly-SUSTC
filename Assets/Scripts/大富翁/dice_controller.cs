using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dice_controller : MonoBehaviour
{

    public static dice_controller instance;
    private Vector3[] childePosition;
    public int total;
    private int[] res;

    public int[] getRes()
    {
        return res;
    }
    private void Awake()
    {
        instance = this;
        childePosition = new Vector3[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            childePosition[i] = transform.GetChild(i).transform.position;
            transform.GetChild(i).gameObject.SetActive(false);
        }
    }
    private void Update()
    {
        //if (Input.GetMouseButtonDown(0))
        //{
        //    StartCoroutine(roll());
        //}
    }

    // Update is called once per frame
    public IEnumerator roll()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(true);
            transform.GetChild(i).transform.position = childePosition[i];
            transform.GetChild(i).transform.rotation = Random.rotation;
        }
        //Debug.Log("Aha");
        yield return new WaitForSeconds(2);
        //判断骰子是否停止运动
        while (true)
        {
            bool die = true;
            Rigidbody[] temp = gameObject.GetComponentsInChildren<Rigidbody>();
            for (int i = 0; i < temp.Length; i++)
            {
                if (!temp[i].IsSleeping())
                {
                    die = false;
                }
            }
            yield return null;
            if (die)
            {
                break;
            }
        }
        this.total = 0;
        single_dice[] a = gameObject.GetComponentsInChildren<single_dice>();
        res = new int[a.Length];

        for (int i = 0; i < a.Length; i++)
        {
            res[i] = a[i].getCount();
            //total += res[i];
        }
       // Debug.Log("the number is " + total);
    }

    
}

