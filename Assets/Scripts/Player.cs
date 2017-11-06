using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{

	private int hp = 20;

	public int Hp
	{
		get { return hp; }
	}

	public int Damage = 1;
	
	private MyCharacterController _controller;
	private Text _textHp;
	public GameObject PanelLose;
	
	// Use this for initialization
	private void Start ()
	{
		_controller = GetComponent<MyCharacterController>();
		GameObject infoHp = GameObject.FindWithTag("hpText");
		_textHp = infoHp.GetComponent<Text>();
		_textHp.text = hp.ToString();
	}
	
	// Update is called once per frame

	public void TakeDamage(int takenDamage)
	{
		_controller.Animator.SetBool("isDamage", true);
		hp -= takenDamage;
		_controller.ForceBackPlayer();
		
		if (hp <= 0)
		{
			_controller.Animator.SetBool("isDead", true);
			_controller.Speed = 0f;
			_controller.SpeedRotate = 0f;
			_controller.MainSpeed = 0f;
			_controller.ForceBack = 0f;
			hp = 0;

			if (PanelLose)
			{
				PanelLose.SetActive(true);
			}
		}

		if (_textHp)
		{
			_textHp.text = hp.ToString();
		} 
	}
}
