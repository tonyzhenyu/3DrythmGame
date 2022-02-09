using UnityEngine;

public class RythmGameMgr : Singleton<RythmGameMgr>
{
    public GameLogic mainLogic;

    private NoteControler noteControler;

    public KeyNote[] keyNotes;
    public float speed = 5;

    private void OnEnable()
    {
        noteControler = this.GetComponent<NoteControler>();
    }
    private void Awake()
    {
        DontDestroyOnLoad(this);
        Init();
    }
    private void Start()
    {
        
    }

    private void Init()
    {
        //Load map
        keyNotes = new KeyNote[50];
        mainLogic = new GameLogic(keyNotes,60000,150);

        mainLogic.failedAction += () =>
        {
            //StartCoroutine(mainLogic.StopTime());
        };

    }

    private void Update()
    {
        mainLogic.GetCurrentKey();
        mainLogic.Update();
        
    }
}
