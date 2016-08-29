using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

  public GameObject Spear;
  public Transform Camera;
  public GameObject fireFromPosition;
  public DeathBox deathBox;
  public AudioSource deathSound;

  private Rigidbody rb;
  private float fireCooldown = 0.5f;
  private float currentSpeed = 16.0f;
  private bool gameRunning = false;

  private const float BaseSpeed = 16.0f;
  private const float MinSpeed = 10.0f;
  private const float MaxSpeed = 30.0f;
  private const float ForwardControlSpeed = 5.0f;
  private const float LateralControlSpeed = 10.0f;
  private const float VerticalControlSpeed = 4.0f;
  private const float FireCooldownPeriod = 0.5f;
  public const float LeftBound = -200;
  public const float RightBound = 200;
  public const float BottomBound = 10;
  public const float TopBound = 30;

  // Use this for initialization
  void Start () {
    rb = GetComponent<Rigidbody>();
    rb.velocity = new Vector3(0,0,4);
  }
	
	// Update is called once per frame
	void Update () {
	  if (!gameRunning) {return;}

	  currentSpeed += Input.GetAxis("Vertical");
	  if (currentSpeed < MinSpeed) {
	    currentSpeed = MinSpeed;}
    if (currentSpeed > MaxSpeed)
    {
      currentSpeed = MaxSpeed;
    }
	  float lateralSpeed = Input.GetAxis("Horizontal") * LateralControlSpeed;
	  if ((transform.position.x <= LeftBound && lateralSpeed < 0 )|| 
      (transform.position.x >= RightBound && lateralSpeed > 0)) {
	    lateralSpeed *= 0;
	  }

	  float verticalSpeed = Input.GetAxis("BirdLift") * VerticalControlSpeed;
    if ((transform.position.y <= BottomBound && verticalSpeed < 0) ||
      (transform.position.y >= RightBound && verticalSpeed > 0)) {
      verticalSpeed *= 0;
    }

    rb.velocity = new Vector3(lateralSpeed , verticalSpeed, currentSpeed);

	  if ((Input.GetButton("Fire1") || Input.GetAxis("FireTrigger") > .9f)&& fireCooldown <= 0) {
      GameObject spear = Instantiate(Spear, fireFromPosition.transform.position, Camera.rotation) as GameObject;
	    Vector3 vel = spear.transform.forward * 6500;
      spear.GetComponent<Rigidbody>().AddForce(vel);
	    fireCooldown = FireCooldownPeriod;
      fireFromPosition.SetActive(false);
    }
	  if (fireCooldown <= 0) {
      fireFromPosition.SetActive(true);
    }
	  fireCooldown -= Time.deltaTime;
	}

  public void StartGame() {
    gameRunning = true;
  }

  void OnCollisionEnter(Collision col)
  {
    Debug.Log("Collision");
    if (col.gameObject.layer == LayerMask.NameToLayer("Enemy")) {
      if (col.gameObject.GetComponent<EnemyPteradon>().IsAlive()) {
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
        deathSound.Play();
        deathBox.Death();
      }
    }
  }
}
