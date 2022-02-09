using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class View_ComboSpriteCtl : MonoBehaviour
{
    RythmGameMgr mgr { get => RythmGameMgr.Instance; }
    private Animation anima;
    // Start is called before the first frame update
    void Start()
    {
        anima = GetComponent<Animation>();
        mgr.mainLogic.tickAction += () =>
        {
            anima.Play();
        };
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
