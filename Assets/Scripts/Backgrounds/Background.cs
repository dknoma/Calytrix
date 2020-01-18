using System;
using UnityEngine;
using static Tilemaps.TilemapConstants;

namespace Backgrounds {
    [Serializable]
    public class Background {
        [SerializeField] private Texture2D spriteTexture;
        [SerializeField] private BackgroundSettings settings;

        private Sprite sprite;

        public Sprite Sprite {
            get => sprite;
            set => sprite = value;
        }

        public Texture2D SpriteTexture {
            get => spriteTexture;
            set => spriteTexture = value;
        }

        public BackgroundSettings Settings {
            get => settings;
            set => settings = value;
        }

        public Background(Texture2D spriteTexture) {
            this.SpriteTexture = spriteTexture;
            SetSprite();
        }

        public void SetSpriteIfNecessary() {
            if(sprite == null) {
                SetSprite();
            }
        }

        private void SetSprite() {
            Rect rec = new Rect(0, 0, spriteTexture.width, spriteTexture.height);
            this.sprite = Sprite.Create(spriteTexture, rec, new Vector2(0.5f,0.5f), PIXELS_PER_UNIT);
            Debug.Log($"sprite={sprite}");
        }
    }
}