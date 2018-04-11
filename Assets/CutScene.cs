using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.ThirdPerson;

public class CutScene : MonoBehaviour
{

    // Use this for initialization
    private GameObject player;
    private GameObject queen;
    private bool first = true;
    public GameObject canvas;
    private SkinTest questionWindow;
    private bool helperMet;
    public Transform finishPoint;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        queen = GameObject.FindGameObjectWithTag("Queen");
        helperMet = false;
        questionWindow = canvas.GetComponent<SkinTest>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null) return;
        if (GetComponent<BoxCollider>().bounds.Contains(player.transform.position))
        {
            if (first && !helperMet)
            {
                Debug.Log("hererererer");
                player.GetComponent<move>().block();
                questionWindow.changePrincessMet();
                first = false;
            }
            else if(first && helperMet)
            {
                queen.GetComponent<Animator>().SetBool("playerMet", true);
                Debug.Log("met");
                player.GetComponent<move>().metPrincess();
                player.transform.LookAt(finishPoint.position);
                queen.transform.LookAt(player.transform.position);
                player.GetComponent<Animator>().SetBool("walkToFinish", true);
                player.GetComponent<Rigidbody>().velocity = Vector3.Normalize(finishPoint.position - player.transform.position);
                first = false;
            }
        }
        else
        {
            if(!helperMet) first = true;
        }
        if(!first && helperMet)
            queen.transform.LookAt(player.transform.position);
    }
    public void MetHelper()
    {
        helperMet = true;
    }
}
