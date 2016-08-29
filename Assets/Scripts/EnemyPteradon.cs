using UnityEngine;
using System.Collections;
using UnityEngine.Assertions.Comparers;

public class EnemyPteradon : MonoBehaviour {

  public GameObject playerFlyingTowards;
  public Score score;
  public AudioSource deathSound;

  private Rigidbody rb;
  private bool alive = true;
  private float timeUntilDeath = float.MaxValue;

	// Use this for initialization
	void Start () {
	  rb = GetComponent<Rigidbody>();
   
  }
	
	// Update is called once per frame
	void Update () {
//	  timeUntilDeath -= Time.deltaTime;
//	  if (timeUntilDeath <= 0) {
////	    Destroy(this.gameObject);
//	  }

    
	}

  public bool IsAlive() {
    return alive;
  }

  public void LeanLeft() {
    if (!alive) {
      return;
    }
    rb = GetComponent<Rigidbody>();
    rb.rotation = Quaternion.AngleAxis(10, Vector3.forward);
  }

  public void LeanRight()
  {
    if (!alive)
    {
      return;
    }
    rb = GetComponent<Rigidbody>();
    rb.rotation = Quaternion.AngleAxis(-10, Vector3.forward);
  }

  public void MakeEmMove(Vector3 v3) {
    if (!alive)
    {
      return;
    }
    GetComponent<Rigidbody>().velocity = v3;
  }

  void OnCollisionEnter(Collision col)
  {
    Debug.Log("Collision");
    if (col.gameObject.layer == LayerMask.NameToLayer("Weapon"))
    {
      Spear spear = col.gameObject.GetComponent<Spear>();
      if (!spear.IsActive()) {
        return;
      }
      score.addPoints(500 + (long)(transform.position.z - playerFlyingTowards.transform.position.z));
      deathSound.Play();
      if (timeUntilDeath > 2.0f) {
        timeUntilDeath = 2.0f;
        alive = false;
      }

      spear.consumed();
    }
  }
}
