using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayStudySets : MonoBehaviour
{
    public GameObject input;
    public GameObject EnterButton;
    public GameObject error;
    public TextMeshProUGUI studysetdisplay;
    public Canvas canvas;
    public GameObject LeftButton;
    public GameObject RightButton;
    public GameObject SelectButton;
    public GameObject Keyboard;
    public Canvas panel;
    public int bolded = 0;
    public ChooseSet cs;
    public GameObject ShuffleButton;
    public GameObject TermButton;
    public TextMeshProUGUI ShuffleText;
    public TextMeshProUGUI TermText;
   

    public void display(List<StudySet> studysets)
    {
        input.SetActive(false);
        EnterButton.SetActive(false);
        error.SetActive(false);
        LeftButton.SetActive(true);
        RightButton.SetActive(true);
        SelectButton.SetActive(true);
        Keyboard.SetActive(false);
        ShuffleButton.SetActive(true);
        TermButton.SetActive(true);
        ShuffleText.gameObject.SetActive(true);
        TermText.gameObject.SetActive(true);
        bolded = 0;
        cs.studySets = studysets;
        float offset = 0;

        List<TMP_Text> sets = new();
        studysetdisplay.rectTransform.anchoredPosition = new Vector2(-116f, 84f);

        foreach (StudySet s in studysets)
        {
            TextMeshProUGUI studyset = Instantiate(studysetdisplay, panel.transform);
            
            studyset.gameObject.SetActive(true);
            sets.Add(studyset);

            studyset.text = s.title;
            Debug.Log($"{studyset} and {studyset.text}");

            float lineoffset = Mathf.Ceil(s.title.Length / 26);
            //shift position down 40 units (fits in more microphones)
            studyset.GetComponent<RectTransform>().anchoredPosition = new Vector2(studysetdisplay.GetComponent<RectTransform>().anchoredPosition.x, studysetdisplay.GetComponent<RectTransform>().anchoredPosition.y - offset * 30 + (lineoffset - offset) * 30);
            offset += 1;        
        }
        sets[bolded].fontStyle = FontStyles.Bold;
        LeftButton.GetComponent<SetLeft>().sets = sets;
        RightButton.GetComponent<SetRight>().sets = sets;
        SelectButton.GetComponent<SetSelect>().sets = sets;
        
    }
}
