using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterChooser : MonoBehaviour
{
    [SerializeField]
    private Transform showPlace;


    public void ShowCharacter(CharacterData gam)
    {
        if(showPlace.childCount > 0)
        {
            Destroy(showPlace.GetChild(0).gameObject);
        }
        GameObject instance = Instantiate(gam.gameObject, showPlace);

    }
}
