using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {

  public Player playerLogic;
  public GameObject game;
  public GameObject spearObject;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
    if (Input.GetButton("Fire1")) {
	    playerLogic.StartGame();
      game.gameObject.SetActive(true);
      spearObject.gameObject.SetActive(true);
      this.gameObject.SetActive(false);
    }
  }
}
