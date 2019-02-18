using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class GameController : MonoBehaviour {
    public Text questionDisplayText;
    public Text scoreDisplayText;
    public SimpleObjectPool answerButtonObjectPool;
    public Transform answerButoonParent;
    public GameObject questionDisplay;
    public GameObject roundOverDisplay;
    public GameObject pauseDisplay;
    public GameObject fadeImage;
    public Text yourLivesDisplayText;
    public Text highScoreDisplay;
   // public Button musicButton;
  //  public Sprite forMusicButtonOn;
  //  public Sprite forMusicButtonOff;
    public GameObject rewardButton;
    public Text currentScoreTextDisplay;
//    public GameObject wrongAnswerImage;
    public Text counterText;
    public GameObject winPanel;
    public GameObject winImage;
    public Sprite[] forWinPanels;
    public Text currentScoreForWinPanel;
    public Text highScoreForWinPanel;
    public RectTransform UI;
    

    private DataController dataController;
    private QuestionData[] questionPool;
    private RoundData currentRoundData;
    private bool isRoundActive;
    // private float timeRemaining =40f;
    private int yourLives;
    private int counter;
    private int questionIndex;
    private int playerScore;
    private GameObject yesno;
    private List<GameObject> answerButtonGameObjects = new List<GameObject>();
    private List<int> answeredQuestions;
    private int forStories;
   // private AudioManager audio;
    private string[] wrongAnswers = { "Tirana", "Astana", "Berlin", "Luanda",
        "Porto-Novo", "Andorra la Vella", "Vaşinqton", "Sent-Cons", "Bişkek",
        "Düşənbə", "Nassau", "Aşqabad", "Brictaun", "Daşkənd", "Belmopan",
        "Macau", "Santo-Dominiko", "Vyana", "Qaborone", "Minsk", "Rozo",
        "Uaqaduqu", "San-Salvador", "Tokio", "Bujumbura", "Əlcəzair", "Ulan Bator",
        "Brüssel","Sarayevo", "Sofiya","Montevideo", "Pxenyan","Pekin", "Cibuti",
        "Sent-Corces", "Paramaribo", "Ncamena", "Kinşasa", "Qvatemala", "Malabo",
        "Asmera", "London", "Port-o-Prens", "Tequsiqalpa", "Praqa","Seul", "Podqoritsa",
        "Addis Ababa", "Kinqston", "Ottava", "Rabat", "Yamusukro", "Librevil", "Banjul",
        "Moskva", "Bəndər-Səri-Bəqavan", "Neypido", "Pnompen", "Dili", "Cakarta",
        "Kopenhagen", "Tallin", "Helsinki", "Paris", "Vyentyan", "Kuala-Lumpur",
        "Akkra", "Manila", "Konakri", "San-Xose", "Sinqapur", "Bisau", "Banqkok",
        "Hanoy", "Kabil", "Havana","Keyptaun", "Kuba", "Yaunde", "Dəkkə", "Thimphu",
        "Mexiko", "Nayrobi", "Dehli", "Moroni", "Brazzavil", "Male", "Maseru",
        "Zaqreb", "Reykyavik", "Dublin","Monroviya", "Tripoli", "Antananarivo",
        "Madrid", "Lilonqve", "Kətməndu", "Bamako", "İslamabad", "Stokholm",
        "Port Luiz", "Manaqua", "Qahirə", "Bern", "Roma","Şri Jayavardenapura Kotte",
        "Bakı", "Nuakşot", "Manama", "Maputu", "Vindhuk", "Panama", "Priştina", "Nikosiya",
        "Tbilisi", "Bağdad", "Baster", "Santyaqo", "Kito", "Riqa", "Vaduts", "Vilnyus", "Niamey",
        "Lüksemburq", "Kastri", "Budapeşt", "Abuca", "Skopye", "Tehran", "Bangi", "Kiqali",
        "Port of Spain", "Buenos-Ayres", "Sukre", "Brazilia", "Valletta", "Təl Əviv",
        "Əmman", "Kişinyov", "Monako", "San Tome", "Dakar", "Karakas", "Corctaun", "Viktoriya",
        "Küveyt", "Beyrut", "Məsqət", "Fritaun","Moqadişo", "Santa-Fe-De-Boqota", "Xartum", "Ramallah",
        "Doha", "Ər-Riyad", "Mbabane", "Amsterdam", "Asunsion", "Oslo", "Dəməşq", "Varşava", "Lissabon",
        "Dodoma", "Lome", "Stenli", "Buxarest", "Əbu-Dabi", "Səna", "San-Marino", "Belqrad", "Bratislava",
        "Lyublyana", "Ankara", "Kiyev", "Vatikan", "Afina", "Tunis", "Kampala", "Pray", "Lusaka", "Harare", "Kayenna", "Qrütviken", "Lima" };
  //  private Vector3 heartpos = new Vector3(-198f,-309f,0f);
 //   private Vector3 scorepos = new Vector3(188f, -309f, 0f);

    

    void Start () {
   
        //  if (!audio)
        //  audio = GameObject.FindObjectOfType<AudioManager>();
        yourLives = 2;
        counter = 1;
        UpdateYourLivesDisplay(yourLives);
        dataController = FindObjectOfType<DataController>();
        currentRoundData = dataController.GetCurrentRoundData(MenuSceneController.questionSelected);

        winImage.GetComponent<Image>().sprite = forWinPanels[MenuSceneController.questionSelected];
 
        counterText.text = counter + "/" + currentRoundData.questions.Length;
        forStories = currentRoundData.questions.Length;
        
        if (PlayerPrefs.GetInt("Music") == 0)
        {
          //  musicButton.image.sprite = forMusicButtonOff;
        }
        else
        {
          //  musicButton.image.sprite = forMusicButtonOn;
        }

        questionPool = currentRoundData.questions;
     //   timeRemaining = 20f;
       // UpdateTimeRemainingDisplay();
        questionIndex = Random.Range(0,forStories);
       // yesno = GameObject.Find("TF");
        answeredQuestions = new List<int>();
        playerScore = 0;
        isRoundActive = true;
        rewardButton.SetActive(true);

        ShowQuestion();
        
	}
    public void ShowQuestion()
    {
        List<string> wrongAnswerforSpecificQuestion = new List<string>();
      //  timeRemaining = 20f;
        RemoveAnswerButtons();
        QuestionData questionData = questionPool[questionIndex];
        string answerOfQuestion = null;
      
        questionDisplayText.text = questionData.questionText;
        for(int i = 0; i < questionData.answers.Length; i++)
        {
            if (questionData.answers[i].answerText != "")
            {
                answerOfQuestion = questionData.answers[i].answerText;
                break;

            }
        }
        
        for(int i = 0; i < questionData.answers.Length; i++)
        {
            
            GameObject answerButtonGameObject = answerButtonObjectPool.GetObject();
            answerButtonGameObject.transform.SetParent(answerButoonParent);
            answerButtonGameObject.transform.localScale = new Vector3(1f, 1f, 1f);
            answerButtonGameObjects.Add(answerButtonGameObject);
            AnswerButton answerButton = answerButtonGameObject.GetComponent<AnswerButton>();
            if (questionData.answers[i].answerText != "")
            {
                answerButton.Setup(questionData.answers[i]);

            }
            else
            {

                if (answerOfQuestion != null)
                {

                    int incorrectIndex = Random.Range(0, wrongAnswers.Length - 1);
                    while (wrongAnswers[incorrectIndex] == answerOfQuestion||wrongAnswerforSpecificQuestion.Contains(wrongAnswers[incorrectIndex]))
                    {
                        incorrectIndex = Random.Range(0, wrongAnswers.Length - 1);
                    }
                    wrongAnswerforSpecificQuestion.Add(wrongAnswers[incorrectIndex]);
                    
                    answerButton.Setup(questionData.answers[i],wrongAnswers[incorrectIndex]);

                }
            }
                
        }

    }
    private void RemoveAnswerButtons()
    {
        int removeButton = Random.Range(0, answerButtonGameObjects.Count - 1);
        while (answerButtonGameObjects.Count > 0)
        {
            answerButtonObjectPool.ReturnObject(answerButtonGameObjects[removeButton]);
            answerButtonGameObjects.RemoveAt(removeButton);
            removeButton = Random.Range(0, answerButtonGameObjects.Count - 1);
        }
    }
    public void AnswerButtonClicked(bool isCorrect)
    {
       
        if (isCorrect)
        {
            playerScore += currentRoundData.pointsAddedForCorrectAnswer;
            scoreDisplayText.text = playerScore.ToString();
            counter++;
            counterText.text = counter + "/" + currentRoundData.questions.Length;
            answeredQuestions.Add(questionIndex);
            forAnswerButtonClicked();
            ShowQuestion();
        }
        else
        {
            playerScore -= 15;
            yourLives--;
            scoreDisplayText.text = playerScore.ToString();
            UpdateYourLivesDisplay(yourLives);
            if (yourLives <= 0)
                EndRound();
            else
            {
                forAnswerButtonClicked();
                ShowQuestion();
            }
        }
      
      
        
        
    } 
    public void forAnswerButtonClicked()
    {
        questionIndex = Random.Range(0, forStories);
        if (answeredQuestions.Count >= forStories)
        {
            ShowWinPanel();
            
        }
        else
        {
            while (true)
            {
                if (!answeredQuestions.Contains(questionIndex))
                {
                    break;
                }
                else
                {
                    questionIndex = Random.Range(0, forStories);

                }
            }
          
        }
    }
    public void ShowWinPanel()
    {
        string highScore ="";
        int bonus = Random.Range(0, 1000);
        playerScore += bonus;
        dataController.SubmitNewPlayerScore(playerScore);
        if (dataController.GetHighestPlayerScore() > 999999999)
        {
           highScore = dataController.GetHighestPlayerScore() / 100000000 + "mlrd";
        }
        else
        {
            highScore = dataController.GetHighestPlayerScore().ToString();
        }
        highScoreForWinPanel.text = highScore;
        
       currentScoreForWinPanel.text = playerScore.ToString()+"(+"+bonus+")";
        rewardButton.SetActive(false);
        winPanel.SetActive(true);
        questionDisplay.SetActive(false);
        AdManager.Instance.ShowVideo();
    }
    public void EndRound()
    {
        AdManager.Instance.RequestRewardedVideoAd();
        isRoundActive = false;
        string highScore = "";
     //   questionDisplay.SetActive(false);
        roundOverDisplay.SetActive(true);
        dataController.SubmitNewPlayerScore(playerScore);
        if (dataController.GetHighestPlayerScore() > 999999999)
        {
            highScore = dataController.GetHighestPlayerScore() / 100000000 + "mlrd";
        }
        else
        {
            highScore = dataController.GetHighestPlayerScore().ToString();
        }
        highScoreDisplay.text = highScore;
       currentScoreTextDisplay.text = playerScore.ToString();
       
      
        AdManager.Instance.ShowVideo();
    	
    }
    public void Restart()
    {
        Start();
        scoreDisplayText.text = playerScore.ToString();
        roundOverDisplay.SetActive(false);
        questionDisplay.SetActive(true);

		AdManager.Instance.RequestInterstitial();
	
    }
    public void Pause()
    {
        UI.anchoredPosition = new Vector3(0, -309f, 0);
        Time.timeScale = 0;
       // questionDisplay.SetActive(false);
        pauseDisplay.SetActive(true);
        
      
      

    }
    public void Continue()
    {
        UI.anchoredPosition = Vector3.zero;
        Time.timeScale = 1;
    //    questionDisplay.SetActive(true);
        pauseDisplay.SetActive(false);

		AdManager.Instance.RequestInterstitial();
    }
    public void ReturnToMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MenuScene");

		AdManager.Instance.RequestInterstitial();
    }
  /*  private void UpdateTimeRemainingDisplay()
    {
        timeRemainingDisplayText.text = //Mathf.Floor(timeRemaining).ToString();
    }*/
    private void UpdateYourLivesDisplay(int number)
    {
        yourLivesDisplayText.text = number.ToString();
    }
    public void TurnOffOnMusic()
    {
        if (PlayerPrefs.HasKey("Music"))
        {
            if (PlayerPrefs.GetInt("Music") == 0)
            {
            //    audio.GetComponent<AudioSource>().UnPause();
                
                PlayerPrefs.SetInt("Music", 1);
           //     musicButton.image.sprite = forMusicButtonOn;
            }
            else
            {
           //     audio.GetComponent<AudioSource>().Pause();
                PlayerPrefs.SetInt("Music", 0);
            //    musicButton.image.sprite = forMusicButtonOff;
            }
        }
        else
        {


          //  musicButton.image.sprite = forMusicButtonOff;
            PlayerPrefs.SetInt("Music", 0);
         //   audio.GetComponent<AudioSource>().Pause();


        }
    }
    public void RewardTheUser()
    {
        AdManager.Instance.ShowRewardedVideo();
    }
    public void forRewardedUser()
    {
        questionIndex = Random.Range(0, forStories);
        forAnswerButtonClicked();
        ShowQuestion();
        questionDisplay.SetActive(true);
        roundOverDisplay.SetActive(false);
        yourLives = 1;
        UpdateYourLivesDisplay(yourLives);
        isRoundActive = true;
    }
    public void Share(){
		#if UNITY_ANDROID
// Create Refernece of AndroidJavaClass class for intent
AndroidJavaClass intentClass = new AndroidJavaClass("android.content.Intent");
// Create Refernece of AndroidJavaObject class intent
AndroidJavaObject intentObject = new AndroidJavaObject("android.content.Intent");
 
// Set action for intent
intentObject.Call<AndroidJavaObject>("setAction", intentClass.GetStatic<string>("ACTION_SEND"));
 
intentObject.Call<AndroidJavaObject>("setType", "text/plain");
 
//Set Subject of action
//intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_SUBJECT"), "Text Sharing ");
//Set title of action or intent
//intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_TITLE"), "Paylaş");
// Set actual data which you want to share
intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_TEXT"), "https://play.google.com/store/apps/details?id=com.elkhan.paytaxt");
 
AndroidJavaClass unity = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
AndroidJavaObject currentActivity = unity.GetStatic<AndroidJavaObject>("currentActivity");
// Invoke android activity for passing intent to share data
AndroidJavaObject jChooser = intentClass.CallStatic<AndroidJavaObject>("createChooser", intentObject, "Paylaş");
currentActivity.Call("startActivity", jChooser);
 
#endif
    }

    // Update is called once per frame
    void Update () {

      /*  if (isRoundActive)
        {
            if (timeRemaining <= 0f)
            {
                EndRound();
            }
            timeRemaining -= Time.deltaTime;
            UpdateTimeRemainingDisplay();
           
        }
        */
        if(Input.GetKeyDown(KeyCode.Escape)){
            if (!fadeImage.activeInHierarchy)
            {
                if (Time.timeScale == 0)
                {
                    Time.timeScale = 1;
                }
                if (pauseDisplay.activeInHierarchy)
                {
                    Continue();
                }
                else
                {
                    SceneManager.LoadScene("QuestionSelection");
                }
            }
        }
	}
}
