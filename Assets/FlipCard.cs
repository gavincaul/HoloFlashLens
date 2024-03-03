using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FlipCard : MonoBehaviour
{

    public TextMeshProUGUI Term;
    public TextMeshProUGUI TermContent;
    public TextMeshProUGUI Definition;
    public TextMeshProUGUI DefinitionContent;
    public GameObject FlipButton;
    public GameObject QuestionButton;
    public GameObject LockButton;
    public GameObject HomeButton;
    public int timer;
    public float x, y, z;
    public void startflip()
    {
        FlipButton.SetActive(false);
        QuestionButton.SetActive(false);
        LockButton.SetActive(false);
        HomeButton.SetActive(false);
        StartCoroutine(CalculateFlip());
    }


    public void Flip()
    {
        if (Term.gameObject.activeSelf)
        {
            Definition.gameObject.SetActive(true);
            Term.gameObject.SetActive(false);
        }
        else
        {
            Term.gameObject.SetActive(true);
            Definition.gameObject.SetActive(false);
        }
    }
    IEnumerator CalculateFlip()
    {
        for (int i = 0; i < 180; i++)
        {
            yield return new WaitForSeconds(0.001f);
            transform.Rotate(new Vector3(x, y, z));
            timer++;
            if (timer == 90 || timer == -90)
            {
                Flip();
            }
        }
        FlipButton.SetActive(true);
        QuestionButton.SetActive(true);
        LockButton.SetActive(true);
        HomeButton.SetActive(true);
        timer = 0;
    }
}
