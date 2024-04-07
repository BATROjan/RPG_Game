using System;
using Player;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Enemy
{
    public class EnemyView : MonoBehaviour
    {
        public Action<PlayerView> PlayerIsFound;
        public Action<EnemyView> OnDead;

        public EnemyType EnemyType;
        public bool IsFound;
        public bool IsReadyToAttack = true;
        public float Speed;
        public int MaxHealth;
        public int Health;
        public float Damage;
        public Image HealthImage;
        
        [SerializeField] private CircleCollider2D _circleCollider2D; 
        [SerializeField] private SpriteRenderer headSprite;
        [SerializeField] private SpriteRenderer[] shoulderSprite;
        [SerializeField] private SpriteRenderer[] foreArmSprite;
        [SerializeField] private SpriteRenderer bodySprite;
        [SerializeField] private SpriteRenderer[] footSprite;

        private PlayerView _playerView;
        private void ReInit(EnemyModel enemyModel)
        {
            _circleCollider2D.radius = enemyModel.RadiusAttack;
            Damage = enemyModel.Damage;
            Health = enemyModel.Health;
            MaxHealth = enemyModel.Health;
            EnemyType = enemyModel.Type;
            if (enemyModel.Sprites.HeadSprite) 
            {
                headSprite.sprite = enemyModel.Sprites.HeadSprite;
            }

            if (enemyModel.Sprites.BodySprite)
            {
                bodySprite.sprite = enemyModel.Sprites.BodySprite;
            }

            if (enemyModel.Sprites.ShoulderSprite)
            {
                foreach (var shoulder in shoulderSprite)
                {
                    shoulder.sprite = enemyModel.Sprites.ShoulderSprite;
                }
            }     
            
            if (enemyModel.Sprites.ForeArmSprite)
            {
                foreach (var foreArm in foreArmSprite)
                {
                    foreArm.sprite = enemyModel.Sprites.ForeArmSprite;
                }
            }
            
            if (enemyModel.Sprites.FootSprite)
            {
                foreach (var foot in footSprite)
                {
                    foot.sprite = enemyModel.Sprites.FootSprite;
                }
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                PlayerIsFound?.Invoke(other.GetComponent<PlayerView>());
                IsFound = true;
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            IsFound = false;
        }

        private void Update()
        {
            if (_playerView)
            {
                if (Vector3.Distance(transform.position, _playerView.transform.position) > 0.57f)
                {
                    transform.position = Vector3.MoveTowards(transform.position, _playerView.transform.position, Speed);
                }
            }
        }

        public class Pool : MonoMemoryPool<EnemyModel, EnemyView>
        {
            protected override void Reinitialize(EnemyModel enemyModel, EnemyView item)
            {
                item.ReInit(enemyModel);
            }
        }
    }
}