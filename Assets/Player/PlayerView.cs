using System;
using Bullet;
using Enemy;
using Gun;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Player
{
    public class PlayerView : MonoBehaviour
    {
        public Action<GunView> OnTakeGun;
        public Action<BulletView> OnTakeBullet;
        
        public Action<EnemyView> OnFindEnemy;
        public Action<EnemyView> OnLoseEnemy;

        public float Heath; 
        public bool IsDead;
        public float Speed;
        
        public Image healthImage;
        public SpriteRenderer GunSprite;
        
        [SerializeField] private SpriteRenderer headSprite;
        [SerializeField] private SpriteRenderer[] handSprite;
        [SerializeField] private SpriteRenderer bodySprite;
        [SerializeField] private SpriteRenderer[] footSprite;

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.CompareTag("Gun"))
            {
                OnTakeGun?.Invoke(other.collider.GetComponent<GunView>());
            }
            if (other.gameObject.CompareTag("Bullet"))
            {
                OnTakeBullet?.Invoke(other.collider.GetComponent<BulletView>());
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            var enemy = other.GetComponent<EnemyView>();
            if (enemy)
            {
                OnFindEnemy?.Invoke(enemy);
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            var enemy = other.GetComponent<EnemyView>();
            if (enemy)
            {
                OnLoseEnemy?.Invoke(enemy);
            }
        }

        private void ReInit(PlayerModel pLayerModel)
        {
            headSprite.sprite = pLayerModel.HeadSprite;
            bodySprite.sprite = pLayerModel.BodySprite;
            Heath = pLayerModel.HealthCount;
            
            foreach (var hand in handSprite)
            {
                hand.sprite = pLayerModel.HandSprite;
            }
            
            foreach (var foot in footSprite)
            {
                foot.sprite = pLayerModel.FootSprite;
            }
        }
        
        public class Pool : MonoMemoryPool<PlayerModel, PlayerView>
        {
            protected override void Reinitialize(PlayerModel playerModel, PlayerView item)
            {
                item.ReInit(playerModel);
            }
        }
    }
}