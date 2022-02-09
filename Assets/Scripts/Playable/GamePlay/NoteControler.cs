using UnityEngine;

public class NoteControler : MonoBehaviour
{
    public SO_NoteProfile so_NoteProfile;
    private GameLogic Logic { get => RythmGameMgr.Instance.mainLogic; }
    private float speed { get => RythmGameMgr.Instance.speed; }

    private GameObject[] objs_y;
    private GameObject[] objs_Lx;
    private GameObject[] objs_Rx;
    private Transform camSpace { get => Camera.main.transform; }

    private GameObject note_objs;
    private void Start()
    {
        note_objs = new GameObject("Note Objs");

        //y axis object
        objs_y = new GameObject[Logic.Keynotes.Length];
        objs_Lx = new GameObject[Logic.Keynotes.Length];
        objs_Rx = new GameObject[Logic.Keynotes.Length];

        for (int i = 0; i < Logic.Keynotes.Length; i++)
        {
            //Load Key
            //keyNotes1[i] = new KeyNote(0, 1, - mainLogic.inputWidth + 200, i * 1000 + 3000);
            Logic.Keynotes[i] = new KeyNote(0, 1, 0, i * 1000 + 3000);
            //

            //
            int index = 0;
            switch (Logic.Keynotes[i].rythmNoteType)
            {
                case RythmNoteType.Left:
                    index = 0;
                    break;
                case RythmNoteType.Right:
                    index = 1;
                    break;
                case RythmNoteType.Up:
                    index = 2;
                    break;
                case RythmNoteType.Down:
                    index = 3;
                    break;
                default:
                    break;
            }
            //

            objs_y[i] = GameObject.Instantiate(so_NoteProfile.note_obj[index], new Vector3(0, Logic.Keynotes[i].beatTime / 1000 * speed, 0), Quaternion.identity);
            objs_y[i].transform.position += new Vector3(0, Logic.Keynotes[i].length / 1000 * speed / 2, 0);

            if (Mathf.Abs(Logic.Keynotes[i].length) / 100 * speed < 1)
            {

            }
            else
            {
                objs_y[i].transform.localScale = new Vector3(1,Mathf.Abs(Logic.Keynotes[i].length)/ 100 * speed, 1);
            }

            objs_y[i].transform.parent = note_objs.transform;

            

            objs_Lx[i] = GameObject.Instantiate(so_NoteProfile.xScroll_obj);
            objs_Lx[i].transform.parent = camSpace;
            objs_Lx[i].transform.localRotation = Quaternion.identity;

            objs_Rx[i] = GameObject.Instantiate(so_NoteProfile.xScroll_obj);
            objs_Rx[i].transform.parent = camSpace;
            objs_Rx[i].transform.localRotation = Quaternion.identity;
        }

        //Action 
        Logic.tickAction += GenFX;
        Logic.lengthAction += GenFX;
    }
    private void Update()
    {
        //set position with speed
        for (int i = 0; i < objs_y.Length; i++)
        {
            objs_y[i].transform.position = new Vector3(0, Logic.Keynotes[i].beatTime / 1000 * speed, 0);
        }
        //set position with speed
        for (int i = 0; i < objs_Lx.Length; i++)
        {

            if (objs_Lx[i].activeSelf == false)
            {
                continue;
            }
            if (Logic.CurrentTime - Logic.Keynotes[i].beatTime > 0)
            {
                objs_Lx[i].SetActive(false);
            }

            objs_Lx[i].transform.localPosition = new Vector3((Logic.CurrentTime - Logic.Keynotes[i].beatTime) / 1000 * speed, 3.16f, 10);

        }
        for (int i = 0; i < objs_Rx.Length; i++)
        {

            if (objs_Rx[i].activeSelf == false)
            {
                continue;
            }
            if (Logic.CurrentTime - Logic.Keynotes[i].beatTime > 0)
            {
                objs_Rx[i].SetActive(false);
            }

            objs_Rx[i].transform.localPosition = new Vector3((Logic.Keynotes[i].beatTime - Logic.CurrentTime) / 1000 * speed, 3.16f, 10);

        }
    }
    private void GenFX()
    {
        GameObject fx = Instantiate(so_NoteProfile.fx_obj, new Vector3(0, Logic.CurrentTime01 * speed, 0), Quaternion.identity);
        Destroy(fx, 0.5f);
    }
}
