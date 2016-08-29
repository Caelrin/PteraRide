using UnityEngine;
using System.Collections;

public class LandGenerator : MonoBehaviour {

  public GameObject player;
  public GameObject terrainPrefab;

  private int numberTerrain = 0;
  private const float TerrainLength = 500f;

	// Use this for initialization
	void Start () {
    Terrain terrain = terrainPrefab.GetComponent<Terrain>();
    var nRows = 513;
    var nCols = 513;
    var heights = new float[nRows, nCols];
	  for (var j = 0; j < nRows; j++) {
	    for (var i = 0; i < nCols; i++) {
	      if (j >= 510) {
	        heights[j, i] = heights[0, i];
	      } else if (i < 25) {
	        heights[j, i] = (25 - i)/25f * .15f;
	      } else if (i > 475) {
	        heights[j, i] = (i - 475)/25f * .15f; 
        } else if (j == 0) {
          heights[j, i] = 0;
        } else {
          heights[j, i] = 0;//Random.Range(0.0f, 0.001f);
	      }
	    }
	  }

	  terrain.terrainData.SetHeights(0, 0, heights);
  }
	
	// Update is called once per frame
	void Update () {
	  if (player.transform.position.z > ((numberTerrain - 2) * TerrainLength)) {
      Debug.Log("Generating new terrain");
	    numberTerrain ++;
	    Instantiate(terrainPrefab, new Vector3(-250, 0, -250 + (numberTerrain * TerrainLength)), Quaternion.identity);

	  }
	}
}
