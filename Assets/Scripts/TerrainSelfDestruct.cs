using UnityEngine;
using System.Collections;

public class TerrainSelfDestruct : MonoBehaviour {
  public Transform player;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	  if (player.position.z > transform.position.z + 800) {
	    Destroy(this.gameObject);
	  }
	
	}
}
