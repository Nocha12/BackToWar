using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalTarget : MonoBehaviour {

	public List<Collider> targetList;
	
	void Awake()
	{
		targetList = new List<Collider> ();
	}

	void OnTriggerEnter (Collider other) {
        if(other.CompareTag("Enemy"))
		    targetList.Add (other);
	}

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy"))
            targetList.Remove(other);
    }
}
