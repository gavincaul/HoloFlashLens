using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ChooseSet : MonoBehaviour
{
    public List<StudySet> studySets;
    public SetStudySet set;
    

    public void chooseset(string title)
    {
        foreach(StudySet s in studySets)
        {
       
            if (s.title == title)
            {
                set.Set(s);
            }
        }
    }
    
}
