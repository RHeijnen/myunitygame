﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MovingObject {
	public int playerDamage;
	private Animator animator;
	public AudioClip enemyAttack1;
	public AudioClip enemyAttack2;
	private Transform target;
	private bool skipMove;

	public string stringy = "test";

	// Use this for initialization
	protected override void Start () {
			GameManager.instance.AddEnemyToList(this);
			animator = GetComponent<Animator>();
			// target = GameObject.FindGameObjectsWithTag("Player")[0].transform;
			target = GameObject.FindGameObjectWithTag("Player").transform;
			base.Start();
	}
	protected override void AttemptMove<T>(int xDir, int yDir){
		if(skipMove){
			skipMove = false;
			return;
		}
		base.AttemptMove <T> (xDir,yDir);
		skipMove  = true;
	}
	public void MoveEnemy(){
		int xDir = 0;
		int yDir = 0;
		if(Mathf.Abs (target.position.x - transform.position.x) < float.Epsilon){
			yDir = target.position.y > transform.position.y ? 1 : -1;
		}else{
			xDir = target.position.x > transform.position.x ? 1 : -1;
		}
		AttemptMove <Player> (xDir,yDir);
	}
	protected override void onCantMove<T>(T component){
		Player hitPlayer = component as Player;
		// hitPlayer.LoseFood(playerDamage);
		GameManager.instance.startBattle(this);
		animator.SetTrigger("enemyAttack");
		Debug.Log("enemy attacking");
		SoundManager.instance.RandomizeSfc(enemyAttack1,enemyAttack2);

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
