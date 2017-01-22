using UnityEngine;

public class Projectile : MonoBehaviour
{
	public enum Targets { Player, Mob, Both }

	[SerializeField]
	private float damage = 10f;

	[SerializeField]
	private Targets targets;


	[Header("Physics")]
	public float speed = 10;
	public Vector3 direction;

	internal Collider coll;

	[Header("Effects")]
	public AudioClip impactSound;
	public Transform impactEffect;

	#region Properties
	public Targets PossibleTargets { get { return targets; } }
	public float Damage { get { return damage; } }
	#endregion

	public virtual void Awake()
	{
		coll = GetComponent<Collider>();
		Destroy(gameObject, 10f);
	}

	public virtual void Update()
	{
		transform.position += direction * speed * Time.deltaTime;
		transform.rotation = Quaternion.LookRotation(direction);
	}

	public virtual void Impact()
	{
		if (impactSound)
		{
			Sound.PlayClipAt(impactSound, transform.position);
		}
		if (impactEffect)
		{
			Instantiate(impactEffect, transform.position, impactEffect.rotation);
		}

		Destroy(gameObject);
	}

	void OnCollisionEnter(Collision collision)
	{
		Impact();
	}

}
