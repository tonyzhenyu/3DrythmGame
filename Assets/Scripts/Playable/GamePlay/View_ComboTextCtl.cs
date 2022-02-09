using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class View_ComboTextCtl : MonoBehaviour
{
    RythmGameMgr mgr { get => RythmGameMgr.Instance; }

    private Text text;
    private Animation anima;
    void Start()
    {
        text = this.GetComponent<Text>();
        anima = this.GetComponent<Animation>();

        mgr.mainLogic.tickAction += () =>
        {
            anima.Play();
            if (mgr.mainLogic.Combo > 4)
            {
                text.text = "COMBO " + mgr.mainLogic.Combo.ToString();
                return;
            }

            if (Mathf.Abs(mgr.mainLogic.Accuracy) < 0.25f && Mathf.Abs(mgr.mainLogic.Accuracy) > 0)
            {
                text.text = "PERFACT";
            }
            else if (Mathf.Abs(mgr.mainLogic.Accuracy) < 0.75f && Mathf.Abs(mgr.mainLogic.Accuracy) > 0)
            {
                text.text = "GOOD";
            }

            
        };
        mgr.mainLogic.failedAction += () =>
        {
            text.text = "";
        };
    }

    // Update is called once per frame
    void Update()
    {

    }
}
