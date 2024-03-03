using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Term
{
    public string term;
    [JsonProperty("description")]
    public string definition;
}

[System.Serializable]
public class StudySet
{
    [JsonProperty("set_name")]
    public string title;
    [JsonProperty("flashcards")]
    public List<Term> terms;
}

[System.Serializable]
public class RootObject
{
    public List<StudySet> study_sets;
}

public class JSONParse : MonoBehaviour
{
    public List<StudySet> StudySets = new List<StudySet>();
    public DisplayStudySets DSS;
    public SetManager SM;

    public void ParseJSON(string jsonString)
    {
        SM.jsonstr = jsonString;
        JArray root = JArray.Parse(jsonString);

        // Create a list to hold the modified structure
        JObject modifiedJson = new JObject();
        modifiedJson["study_sets"] = root;

        print(modifiedJson);
        // Deserialize the modified JSON into a list of RootObject
        RootObject studySets = modifiedJson.ToObject<RootObject>();

        // Clear the existing list and add the new study sets
        StudySets.Clear();
        foreach (StudySet studySet in studySets.study_sets)
        {
            StudySets.Add(studySet);
            //Debug.Log("Study Set Title: " + studySet.title);
            //foreach (Term term in studySet.terms)
            //{
            //Debug.Log("Term: " + term.term);
            //Debug.Log("Definition: " + term.definition);
            //}
        }

        // Display the study sets
        DSS.display(StudySets);
    }
}
