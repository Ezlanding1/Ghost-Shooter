using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class WeightedRandom : MonoBehaviour
{
    public List<PossessableObject> choices =  new List<PossessableObject>();
    public List<PossessableObject> delete =  new List<PossessableObject>();
    int totalWeight;
    public GameObject RandomWeighted(bool cracked)
    {
        choices.Clear(); delete.Clear();
        totalWeight = 0;
        choices = FindObjectsOfType<PossessableObject>().ToList();
        foreach (PossessableObject entry in choices)
        {
            if (entry.cracked == cracked) { totalWeight += entry.weight; } else { delete.Add(entry); }
        }
        choices = choices.Except(delete).ToList();
        PossessableObject selectedChoice = ChooseFromOptions();
        if (selectedChoice != null)
        {
            return selectedChoice.gameObject;
        }
        else
        {
            return null;
        }
        
    }
    PossessableObject ChooseFromOptions()
    {
        int randomNumber = Random.Range(1, totalWeight + 1);
        int pos = 0;
        for (int i = 0; i < choices.Count; i++)
        {
            if (randomNumber <= choices[i].weight + pos)
            {
                return choices[i];
            }
            pos += choices[i].weight;
        }
        Debug.Log("nothing to choose from");
        return null;
    }
}