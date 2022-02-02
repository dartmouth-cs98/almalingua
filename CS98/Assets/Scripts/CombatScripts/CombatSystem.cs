using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum CombatState { START, PLAYERTURN, ENEMYTURN, WON, LOST}
public class CombatSystem : MonoBehaviour
{
    public GameObject playerPrefab;

    public Transform  playerCombatStation;
    public Transform enemyCombatStation;
    
    private GameObject NPC;
    Unit playerUnit;
    Unit enemyUnit;

    public GameObject dialogueText;

    public GameObject SceneLoader;

    public BattleHUD playerHUD;
    public BattleHUD enemyHUD;

    public CombatState state;

    string currentQuest;
    string NPCName;


    void OnEnable() {
        state = CombatState.START;
        string currentQuest = PlayerPrefs.GetInt("Quest").ToString() + PlayerPrefs.GetInt("QuestStep").ToString();
        string[] questDetails = new string[PlayerPrefs.GetInt("QuestLength")];
        Debug.Log(currentQuest);
        if (QuestUI.questNPC.TryGetValue(currentQuest, out questDetails))
        {
            print("hi");
            string NPCName = questDetails[0];
            NPC = GameObject.Find(NPCName);
            NPC.GetComponent<HideShowObjects>().Show();
            StartCoroutine(SetupCombat());

        }
        print(questDetails);
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    IEnumerator SetupCombat()
    {
        GameObject playerGO = Instantiate(playerPrefab, playerCombatStation);
        print(playerGO); 
        playerUnit = playerGO.GetComponent<Unit>();

        
        enemyUnit = NPC.GetComponent<Unit>();

        dialogueText.GetComponent<TMPro.TextMeshProUGUI>().text = "A wild " + enemyUnit.unitName + " approaches...";

        playerHUD.SetHUD(playerUnit);
        enemyHUD.SetHUD(enemyUnit);

        yield return new WaitForSeconds(2f);

        state = CombatState.PLAYERTURN;
        PlayerTurn();
    }

    void PlayerTurn()
    {
        dialogueText.GetComponent<TMPro.TextMeshProUGUI>().text = "Choose an action: ";
    }


    IEnumerator PlayerAttack()
    {
        // Damage the enemy
        bool isDead = enemyUnit.TakeDamage(playerUnit.damage);

        enemyHUD.SetHP(enemyUnit.currentHP);
        dialogueText.GetComponent<TMPro.TextMeshProUGUI>().text = "The attack is successful!";

        yield return new WaitForSeconds(2f);

        // Check if the enemy is dead
        if (isDead)
        {
            // End the battle
            state = CombatState.WON;
            EndBattle();
        }
        else
        {
            // Enemy's turn
            state = CombatState.ENEMYTURN;
            StartCoroutine(EnemyTurn());
        }
    }

    IEnumerator PlayerHeal()
    {
        playerUnit.Heal(10);

        playerHUD.SetHP(playerUnit.currentHP);
        dialogueText.GetComponent<TMPro.TextMeshProUGUI>().text = "You feel renewed strength!";

        yield return new WaitForSeconds(2f);

        state = CombatState.ENEMYTURN;
        StartCoroutine(EnemyTurn());
    }
    

    IEnumerator EnemyTurn()
    {
        dialogueText.GetComponent<TMPro.TextMeshProUGUI>().text = enemyUnit.unitName + " attacks!";

        yield return new WaitForSeconds(1f);

        bool isDead = playerUnit.TakeDamage(enemyUnit.damage);

        playerHUD.SetHP(playerUnit.currentHP);

        yield return new WaitForSeconds(1f);

        if (isDead)
        {
            state = CombatState.LOST;
            EndBattle();
        }
        else 
        {
            state = CombatState.PLAYERTURN;
            PlayerTurn();
        }

    }

    void EndBattle()
    {
        if (state == CombatState.WON)
        {
            dialogueText.GetComponent<TMPro.TextMeshProUGUI>().text = "You won the battle!";
        }
        else if (state == CombatState.LOST)
        {
            // Return to the Witch's House
            dialogueText.GetComponent<TMPro.TextMeshProUGUI>().text = "You were defeated!";
            SceneLoader.GetComponent<SceneLoader>().LoadScene("WitchHouse");
        }
    }

    public void OnAttackButton()
    {
        if (state != CombatState.PLAYERTURN)
            return;
        
        StartCoroutine(PlayerAttack());
    }

    public void OnHealButton()
    {
        if (state != CombatState.PLAYERTURN)
            return;
        
        StartCoroutine(PlayerHeal());
    }

}
