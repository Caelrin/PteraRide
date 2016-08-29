using System;
using UnityEngine;
using System.Collections;

public class Spear : MonoBehaviour {

  public AudioSource hitSound;

  private bool active = true;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

  public void consumed() {
    active = false;
  }

  public bool IsActive() {
    return active;
  }

  void OnCollisionEnter(Collision col) {
    hitSound.Play();
    if (col.gameObject.layer == LayerMask.NameToLayer("Terrain")) {
      Destroy(this.gameObject);
    }
  }
}
