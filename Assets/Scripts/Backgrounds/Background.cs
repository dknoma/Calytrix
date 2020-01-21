using System;
using UnityEngine;
using static Utility.ProjectConstants;

namespace Backgrounds {
    [Serializable]
    public class Background {
        [SerializeField] private Texture2D spriteTexture;
        [SerializeField] private BackgroundSettings settings;
        [SerializeField] private bool overwriteSprite;

        /// <summary>
        /// The 2D texture of the background's sprite. Can be used to obtain the Sprite object representation of the background.
        /// </summary>
        public Texture2D SpriteTexture {
            get => spriteTexture;
            set => this.spriteTexture = value;
        }

        /// <summary>
        /// The background settings to determine it's various metadata.
        /// </summary>
        public BackgroundSettings Settings {
            get => settings;
            set => this.settings = value;
        }

        public Background(Texture2D spriteTexture) {
            this.SpriteTexture = spriteTexture;
        }

        public Sprite GetSprite() {
            Rect rec = new Rect(0, 0, spriteTexture.width, spriteTexture.height);
            Sprite sprite = Sprite.Create(spriteTexture, rec, new Vector2(0.5f,0.5f), PIXELS_PER_UNIT);
            sprite.name = spriteTexture.name;
//            Debug.Log($"sprite={sprite}");
            
            return sprite;
        }
    }
}