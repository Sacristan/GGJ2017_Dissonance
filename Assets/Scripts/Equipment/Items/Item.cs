using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
	public enum Type
	{
		Generic,
		Health,
		Mana,
		ShotgunShells,
		PistolClip,
		UziClip,
		Rockets,
		PlasmaClip,
	}

	public Type type;
	public int amount;
}
