using System;
using System.Collections.Generic;
using UnityEngine;

namespace Bullet
{
    [CreateAssetMenu(fileName = "BulletConfig", menuName = "Configs/BulletConfig")]

    public class BulletConfig : ScriptableObject
    {
        [NonSerialized] private bool _inited;
        [SerializeField]private BulletModel[] bulletModels;
        private Dictionary<int, BulletModel> _dictionaryOfBulletModels = new Dictionary<int, BulletModel>();

        public BulletModel GetBulletModelByID(int id)
        {
            if (!_inited)
            {
                Init();
            }

            if (_dictionaryOfBulletModels.ContainsKey(id))
            {
                return _dictionaryOfBulletModels[id];
            }

            Debug.LogError($"There no such obstacle list with type: {id}");

            return new BulletModel();
        }
        
        private void Init()
        {
            foreach (var model in bulletModels)
            {
                _dictionaryOfBulletModels.Add(model.BulletCount, model);
            }

            _inited = true;
        }
    }

    [Serializable]
    public struct BulletModel
    {
        public int BulletCount;
        public Sprite BulletSprite;
    }
    
}