using UnityEngine;
using Zenject;

namespace Gun
{
    public class GunView: MonoBehaviour
    {
        [SerializeField] private SpriteRenderer gunSprite;
        [SerializeField] private BoxCollider2D boxCollider2D;
        private GunConfig.GunType type;
        private int damage;
        private int magazineCount;
        private int ammunitionCount;
        private float reloadTime;
        
        private void Reinit(GunConfig.GunModel gunModel)
        {
            type = gunModel.Type;
            gunSprite.sprite = gunModel.GunSprite;
            damage = gunModel.Damage;
            magazineCount = gunModel.MagazineCount;
            ammunitionCount = gunModel.AmmunitionCount;
            reloadTime = gunModel.ReloadTime;
            boxCollider2D.size = gunModel.BoxColliderSize;
            boxCollider2D.offset = gunModel.BoxColliderOffset;
        }

        public GunConfig.GunType GetGunType()
        {
            return type;
        }

        public Sprite GetGunImage()
        {
            return gunSprite.sprite;
        }
        public class  Pool : MonoMemoryPool<GunConfig.GunModel, GunView>
        {
            protected override void Reinitialize(GunConfig.GunModel gunModel, GunView item)
            {
                item.Reinit(gunModel);
            }
        }
    }
}