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
	}

}
