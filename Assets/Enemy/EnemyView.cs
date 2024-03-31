using Player;
using UnityEngine;
using Zenject;

namespace Enemy
{
    public class EnemyView : MonoBehaviour
    {
        public bool IsDead;
        public float Speed;

        [SerializeField] private CircleCollider2D _circleCollider2D; 
        [SerializeField] private SpriteRenderer headSprite;
        [SerializeField] private SpriteRenderer[] shoulderSprite;
        [SerializeField] private SpriteRenderer[] foreArmSprite;
        [SerializeField] private SpriteRenderer bodySprite;
        [SerializeField] private SpriteRenderer[] footSprite;

        private void ReInit(EnemyModel enemyModel)
        {
            _circleCollider2D.radius = enemyModel.RadiusAttack;
            
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

        public class Pool : MonoMemoryPool<EnemyModel, EnemyView>
        {
            protected override void Reinitialize(EnemyModel enemyModel, EnemyView item)
            {
                item.ReInit(enemyModel);
            }
        }
    }
}