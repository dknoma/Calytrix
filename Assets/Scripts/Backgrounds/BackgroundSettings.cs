using System;
using UnityEngine;

namespace Backgrounds {
    [Serializable]
    public class BackgroundSettings {
        [SerializeField] private float m_x;
        [SerializeField] private float m_y;
        [SerializeField] private string sortLayer;
        [SerializeField] private int orderInLayer;
        [SerializeField] [Range(-16, 16)] private float horizontalScrollRate;
        [SerializeField] [Range(-16, 16)] private float verticalScrollRate;
        [SerializeField] private ScrollType.Type type;
        [SerializeField] private ScrollType.ScrollDirection direction;

        /// <summary>
        /// Background x position.
        /// </summary>
        public float x {
            get => m_x;
            set => this.m_x = value;
        }

        /// <summary>
        /// Background y position.
        /// </summary>
        public float y {
            get => m_y;
            set => this.m_y = value;
        }

        /// <summary>
        /// Sort layer of the background.
        /// </summary>
        public string SortLayer {
            get => sortLayer;
            set => this.sortLayer = value;
        }

        /// <summary>
        /// Order in layer of the background.
        /// </summary>
        public int OrderInLayer {
            get => orderInLayer;
            set => this.orderInLayer = value;
        }

        /// <summary>
        /// Horizontal scroll rate of the background.
        /// </summary>
        public float HorizontalScrollRate {
            get => horizontalScrollRate;
            set => this.horizontalScrollRate = value;
        }

        /// <summary>
        /// Vertical scroll rate of the background.
        /// </summary>
        public float VerticalScrollRate {
            get => verticalScrollRate;
            set => this.verticalScrollRate = value;
        }

        /// <summary>
        /// The type of scrolling of the background. Valid values: NORMAL, AUTO, NONE.
        /// </summary>
        public ScrollType.Type Type {
            get => type;
            set => this.type = value;
        }

        /// <summary>
        /// Scrolling direction of the background/
        /// </summary>
        public ScrollType.ScrollDirection Direction {
            get => direction;
            set => this.direction = value;
        }
    }
}