﻿using UnityEngine;
using System.Collections;

public class Boss : MonoBehaviour {
    Animator anim;
    private Transform track;
    public GameObject projectile;

    private SpriteRenderer rend;
    private float nextFire = 0f;//controls first instance of instantiated object
    private float fireRate = 0.5f; //intervals for projectile to emit
    public float moveSpeed = 2;
    private float slowDown;
    private bool fire = true;
    private int health = 100;
    public bool isBoss = false;


    void Start() {
        track = GameObject.Find("Turret").transform;
        rend = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }
    // Update is called once per frame
    void Update() {
        Random.seed = (int)System.DateTime.Now.Ticks;
        slowDown = Random.Range(0, moveSpeed - 1);
        float move = moveSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, track.position, move);

        StartFiring();
    }

    void StartFiring() {
        if (Time.time > nextFire && fire) {
            nextFire = Time.time + fireRate;
            Vector3 position = transform.position;
            Instantiate(projectile, position, transform.rotation);
        }
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (moveSpeed >= 0) {
            if (other.tag == "PlayerRad") {
                moveSpeed -= slowDown;
            }
            if (other.tag == "Bullet") {
                health -= 1;
                Destroy(other.gameObject);
                if (health == 0) {
                    PlayerData.GetInstance.score += 1;
                    moveSpeed = 0;
                    Destroy(anim);
                    rend.sprite = Resources.Load<Sprite>("UsedAssets/oryx_16bit_scifi_items_86");
                    StartCoroutine("Death");
                }
            }
        }
    }
    IEnumerator Death() {
        //Time.timeScale = 0;
        if (isBoss) {
            GameObject.Find("Main Camera").GetComponent<CameraFilterPack_FX_Glitch1>().enabled = true;
            yield return new WaitForSeconds(1f);
            AudioClip reel = Resources.Load<AudioClip>("Sounds/win") as AudioClip;
            AudioSource.PlayClipAtPoint(reel, Vector2.zero);
            yield return new WaitForSeconds(1f);
            Application.LoadLevel("MainMenu");

        }
    }

}
