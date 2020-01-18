using System;
using UnityEngine;

namespace Backgrounds {
    [Serializable]
    public class BackgroundSettings {
        [SerializeField] private int orderInLayer;
        [SerializeField] private int horizontalScrollRate;
        [SerializeField] private int verticalScrollRate;
        [SerializeField] private ScrollType.Type type;
        [SerializeField] private ScrollType.ScrollDirection direction;

        public int OrderInLayer {
            get => orderInLayer;
            set => orderInLayer = value;
        }

        public int HorizontalScrollRate {
            get => horizontalScrollRate;
            set => this.horizontalScrollRate = value;
        }

        public int VerticalScrollRate {
            get => verticalScrollRate;
            set => verticalScrollRate = value;
        }

        public ScrollType.Type Type {
            get => type;
            set => type = value;
        }

        public ScrollType.ScrollDirection Direction {
            get => direction;
            set => direction = value;
        }
    }
}