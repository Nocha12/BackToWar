using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeManager : MonoBehaviour {

	public static FadeManager instance;  
	public void Awake()  
	{  
		FadeManager.instance = this;  
	}  

	public void FaidOut(GameObject gameObject)
	{
		MeshRenderer[] meshRenderers = gameObject.GetComponentsInChildren<MeshRenderer> ();
		StartCoroutine (RendererFadeOut(meshRenderers));
	}

	IEnumerator RendererFadeOut (MeshRenderer[] meshRenderers) {
		for (float f = 1f; f > 0f; f -= 0.02f) {
			foreach (MeshRenderer renderer in meshRenderers) {
				Material[] materials = renderer.materials;
				foreach (Material m in materials) {
					Color c = m.color;
					c.a = f;
					m.color = c;
				}
			}
			yield return new WaitForSeconds (.01f);
		}
		foreach (MeshRenderer renderer in meshRenderers) {
			Material[] materials = renderer.materials;
			foreach (Material m in materials) {
				Color c = m.color;
				c.a = 0;
				m.color = c;
			}
		}
	}

	public void FaidIn(GameObject gameObject)
	{
		MeshRenderer[] meshRenderers = gameObject.GetComponentsInChildren<MeshRenderer> ();
		StartCoroutine (RendererFadeIn(meshRenderers));
	}

	IEnumerator RendererFadeIn (MeshRenderer[] meshRenderers) {
		for (float f = 1f; f > 0f; f -= 0.02f) {
			foreach (MeshRenderer renderer in meshRenderers) {
				Material[] materials = renderer.materials;
				foreach (Material m in materials) {
					Color c = m.color;
					c.a = 1 - f;
					m.color = c;
				}
			}
			yield return new WaitForSeconds (.01f);
		}
		foreach (MeshRenderer renderer in meshRenderers) {
			Material[] materials = renderer.materials;
			foreach (Material m in materials) {
				Color c = m.color;
				c.a = 1;
				m.color = c;
			}
		}
	}
}
