using UnityEngine;
using System.Collections;

public class PlayerControler : MonoBehaviour {
	
	public CharacterController characterControler;

	public float predkoscPoruszania = 3.0f;
	public float wysokoscSkoku = 6.0f;
	private float aktualnaWysokoscSkoku = 0f;

	void Start () {
		characterControler = GetComponent<CharacterController>();
	}

	void Update() {
		klawiatura();
	}

	private void klawiatura(){
		float rochPrzodTyl = Input.GetAxis("Vertical") * predkoscPoruszania;
		float rochLewoPrawo = Input.GetAxis("Horizontal") * predkoscPoruszania;

		if(characterControler.isGrounded && Input.GetButton("Jump")){
			aktualnaWysokoscSkoku = wysokoscSkoku;
		} else if (!characterControler.isGrounded ){
			aktualnaWysokoscSkoku += Physics.gravity.y * Time.deltaTime;
		}

		Debug.Log (Physics.gravity.y);

		Vector3 ruch = new Vector3(rochLewoPrawo, aktualnaWysokoscSkoku, rochPrzodTyl);

		ruch = transform.rotation * ruch;

		characterControler.Move(ruch * Time.deltaTime);
	}

}