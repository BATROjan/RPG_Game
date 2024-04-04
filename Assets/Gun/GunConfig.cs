using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Gun
{
    [CreateAssetMenu(fileName = "GunConfig", menuName = "Configs/GunConfig")]
    public class GunConfig : ScriptableObject
    {
        [NonSerialized] private bool _inited;

        [SerializeField] private GunModel[] gunModels;

        private Dictionary<GunType, GunModel> _dictionaryOfGun = new Dictionary<GunType, GunModel>();

        public GunModel GetGunModelByType(GunType type)
        {
            if (!_inited)
            {
                Init();
            }

            if (_dictionaryOfGun.ContainsKey(type))
            {
               return _dictionaryOfGun[type];
            }

            Debug.LogError($"There no such obstacle list with type: {type}");

            return new GunModel();
        }

        private void Init()
        {
            foreach (var model in gunModels)
            {
                _dictionaryOfGun.Add(model.Type, model);
            }

            _inited = true;
        }

        [Serializable]
        public struct GunModel
        {
            public GunType Type;
            public Sprite GunSprite;
            public int Damage;
            public int MagazineCount;
            public int AmmunitionCount;
            public float ReloadTime;
        }

        public enum GunType
        {
            AK74,
            Makarov
        }
    }
}