using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour {
	public int normalDamage = 10;
	public NormalTarget normalTarget;

	public GameObject particle;
	public Transform t;

	public void NormalAttack()
	{
		List<Collider> targetList = new List<Collider>(normalTarget.targetList);

        foreach (Collider one in targetList) {
            if (one && one.GetComponent<EnemyHealth>())
            {
                EnemyHealth enemy = one.GetComponent<EnemyHealth>();
                if (enemy)
                {
                    StartCoroutine(enemy.StartDamage(normalDamage, transform.position, 0.5f, 20));
                }
            }
		}
		StartCoroutine (MakeEffect());
	}
	IEnumerator MakeEffect()
	{
		yield return new WaitForSeconds (0.3f);
		GameObject p = Instantiate(particle);
		p.transform.position = t.position;
		Destroy(p, 1f);
	}
}