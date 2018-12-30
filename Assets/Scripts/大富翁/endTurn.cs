using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class endTurn : MonoBehaviour
{
    public static endTurn instance;
   
    private void Awake()
    {
        instance = this;
        Button end = transform.GetChild(0).GetComponent<Button>();
        end.onClick.AddListener(OnClick);
        transform.GetChild(0).gameObject.SetActive(false);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    [HideInInspector]
    public bool decisionMade = false;

    public void OnClick()
    {
        this.decisionMade = true;
    }
    public IEnumerator endturn(player player)
    {
        decisionMade = false;
        transform.GetChild(0).gameObject.SetActive(true);
        while (!decisionMade)
        {
            if (player.IsAI())
            {
                yield return new WaitForSeconds(2);
                this.decisionMade = true;
            }
            yield return null;
        }
        transform.GetChild(0).gameObject.SetActive(false);
    }
}
