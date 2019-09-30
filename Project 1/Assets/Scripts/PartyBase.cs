//Script: PartyBase
//Assignment: Project
//Description: Handles the party of characters and their generation
//Edits made by: Nicole, Emma
//Last edited by and date: Nicole 9/23

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartyBase : MonoBehaviour
{
    private CharacterBase[] partyMembers = null;
    private TileBase tiles;

    // Start is called before the first frame update
    void Start()
    {
        tiles = GetComponent<TileBase>();

        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public CharacterBase[] GetPartyMembers()
    {

        //call FollowPath
        
        return partyMembers;
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
