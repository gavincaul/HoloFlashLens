using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;
using System;
using MixedReality.Toolkit.SpatialManipulation;
using MixedReality.Toolkit.UX;
using UnityEngine.UI;

public class SetManager : MonoBehaviour
{

    public GameObject flashcard;
    public FlipCard flipper;
    public Canvas initialcanvas;
    public TextMeshProUGUI title;
    public TextMeshProUGUI Term;
    public TextMeshProUGUI TermContent;
    public TextMeshProUGUI Definition;
    public TextMeshProUGUI DefinitionContent;
    [NonSerialized]
    public bool term;
    public List<Term> currTerms = new();
    private int card;
    public FontIconSelector locksymbol;
    public GameObject LockButton;
    public GameObject GPTButton;
    public GameObject HomeButton;
    public GameObject FlipButton;
    public JSONParse json;
    public string jsonstr;
    public MakeRequest makereq;
    [NonSerialized]
    public bool shuffle;
    public Image gif;
    public GameObject bubble;


    public void loadSet(StudySet s)
    {
        card = 0;
        Term.text = "Term";
        Definition.text = "Definition";
        flashcard.SetActive(true);
        LockButton.SetActive(true);
        GPTButton.SetActive(true);
        HomeButton.SetActive(true);
        FlipButton.SetActive(true);
        title.text = s.title;
        currTerms = shuffle ? s.terms.OrderBy(i => Guid.NewGuid()).ToList() : s.terms; //shuffle
        loadterm(currTerms[card]);
        if (term)
        {
            Quaternion newRotation = Quaternion.Euler(Definition.transform.rotation.eulerAngles.x, 180, Definition.transform.rotation.eulerAngles.z);
            Definition.transform.rotation = newRotation;
            Quaternion newRotationQ = Quaternion.Euler(Term.transform.rotation.eulerAngles.x, 0, Term.transform.rotation.eulerAngles.z);
            Term.transform.rotation = newRotationQ;
        }
        else
        {
            Quaternion newRotation = Quaternion.Euler(Term.transform.rotation.eulerAngles.x, 180, Term.transform.rotation.eulerAngles.z);
            Term.transform.rotation = newRotation;
            Quaternion newRotationQ = Quaternion.Euler(Definition.transform.rotation.eulerAngles.x, 0, Definition.transform.rotation.eulerAngles.z);
            Definition.transform.rotation = newRotationQ;
        }
    }
    public void loadterm(Term t)
    {

        TermContent.text = t.term;
        DefinitionContent.text = t.definition;
        
        if (term)
        {
            
            Term.gameObject.SetActive(true);
            Definition.gameObject.SetActive(false);
            Quaternion newRotation = Quaternion.Euler(Definition.transform.rotation.eulerAngles.x, 180, Definition.transform.rotation.eulerAngles.z);
            Definition.transform.rotation = newRotation;
            Quaternion newRotationQ = Quaternion.Euler(Term.transform.rotation.eulerAngles.x, 0, Term.transform.rotation.eulerAngles.z);
            Term.transform.rotation = newRotationQ;

        }
        else
        {
            Definition.gameObject.SetActive(true);
            Term.gameObject.SetActive(false);
            Quaternion newRotation = Quaternion.Euler(Term.transform.rotation.eulerAngles.x, 180, Term.transform.rotation.eulerAngles.z);
            Term.transform.rotation = newRotation;
            Quaternion newRotationQ = Quaternion.Euler(Definition.transform.rotation.eulerAngles.x, 0, Definition.transform.rotation.eulerAngles.z);
            Definition.transform.rotation = newRotationQ;

        }
    }
    public void nextTerm()
    {
        bubble.SetActive(false);
        card += 1;
        if (card > currTerms.Count-1)
        {
            card = 0;
            finishset();
            return;
        }
        if (flipper.transform.rotation.y > 0)
        {
            Quaternion newRotation = Quaternion.Euler(flipper.transform.rotation.eulerAngles.x, 0, flipper.transform.rotation.eulerAngles.z);
            flipper.transform.rotation = newRotation;
        }

        Term.gameObject.SetActive(false);
  
        Definition.gameObject.SetActive(false);

        loadterm(currTerms[card]);
    }
    private void finishset()
    {
        Term.text = "Congratulations!";
        gif.gameObject.SetActive(true);
        TermContent.text = "You have completed the set.";
        Definition.text = "Congratulations!";
        DefinitionContent.text = "You have completed the set.";
        LockButton.SetActive(false);
        FlipButton.SetActive(false);
        GPTButton.SetActive(false);
    }

    public void home()
    {
        flashcard.SetActive(false);
        gif.gameObject.SetActive(false);
        initialcanvas.enabled = true;
        json.ParseJSON(jsonstr);
    }

    public void lockcard()
    {
        flashcard.GetComponent<Follow>().enabled = !flashcard.GetComponent<Follow>().enabled;
        locksymbol.CurrentIconName = locksymbol.CurrentIconName.ToString() == "Icon 17" ? "Icon 18" :"Icon 17";
    }

    public void GPT()
    {
        makereq.Request(currTerms[card].term);
    }
    
}
