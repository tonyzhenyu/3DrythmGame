using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameLogic
{
    private Action<KeyCode, KeyCode> checkKey;

    public Action tickAction;
    public Action lengthAction;
    public Action failedAction;

    public float factor = 1;
    
    public int Combo { get => combo; }
    public float MainDistance { get => (float)currentIndex / keyNotes.Length; }
    public float Accuracy { get => accuracy; }
    public float Distance { get => distance; }
    public float CurrentTime { get => curGameTime * 1000; }
    public float CurrentTime01 { get => curGameTime; }
    public int CurrentIndex { get => currentIndex; }
    public KeyNote[] Keynotes { get => keyNotes; }

    private int combo = 0;
    private float inputWidth = 250;
    private KeyNote[] keyNotes;
    private int currentIndex = 0;
    private float mapLength = 1000;
    private float accuracy = 0;
    private float distance = 0;
    private float inputTimeTicker = 0;
    private float curGameTime = 0;
    private float stopT = 1;
    private KeyCode currentHit;

    public GameLogic(KeyNote[] keyNotes,float mapLength, float inputWidth = 250)
    {
        this.keyNotes = keyNotes;
        this.inputWidth = inputWidth;
        this.mapLength = mapLength;

        Init();
    }

    public void Init()
    {
        checkKey += CheckMethod;
        tickAction += () =>
        {
            combo += 1;
        };

        combo = 0;
        currentIndex = 0;
        inputTimeTicker = 0;
        curGameTime = 0;
    }
    #region CheckNoteMethod
    private void CheckMethod(KeyCode arg1, KeyCode arg2)
    {
        if (arg1 == arg2)
        {
            
            float time = CurrentTime;

            inputTimeTicker = time;

            if (Mathf.Abs(keyNotes[currentIndex].length) > 0)
            {
                accuracy = (inputTimeTicker - keyNotes[currentIndex].beatTime) / (inputWidth + keyNotes[currentIndex].length);

                //Lengthing Acc Caculate
                if (accuracy >= 1f)
                {
                    JumpToNextNode();
                }
                else if (accuracy < 1f && accuracy >= -1f)
                {
                    Debug.Log("lengthing");
                    lengthAction?.Invoke();
                }
                /*else if (accuracy < -1f)
                {

                }*/

                return;

            }

            accuracy = (inputTimeTicker - keyNotes[currentIndex].beatTime) / inputWidth;

            /*if (accuracy >= 1f)
            {

            }
            else */if (accuracy < 1f && accuracy >= -1f)
            {
                tickAction?.Invoke();
                JumpToNextNode();
            }
            /*else if (accuracy < -1f)
            {

            }*/
        }
        else if (arg1 != KeyCode.None)
        {
            FailedAccuracy((CurrentTime - keyNotes[currentIndex].beatTime) / inputWidth);
        }
    }
    #endregion
    #region SeekingNote
    struct IndexPacker
    {
        public int index;
        public float time;

        public IndexPacker(int index, float time)
        {
            this.index = index;
            this.time = time;
        }
    }

    private void SeekingPoint()
    {
        List<IndexPacker> indexPackers = new List<IndexPacker>();
        for (int i = 0; i < keyNotes.Length; i++)
        {
            indexPackers.Add(new IndexPacker(i, keyNotes[i].beatTime - CurrentTime));
        }

        indexPackers.Sort((a, b) =>
        {
            return Mathf.Abs(a.time).CompareTo(Mathf.Abs(b.time));
        });

        currentIndex = indexPackers[0].index + 1;
    }
    #endregion
    #region AccuracyFailed
    private void FailedAccuracy(float arg)
    {
        if (arg >= 1f)
        {
            combo = 0;
            accuracy = 0;
            failedAction?.Invoke();
            JumpToNextNode();
        }
    }
    #endregion
    #region SwitchNextNode
    private void JumpToNextNode()
    {
        if (currentIndex == keyNotes.Length - 1)
        {
            return;
        }
        Debug.Log("time:" + curGameTime + ",index:" + currentIndex + ",Acc:" + accuracy);

        currentIndex++;
    }
    #endregion
    public void Update()
    {
        if (CurrentTime <= mapLength)
        {
            curGameTime += Time.deltaTime * factor * stopT;

            distance = CurrentTime / mapLength;
        }
        else return;

        if (keyNotes[currentIndex].beatTime - CurrentTime >= -(inputWidth + keyNotes[currentIndex].length))
        {
            checkKey?.Invoke(currentHit, keyNotes[currentIndex].keyCode);
        }
        else if(keyNotes[currentIndex].beatTime - CurrentTime < (inputWidth + keyNotes[currentIndex].length))
        {
            
            FailedAccuracy((CurrentTime - keyNotes[currentIndex].beatTime) / inputWidth);
            checkKey?.Invoke(currentHit, keyNotes[currentIndex].keyCode);
        }
        //Debug.Log((curGameTime * 1000 - KeyNotes[currentIndex].beatTime) / inputWidth);
    }

    public void GetCurrentKey()
    {
        currentHit = KeyCode.None;

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            currentHit = KeyCode.LeftArrow;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            currentHit = KeyCode.RightArrow;
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            currentHit = KeyCode.UpArrow;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            currentHit = KeyCode.DownArrow;
        }
    }
    public IEnumerator StopTime()
    {
        stopT = 0;
        yield return new WaitForSeconds(0.1f);
        stopT = 1;
    }
}