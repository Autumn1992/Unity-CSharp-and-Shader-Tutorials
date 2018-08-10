using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NucleonSpawner : MonoBehaviour {

    public float timeBetweenSpawns;

    public float spwanDistance;

    public Nucleon[] nucleonPrefabs;

    float timeSinceLastSpwan;

    private void FixedUpdate()
    {
        timeSinceLastSpwan += Time.deltaTime;

        if(timeSinceLastSpwan >= timeBetweenSpawns){

            timeSinceLastSpwan -= timeBetweenSpawns;
            SpawnNucleon();
        }
    }

    void SpawnNucleon(){

        Nucleon prefab = nucleonPrefabs[Random.Range(0, nucleonPrefabs.Length)];
        Nucleon spawn = Instantiate<Nucleon>(prefab);
        spawn.transform.localPosition = Random.onUnitSphere * spwanDistance;
    }

}
