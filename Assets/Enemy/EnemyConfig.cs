using System;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    [CreateAssetMenu(fileName = "EnemyConfig", menuName = "Configs/EnemyConfig")]

    public class EnemyConfig : ScriptableObject
    {
        [SerializeField] private EnemyModel[] enemyModels;
        [NonSerialized] private bool _inited;
        
        private Dictionary<EnemyType, EnemyModel> _pLayerModels = new Dictionary<EnemyType, EnemyModel>();
        
        public EnemyModel GetEnemyModelByType(EnemyType type)
        {
            if (!_inited)
            {
                Init();
            }
            
            if (_pLayerModels.ContainsKey(type))
            {
                return _pLayerModels[type];
            }

            Debug.LogError($"There no such world with type: {type}");
            
            return new EnemyModel();
        }
        
        private void Init()
        {
            foreach (var model in enemyModels)
            {
                _pLayerModels.Add(model.Type, model);
            }

            _inited = true;
        }
    }
    
    [Serializable]
    public struct EnemyModel
    {
        public EnemyType Type;
        public int Health;
        public int Damage;
        public EnemySprites Sprites;
        public float RadiusAttack;
    }
    
    [Serializable]
    public struct EnemySprites
    {
        public Sprite HeadSprite;
        public Sprite ShoulderSprite;
        public Sprite ForeArmSprite;
        public Sprite BodySprite;
        public Sprite FootSprite;
        public Sprite RadiusAttackSprite;
    }

    public enum EnemyType
    {
        Zombie,
        Fresh
    }
}