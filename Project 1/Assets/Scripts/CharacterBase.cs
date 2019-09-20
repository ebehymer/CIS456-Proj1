using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBase : MonoBehaviour
{
    public enum characterType { Rogue, Wizard, Fighter };

    protected characterType type;
    protected int characterHealth;
    public bool isAlive;

    void Awake()
    {
        isAlive = true;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public characterType GetCharacterType()
    {
        return type;
    }

    public void SetCharacterType(characterType ct)
    {
        type = ct;
    }

    public int GetCharacterHealth()
    {
        return characterHealth;
    }

    public void SetCharacterHealth(int amount)
    {
        characterHealth = amount;
    }
}
