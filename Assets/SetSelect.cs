using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SetSelect : MonoBehaviour
{
    public List<TMP_Text> sets;

    public DisplayStudySets controller;
    public GameObject LeftButton;
    public GameObject RightButton;
    public GameObject SelectButton;
    public ChooseSet choose;
    public GameObject ShuffleButton;
    public GameObject TermButton;
    public TextMeshProUGUI ShuffleText;
    public TextMeshProUGUI TermText;

    public void select()
    {
        int option = controller.GetComponent<DisplayStudySets>().bolded;
        
        choose.chooseset(sets[option].text);
        LeftButton.SetActive(false);
        RightButton.SetActive(false);
        SelectButton.SetActive(false);
        ShuffleButton.SetActive(false);
        TermButton.SetActive(false);
        ShuffleText.gameObject.SetActive(true);
        TermText.gameObject.SetActive(true);
        foreach (TextMeshProUGUI set in sets)
        {
            Destroy(set.gameObject);
        }
    }
}
