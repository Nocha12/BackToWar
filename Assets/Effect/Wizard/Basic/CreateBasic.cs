using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateBasic : MonoBehaviour {

    public GameObject particle;
    public Transform t;

	void Start () {
        InvokeRepeating("SpawnParticle", 1, 1);
	}

    void SpawnParticle()
    {
        Instantiate(particle, t);
        Destroy(particle, 1f);
    }
}
