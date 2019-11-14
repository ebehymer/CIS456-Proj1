//Script: PartyBase
//Assignment: Project
//Description: Handles the party of characters and their generation
//Edits made by: Nicole, Emma, Robyn
//Last edited by and date: Nicole 11/14

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PartyBase : MonoBehaviour
{
    [SerializeField] private CharacterBase[] partyMembers = new CharacterBase[3];
    private TileBase tiles;

    public bool allDead = false;

    public Text text;

    public AudioSource death;

    // Start is called before the first frame update
    void Start()
    {
        
        tiles = GetComponent<TileBase>();

        text.text = "Party Contains\n";

        //GenerateRandomParty();
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
    public void DealDamage(int damage, TileBase.tileType tileType)
    {

        text.text = "Party Contains\n";

        foreach (CharacterBase member in partyMembers)
        {
            //Checks for the party member type before dealing the damage
            //Fighter takes 1/2 damage on enemy tiles
            if(member.GetCharacterType() == CharacterBase.characterType.Fighter && tileType == TileBase.tileType.enemy)
            {
                member.SetCharacterHealth(member.GetCharacterHealth() - (damage / 2));
            }
            //Rogue takes 1/2 damage on Trap tiles
            else if(member.GetCharacterType() == CharacterBase.characterType.Rogue && tileType == TileBase.tileType.trap)
            {
                member.SetCharacterHealth(member.GetCharacterHealth() - (damage / 2));
            }
            //Wizard takes 1/2 damage on magic tiles
            else if(member.GetCharacterType() == CharacterBase.characterType.Wizard && tileType == TileBase.tileType.magic)
            {
                member.SetCharacterHealth(member.GetCharacterHealth() - (damage/2));
            }
            //Otherwise take normal damage
            else
            {
                member.SetCharacterHealth(member.GetCharacterHealth() - damage);
            }

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
            //Play Wilheim Scream - currently when any player dies
            if (death != null)
            {
                death.Play();
            }
            allDead = true;
            
        }
        Debug.Log(allDead);
    }

    public void updateText()
    {

        text.text = "Party Contains\n";

        foreach (CharacterBase member in partyMembers)
        {
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
        }
    }
    //Generates 1 of each character type in the party
    private void GenerateRandomParty()
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


            partyMembers[index].maxHealth = partyMembers[index].GetCharacterHealth();
        }



        for (int i = 0; i < partyMembers.Length; i++)
        {
            text.text += GetPartyMembers()[i].GetCharacterType() + ": " + GetPartyMembers()[i].GetCharacterHealth() + "\n";
        }
    }
}
