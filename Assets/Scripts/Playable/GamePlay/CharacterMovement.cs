using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    RythmGameMgr Mgr { get => RythmGameMgr.Instance; }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //set character position
        transform.position = new Vector3(0, Mgr.mainLogic.CurrentTime01 * Mgr.speed, 0);
    }
}
