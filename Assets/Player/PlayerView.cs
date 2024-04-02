using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Player
{
    public class PlayerView : MonoBehaviour
    {
        public float Heath; 
        public bool IsDead;
        public float Speed;
        
       public Image healthImage;
        [SerializeField] private SpriteRenderer headSprite;
        [SerializeField] private SpriteRenderer[] handSprite;
        [SerializeField] private SpriteRenderer bodySprite;
        [SerializeField] private SpriteRenderer[] footSprite;
      
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