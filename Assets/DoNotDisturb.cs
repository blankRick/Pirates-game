using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoNotDisturb : MonoBehaviour {

    // Use this for initialization
    private GameObject player;
    public GameObject[] spaces;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
	// Update is called once per frame
	void Update () {
        bool isPresent = false;
        if(player == null)
        {
            return;
        }
        foreach(GameObject s in spaces)
        {
            if (s.GetComponent<BoxCollider>().bounds.Contains(player.transform.position))
            {
                isPresent = true;
                break;
            }
        }
        if (isPresent)
        {
            foreach(GameObject z in GameObject.FindGameObjectsWithTag("Zombie"))
            {
                z.GetComponent<UnityStandardAssets.Characters.ThirdPerson.AICharacterControl>().target = null;
            }
        }
        else
        {
            foreach (GameObject z in GameObject.FindGameObjectsWithTag("Zombie"))
            {
                z.GetComponent<UnityStandardAssets.Characters.ThirdPerson.AICharacterControl>().target = player.transform;
            }
        }
	}
}
