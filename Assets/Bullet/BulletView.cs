using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace Bullet
{
    public class BulletView :MonoBehaviour
    {
        private int _bulletCount;
        [SerializeField]private SpriteRenderer bulletSpriteRenderer;

        public int GetBulletCount()
        {
            return _bulletCount;
        }  
        public Sprite GetBulletSprite()
        {
            return bulletSpriteRenderer.sprite;
        }
        private void Reinit(BulletModel model)
        {
            _bulletCount = model.BulletCount;
            bulletSpriteRenderer.sprite = model.BulletSprite;
        }
        public class Pool : MonoMemoryPool<BulletModel, BulletView>
        {
            protected override void Reinitialize(BulletModel model, BulletView item)
            {
                item.Reinit(model);
            }
        }
    }
}