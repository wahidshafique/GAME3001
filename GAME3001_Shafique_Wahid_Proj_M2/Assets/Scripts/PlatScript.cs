using UnityEngine;
using System.Collections;

public class PlatScript : MonoBehaviour {
    private SpriteRenderer[] turrSprite;
    public GameObject parent;
    void Start() {
        turrSprite = parent.GetComponentsInChildren<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update() {
        if (PlayerData.GetInstance.health <= 40) {
            foreach (SpriteRenderer child in turrSprite) {
                child.color = Color.red;
            }
        }
    }
}
