using System;
using UnityEngine;

namespace Player
{
    [CreateAssetMenu(fileName = "PlayerConfig", menuName = "Configs/PlayerConfig")]
    
    public class PlayerConfig : ScriptableObject
    {
        [SerializeField] private PlayerModel PlayerModel;

        public PlayerModel GetPlayerModel()
        {
            return PlayerModel;
        }
    }
}

[Serializable]
public struct PlayerModel
{
    public int HealthCount;
    public Sprite HeadSprite;
    public Sprite HandSprite;
    public Sprite BodySprite;
    public Sprite FootSprite;
}