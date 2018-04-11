using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SkinTest : MonoBehaviour
{
    private GameObject zombieMet = null;
    public GUISkin thisMetalGUISkin;
    private Rect rctWindow2;
    private bool[,] blnToggleState = { { false, false },
                                        { false, false },
                                        { false, false },
                                        { false, false },
                                        };

    private string[] Questions = { "How do you do?", "Are you an asshole?", "Be my girfriend?", "Should I eat you?" };
    private string[,] choices = { { "seasick", "fine" },
                                    { "yes", "no" },
                                    { "yes", "no" },
                                    { "yes", "no" } };
    private bool[,] answers = { { true, false },
                                        { true, false },
                                        { true, false },
                                        { false, false },
                                        };
    private bool notMet = true;
    private bool princessNotMet = true;
    private bool helperNotMet = true;
    private int choiceOfQuestion;
    private GameObject player;
    private Transform queen;
    private GameObject helper;
    private FollowPlayer camController;
    private int numChoices;
    public GameObject CutScene;
    public GameObject lifeController;
    void Start()
    {
        Vector3 origin = Camera.main.ViewportToScreenPoint(new Vector3(0.25F, 0.75F, 0));
        Vector3 extent = Camera.main.ViewportToScreenPoint(new Vector3(0.5F, 0.2F, 0));
        rctWindow2 = new Rect(origin.x, origin.y, extent.x, extent.y);
        player = GameObject.FindGameObjectWithTag("Player");
        queen = GameObject.FindGameObjectWithTag("Queen").GetComponent<Transform>();
        helper = GameObject.FindGameObjectWithTag("Helper");
        helper.transform.LookAt(queen.position);
        camController = GameObject.FindGameObjectWithTag("GameController").GetComponent<FollowPlayer>();
        numChoices = 2;
    }
    void OnGUI()
    {
        if(!notMet)
        {
            GUI.skin = thisMetalGUISkin;
            rctWindow2 = GUILayout.Window(1, rctWindow2, DoMyWindow2, "Metal Vista", GUI.skin.GetStyle("window"));
        }
        else if (!princessNotMet)
        {
            GUI.skin = thisMetalGUISkin;
            rctWindow2 = GUILayout.Window(1, rctWindow2, DoMyWindow4, "Metal Vista", GUI.skin.GetStyle("window"));
        }
        else if(!helperNotMet)
        {
            GUI.skin = thisMetalGUISkin;
            rctWindow2 = GUILayout.Window(1, rctWindow2, DoMyWindow3, "Metal Vista", GUI.skin.GetStyle("window"));
        }
        else
        {
            choiceOfQuestion = Random.Range(0, 3);
        }
    }

    public void changeMet(GameObject zombie)
    {
        notMet = false;
        zombieMet = zombie;
        camController.ToggleCutScene();
    }
    public void changeHelperMet()
    {
        helperNotMet = false;
        camController.ToggleCutScene();
    }
    public void changePrincessMet()
    {
        princessNotMet = false;
        camController.ToggleCutScene();
    }
    void DoMyWindow4(int windowID)
    {
        GUILayout.Label("Locked");
        GUILayout.BeginVertical();
        GUILayout.TextArea("Go find the helper, or you could \nsit and watch my belly dance <3, Watch my ass");
        GUILayout.EndVertical();
        GUILayout.BeginVertical();
        if (GUILayout.Button("Okay!"))
        {
            princessNotMet = !princessNotMet;
            player.GetComponent<move>().changeMobility();
            camController.ToggleCutScene();
        }
        GUILayout.EndVertical();
        GUI.DragWindow();
    }
    void DoMyWindow3(int windowID)
    {
        GUILayout.Label("Madame");
        GUILayout.BeginVertical();
        GUILayout.TextArea(Questions[choiceOfQuestion]);
        GUILayout.EndVertical();
        GUILayout.BeginVertical();
        GUILayout.BeginHorizontal();
        for (int j = 0; j < numChoices; j++)
        {
            blnToggleState[choiceOfQuestion, j] = GUILayout.Toggle(blnToggleState[choiceOfQuestion, j], choices[choiceOfQuestion, j]);
        }
        GUILayout.EndHorizontal();
        GUILayout.EndVertical();
        GUILayout.BeginVertical();
        if (GUILayout.Button("Lock Answers"))
        {
            int count = 0;
            for (int j = 0; j < numChoices; j++)
            {
                if (!blnToggleState[choiceOfQuestion, j])
                {
                    count++;
                }
            }
            if (count != numChoices)
            {
                bool correct = true;
                for (int j = 0; j < numChoices; j++)
                {
                    if (blnToggleState[choiceOfQuestion, j] != answers[choiceOfQuestion, j])
                    {
                        correct = false;
                    }
                    blnToggleState[choiceOfQuestion, j] = false;
                }
                if (correct)
                {
                    GameObject.FindGameObjectWithTag("PrisonDoor").SetActive(false);
                    //helper.transform.LookAt(queen.position);
                    helper.GetComponent<Animator>().SetBool("playerMet", true);
                    helperNotMet = !helperNotMet;
                    camController.ToggleCutScene();
                    player.GetComponent<move>().changeMobility();

                    CutScene.GetComponent<CutScene>().MetHelper();
                }
                else
                {
                    if(lifeController.GetComponent<LifeController>().takeLife())
                    {
                        camController.ToggleCutScene();
                        player.GetComponent<move>().changeMobility();
                    }
                    helperNotMet = !helperNotMet;
                }
            }

        }
        GUILayout.EndVertical();
        GUI.DragWindow();
    }
    void DoMyWindow2(int windowID)
    {
        GUILayout.Label("Parley");
        GUILayout.BeginVertical();
        GUILayout.TextArea(Questions[choiceOfQuestion]);
        GUILayout.EndVertical();
        GUILayout.BeginVertical();
        GUILayout.BeginHorizontal();
        for (int j = 0; j < numChoices; j++)
        {
            blnToggleState[choiceOfQuestion,j] = GUILayout.Toggle(blnToggleState[choiceOfQuestion,j], choices[choiceOfQuestion,j]);
        }
        GUILayout.EndHorizontal();
        GUILayout.EndVertical();
        GUILayout.BeginVertical();
        if(GUILayout.Button("Lock Answers"))
        {
            int count = 0;
            for (int j = 0; j < numChoices; j++)
            {
                if(!blnToggleState[choiceOfQuestion, j])
                {
                    count++;
                }
            }
            if(count != numChoices)
            {
                bool correct = true;
                for (int j = 0; j < numChoices; j++)
                {
                    if (blnToggleState[choiceOfQuestion, j] != answers[choiceOfQuestion, j])
                    {
                        correct = false;
                    }
                    blnToggleState[choiceOfQuestion, j] = false;
                }
                if(correct)
                {
                    zombieMet.SetActive(false);
                    notMet = !notMet;
                    camController.ToggleCutScene();
                    player.GetComponent<move>().changeMobility();
                }
                else
                {
                    if(lifeController.GetComponent<LifeController>().takeLife())
                    {
                        zombieMet.SetActive(false);
                        camController.ToggleCutScene();
                        player.GetComponent<move>().changeMobility();
                    }
                    notMet = !notMet;
                }
            }
            
        }
        GUILayout.EndVertical();
        GUI.DragWindow();
    }
}
