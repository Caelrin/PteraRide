using UnityEngine;
using System.Collections;

public class PackScript : MonoBehaviour {

  public GameObject playerFlyingTowards;
  public EnemyPteradon[] packCreatures;

  private Rigidbody rb;
  private bool lastLeanedRight = false;
  private const float Speed = -10;
  private const float LateralSpeed = 10;

  // Use this for initialization
  void Start () {
    rb = GetComponent<Rigidbody>();
    if (playerFlyingTowards.transform.position.x < transform.position.x) {
      Left();
    } else {
      Right();
    }
  }
	
	// Update is called once per frame
	void Update () {
	  float difference = playerFlyingTowards.transform.position.x - transform.position.x;
//    Debug.Log(difference);
    if (playerFlyingTowards.transform.position.x < transform.position.x) {
//      rb.velocity = new Vector3(-1 * LateralSpeed, 0, Speed);
      if (lastLeanedRight && Mathf.Abs(difference) > 5) {
        Left();
      }
    }
    else {
//      rb.velocity = new Vector3(LateralSpeed, 0, Speed);
      if (!lastLeanedRight && Mathf.Abs(difference) > 5)
      {
       Right();
      }
    }

    // Destroy if it has flown past the player
    if (playerFlyingTowards.transform.position.z - 100 > transform.position.z)
    {
      Destroy(this.gameObject);
    }
  }

  private void Left() {
    rb.velocity = new Vector3(-1 * LateralSpeed, 0, Speed);
    foreach (EnemyPteradon pteradon in packCreatures)
    {
      pteradon.LeanLeft();
      pteradon.MakeEmMove(new Vector3(-1 * LateralSpeed, 0, Speed));
    }
    lastLeanedRight = false;
  }
  private void Right() {
    rb.velocity = new Vector3(LateralSpeed, 0, Speed);
    foreach (EnemyPteradon pteradon in packCreatures)
    {
      pteradon.LeanRight();
      pteradon.MakeEmMove(new Vector3(LateralSpeed, 0, Speed));
    }
    lastLeanedRight = true;
  }
}
