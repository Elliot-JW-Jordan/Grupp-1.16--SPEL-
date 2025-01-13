using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Boss1BulletSummening : MonoBehaviour
{
    //Rigidbody
    Rigidbody2D rb;

    //Bullet prefab
    public GameObject boss1bullet;

    //Bullet settings
    public float BulletDamege = 10f;
    public float BulletSpeed = 0f;
    public float BulletAmount = 0f;
    public float Bulletspread = 0f;
    public float Bulletwait = 0.1f;
    public float BulletLifeTime = 5f;

    //boss settings
    public float MaxBossHealth = 1000f;
    public float BossHealt = 0f;
    public int CurentPhase = 0;

    //setting for logic
    private int bulletsFired = 0;
    private float nextFireTime = 0f;
    public float fireCooldown = 0.1f; 
    private bool isPhase1Active = false;
    public float nextPlayerBulletTime = 1f;
    public float playerBulletCooldown = 0f;

    //player
    public GameObject Player;

    private void Start()
    {
        //sets rigidbody
        rb = GetComponent<Rigidbody2D>();

        //geting player
        Player = GameObject.FindWithTag("Player");

        BossHealt = MaxBossHealth;
        CurentPhase = 1;

    }

    private void Update()
    {
        // phase 1
        if (BossHealt >= MaxBossHealth / 3 * 2 || BossHealt == MaxBossHealth)
        {
            if (CurentPhase != 1)
            {
                CurentPhase = 1;
            }

            phase1();
        }
        // phase 2
        else if (BossHealt >= MaxBossHealth / 3 && BossHealt < MaxBossHealth / 3 * 2)
        {
            if (CurentPhase != 2)
            {
                CurentPhase = 2;
            }

            phase2();
        }
        // phase 3
        else if (BossHealt < MaxBossHealth / 3 || BossHealt > 0)
        {
            if (CurentPhase != 3)
            {
                CurentPhase = 3;
            }

            phase3();
        }
        // dead
        else if (BossHealt == 0)
        {
            if (CurentPhase != 4)
            {
                CurentPhase = 4;
            }
        }
    }

    void phase1() // Phase 1 of the boss
    {
        if (Time.time >= nextPlayerBulletTime)
        {
            FireBulletTowardsPlayer(); ;
            nextPlayerBulletTime = Time.time + playerBulletCooldown;
        }

        if (bulletsFired < 8 && Time.time >= nextFireTime)
        {
            Phase1bullet1(bulletsFired);

            bulletsFired++;
            nextFireTime = Time.time + fireCooldown;
        }

        // Once all 8 bullets have been fired, reset bulletsFired
        if (bulletsFired >= 8)
        {
            bulletsFired = 0;
            Debug.Log("Phase 1 completed");
        }
    }

    void phase2() // Phase 2 of the boss
    {
        FireBulletTowardsPlayer();
    }

    void phase3() // Phase 3 of the boss
    {
        GameObject bullet3 = Instantiate(boss1bullet, transform.position, Quaternion.identity);

        Destroy(bullet3, BulletLifeTime);
    }




    //extention to phase 1
    private void Phase1bullet1(int index)
    {
        GameObject bullet = Instantiate(boss1bullet, transform.position, Quaternion.identity);

        float angle = index * 360f / 8; // Divide 360 degrees into 8 parts
        Vector2 direction = new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad)).normalized;

        Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();
        if (bulletRb != null)
        {
            bulletRb.velocity = direction * BulletSpeed;
        }

        float rotationAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        bullet.transform.rotation = Quaternion.Euler(new Vector3(0, 0, rotationAngle));

        Destroy(bullet, BulletLifeTime);
    }

    //code for when the bullet shots the player
    private void FireBulletTowardsPlayer()
    {
        GameObject bullet = Instantiate(boss1bullet, transform.position, Quaternion.identity);
        Vector2 direction = (Player.transform.position - transform.position).normalized;

        Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();
        if (bulletRb != null)
        {
            bulletRb.velocity = direction * BulletSpeed;
        }

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        bullet.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

        Destroy(bullet, BulletLifeTime);
    }
}
