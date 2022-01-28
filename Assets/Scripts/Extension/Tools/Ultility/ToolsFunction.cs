using UnityEngine;

public class ToolsFunction
{
    public Transform FindOrCreate(string objName)
    {
        if (GameObject.Find(objName).activeSelf)
        {
            return GameObject.Find(objName).transform;
        }
        else
        {
            return new GameObject(objName).transform;
        }
    }
}
