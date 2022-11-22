using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy", menuName = "New Enemy")]
public class SO_Enemy : ScriptableObject
{
    public string base_EnemyName;
    public int enemyHP;
    public int enemyType;
    public Sprite image;
}
