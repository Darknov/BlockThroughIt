using UnityEngine;
using UnityEngine.UI;

public class HerbasShoesController : BoostItem {

    private bool isActive = false;
    public float initialFlyingDuration = 5f;
    public GameObject partEffect;
    private float flyingDuration;

    private void Start()
    {
        flyingDuration = initialFlyingDuration;
    }

    public override void ActivateItem()
    {
        this.isActive = true;
        Instantiate(partEffect, new Vector3( player.transform.position.x, player.transform.position.y, player.transform.position.z) , player.transform.rotation, player.transform);
    }

	void Update() {

        if (!isActive) return;
			ItemCountDown.itemTimeRemaining = flyingDuration;
			flyingDuration -= Time.deltaTime;
            player.GetComponent<Rigidbody>().useGravity = false;
            player.GetComponent<Player1Controller>().isFlying = true;

        if (flyingDuration <= 0f) {
				ItemCountDown.started = false;
				player.GetComponent<Player1Controller>().isFlying = false;
				player.GetComponent<Rigidbody>().useGravity = true;
                isActive = false;
                flyingDuration = initialFlyingDuration;
        /*    foreach (var item in partEffect.GetComponentsInChildren<Transform>())
            {
                Destroy(item.gameObject);
                Destroy(item.particleSystem.gameObject);
            }
            foreach (var item in partEffect.GetComponentsInParent<Transform>())
            {
                Destroy(item.gameObject);
            }*/
           // Destroy(partEffect.particleSystem.gameObject);
           // partEffect.particleEmitter.emit = false;
            partEffect.transform.parent = null; // detach particle system
            Destroy(partEffect.gameObject, 3);
            Destroy(gameObject);

        }
    }

}
