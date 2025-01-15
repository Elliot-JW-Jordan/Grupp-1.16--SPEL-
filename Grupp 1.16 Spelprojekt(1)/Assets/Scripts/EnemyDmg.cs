using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDmg : MonoBehaviour
{
    public playerHealth playerhealth;
    public int damage = 2;

    void Start()
    {
        GameObject player = GameObject.FindWithTag("Player");

        if (player != null)
        {
            playerhealth = player.GetComponent<playerHealth>();
        }
        else
        {
            Debug.LogError("No GameObject with the tag 'Player' found in the scene!");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
           playerhealth.TakeDamage(damage);
        }
    }
}
