using UnityEngine;

[CreateAssetMenu(fileName = "Weapon", menuName = "Scriptable Objects/Weapon")]
public class Weapon : ScriptableObject
{
    [SerializeField] float Damage = 5;
    [SerializeField] Sprite weaponSpr;
    public void Attack()
    {

    }
}
