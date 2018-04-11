using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move : MonoBehaviour {

    // Use this for initialization
    public Rigidbody obj;
    private bool mobile;
    public GameObject canvas;
    private SkinTest questionWindow;
    private List<GameObject> zombies;
    private bool met= false;
    void Start () {
        obj.freezeRotation = true;
        mobile = true;
        questionWindow = canvas.GetComponent<SkinTest>();
        zombies = new List<GameObject>();
    }
    private void FixedUpdate()
    {
        if (met)
            return;
        if (mobile)
        {
            float v = Input.GetAxisRaw("Horizontal");

            float u = Input.GetAxisRaw("Vertical");
            if (v != 0 || u != 0)
            {
                GetComponent<Animator>().SetFloat("Forward", 0.5f);
            }
            else
            {
                GetComponent<Animator>().SetFloat("Forward", -1.0f);
            }
        }
        else
        {
            GetComponent<Animator>().SetFloat("Forward", -1.0f);
        }
    }
    // Update is called once per frame
    void LateUpdate () {
        if (met) return;
        float v = Input.GetAxis("Horizontal");

        float u = Input.GetAxis("Vertical");

        Vector3 targetObj = Vector3.Normalize(new Vector3(-v, 0, -u))*1.414f;

        // Smoothly rotate towards the target point.
        
        if (mobile)
        {
            transform.LookAt(targetObj + transform.position);
            obj.velocity = targetObj;
        }
        else
        {
            obj.velocity = Vector3.zero;
        }

        if (zombies.Count > 0)
        {
            if (!zombies[0].activeSelf)
            {
                Debug.Log("here " + zombies.Count);
                zombies.ForEach(z => z.SetActive(false));
                zombies.Clear();
            }
        }
    }
    
    public void changeMobility()
    {
        mobile = !mobile;
    }
    public void block()
    {
        mobile = false;
    }
    public void metPrincess()
    {
        met = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag.Equals("Zombie"))
        {
            if (zombies.Count == 0)
            {
                zombies.Add(collision.gameObject);
            }
            else if (!zombies[0].activeSelf)
            {
                Debug.Log("here " + zombies.Count);
                zombies.ForEach(z => z.SetActive(false));
                zombies.Clear();
                zombies.Add(collision.gameObject);
            }
            else
            {
                Debug.Log("woah");
                collision.gameObject.GetComponent<UnityStandardAssets.Characters.ThirdPerson.AICharacterControl>().enabled = false;
                collision.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
                collision.gameObject.GetComponent<Animator>().SetBool("playerMet", true);
                zombies.Add(collision.gameObject);
                return;
            }
            collision.gameObject.GetComponent<UnityStandardAssets.Characters.ThirdPerson.AICharacterControl>().enabled = false;
            collision.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
            collision.gameObject.GetComponent<Animator>().SetBool("playerMet", true);
            this.GetComponent<move>().block();
            questionWindow.changeMet(collision.gameObject);
        }
        else if (collision.gameObject.tag.Equals("Helper"))
        {
            this.GetComponent<move>().block();
            //collision.gameObject.transform.LookAt(this.gameObject.transform.position);
            questionWindow.changeHelperMet();
        }
    }
}
