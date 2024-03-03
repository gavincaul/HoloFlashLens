using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SetLeft : MonoBehaviour
{
    public List<TMP_Text> sets;
    public DisplayStudySets controller;
    public void MoveLeft()
    {

        int curr = controller.GetComponent<DisplayStudySets>().bolded;
        controller.GetComponent<DisplayStudySets>().bolded -= 1;

        //Ensures we don't go negative and out of bounds
        if (controller.GetComponent<DisplayStudySets>().bolded < 0) { controller.GetComponent<DisplayStudySets>().bolded = sets.Count - 1; }

        int index = controller.GetComponent<DisplayStudySets>().bolded;

        sets[index].fontStyle = FontStyles.Bold;

        sets[curr].fontStyle = FontStyles.Normal;
    }
}
