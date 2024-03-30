using UnityEngine;
using Zenject;

namespace Player
{
    public class PlayerView : MonoBehaviour
    {
        public bool IsDead;
        public float Speed;
        
        [SerializeField] private SpriteRenderer headSprite;
        [SerializeField] private SpriteRenderer[] handSprite;
        [SerializeField] private SpriteRenderer bodySprite;
        [SerializeField] private SpriteRenderer[] footSprite;
      
        private void ReInit(PlayerModel pLayerModel)
        {
            headSprite.sprite = pLayerModel.HeadSprite;
            bodySprite.sprite = pLayerModel.BodySprite;
                
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