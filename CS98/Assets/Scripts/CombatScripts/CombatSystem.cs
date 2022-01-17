using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum CombatState { START, PLAYERTURN, ENEMYTURN, WON, LOST}
public class CombatSystem : MonoBehaviour
{
    public GameObject playerPrefab;
    public GameObject enemyPrefab;
    public Transform  playerCombatStation;
    public Transform enemyCombatStation;

    Unit playerUnit;
    Unit enemyUnit;

    public GameObject dialogueText;

    public BattleHUD playerHUD;
    public BattleHUD enemyHUD;

    public CombatState state;

    // Start is called before the first frame update
    void Start()
    {
        state = CombatState.START;
        StartCoroutine(SetupCombat());
    }

    IEnumerator SetupCombat()
    {
        GameObject playerGO = Instantiate(playerPrefab, playerCombatStation);
        playerUnit = playerGO.GetComponent<Unit>();

        GameObject enemyGO = Instantiate(enemyPrefab, enemyCombatStation);
        enemyUnit = enemyGO.GetComponent<Unit>();

        dialogueText.GetComponent<TMPro.TextMeshProUGUI>().text = "A wild " + enemyUnit.unitName + " approaches...";

        playerHUD.SetHUD(playerUnit);
        enemyHUD.SetHUD(enemyUnit);

        yield return new WaitForSeconds(2f);

        state = CombatState.PLAYERTURN;
    }

    void PlayerTurn()
    {
        dialogueText.GetComponent<TMPro.TextMeshProUGUI>().text = "Choose an action: ";
    }


}
