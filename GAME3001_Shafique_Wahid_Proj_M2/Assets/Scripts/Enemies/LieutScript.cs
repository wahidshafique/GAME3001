using UnityEngine;
using System.Collections;

public class LieutScript : MonoBehaviour {
    Animator anim;
    private Transform track;
    public GameObject projectile;

    private SpriteRenderer rend;
    private float nextFire = 0f;//controls first instance of instantiated object
    private float fireRate = 0.5f; //intervals for projectile to emit
    public float moveSpeed = 2;
    private bool fire = true;
    private float slowDown;
    private Vector2 point;
    private float epsilon = 0.5f;

    void Start() {
        point = Random.insideUnitCircle * 5;
        track = GameObject.Find("Turret").transform;
        rend = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }
    // Update is called once per frame
    void Update() {
        
        Wander();

        StartFiring();
    }

    void Wander() {
        Random.seed = (int)System.DateTime.Now.Ticks;
        slowDown = Random.Range(0, moveSpeed - 1);
        float move = moveSpeed * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, point, move);
        if (Vector2.Distance(transform.position, point) < epsilon) {
            point = Random.insideUnitCircle * 5;
        }
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
            }
            if (other.tag == "Bullet") {
                PlayerData.GetInstance.score += 1;
                Destroy(other.gameObject);
                moveSpeed = 0; 
                Destroy(anim);
                rend.sprite = Resources.Load<Sprite>("UsedAssets/oryx_16bit_scifi_creatures_157");
                StartCoroutine("Death");
            }
        }
    }
    IEnumerator Death() {
        yield return new WaitForSeconds(.1f);
        Destroy(this.gameObject);
    }

}
