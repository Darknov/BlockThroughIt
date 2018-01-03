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
        //Instantiate(timebar, new Vector3(0, 2, -9), new Quaternion(39, 1, 180, 0) );
    }

	void Update() {

        if (!isActive) return;
			ItemCountDown.itemTimeRemaining = flyingDuration;
			flyingDuration -= Time.deltaTime;
        GameObject.FindGameObjectWithTag("Timebar").SendMessage("SubTime", flyingDuration);

            player.GetComponent<Rigidbody>().useGravity = false;
            StaticOptions.isFlying = true;

        if (flyingDuration <= 0f) {
				ItemCountDown.started = false;
				StaticOptions.isFlying = false;
				player.GetComponent<Rigidbody>().useGravity = true;
                isActive = false;
                flyingDuration = initialFlyingDuration;
            partEffect.transform.parent = null; // detach particle system
            Destroy(partEffect.gameObject, 3);
            Destroy(gameObject);

        }
    }

}
