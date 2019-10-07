//Script: PartyBase
//Assignment: Project
//Description: Handles the party of characters and their generation
//Edits made by: Nicole, Emma, Robyn
//Last edited by and date: Robyn 10/3

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PartyBase : MonoBehaviour
{
    private CharacterBase[] partyMembers = new CharacterBase[3];
    private TileBase tiles;

    public bool allDead = false;

    public Text text;

    // Start is called before the first frame update
    void Start()
    {
        tiles = GetComponent<TileBase>();

        text.text = "Party Contains\n";

        GenerateParty();
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
    public void DealDamage(int damage)
    {

        text.text = "Party Contains\n";

        foreach (CharacterBase member in partyMembers)
        {
            member.SetCharacterHealth(member.GetCharacterHealth() - damage);

            if (member.GetCharacterHealth() < 0)
            {
                Debug.Log(member.GetCharacterType() + " Dead"); //temp holders untill we implement death

                text.text += member.GetCharacterType() + ": Dead\n";
            }
            else
            {
                Debug.Log(member.GetCharacterType() + " not Dead");//temp holders untill we implement death

                text.text += member.GetCharacterType() + ": " + member.GetCharacterHealth() + "\n";
            }
            //Decide how to handle death

            
        }

        foreach(CharacterBase member in partyMembers)
        {
            if (member.GetCharacterHealth() > 0)
            {
                allDead = false;
                break;
            }
            allDead = true;
        }

        Debug.Log(allDead);
    }

    //Generates 1 of each character type in the party
    private void GenerateParty()
    {
        //Adds a prefab to the list for each character type
        for(int index = 0; index < partyMembers.Length; index++)
        {
            int rng = Random.Range(1, 4);
            if(rng == 1) // Add a rogue
            {
                partyMembers[index] = new CharacterBase();
                partyMembers[index].SetCharacterType(CharacterBase.characterType.Rogue);
                partyMembers[index].SetCharacterHealth(Random.Range(90, 150));
            }
            else if(rng == 2) //Add a wizard
            {
                partyMembers[index] = new CharacterBase();
                partyMembers[index].SetCharacterType(CharacterBase.characterType.Wizard);
                partyMembers[index].SetCharacterHealth(Random.Range(60, 100));
            }
            else if(rng == 3) //Add a fighter
            {
                partyMembers[index] = new CharacterBase();
                partyMembers[index].SetCharacterType(CharacterBase.characterType.Fighter);
                partyMembers[index].SetCharacterHealth(Random.Range(125, 180));
            }


        }



        for (int i = 0; i < partyMembers.Length; i++)
        {
            text.text += GetPartyMembers()[i].GetCharacterType() + ": " + GetPartyMembers()[i].GetCharacterHealth() + "\n";
        }
    }
}
