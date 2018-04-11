using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameCleared : MonoBehaviour {

    // Use this for initialization
    private GameObject player;
    public ParticleSystem fireworks;
    public Text winText;
    private bool first = true;
    void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
	}

    // Update is called once per frame
    void Update()
    {
        if (player == null) return;
        if (GetComponent<BoxCollider>().bounds.Contains(player.transform.position))
        {
            if (first)
            {
                player.GetComponent<Rigidbody>().velocity = Vector3.zero;
                if (!fireworks.isPlaying)
                {
                    fireworks.Play();
                }
                winText.enabled = true;
                Camera.main.GetComponent<Animator>().SetBool("gameCleared", true);
                GameObject.FindGameObjectWithTag("GameController").GetComponent<FollowPlayer>().GameCleared();
                first = false;
            }
        }
        if(!first)
        {
            GameObject.Destroy(player);
        }
    }
}
