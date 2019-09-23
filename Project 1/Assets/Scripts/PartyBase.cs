using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartyBase : MonoBehaviour
{
    private CharacterBase[] partyMembers = null;
    //For DealDamage
    //Reference to TileBase
    //currentTile varriable

        //reference to tileplacement
        //party is own tile
        //if path has a tile, get the tile
        //do the thing

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public CharacterBase[] GetPartyMembers()
    {
        return partyMembers;
    }

    //Deals damage to every member in the party
    private void DealDamage()
    {
        //Get the current tile and its damage
        int damage = 0; //temporary till we get actual damage

        foreach (CharacterBase member in partyMembers)
        {
            member.GetComponent<CharacterBase>().SetCharacterHealth(member.GetComponent<CharacterBase>().GetCharacterHealth() - damage);
            //Decide how to handle death
        }
    }

    //Generates 1 of each character type in the party
    private void GenerateParty()
    {
        //Adds a prefab to the list for each character type
        for(int index = 0; index <= partyMembers.Length; index++)
        {
            int rng = Random.Range(1, 3);
            if(rng == 1) // Add a rogue
            {
                partyMembers[index] = new CharacterBase();
                partyMembers[index].GetComponent<CharacterBase>().SetCharacterType(CharacterBase.characterType.Rogue);
                partyMembers[index].GetComponent<CharacterBase>().SetCharacterHealth(Random.Range(20, 50));
            }
            else if(rng == 2) //Add a wizard
            {
                partyMembers[index] = new CharacterBase();
                partyMembers[index].GetComponent<CharacterBase>().SetCharacterType(CharacterBase.characterType.Wizard);
                partyMembers[index].GetComponent<CharacterBase>().SetCharacterHealth(Random.Range(20, 50));
            }
            else if(rng == 3) //Add a fighter
            {
                partyMembers[index] = new CharacterBase();
                partyMembers[index].GetComponent<CharacterBase>().SetCharacterType(CharacterBase.characterType.Fighter);
                partyMembers[index].GetComponent<CharacterBase>().SetCharacterHealth(Random.Range(20, 50));
            }
        }
    }
}
