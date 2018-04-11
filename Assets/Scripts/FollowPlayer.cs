using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour {
    private GameObject player;
    private bool gameCleared = false;
    private bool cutScene = false;
    private Animator main;
    // Use this for initialization
    void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        main = Camera.main.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        if (gameCleared) return;
        this.transform.position = new Vector3(player.transform.position.x,
                                                this.transform.position.y,
                                                player.transform.position.z);
	}
    public void GameCleared()
    {
        gameCleared = true;
        return;
    }
    public void ToggleCutScene()
    {
        main.SetBool("cutScene", !cutScene);
        cutScene = !cutScene;
    }
}
