using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour
{
    [SerializeField]
    private StatBar statBar;
    
    public void InitStats(CharacterData characterData)
    {
        StatBarValues[] stats = characterData.GetStats();
        if (transform.childCount > 0)
        {
            for (int i = transform.childCount-1; i >= 0; i--)
            {
                Destroy(transform.GetChild(i).gameObject);
            }
        }

        for(int i=0; i<stats.Length; i++)
        {
            GameObject statBarGameObjectInstance = Instantiate(statBar.gameObject, transform);
            StatBar statBarInstance = statBarGameObjectInstance.GetComponent<StatBar>();
            statBarInstance.statName = stats[i].name;
            statBarInstance.statValue = stats[i].value;
            statBarInstance.InitTexts();
        }
    }
}
