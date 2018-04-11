using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeController : MonoBehaviour {

    // Use this for initialization
    public GameObject[] lifeOrbs;
    private int numLives;
    public Text GameOver;
    public GameObject fadePlane;
    private Animator fadeAnim;
    void Start () {
        numLives = lifeOrbs.Length;
        fadeAnim = fadePlane.GetComponent<Animator>();
	}
	
	// return false if numLives becomes 0
	public bool takeLife()
    {
        numLives--;
        lifeOrbs[numLives].GetComponent<MeshFilter>().mesh = null;
        lifeOrbs[numLives].GetComponent<ParticleSystem>().Play();
        Behaviour h = (Behaviour)(lifeOrbs[numLives].GetComponent("Halo"));
        h.enabled = false;
        if (numLives<=0)
        {
            GameOver.enabled = true;
            fadeAnim.SetBool("gameOver", true);
            return false;
        }
        return true;
    }
}
