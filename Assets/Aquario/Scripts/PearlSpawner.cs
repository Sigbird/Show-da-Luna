using System.Collections;
using UnityEngine;

namespace YupiPlay.Luna.Aquario {
    public class PearlSpawner : MonoBehaviour {
    public Transform SpawnPoint;    
    public float MinSpawnInterval = 1f;
    public float MaxSpawnInterval = 3f;
    public float LaunchForceMagnitude = 20f;
    public float LaunchTorqueAmount = 0.1f;
    public float LaunchAngle = 60f;

    private float currentSpawnInterval;
    private int torqueDirection = 1;    

    private ObjectPool pearlPool;
    private Rigidbody2D pearlRb;
    private GameObject pearl;

    // Use this for initialization
    void Start () {
        pearlPool = PearlPool.Instance.Pool;        
        StartCoroutine(SpawnPearl());
    }

    private IEnumerator SpawnPearl() {
        while (true) {
            if (GameController.IsPlaying) {
                currentSpawnInterval = Random.Range(MinSpawnInterval, MaxSpawnInterval);                
                yield return new WaitForSeconds(currentSpawnInterval);

                pearl = pearlPool.GetObject();
               
                if (pearl != null && SpawnPoint != null) {
                    pearl.transform.position = SpawnPoint.position;

                    pearl.transform.rotation = Quaternion.identity;                    
                    pearl.transform.rotation = Quaternion.Euler(0, 0, Random.Range(-LaunchAngle / 2, LaunchAngle / 2));
                    
                    pearlRb = pearl.GetComponent<Rigidbody2D>();

                    pearlRb.AddRelativeForce(Vector2.up * LaunchForceMagnitude);
                    pearlRb.AddTorque(torqueDirection * LaunchTorqueAmount);
                    torqueDirection = -torqueDirection;
                }
            }
            yield return new WaitForEndOfFrame();
        }              
    }
    
    // Update is called once per frame
    void Update () {
        
    }
}
}

