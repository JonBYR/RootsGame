using System.Collections;
using System.Collections.Generic;
using TMPro;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class TIleInteraction : MonoBehaviour
{
    public GameObject[] roots;
    private bool escapePress = false;
    private CameraMovement cameraMove;
    private Camera cam;
    public Image TreeGrowthChart;
    public Transform Tree;
    public Transform TreeView;
    public TMP_Text ScoreText;
    public TMP_Text highScoreText;
    public TMP_Text endlessText;
    public Button backButton;
    public Button resumeButton;
    public Button endButton;
    public AudioSource Place;
    public AudioSource TreeGrow;
    public static bool arcade = true;
    int numberOfCards = 6;
    GameObject deckObjects;
    float round = 1;
    float NextGrowth = 1;
    public float speed = 2f;
    private TMP_Text countdown;
    private float loseTime = 0f;
    [HideInInspector]
    public int RootID;
    bool RefreshDeck;
    bool i;
    [HideInInspector]
    public static int Score = 0;
    
    //   public MeshCollider outerPLane;

    [HideInInspector]
    public GameObject UsedCard;
    private void Start()
    {
        Time.timeScale = 1;
        cam = GameObject.Find("Main Camera").GetComponent<Camera>();
        cameraMove = GameObject.Find("Main Camera").GetComponent<CameraMovement>(); //use this to turn off camera movement when game ends
        deckObjects = GameObject.Find("Deck");
        countdown = GameObject.Find("Timer").GetComponent<TMP_Text>();
        TreeView = GameObject.Find("TreeView").GetComponent<Transform>();
        countdown.enabled = false; //this will be enabled as true when we reach the tree.
        highScoreText.enabled = false;
        endlessText.enabled = false;
        backButton.gameObject.SetActive(false);
        resumeButton.gameObject.SetActive(false);
        endButton.gameObject.SetActive(false);
        resumeButton.onClick.AddListener(Clicked);
    }
    void Update()
    {
        loseTime += Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (resumeButton.gameObject.activeSelf == false) { escape(); }
            else { Clicked(); }
        }
        if (Holding)
        {
            Placing();
        }
        if (numberOfCards == 0)
        {
            Time.timeScale = 0;
            scrollBack();
        }
        if (i)
        {
            cam.transform.position = cameraMove.transform.position;
            cameraMove.enabled = false;
            cam.fieldOfView = 90f;
            cam.transform.position = Vector3.MoveTowards(cam.transform.position, TreeView.position, speed * 0.01f);
            
            if (cam.transform.position == TreeView.position)
            {
                countdown.enabled = true;
                countdown.text = ("Congradulations! You just wasted " + Mathf.Round(loseTime) + " seconds of your life!");
                
            }
            if (arcade)
            {
                if (Score > PlayerPrefs.GetInt("ArcadeHighScore", 0))
                {
                    PlayerPrefs.SetInt("ArcadeHighScore", Score);
                }
                highScoreText.enabled = true;
                highScoreText.text = ("High Score " + PlayerPrefs.GetInt("ArcadeHighScore", 0));
            }
            else
            {
                if (Score > PlayerPrefs.GetInt("EndlessHighScore", 0))
                {
                    PlayerPrefs.SetInt("EndlessHighScore", Score);
                }
                endlessText.enabled = true;
                endlessText.text = ("High Score " + PlayerPrefs.GetInt("EndlessHighScore", 0));
            }
            backButton.gameObject.SetActive(true);
        }

    }
    [HideInInspector]
    public bool Holding;
    GameObject rootToPlace;
    public void PickRoot()
    {
        if (!Holding)
        {
            rootToPlace = Instantiate(roots[RootID]);
            float CameraZDistance = Camera.main.WorldToScreenPoint(transform.position).z;
            Vector3 ScreenPos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, CameraZDistance);
            Vector3 NewPos = Camera.main.ScreenToWorldPoint(ScreenPos);
            rootToPlace.transform.position = NewPos;
            Holding = true;
        }
    }

    private void Placing()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitData;
        if (Physics.Raycast(ray, out hitData) && hitData.transform.tag == "tile")
        {
            if (rootToPlace) {
                
                var rootTransform = rootToPlace.transform.position;
                var hitTransform = hitData.transform.position;
                rootToPlace.transform.position = Vector3.MoveTowards(rootTransform, hitTransform, 20f * Time.deltaTime);
                if (Input.GetKeyDown(KeyCode.Mouse0) && hitData.transform.GetComponent<tileState>().Occupied == false &&
                    rootToPlace.GetComponent<rootScript>().TouchesRoot)
                {
                    if(hitData.transform.Find("Water(Clone)") || hitData.transform.name == "WaterCube(Clone)")
                    {
                        float ToNextGrowth = 1f/round;
                        TreeGrowthChart.fillAmount+=ToNextGrowth;
                        NextGrowth--;
                        if(TreeGrowthChart.fillAmount == 1)
                        {
                            RefreshDeck = true;
                            Tree.localScale = new Vector3(Tree.localScale.x + 1, Tree.localScale.y + 1, Tree.localScale.z + 1);
                            TreeGrow.Play();
                            TreeGrowthChart.fillAmount = 0;
                            if (round < 4) { round++; }
                            Score++;
                            ScoreText.text = "Score \n"+ Score.ToString();
                            NextGrowth = round;                         
                        }
                        TreeGrowthChart.GetComponentInChildren<TMP_Text>().text = NextGrowth.ToString();
                        if (hitData.transform.name == "WaterCube(Clone)")
                        {
                            hitData.transform.GetComponent<WaterTileManager>().Dry();
                        }
                    }
                    Place.Play();
                    rootToPlace.transform.position = hitTransform;
                    hitData.transform.GetComponent<tileState>().Occupied = true;
                    DeckController deck = GameObject.Find("Deck").GetComponent<DeckController>();
                    deck.CardList.Remove(UsedCard);
                    UsedCard.SetActive(false);
                    numberOfCards--;
                    if (RefreshDeck) { newDeck(); }
                    Holding= false;
                }
            }
        }
        else
        {
            if (rootToPlace)
            {
                float CameraZDistance = Camera.main.WorldToScreenPoint(transform.position).z;
                Vector3 ScreenPos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, CameraZDistance);
                Vector3 NewPos = Camera.main.ScreenToWorldPoint(ScreenPos);
                rootToPlace.transform.position = NewPos;
            }
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            rootToPlace.transform.Rotate(0, 90, 0); 
        }

        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            Destroy(rootToPlace);
            Holding = false;
        }
        
    }
    public void newDeck()
    {
        foreach (Transform Card in deckObjects.transform)
        {
            if (Card.gameObject.activeSelf == false)
            {
                Card.gameObject.SetActive(true);
                Card.GetComponent<CardScript>().ChangeCard();
            }
        }
        RefreshDeck = false;
        numberOfCards = 6;
    }

    public void scrollBack()
    {
        i = true;
    }
    public void mainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
    }
    public void quitMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
    }
    void escape()
    {
        Time.timeScale = 0;
        deckObjects.SetActive(false);
        Destroy(rootToPlace);
        Holding = false;
        resumeButton.gameObject.SetActive(true);
        endButton.gameObject.SetActive(true);
    }
    void Clicked()
    {
        Time.timeScale = 1;
        deckObjects.SetActive(true);
        resumeButton.gameObject.SetActive(false);
        endButton.gameObject.SetActive(false);
    }
}
