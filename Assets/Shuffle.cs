using MixedReality.Toolkit.UX;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shuffle : MonoBehaviour
{
    public FontIconSelector checkmark;
    private bool shufflebool = false;
    public SetManager SM;
    public void shuffle()
    {
        shufflebool = !shufflebool;
        SM.shuffle = shufflebool;
        checkmark.CurrentIconName = shufflebool ? "Icon 57" : "Icon 79";
    }
}
