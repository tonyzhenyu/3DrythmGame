using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class NoteGenerator : MonoBehaviour
{
    #region Properties
    public Vector3 trackSpot = new Vector3(0,0,0);      //track origin
    public int bpm = 120;                               //beat per minute
    public float trackDistance = 100;                   //100 seconds
    public float noteLenght = 1;                        //per notes length velocity
    public float roadLong = 20;                         //length of road floor

    public List<Note> notes;                            //notes
    public GameObject[] origins;                        //origin object object 0 : floor \ 1 : stoppanel

    private int notecount { get { return Mathf.FloorToInt(bpm / 60 * trackDistance); } }
    #endregion

    void Start()
    {
        Init();
        Generate();
    }
    
    private void Init()
    {
        GameObject.Find("Player").GetComponent<ZY.LocomotionHandler>().moveSpeed = CaculatePlayerVelocity();
    }
    
    private void Generate()
    {
        float tScale = bpm / 60 / 4;
        for (int i = 0; i < notecount; i++)
        {
            if (Random.value <= .5f)
            {
                continue;
            }
            Note note       = new Note();
            note.id         = i;
            note.timeSpot   = tScale*i;

            GameObject noteObjs = Instantiate(origins[Random.Range(2,origins.Length)],new ToolsFunction().FindOrCreate("NoteHolder"));
            noteObjs.name = $"note_obj{note.id}";

            //select right and left
            if (Random.value <= .5f)
            {
                noteObjs.transform.position = i * noteLenght * Vector3.forward + trackSpot + Vector3.left;
                note.ntype = NoteType.Left;
            }
            else
            {
                noteObjs.transform.position = i * noteLenght * Vector3.forward + trackSpot + Vector3.right;
                note.ntype = NoteType.Right;
            }
            
            note.GameObject = noteObjs;

            notes.Add(note);
        }

        //-- road
        for (int i = 0; i < CaculateRoadCount(roadLong) +1; i++)
        {
            GameObject roadObjs = Instantiate(origins[0], new ToolsFunction().FindOrCreate("Environment"));
            roadObjs.name = $"Road_{i}";
            roadObjs.transform.position += Vector3.forward * roadLong * i;
        }
        
        GameObject stopPanel = Instantiate(origins[1], new ToolsFunction().FindOrCreate("Environment"));
        stopPanel.name = $"{origins[1].name}";
        stopPanel.transform.position += Vector3.forward * roadLong * (CaculateRoadCount(roadLong)+.5f);
        stopPanel.GetComponent<ZY.PropsController>().OndestroyHandler.AddListener(new UnityAction(() => {
            GameObject.Find("Player").GetComponent<ZY.LocomotionHandler>().movable = false;
        }));
        
        //-- generate

    }

    float CaculateRoadCount(float roadDistance = 20)
    {
        return notecount * noteLenght / roadDistance;
    }

    float CaculatePlayerVelocity()
    {
        int temp = bpm / 60;
        int t = temp / 4;
        return noteLenght * (1 - t) * 4;//v = s/t
    }
}
[System.Serializable]
public class Note
{
    public int id;
    public NoteType ntype;
    public float timeSpot;
    public GameObject GameObject;
}
public enum NoteType
{
    Left,
    Right,
    Up,
    Down,

    LongLeft,
    LongRight,
    LongUp,
    LongDown,

    AvoidLeft,
    AvoidRight,
    AvoidUp,
    AvoidDown,

    UpLeft,
    UpRight,
    DownLeft,
    DownRight
}