using UnityEngine;
using System.Collections;

public class EnemyGenerator : MonoBehaviour {

  public GameObject player;
  public EnemyPteradon enemyPteradonPrefab;
  public PackScript packPrefab;
  public Score score;

  private float timeSinceSpawn = 5.0f;
  private float SpawningTime = 5.0f;
  private int wave = 5;
  private int toSpawn = 0;

  private const int MaxWaveSize = 50;
  private const float MinSpawnInterval = 2.0f;

	// Use this for initialization
	void Start () {
    enemyPteradonPrefab.playerFlyingTowards = player;
    enemyPteradonPrefab.score = score;
  }
	
	// Update is called once per frame
	void Update () {
	  timeSinceSpawn += Time.deltaTime;
	  if (timeSinceSpawn >= SpawningTime) {
	    timeSinceSpawn = 0.0f;
      spawn(wave);
	    if (wave < MaxWaveSize) {
        wave++;
      }
	    if (SpawningTime > MinSpawnInterval) {
        SpawningTime *= .95f;
      }
	  }
    
  }

  private void spawn(int toSpawn) {
    
    int rows = Random.Range(Mathf.Min(toSpawn/10, 1), 4);
    if (rows <= 1) {rows = 1;}

    float leftPoint = player.transform.position.x - (toSpawn/rows * 1) + Random.Range(-40, 40);
    if (leftPoint < Player.LeftBound)
    {
      leftPoint = Player.LeftBound;
    }

    PackScript newPack = Instantiate(packPrefab,
      new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z + 200),
      Quaternion.identity) as PackScript;
    newPack.playerFlyingTowards = player;
    newPack.packCreatures = new EnemyPteradon[toSpawn];

    float firstBirdX = leftPoint;
    float lastBirdX = leftPoint;

    for (int i = 0; i < toSpawn;) {
      for (int j = 0; j < rows  && i < toSpawn; j++, i++) {
        EnemyPteradon ptera = Instantiate(enemyPteradonPrefab,
          new Vector3(leftPoint + Random.Range(-1.0f, 1.0f), Random.Range(Player.BottomBound, Player.TopBound),
            player.transform.position.z + 200), Quaternion.identity) as EnemyPteradon;
        ptera.playerFlyingTowards = player;
        ptera.transform.parent = newPack.transform;
        newPack.packCreatures[i] = ptera;

      }
     
      
      leftPoint += Random.Range(2, 6);
      if (leftPoint >= Player.RightBound){
        leftPoint = 0;
      }
      if (leftPoint > lastBirdX)
      {
        lastBirdX = leftPoint;
      }
    }
    
    float middleX = (firstBirdX + lastBirdX) / 2.0f;
//    Debug.Log("F: " + firstBirdX + " L: " + lastBirdX + "M: " + middleX);
    newPack.transform.position = new Vector3(middleX + Random.Range(-10, 10), player.transform.position.y, player.transform.position.z + 200);
  }
}
