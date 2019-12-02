using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

//Script: PartyManager
//Assignment: Project
//Description: Guides the party along the dungeon path
//Edits made by: Robyn, Emma, and CJ
//Last edited by and date: CJ 11/25

public class PartyManager : MonoBehaviour
{
    DungeonGenerator man;
    MenuManager menMan;
    bool moveHoriz;

    //public Text fail;

    public float waitTime;

    RaycastHit2D col;

    List<GameObject> stepped = new List<GameObject>();

    public AudioSource FinishedSound;
    private BudgetManager bugMan;
    private MenuManager menuMan;

    private TextMeshProUGUI specialAbilityPrompt;

    // Start is called before the first frame update
    void Start()
    {
        specialAbilityPrompt = GameObject.Find("Special Ability Prompt Text").GetComponent<TextMeshProUGUI>();
        menuMan = GameObject.Find("Menu Manager").GetComponent<MenuManager>();
        bugMan = GameObject.Find("Budget Manager").GetComponent<BudgetManager>();
        man = GameObject.Find("DungeonManager").GetComponent<DungeonGenerator>();
        menMan = GameObject.Find("Menu Manager").GetComponent<MenuManager>();
        //fail.text = "";

        specialAbilityPrompt.gameObject.SetActive(false);

        if (SceneManager.GetActiveScene().name == "Level 1" || SceneManager.GetActiveScene().name == "Level 2" || SceneManager.GetActiveScene().name == "Level 3")
        {
            specialAbilityPrompt.gameObject.SetActive(false);
        }
        else
        {
            //specialAbilityPrompt.gameObject.SetActive(true);
            StartCoroutine(FlashText());
        }
    }

    // Could just make a method that uses invoke repeating but oh well.
    public IEnumerator FlashText()
    {
        specialAbilityPrompt.gameObject.SetActive(true);
        yield return new WaitForSeconds(.5f);
        specialAbilityPrompt.gameObject.SetActive(false);
        yield return new WaitForSeconds(.5f);
        specialAbilityPrompt.gameObject.SetActive(true);
        yield return new WaitForSeconds(.5f);
        specialAbilityPrompt.gameObject.SetActive(false);
        yield return new WaitForSeconds(.5f);
        specialAbilityPrompt.gameObject.SetActive(true);
        yield return new WaitForSeconds(.5f);
        specialAbilityPrompt.gameObject.SetActive(false);
        yield return new WaitForSeconds(.5f);
        specialAbilityPrompt.gameObject.SetActive(true);
        yield return new WaitForSeconds(.5f);
        specialAbilityPrompt.gameObject.SetActive(false);

    }

    private void Update()
    {
        col = Physics2D.Raycast(transform.position, Vector2.zero, Mathf.Infinity, LayerMask.GetMask("Placable"));

        if (col)
        {
            if (col.collider.gameObject.tag == "Magic")
            {
                GetComponent<PartyBase>().DealDamage(TileManager.magictile.GetTileDamage(), TileBase.tileType.magic);
            }
            if (col.collider.gameObject.tag == "Enemy")
            {
                GetComponent<PartyBase>().DealDamage(TileManager.enemytile.GetTileDamage(), TileBase.tileType.enemy);
            }
            if (col.collider.gameObject.tag == "Trap")
            {
                GetComponent<PartyBase>().DealDamage(TileManager.traptile.GetTileDamage(), TileBase.tileType.trap);
            }

            col.collider.enabled = false;
            col.collider.GetComponent<SpriteRenderer>().enabled = false;
            stepped.Add(col.collider.gameObject);
        }

        if (GetComponent<PartyBase>().allDead)
        {
            StopCoroutine(partyMove);
            if (menuMan.scoreMenu.activeSelf == false)
            {

                menuMan.scoreMenu.SetActive(true);
            }
        }
    }

    Coroutine partyMove;

    IEnumerator Walk()
    {
        GameManager.current = GameManager.GameState.running;
        transform.position = man.wayPoints[0];
        for (int i = 0; i < man.numWayPoints - 1; i++)
        {
            if (moveHoriz)
            {
                if (man.wayPoints[i].x > man.wayPoints[i + 1].x)
                {
                    for (float k = man.wayPoints[i].x - 1; k >= man.wayPoints[i + 1].x; k--)
                    {
                        transform.position = new Vector3(k, transform.position.y);
                        yield return new WaitForSeconds(waitTime);
                    }
                }
                else
                {
                    for (float k = man.wayPoints[i].x + 1; k <= man.wayPoints[i + 1].x; k++)
                    {
                        transform.position = new Vector3(k, transform.position.y);
                        yield return new WaitForSeconds(waitTime);
                    }
                }
            }
            else
            {
                if (man.wayPoints[i].y > man.wayPoints[i + 1].y)
                {
                    for (float k = man.wayPoints[i].y - 1; k >= man.wayPoints[i + 1].y; k--)
                    {
                        transform.position = new Vector3(transform.position.x, k);
                        yield return new WaitForSeconds(waitTime);
                    }
                }
                else
                {
                    for (float k = man.wayPoints[i].y + 1; k <= man.wayPoints[i + 1].y; k++)
                    {
                        transform.position = new Vector3(transform.position.x, k);
                        yield return new WaitForSeconds(waitTime);
                    }
                }
            }

            moveHoriz = !moveHoriz;
        }

        if (!GetComponent<PartyBase>().allDead)
        {

            if (bugMan.usedMoney >= bugMan.maxMoney / 2)
            {
                menuMan.specialAbilityMenu.SetActive(false);
                menuMan.scoreMenu.SetActive(true);
                Debug.Log("Special");
            }
            else
            {

                //if (SceneManager.GetActiveScene() != SceneManager.GetSceneByName("Level 1") && SceneManager.GetActiveScene() != SceneManager.GetSceneByName("Level 2"))
                //{
                //    menuMan.specialAbilityMenu.SetActive(true);
                //    Debug.Log("Special");
                //}
                //else
                //{
                //    menuMan.specialAbilityMenu.SetActive(false);
                //    menuMan.scoreMenu.SetActive(true);
                //}

                if (SceneManager.GetActiveScene().name == "Level 1" || SceneManager.GetActiveScene().name == "Level 2" || SceneManager.GetActiveScene().name == "Level 3")
                {
                    // Make it so that the player cannot get the special ability until after level 3
                    menuMan.specialAbilityMenu.SetActive(false);
                    menuMan.scoreMenu.SetActive(true);
                    //menuMan.specialAbilityPrompt.gameObject.setActive(false);
                }
                else
                {
                    specialAbilityPrompt.gameObject.SetActive(true);
                    menuMan.specialAbilityMenu.SetActive(true);
                    // menuMan.scoreMenu.SetActive(false);
                }


            }
            //menMan.ShowMenu(menMan.scoreMenu);
            //menMan.specialAbilityMenu.SetActive(true);
            //fail.text = "Quest Failed";
            //yield return new WaitForSeconds(4.0f);
            //fail.text = "";
            //Restart();
        }
    }

    public void Finished()
    {

        if (GameManager.current == GameManager.GameState.placing)
        {
            moveHoriz = true;
            if (FinishedSound != null)
                FinishedSound.Play();
            partyMove = StartCoroutine(Walk());
        }
    }


    public void Restart()
    {
        foreach (GameObject i in stepped)
        {
            i.GetComponent<BoxCollider2D>().enabled = true;
            i.GetComponent<SpriteRenderer>().enabled = true;
        }
        stepped.Clear();
        transform.position = new Vector3(-20, 0, 0);
        GameManager.current = GameManager.GameState.placing;


        for (int i = 0; i < GetComponent<PartyBase>().GetPartyMembers().Length; i++)
        {
            GetComponent<PartyBase>().GetPartyMembers()[i].SetCharacterHealth(GetComponent<PartyBase>().GetPartyMembers()[i].maxHealth);
        }

        GetComponent<PartyBase>().updateText();
    }



    public void timeChange(Text t)
    {
        if (t.text == "Slow")
        {
            waitTime = .5f;
            t.text = "Medium";
        }
        else if (t.text == "Medium")
        {
            waitTime = .1f;
            t.text = "Fast";
        }
        else if (t.text == "Fast")
        {
            waitTime = .9f;
            t.text = "Slow";
        }
    }

}
