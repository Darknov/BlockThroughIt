using UnityEngine;
using UnityEngine.UI;

public class HerbasShoesController : BoostItem {

    private bool isActive = false;
    public float initialFlyingDuration = 5f;

    private float flyingDuration;

    private void Start()
    {
        flyingDuration = initialFlyingDuration;
    }

    public override void ActivateItem()
    {
        this.isActive = true;
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
		}
	}

}
