using UnityEngine;
using System.Collections;

public class ScoutScript : MonoBehaviour {
    Animator anim;
    public Transform track;
    public GameObject projectile;
    private float nextFire = 0f;//controls first instance of instantiated object
    private float fireRate = 0.5f; //intervals for projectile to emit
    private float moveSpeed = 2;
    private float slowDown;
    private bool fire = false;
    private SpriteRenderer rend;

    void Start() {
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
            if (other.tag == "Player") {
                moveSpeed = 0f;
                anim.SetBool("FiringState", true);
                fire = true;
            }
            if (other.tag == "Bullet") {
                Destroy(anim);
                rend.sprite = Resources.Load<Sprite>("UsedAssets/oryx_16bit_scifi_creatures_126");
                StartCoroutine("Death");
            }
        }

    }
    IEnumerator Death() {
        yield return new WaitForSeconds(.1f);
        Destroy(this.gameObject);
    }

}
