using UnityEngine;

public enum RythmNoteType
{
    Left = 0,
    Right = 2,
    Up = 4,
    Down = 8
}
public struct KeyNote
{

    public float length;
    public float beatTime;
    public float pitch;

    public RythmNoteType rythmNoteType;
    public KeyCode keyCode {
        get {
            switch (this.rythmNoteType)
            {
                case RythmNoteType.Left:
                    return KeyCode.LeftArrow;
                case RythmNoteType.Right:
                    return KeyCode.RightArrow;
                case RythmNoteType.Up:
                    return KeyCode.UpArrow;
                case RythmNoteType.Down:
                    return KeyCode.DownArrow;
                default:
                    return KeyCode.None;
            }
        }
    }
    public KeyNote(int type,float pitch,float length, float beatTime)
    {
        this.rythmNoteType = (RythmNoteType)type;
        this.beatTime = beatTime;
        this.pitch = pitch;
        this.length = length;
    }
    
}
