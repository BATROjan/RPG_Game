using System;
using Gun;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Player
{
    public class PlayerView : MonoBehaviour
    {
        public Action<GunView> OnTakeGun;

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