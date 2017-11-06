using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MyCharacterController : MonoBehaviour {

    public float Speed = 6f;
	public float SpeedRotate = 3f;
	public float ForceBack = 2f;
	
	[HideInInspector]
	public float MainSpeed;

	private Vector3 _moveDirection;
	private Vector3 _turnPlayer;
	
	private CharacterController _controller;
	private Animator _animator;

	public Animator Animator
	{
		get { return _animator; }
	}

	public float MainSpeed1
	{
		get { return MainSpeed; }
	}

	private Player _player;
//	private ArrayList states;
	
	// Use this for initialization
	void Start ()
	{
		_controller = GetComponent<CharacterController>();
		_animator = GetComponent<Animator>();
		_player = GetComponent<Player>();

		StartCoroutine(SetAnimation());
		MainSpeed = Speed;
		Cursor.visible = false;
	}
	
	// Update is called once per frame
	void Update () {
		_moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
		_moveDirection = transform.TransformDirection(_moveDirection);
		_moveDirection *= Speed;
		_controller.Move(_moveDirection * Time.deltaTime);

		_turnPlayer = new Vector3(0, Input.GetAxis("Mouse X"), 0);
		transform.TransformVector(_turnPlayer);
		_turnPlayer *= SpeedRotate;
		transform.Rotate(_turnPlayer * Time.deltaTime);
	}

	IEnumerator SetAnimation()
	{
		while (true)
		{
			if (_moveDirection != Vector3.zero)
			{
				_animator.SetBool("playerWalk", true);
			}
			else
			{
				_animator.SetBool("playerWalk", false);
			}

//			Debug.Log(speed);
			if (Input.GetMouseButton(0))
			{
				_animator.SetBool("isAttak", true);
				_animator.SetBool("playerWalk", false);
				Speed = 0f;
			}

//			Debug.Log(speed);
			if (_animator.GetBool("isAttak") && !Input.GetMouseButton(0))
			{
				
				if (_animator.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
				{
					Speed = MainSpeed;
					_animator.SetBool("isAttak", false);
				}
			}
			
			if (_animator.GetCurrentAnimatorStateInfo(0).IsName("Damage"))
			{
				_animator.SetBool("isDamage", false);			
			}
			yield return null;
		}
	}
	
	public void ForceBackPlayer()
	{
		Vector3 dir = transform.forward;
		dir *= -ForceBack;
		_controller.Move(dir);
	}

	private void OnTriggerEnter(Collider other)
	{
		if (!other.gameObject.CompareTag("slime") || !_animator.GetBool("isAttak")) return;
		
		GameObject slime = other.gameObject;
		SlimeController slimeConroller = slime.GetComponent<SlimeController>();
		slimeConroller.TakeDamage(_player.Damage);
	}
}
