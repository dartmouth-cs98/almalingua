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
    public GameObject CombatButtons;
    public GameObject SpellButtons;

    public GameObject SceneLoader;

    public BattleHUD playerHUD;
    public BattleHUD enemyHUD;
    public CombatState state;

    private System.Random rnd;
    string currentQuest;
    string NPCName;
    string[] questDetails;
    public static List<string> spells = new List<string>();
    Dictionary<string, string[]> spellInfo = new Dictionary<string, string[]>();
    string spellOne;
    string spellTwo;
    float playerSpellDamage;
    bool increasedStrength = false;
    void OnEnable()
    {
        rnd = new System.Random();

        if (spellInfo.Count == 0)
        {
            string[] spellDetails = new string[] { "0.2" };
            spellInfo.Add("quema", spellDetails);

            spellDetails = new string[] { "0.2" };
            spellInfo.Add("congela", spellDetails);

            spellDetails = new string[] { "0.4" };
            spellInfo.Add("tempestad", spellDetails);

            spellDetails = new string[] { "0" };
            spellInfo.Add("teme", spellDetails);

            spellDetails = new string[] { "0.25" };
            spellInfo.Add("grita", spellDetails);

            spellDetails = new string[] { "0.3" };
            spellInfo.Add("protege", spellDetails);

            spellDetails = new string[] { "-2" };
            spellInfo.Add("fortalece", spellDetails);

            spellDetails = new string[] { "0.6" };
            spellInfo.Add("relampaguea", spellDetails);

            spellDetails = new string[] { "-1" };
            spellInfo.Add("sana", spellDetails);

            spellDetails = new string[] { "0.6" };
            spellInfo.Add("detona", spellDetails);
        }
        foreach (KeyValuePair<string, string[]> values in spellInfo) {
          if (PlayerPrefs.HasKey(values.Key) && PlayerPrefs.GetString(values.Key) == "true")
            spells.Add(values.Key);
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
            NPC.SetActive(true);
            StartCoroutine(SetupCombat());
        }
    }

    IEnumerator SetupCombat()
    {

        playerUnit = playerGO.GetComponent<Unit>();


        enemyUnit = NPC.GetComponent<Unit>();

        dialogueText.GetComponent<TMPro.TextMeshProUGUI>().text = "Un salvaje " + enemyUnit.unitName + " se acrerca...";

        playerHUD.SetHUD(playerUnit);
        enemyHUD.SetHUD(enemyUnit);

        yield return new WaitForSeconds(2f);

        state = CombatState.PLAYERTURN;
        PlayerTurn();
    }

    void PlayerTurn()
    {
        dialogueText.GetComponent<TMPro.TextMeshProUGUI>().text = "Escoge una acción: ";
    }


    IEnumerator PlayerAttack()
    {
        if (increasedStrength)
        {
            dialogueText.GetComponent<TMPro.TextMeshProUGUI>().text = "¡El daño de ataque subió por 10%!";
            yield return new WaitForSeconds(1f);
            increasedStrength = false;

        }
        // Damage the enemy
        bool isDead = enemyUnit.TakeDamage(playerUnit.damage);

        enemyHUD.SetHP(enemyUnit.currentHP);
        dialogueText.GetComponent<TMPro.TextMeshProUGUI>().text = "El ataque fue exitoso";

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
        dialogueText.GetComponent<TMPro.TextMeshProUGUI>().text = "¡Te sientes mas fuerte!";

        yield return new WaitForSeconds(2f);

        state = CombatState.ENEMYTURN;
        StartCoroutine(EnemyTurn());
    }

    IEnumerator PlayerSpell()
    {
        bool isDead = enemyUnit.TakeDamage(Mathf.FloorToInt(playerSpellDamage * enemyUnit.currentHP));
        enemyHUD.SetHP(enemyUnit.currentHP);
        dialogueText.GetComponent<TMPro.TextMeshProUGUI>().text = "¡El ataque fue exitoso!";

        yield return new WaitForSeconds(2f);
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

    IEnumerator PlayerSkip()
    {
        dialogueText.GetComponent<TMPro.TextMeshProUGUI>().text = "El enemigo pierde su turno";
        yield return new WaitForSeconds(2f);
        PlayerTurn();
    }
    IEnumerator EnemyTurn()
    {
        dialogueText.GetComponent<TMPro.TextMeshProUGUI>().text = "¡El " + enemyUnit.unitName + " te ataca!";

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
            dialogueText.GetComponent<TMPro.TextMeshProUGUI>().text = "¡Ganaste la batalla!";
            PlayerPrefs.SetInt("QuestStep", PlayerPrefs.GetInt("QuestStep") + 1);
            SceneLoader.GetComponent<SceneLoader>().LoadScene(questDetails[3]);
            questDetails = new string[PlayerPrefs.GetInt("QuestLength")];
            print("NPC being destroyed: " + NPC);
            NPC.GetComponent<HideShowObjects>().Hide();
        }
        else if (state == CombatState.LOST)
        {
            dialogueText.GetComponent<TMPro.TextMeshProUGUI>().text = "¡Fuiste derrotado! ¡Volver a intentar!";
            SceneLoader.GetComponent<SceneLoader>().LoadScene(questDetails[3]);
        }
    }

    public void OnAttackButton()
    {
        if (state != CombatState.PLAYERTURN)
            return;

        StartCoroutine(PlayerAttack());
    }

    public void OnSpellButton()
    {

        if (state != CombatState.PLAYERTURN)
            return;
        CombatButtons.SetActive(!CombatButtons.activeSelf);
        SpellButtons.SetActive(!SpellButtons.activeSelf);

        dialogueText.GetComponent<TMPro.TextMeshProUGUI>().text = "Tus dos hechizos son... ";
        int randIndex = rnd.Next(spells.Count);
        int nextIndex = rnd.Next(spells.Count);

        while (nextIndex == randIndex)
        {
            nextIndex = rnd.Next(spells.Count);
        }
        spellOne = spells[randIndex];
        spellTwo = spells[nextIndex];

        SpellButtons.transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text = spells[randIndex];
        SpellButtons.transform.GetChild(1).GetComponent<TMPro.TextMeshProUGUI>().text = spells[nextIndex];

    }

    public void ChooseSpell(int num)
    {
        string spellName;
        if (num == 1)
        {
            spellName = spellOne;
        }
        else
        {
            spellName = spellTwo;
        }
        string[] spellDetails = new string[1];
        CombatButtons.SetActive(!CombatButtons.activeSelf);
        SpellButtons.SetActive(!SpellButtons.activeSelf);
        if (spellInfo.TryGetValue(spellName, out spellDetails))
        {
            playerSpellDamage = float.Parse(spellDetails[0]);
            if (playerSpellDamage == 0)
            {
                StartCoroutine(PlayerSkip());
            }
            else if (playerSpellDamage == -1)
            {
                StartCoroutine(PlayerHeal());
            }
            else if (playerSpellDamage == -2)
            {
                playerUnit.damage = Mathf.FloorToInt((float)(playerUnit.damage * 1.1));
                dialogueText.GetComponent<TMPro.TextMeshProUGUI>().text = "¡Tu ataque es más poderoso ahora!";
                increasedStrength = true;
                StartCoroutine(PlayerAttack());
            }
            else
            {
                StartCoroutine(PlayerSpell());

            }
        }
    }

}
