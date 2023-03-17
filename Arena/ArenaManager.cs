using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArenaManager : MonoBehaviour {

	//game mechanics variables
	public GameObject Player;
	public Vector3 PlayerPosition;
	public float waitTime = 0.1f;
	private float soundDelay = 0f;
	private bool complete;
	private bool gameOver;
	public bool playerTurn;
	public bool enemyTurn;
	public bool allowTurn;
	public bool completeBattleLog;
	private string currentPlayerAction;
	private string previousEnemyAction;
	private string damageDealt;
	public KeyCode interactKey = KeyCode.F;

	//ui element variables
	public GameObject attackButton;
	public GameObject defendButton;
	public GameObject EasyEnemyPrefabs;
	public GameObject MediumEnemyPrefabs;
	public GameObject HardEnemyPrefabs;
	public Text battleLog;
	public Text enemyName;
	public Text playerHp;
	public Text enemyHp;
	private string log;

	//animators
	private Animator playerAnimator;

	//sliders
	public Slider playerHpBar;
	public Slider enemyHpBar;

	//battle report
	public GameObject battleReportPanel;
	public GameObject returnButton;
	private string moneyGain;
	private string expGain;
	private string strGain;
	private string armorGain;
	private string maxHpGain;
	private string lvlGain;
	public Text compiled;
	public Text outcome;

	//audio sources
	public AudioSource BGM1;
	public AudioSource BGM2;
	private AudioSource currentBGM;
	public AudioSource PlayerAttackSFX;
	public AudioSource PlayerHealSFX;
	private AudioSource currentEnemyAttackSFX;
	public AudioSource DefendSFX;

	public AudioSource BoneAttack;
	public AudioSource WraithAttack;

	//enemy controller
	private EnemyController currentEnemy;

	public bool randomGenerated = false;

	void Awake(){
		PlayerPosition = new Vector3 (-4.07f, -3.73f, 0f);
		gameOver = false;
		playerTurn = true;
		enemyTurn = false;
		allowTurn = true;
		completeBattleLog = false;
		randomGenerated = false;

		//ui element variables
		attackButton.SetActive(true);
		defendButton.SetActive(true);
		returnButton.SetActive (false);
		battleReportPanel.SetActive (false);

		//stop character movement
		PlayerMovement.canMove = false;
		playerAnimator = Player.GetComponentInChildren<Animator> ();

		//Spawn
		if (Prompt.difficulty == "Easy") {
			StartCoroutine ("SpawnEasyEnemy");
			currentBGM = BGM1;
			currentEnemyAttackSFX = BoneAttack;
			soundDelay = 0.4f;

		} else if (Prompt.difficulty == "Medium") {
			StartCoroutine ("SpawnMediumEnemy");
			currentBGM = BGM1;
			currentEnemyAttackSFX = BoneAttack;
			soundDelay = 0.4f;

		} else if (Prompt.difficulty == "Hard") {
			StartCoroutine ("SpawnHardEnemy");
			currentBGM = BGM2;
			currentEnemyAttackSFX = WraithAttack;
			soundDelay = 1f;
		}

		currentBGM.Play ();
	}

	void Start(){
		//player sliders & name
		playerHpBar.value = GameState.currentPlayer.Health/GameState.currentPlayer.MaxHealth;
		playerHp.text =  GameState.currentPlayer.Health.ToString()+"/"+GameState.currentPlayer.MaxHealth.ToString();

		//texts
		battleLog.text = "A new challenger approaches!";
		currentPlayerAction = "";
		previousEnemyAction = "";
		moneyGain = "";
		expGain = "";
		strGain = "";
		maxHpGain = "";
		lvlGain = "";
		compiled.text = "";
		outcome.text = "";
	}

	void LateStart(){
		Player.transform.position = new Vector3 (-4.07f, -3.73f, 0f);
	}

	void Update () {
		if (Input.GetKeyDown (interactKey) && !completeBattleLog) {
			DisplayFullSentence ();
		}

		if(!gameOver){
			
			if(!playerTurn && allowTurn && enemyTurn ){
				//perform enemy action
				if (randomGenerated == false) {//allow only one coroutine
					float randomiser = Random.value;
					Debug.Log (randomiser);
					if (randomiser <= 0.8) {
						DoAction ("Attack");

					} else if (randomiser > 0.8) {
						DoAction ("Defend");
					}
				}
			}
		}

		if (Player.transform.position.x != -4.07f && Player.transform.position.y != -3.77f) {
			Player.transform.position = PlayerPosition;;
		}
			
	}

	void ResetTurn(){
		playerTurn = true;
		allowTurn = true;
		enemyTurn = false;

		currentPlayerAction = "";
		StartCoroutine("ResetCommandButtons");
	}

	public void DoAction(string action){
		StartCoroutine (PerformAction (action));
		if(!playerTurn)
		randomGenerated = true;
	}

	IEnumerator PerformAction(string action){
		//allowTurn = false;


		if (playerTurn) {

			currentEnemy.animator.SetBool ("attacking", false);
			playerAnimator.SetBool ("defend", false);

			if (action == "Attack") {
				playerAnimator.SetBool ("attacking", true);
				PlayerAttackSFX.PlayDelayed (0.7f);
				yield return new WaitForSeconds (1.0f);


				Debug.Log ("Player is performing an Attack!");

				if(previousEnemyAction != "Defend"){
					damageDealt = currentEnemy.EnemyProfile.TakeDamage (GameState.currentPlayer.Strength).ToString();

				}else{
					damageDealt = currentEnemy.EnemyProfile.TakeResistedDamage (GameState.currentPlayer.Strength).ToString();
				}
				UpdateEnemyHp ();

				log = "Player strikes the enemy! Dealing "+damageDealt+" damage!";

				StopAllCoroutines ();
				StartCoroutine (TypeDialogue(log));

			} else {
				Debug.Log ("Player is performing a Defend!");
				playerAnimator.SetBool ("defend", true);
				yield return new WaitForSeconds (0.5f);

				currentPlayerAction = "Defend";

				DefendSFX.Play ();
				//PlayerHealSFX.PlayDelayed();

				int restoreHp = Mathf.FloorToInt (GameState.currentPlayer.GetMaxHealth () / 10);
				GameState.currentPlayer.HealHp (restoreHp);
				UpdatePlayerHp ();
				log = "Player assumes a Defensive stance! Reduce incoming damage by 50% and restore "+restoreHp+" health!";

				StopAllCoroutines ();
				StartCoroutine (TypeDialogue(log));
			}
		} else if (enemyTurn) {
			yield return new WaitForSeconds (3f);
			if (action.CompareTo("Defend")==0) {
				Debug.Log ("Enemy is performing a Defend!");

				yield return new WaitForSeconds (1f);
				DefendSFX.Play ();
				previousEnemyAction = "Defend";



				log = "Foe "+enemyName.text+" is assuming a Defensive stance! Reduce incoming damage and boosting own damage by 50% in the next turn!"; 
				StopAllCoroutines ();
				StartCoroutine (TypeDialogue(log));

			} else if (action == "Attack"){
				Debug.Log ("Enemy is performing an Attack!");

				currentEnemy.animator.SetBool ("attacking", true);
				currentEnemyAttackSFX.PlayDelayed (soundDelay);

				if(currentPlayerAction != "Defend"){
					yield return new WaitForSeconds (1f);

					if (previousEnemyAction != "Defend") {
						damageDealt = GameState.currentPlayer.TakeDamage (currentEnemy.EnemyProfile.Strength).ToString ();
					} else {
						float boost = currentEnemy.EnemyProfile.Strength * 1.5f;
						damageDealt = GameState.currentPlayer.TakeDamage (Mathf.FloorToInt(boost)).ToString ();
					}
				}else{
					yield return new WaitForSeconds (1f);

					if (previousEnemyAction != "Defend") {
						damageDealt = GameState.currentPlayer.TakeResistedDamage (currentEnemy.EnemyProfile.Strength).ToString ();
					} else {
						float boost = currentEnemy.EnemyProfile.Strength * 1.5f;
						damageDealt = GameState.currentPlayer.TakeResistedDamage (Mathf.FloorToInt(boost)).ToString ();
					}
				}
				UpdatePlayerHp ();

				previousEnemyAction = "Attack";

				playerAnimator.SetBool ("hit", true);

				log = "Foe "+enemyName.text+" strikes the Player! Dealing "+damageDealt+" damage!";
				StopAllCoroutines ();
				StartCoroutine (TypeDialogue(log));
			}
		}
		playerAnimator.SetBool("attacking",false);
		playerAnimator.SetBool("hit",false);


		DetermineNextAction ();
	}


	void UpdatePlayerHp(){
		playerHpBar.value = (float)GameState.currentPlayer.GetHealth()/(float)GameState.currentPlayer.GetMaxHealth();
		playerHp.text =  GameState.currentPlayer.Health.ToString()+"/"+GameState.currentPlayer.MaxHealth.ToString();
	}

	void UpdateEnemyHp(){
		enemyHpBar.value = (float)currentEnemy.EnemyProfile.GetHealth() / (float)currentEnemy.EnemyProfile.GetMaxHealth();
		enemyHp.text = currentEnemy.EnemyProfile.Health.ToString()+"/"+ currentEnemy.EnemyProfile.MaxHealth.ToString();
	}

	void DetermineNextAction(){
		//if Enemy health <= 0
		if(currentEnemy.EnemyProfile.Health <= 0){
			GameOver("Player");
		}else if(GameState.currentPlayer.Health <= 0){
			GameOver("Enemy");
		}else if(playerTurn&&!enemyTurn){
			playerTurn = false;
			enemyTurn = true;
			allowTurn = true;
			//EnemyTurn
		}else{
			randomGenerated = false;
			ResetTurn();
		}
	}

	void GameOver(string winner){
		gameOver = true;
		playerTurn = false;
		enemyTurn = false;
		allowTurn = false;
		battleLog.text = "";
		CloseCommandButtons ();

		if(winner == "Player"){
			Debug.Log ("Player won the match!");
			outcome.text = "VICTORY!";
			currentEnemy.animator.SetBool ("attacking", false);
			currentEnemy.animator.SetBool ("hit", false);
			currentEnemy.animator.SetBool ("death", true);
			PrepareBattleReport (winner);
		}else{
			Debug.Log("Enemy won the match!");
			outcome.text = "DEFEAT";
			playerAnimator.SetBool ("attacking", false);
			playerAnimator.SetBool ("hit", true);
			playerAnimator.SetBool ("death", true);
			PrepareBattleReport (winner);
		}
	}

	void PrepareBattleReport(string winner){

		if (winner == "Player") {
			GameState.currentPlayer.GainEXP (currentEnemy.EnemyProfile.Experience);
			GameState.currentPlayer.GainMoney (currentEnemy.EnemyProfile.Money);
			moneyGain = "+"+currentEnemy.EnemyProfile.GetMoney ().ToString ();
			expGain = "+"+currentEnemy.EnemyProfile.GetExperience().ToString();
			lvlGain = "+0";
			strGain = "+0";
			armorGain = "+0";
			maxHpGain = "+0";

			if (GameState.currentPlayer.Experience >= GameState.currentPlayer.MaxExperience) {
				GameState.currentPlayer.LevelUp ();

				lvlGain = "+1";
				strGain = "+4";
				armorGain = "+1";
				maxHpGain = "+50";
			}
		} else {
			float money = currentEnemy.EnemyProfile.GetMoney() * 0.05f;
			GameState.currentPlayer.GainMoney (Mathf.FloorToInt (money));

			moneyGain = "+"+Mathf.FloorToInt (money).ToString ();
			expGain = "+0";
			lvlGain = "+0";
			strGain = "+0";
			armorGain = "+0";
			maxHpGain = "+0";
		}

		compiled.text = lvlGain + "\n" + expGain + "\n" + strGain + "\n" + armorGain + "\n" + maxHpGain + "\n" + moneyGain;

		StopAllCoroutines ();
		StartCoroutine ("ShowBattleReport");
	}

	IEnumerator ShowBattleReport (){
		battleReportPanel.SetActive (true);
		yield return new WaitForSeconds (4.0f);
		returnButton.SetActive (true);
	}

	public void ReturnToTown(){
		PlayerMovement.canMove = true;
		GameState.currentPlayer.HealHp (99999);

		StartCoroutine ("ChangeScene");
	}

	IEnumerator ChangeScene(){
		yield return new WaitForSeconds (1f);
		NavigationManager.GoBack ();
	}

	IEnumerator ResetCommandButtons(){
		yield return new WaitForSeconds (3f);
		attackButton.SetActive(true);
		defendButton.SetActive(true);
	}

	public void CloseCommandButtons(){
		attackButton.SetActive(false);
		defendButton.SetActive(false);
	}

	public void SetCurrentEnemy(EnemyController enemy)
	{
		currentEnemy = enemy;
	}

	IEnumerator SpawnEasyEnemy(){
		{
			
			yield return new WaitForSeconds (0.0f);
			var newEnemy = (GameObject)Instantiate(EasyEnemyPrefabs);
			Debug.Log ("Challenger arrives!");

			var controller = newEnemy.GetComponent<EnemyController>();
			controller.ArenaManager = this;

			var EnemyProfile = ScriptableObject.CreateInstance<Enemy>();
			EnemyProfile.Class = EnemyClass.SkeletonWarrior;
			EnemyProfile.Name = "Lobart";
			EnemyProfile.Occupation = "Skeleton Warrior";
			EnemyProfile.Level = 1;
			EnemyProfile.Health = 100;
			EnemyProfile.MaxHealth = EnemyProfile.Health;
			EnemyProfile.Strength = 30;
			EnemyProfile.Armor = -25;
			EnemyProfile.Experience = 10;
			EnemyProfile.Money = 200;
			controller.EnemyProfile = EnemyProfile;
			controller.SetCurrentEnemy ();
			enemyHpBar.value = currentEnemy.EnemyProfile.Health / currentEnemy.EnemyProfile.MaxHealth;
			enemyHp.text = currentEnemy.EnemyProfile.Health.ToString()+"/"+ currentEnemy.EnemyProfile.MaxHealth.ToString();
			enemyName.text = currentEnemy.EnemyProfile.GetOccupation()+" "+currentEnemy.EnemyProfile.GetName ();
		}
	}

	IEnumerator SpawnMediumEnemy(){
		{
			yield return new WaitForSeconds (0.0f);
			var newEnemy = (GameObject)Instantiate(MediumEnemyPrefabs);
			Debug.Log ("Challenger arrives!");

			var controller = newEnemy.GetComponent<EnemyController>();
			controller.ArenaManager = this;

			var EnemyProfile = ScriptableObject.CreateInstance<Enemy>();
			EnemyProfile.Class = EnemyClass.BoneKnight;
			EnemyProfile.Name = "Torkell";
			EnemyProfile.Occupation = "Bone Knight";
			EnemyProfile.Level = 2;
			EnemyProfile.Health = 180;
			EnemyProfile.MaxHealth = EnemyProfile.Health;
			EnemyProfile.Strength = 40;
			EnemyProfile.Armor = -15;
			EnemyProfile.Experience = 20;
			EnemyProfile.Money = 550;
			controller.EnemyProfile = EnemyProfile;
			controller.SetCurrentEnemy ();
			enemyHpBar.value = currentEnemy.EnemyProfile.Health / currentEnemy.EnemyProfile.MaxHealth;
			enemyHp.text = currentEnemy.EnemyProfile.Health.ToString()+"/"+ currentEnemy.EnemyProfile.MaxHealth.ToString();
			enemyName.text = currentEnemy.EnemyProfile.GetOccupation()+" "+currentEnemy.EnemyProfile.GetName ();
		}
	}

	IEnumerator SpawnHardEnemy(){
		{
			yield return new WaitForSeconds (0.0f);
			var newEnemy = (GameObject)Instantiate(HardEnemyPrefabs);
			Debug.Log ("Challenger arrives!");

			var controller = newEnemy.GetComponent<EnemyController>();
			controller.ArenaManager = this;

			var EnemyProfile = ScriptableObject.CreateInstance<Enemy>();
			EnemyProfile.Class = EnemyClass.PhantomWraith;
			EnemyProfile.Name = "Gawain";
			EnemyProfile.Occupation = "Phantom Wraith";
			EnemyProfile.Level = 3;
			EnemyProfile.Health = 300;
			EnemyProfile.MaxHealth = EnemyProfile.Health;
			EnemyProfile.Strength = 60;
			EnemyProfile.Armor = -10;
			EnemyProfile.Experience = 50;
			EnemyProfile.Money = 1200;
			controller.EnemyProfile = EnemyProfile;
			controller.SetCurrentEnemy ();
			enemyHpBar.value = currentEnemy.EnemyProfile.Health / currentEnemy.EnemyProfile.MaxHealth;
			enemyHp.text = currentEnemy.EnemyProfile.Health.ToString()+"/"+ currentEnemy.EnemyProfile.MaxHealth.ToString();
			enemyName.text = currentEnemy.EnemyProfile.GetOccupation()+" "+currentEnemy.EnemyProfile.GetName ();
		}
	}

	IEnumerator TypeDialogue (string log){

		battleLog.text = "";
		foreach (char letter in log.ToCharArray()) {
			battleLog.text += letter;
			yield return new WaitForSecondsRealtime (waitTime);
		}

		completeBattleLog = true;
	}

	void DisplayFullSentence(){
		completeBattleLog = true;
		StopAllCoroutines ();

		//battleLog.text = "";
		battleLog.text = log;
	}

}

