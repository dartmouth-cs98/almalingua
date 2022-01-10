using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CombatState { START, PLAYERTURN, ENEMYTURN, WON, LOST}
public class CombatSystem : MonoBehaviour
{
    public GameObject playerPrefab;
    public GameObject enemyPrefab;
    public Transform  playerCombatStation;
    public Transform enemyCombatStation;

    public CombatState state;

    // Start is called before the first frame update
    void Start()
    {
        state = CombatState.START;
        SetupCombat();
    }

    void SetupCombat()
    {
        Instantiate(playerPrefab, playerCombatStation);
        Instantiate(enemyPrefab, enemyCombatStation);
    }


}
