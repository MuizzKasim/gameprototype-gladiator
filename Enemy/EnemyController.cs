using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {
	
	public Enemy EnemyProfile;
	Animator enemyAI;
	private ArenaManager arenaManager;
	public Animator animator;


	public ArenaManager ArenaManager
	{
		get
		{
			return arenaManager;
		}
		set
		{
			arenaManager = value;
		}
	}

	void Start(){
		animator = this.GetComponent<Animator> ();
	}

	public void SetCurrentEnemy(){ 
		arenaManager.SetCurrentEnemy (this);
	}
}
