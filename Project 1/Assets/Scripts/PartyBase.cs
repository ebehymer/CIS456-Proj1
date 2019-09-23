using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartyBase : MonoBehaviour
{
    private CharacterBase[] partyMembers;
    private TileBase tiles;
    //For DealDamage
    //Reference to TileBase
    //currentTile varriable

    //Generate the party

    // Start is called before the first frame update
    void Start()
    {
        tiles = GetComponent<TileBase>();

    }

    // Update is called once per frame
    void Update()
    {

    }

    //Party moves through the dungeon
    private void Charge()
    {

    }

    //Deals damage to every member in the party
    private void DealDamage()
    {
        //Get the current tile and its damage
        int damage = tiles.GetTileDamage(); 

        foreach (CharacterBase member in partyMembers)
        {
            member.SetCharacterHealth(member.GetCharacterHealth() - damage);

            if (member.GetCharacterHealth() <= 0)
            {
                Debug.Log(member.GetCharacterType() + " Dead"); //temp holders untill we implement death
            }
            else
            {
                Debug.Log(member.GetCharacterType() + " not Dead");//temp holders untill we implement death
            }
        }
    }

    //Generates 1 of each character type in the party
    private void GenerateParty()
    {
        //Adds a prefab to the list for each character type
    }
}
