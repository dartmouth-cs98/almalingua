using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public enum CombatState { START, PLAYERTURN, ENEMYTURN, WON, LOST }
public class CombatSystem : MonoBehaviour
{
    public GameObject playerGO;

    public Transform playerCombatStation;
    public Transform enemyCombatStation;
    public GameObject Enemy;

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
    string[] questDetails;

    public static List<string> spells = new List<string>();

    Dictionary<string, string[]> spellInfo = new Dictionary<string, string[]>();
    void OnEnable()
    {
        if (spellInfo.Count == 0)
        {
            string[] spellDetails = new string[] { "70" };
            spellInfo.Add("quema", spellDetails);
            spellDetails = new string[] { "50" };
            spellInfo.Add("congela", spellDetails);
            spellDetails = new string[] { "30" };
            spellInfo.Add("gritar", spellDetails);
            spellDetails = new string[] { "35" };
            spellInfo.Add("protegar", spellDetails);
            spellDetails = new string[] { "60" };
            spellInfo.Add("fortalecer", spellDetails);
            spellDetails = new string[] { "40" };
            spellInfo.Add("dormir", spellDetails);
        }

        state = CombatState.START;
        string currentQuest = PlayerPrefs.GetInt("Quest").ToString() + PlayerPrefs.GetInt("QuestStep").ToString();
        questDetails = new string[PlayerPrefs.GetInt("QuestLength")];
        Debug.Log("Current quest: " + currentQuest);
        Debug.Log("There are " + QuestUI.questNPC.Count + " named objects.");
        if (QuestUI.questNPC.TryGetValue(currentQuest, out questDetails))
        {
            string NPCName = questDetails[0];
            print("NPC Name: " + NPCName);
            NPC = Enemy.transform.Find(NPCName).gameObject;
            NPC.GetComponent<HideShowObjects>().Show();
            StartCoroutine(SetupCombat());
        }
    }
    // Start is called before the first frame update
    void Start()
    {
    }

    IEnumerator SetupCombat()
    {
        print("got into the setup combat");
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
            PlayerPrefs.SetInt("QuestStep", PlayerPrefs.GetInt("QuestStep") + 1);
            SceneLoader.GetComponent<SceneLoader>().LoadScene(questDetails[2]);
            questDetails = new string[PlayerPrefs.GetInt("QuestLength")];
            print("NPC being destroyed: " + NPC);
            NPC.GetComponent<HideShowObjects>().Hide();
        }
        else if (state == CombatState.LOST)
        {
            dialogueText.GetComponent<TMPro.TextMeshProUGUI>().text = "You were defeated! Try again!";
            SceneLoader.GetComponent<SceneLoader>().LoadScene(questDetails[2]);
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
