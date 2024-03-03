using MixedReality.Toolkit.UX;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TermOrDef : MonoBehaviour
{
    public FontIconSelector checkmark;
    private bool termbool = false;
    public SetManager SM;
    public void term()
    {
        termbool = !termbool;
        SM.term = termbool;
        checkmark.CurrentIconName = termbool ? "Icon 57" : "Icon 79";
    }
}
