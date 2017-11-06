using System.Collections;
using UnityEngine;

public class SlimeController : MonoBehaviour
{

	public float Speed = 3f;
	
	[HideInInspector]
	public GameObject Target;

	public int Hp = 2;
	public int Damage = 1;
	public float ForceBack = 2f;
	
	private float _mainSpeed;
	private Vector3 _directionMove;
	private Animator _animator;
	
	// Use this for initialization
	private void Start () {
		_mainSpeed = Speed;
		_animator = GetComponent<Animator>();
		StartCoroutine(ChangeAnimation());
	}
	
	// Update is called once per frame
	private void Update ()
	{
		if (!Target) return;
		_directionMove = Target.transform.position - transform.position;
		_directionMove = _directionMove.normalized;
		_directionMove *= Speed * Time.deltaTime;
		transform.position += _directionMove;
		transform.LookAt(Target.transform);
	}

	private IEnumerator ChangeAnimation()
	{
		while (true)
		{
			if (!_animator) yield return null;
			
			_animator.SetBool("isWalk", _directionMove != Vector3.zero);

			if (_animator.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
			{
				Speed = _mainSpeed;
				_animator.SetBool("isAttak", false);
			}
			
			if (_animator.GetCurrentAnimatorStateInfo(0).IsName("Damage"))
			{
				_animator.SetBool("isDamage", false);
			}
			
			yield return null;
		}
		// ReSharper disable once IteratorNeverReturns
	}

	private void OnTriggerEnter(Collider other)
	{
		if (Hp <= 0) return;
		
		GameObject player = other.gameObject;
		Animator animPlayer = player.GetComponent<Animator>();

		if (!player.CompareTag("Player") || animPlayer.GetBool("isAttak") || _animator.GetBool("isAttak")) return;
		
		_animator.SetBool("isAttak", true);
		Speed = 0f;
		Player playerClass = player.GetComponent<Player>();
		playerClass.TakeDamage(Damage);
	}

	public void TakeDamage(int takenDamage)
	{
		_animator.SetBool("isDamage", true);
		Hp -= takenDamage;
		ForceBackSlime();

		if (Hp > 0) return;
		
		_animator.SetBool("isDamage", false);
		_animator.SetBool("isDead", true);
		Target = null;
		Speed = 0f;
		_mainSpeed = 0f;
		ForceBack = 0f;
		GameController.CountEnemy--;
	}

	private void ForceBackSlime()
	{
		Vector3 dir = transform.forward;
		dir *= -ForceBack;
		transform.position += dir;
	}
}
