using UnityEngine;
using System.Collections;
using System.Linq;
using UnityEngine.Rendering;

public class DeathBox : MonoBehaviour {

  private bool dieing = false;
  private float timeToDeath = 0.0f;
  private Material material;
  private MeshRenderer renderer;

  private const float DeathDuration = 3.0f;
  private const float TimeToBlack = 1.5f;

  // Use this for initialization
  void Start () {
    Mesh mesh = GetComponent<MeshFilter>().mesh;
    mesh.triangles = mesh.triangles.Reverse().ToArray();
  }
	
	// Update is called once per frame
	void Update () {
	  if (!dieing) {return;}

	  timeToDeath += Time.deltaTime;
    if (timeToDeath >= DeathDuration) {
      Application.LoadLevel(Application.loadedLevel);
    }
	  float lerp = timeToDeath/ TimeToBlack;
	  if (lerp > 1) {lerp = 1;}
    material.color = Color.Lerp(new Color(0.0f, 0.0f, 0.0f, 0.0f), new Color(0.0f, 0.0f, 0.0f, 1.0f), lerp);
  }

  public void Death() {
    renderer = gameObject.GetComponent<MeshRenderer>();
    material = renderer.material;
    material.color = new Color(0,0,0,0f);
    dieing = true;
    this.gameObject.SetActive(true);
  }
}
