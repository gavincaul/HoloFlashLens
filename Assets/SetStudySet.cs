using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetStudySet : MonoBehaviour
{
    public Canvas canvas;
    public SetManager SM;
    public void Set(StudySet s)
    {
        canvas.enabled = false;
        SM.loadSet(s);
        Debug.Log($"set {s}!");
        return;
    }
}
