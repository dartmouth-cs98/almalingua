using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuinsObject : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject enemyPrefab;           //the prefab square that shows up above a npc
    private GameObject enemy; 
    private void OnCollisionEnter2D(Collision2D other) {
        QuestUI.SetQuestStep(2);
        enemy = GameObject.Find("Devil Variant");
        enemy.transform.position = new Vector3(gameObject.transform.position.x,
                                                            gameObject.transform.position.y + (gameObject.GetComponent<BoxCollider2D>().bounds.size.y) / 2 + enemy.transform.GetChild(0).GetComponent<SpriteRenderer>().bounds.size.y
                                                            , 0);
    }
}
