using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// usage: attached this script to armament object
/// goal: global function to use for all characters in order to get data from attack data scriptable objects
/// </summary>

namespace game_ideas
{
    public class ArmamentAttackData : MonoBehaviour
    {
        [SerializeField] private AttackData attackData;
        
        public AttackType GetAttackType()
        {
            return attackData.attackType;
        }
        
        public int GetDamage()
        {
            return attackData.damage;
        }

        public float GetSpeed()
        {
            return attackData.speed;
        }

        public float GetAoE()
        {
            return attackData.aoe;
        }

        public string GetExplosionPoolName()
        {
            return attackData.explosionPoolName;
        }

        public bool HasMuzzleFlash()
        {
            if (attackData.muzzleFlashPoolName == null)
            {
                return false;
            }

            return true;
        }

        public string GetMuzzleFlashPoolName()
        {
            return attackData.muzzleFlashPoolName;
        }

        public Vector3 GetMuzzleFlashScale()
        {
            return attackData.muzzleFlashScale;
        }
    }
}
