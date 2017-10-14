using UnityEngine;
using System.Collections;

public class PlayerControler : MonoBehaviour {
	
	public CharacterController characterController;

	public float predkoscPoruszania = 3.0f;
	public float wysokoscSkoku = 6.0f;
	private float aktualnaWysokoscSkoku = 0f;

	void Start () {
		characterController = GetComponent<CharacterController>();
	}

	void Update() {
		klawiatura();
	}

	private void klawiatura(){
		float ruchPrzodTyl = Input.GetAxis("Vertical") * predkoscPoruszania;
		float ruchLewoPrawo = Input.GetAxis("Horizontal") * predkoscPoruszania;

		if(characterController.isGrounded && Input.GetButton("Jump")){
			aktualnaWysokoscSkoku = wysokoscSkoku;
		} else if (!characterController.isGrounded ){
			aktualnaWysokoscSkoku += Physics.gravity.y * Time.deltaTime;
		}

		Debug.Log (Physics.gravity.y);

		Vector3 ruch = new Vector3(ruchLewoPrawo, aktualnaWysokoscSkoku, ruchPrzodTyl);

		ruch = transform.rotation * ruch;

		characterController.Move(ruch * Time.deltaTime);
	}

}