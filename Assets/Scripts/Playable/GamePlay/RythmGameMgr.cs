using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class RythmGameMgr : MonoBehaviour
{
    public GameLogic mainLogic;

    public GameObject cam;
    public GameObject aobj;
    public GameObject ex;

    private GameObject[] objs;
    private KeyNote[] keyNotes1;

    public float speed = 5;
    private void Start()
    {
        keyNotes1 = new KeyNote[50];
        objs = new GameObject[50];
        mainLogic = new GameLogic(keyNotes1);
        mainLogic.mapLength = 60000;

        for (int i = 0; i < 50; i++)
        {
            
            //keyNotes1[i] = new KeyNote(0, 1, - mainLogic.inputWidth + 200, i * 1000 + 3000);
            keyNotes1[i] = new KeyNote(0, 1, 0, i * 1000 + 3000);

            objs[i] = GameObject.Instantiate(aobj, new Vector3(0, keyNotes1[i].beatTime / 1000 * speed , 0), Quaternion.identity);
            objs[i].transform.position += new Vector3(0, keyNotes1[i].length / 1000 * speed / 2 , 0);
            //objs[i].transform.localScale = new Vector3(1,Mathf.Abs(keyNotes1[i].length)/ 100 * speed, 1);
        }

        mainLogic.tickAction += (arg1) =>
        {
            if (arg1 < 1f && arg1 >= -1f)
            {
                GameObject fx = Instantiate(ex, new Vector3(0, mainLogic.CurrentTime / 1000 * speed, 0), Quaternion.identity);
                Destroy(fx, 0.5f);
            }
        };

        mainLogic.lengthAction += (arg1) =>
        {
            if (arg1 < 1f && arg1 >= -1f)
            {
                GameObject fx = Instantiate(ex, new Vector3(0, mainLogic.CurrentTime / 1000 * speed, 0), Quaternion.identity);
                Destroy(fx, 0.5f);
            }
        };
    }
    private void Update()
    {
        mainLogic.Update();

        for (int i = 0; i < 50; i++)
        {
            objs[i].transform.position = new Vector3(0, keyNotes1[i].beatTime / 1000 * speed, 0);
        }

        cam.transform.position = new Vector3(0, mainLogic.CurrentTime / 1000 * speed, 0);

        //Debug.Log(mainLogic.Distance);

        mainLogic.GetCurrentKey();
    }
}


public class GameLogic
{
    private Action<KeyCode, KeyCode> checkKey;

    public Action<float> tickAction;
    public Action<float> lengthAction;
    public Action<float> failedAction;

    public float bpm = 120;
    public float factor = 1;
    public float mapLength = 1000;
    public float inputWidth = 250;

    public float MainDistance { get => (float)currentIndex / KeyNotes.Length; }
    public float Accuracy { get => accuracy; }
    public float Distance { get => distance; }
    public float CurrentTime { get => curGameTime * 1000; }
    public int CurrentIndex { get => currentIndex; }

    private KeyNote[] KeyNotes;

    private int currentIndex = 0;
    private float accuracy = 0;
    private float distance = 0;
    private float inputTimeTicker = 0;
    private float curGameTime = 0;

    private KeyCode CurrentHit;
    public GameLogic(KeyNote[] keyNotes, float bpm = 60, float factor = 1)
    {
        this.KeyNotes = keyNotes;
        this.bpm = bpm;
        this.factor = factor;


        Init();
    }

    public void Init()
    {
        checkKey += CheckMethod;
        tickAction += SuccessAccuracy;
        failedAction += FailedAccuracy;
        lengthAction += LengthAccuracy;

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

            if (Mathf.Abs(KeyNotes[currentIndex].length) > 0)
            {
                accuracy = (inputTimeTicker - KeyNotes[currentIndex].beatTime) / (inputWidth + KeyNotes[currentIndex].length);

                lengthAction?.Invoke(accuracy);

                return;

            }

            accuracy = (inputTimeTicker - KeyNotes[currentIndex].beatTime) / inputWidth;

            tickAction?.Invoke(accuracy);
        }
    }
    #endregion
    #region Seeking
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
        for (int i = 0; i < KeyNotes.Length; i++)
        {
            indexPackers.Add(new IndexPacker(i, KeyNotes[i].beatTime - CurrentTime));
        }

        indexPackers.Sort((a, b) =>
        {
            return Mathf.Abs(a.time).CompareTo(Mathf.Abs(b.time));
        });

        currentIndex = indexPackers[0].index + 1;
    }
    #endregion
    #region AccuracySuccess
    private void LengthAccuracy(float accuracy)
    {
        

        if (accuracy >= 1f)
        {
            JumpToNextNode();
        }
        else if (accuracy < 1f && accuracy >= -1f)
        {
            Debug.Log("lengthing");
        }
        else if (accuracy < -1f)
        {
            
        }
    }
    #endregion
    #region AccuracySuccess
    private void SuccessAccuracy(float accuracy)
    {

        if (accuracy >= 1f)
        {

        }
        else if (accuracy < 1f && accuracy >= -1f)
        {
            JumpToNextNode();
        }
        else if (accuracy < -1f)
        {

        }
    }
    #endregion
    #region AccuracyFailed
    private void FailedAccuracy(float arg)
    {
        if (arg >= 1f)
        {
            accuracy = 0;
            JumpToNextNode();
        }
        else if (arg < 1f && arg >= -1f)
        {
            
        }
        else if (arg < -1f)
        {
            
        }
    }
    #endregion
    #region SwitchNextNode
    private void JumpToNextNode()
    {
        if (currentIndex == KeyNotes.Length - 1)
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
            curGameTime += Time.deltaTime * factor;

            distance = CurrentTime / mapLength;
        }
        else return;

        if (KeyNotes[currentIndex].beatTime - CurrentTime >= -(inputWidth + KeyNotes[currentIndex].length))
        {
            checkKey?.Invoke(CurrentHit, KeyNotes[currentIndex].keyCode);
        }
        else if(KeyNotes[currentIndex].beatTime - CurrentTime < (inputWidth + KeyNotes[currentIndex].length))
        {
            failedAction?.Invoke((CurrentTime - KeyNotes[currentIndex].beatTime) / inputWidth);
            checkKey?.Invoke(CurrentHit, KeyNotes[currentIndex].keyCode);
        }
        //Debug.Log((curGameTime * 1000 - KeyNotes[currentIndex].beatTime) / inputWidth);
    }
    public void GetCurrentKey()
    {
        CurrentHit = KeyCode.None;

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            CurrentHit = KeyCode.LeftArrow;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            CurrentHit = KeyCode.RightArrow;
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            CurrentHit = KeyCode.UpArrow;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            CurrentHit = KeyCode.DownArrow;
        }
    }
    IEnumerator a()
    {
        yield return new WaitForSeconds(0);
    }
}