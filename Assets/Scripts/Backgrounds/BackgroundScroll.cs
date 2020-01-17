using System;
using System.Collections.Generic;
using Backgrounds;
using UnityEngine;
using UnityEngine.Tilemaps;
using Utility;

public class BackgroundScroll : MonoBehaviour {
    [SerializeField] private int horizontalScrollRate;
    [SerializeField] private int verticalScrollRate;
    [SerializeField] private ScrollType.Type scrollType;
    [SerializeField] private ScrollType.ScrollDirection scrollDirection;
    
    [SerializeField] private float threshold = 0.25f;

    public ScrollType.Type BGScrollType {
        get => scrollType;
        set => scrollType = value;
    }

    public ScrollType.ScrollDirection BGScrollDirection {
        get => scrollDirection;
        set => scrollDirection = value;
    }

    private GameObject originalObject;
    private Camera main;
    private Renderer renderer;
    
    private SpriteRenderer spriteRender;
//    private Sprite sprite;
    private TilemapRenderer tilemapRenderer;
//    private Tilemap tilemap;

    private Action scroller;
    private Action reposition;
    private Vector3 previousCamPos;
    private Vector2 screenBounds;
    private bool usingTilemap;
    
    private readonly LinkedList<GameObject> backgrounds = new LinkedList<GameObject>();

    private void Awake() {
        // If no scrolling, then background does not move; disable script
        switch(scrollType) {
            case ScrollType.Type.NORMAL:
                this.scroller = NormalScrolling;
                
                if(horizontalScrollRate == 0 && verticalScrollRate == 0) {
                    this.enabled = false;
                }
                break;
            case ScrollType.Type.AUTO:
                this.scroller = AutoScrolling;
                break;
            case ScrollType.Type.NONE:
                this.scroller = NoScrolling;
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
        
        this.main = Camera.main;
        Debug.Assert(main != null, nameof(main) + " != null");

        Transform mainCameraTransform = main.transform;
        Vector3 position = mainCameraTransform.position;
        this.previousCamPos = position;
        
        this.screenBounds =
            main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, position.z));
        
        this.originalObject = transform.GetChild(0).gameObject;
        this.renderer = originalObject.GetComponent<Renderer>();
        
        switch(renderer) {
            case SpriteRenderer sRender:
                this.spriteRender = sRender;
                InstantiateSprite();
//                this.sprite = spriteRender.sprite;
//                this.usingTilemap = false;
//                instantiator = InstantiateSprite;
                break;
            case TilemapRenderer tRender:
                this.tilemapRenderer = tRender;
                InstantiateTilemap();
//                this.tilemap = GetComponent<Tilemap>();
//                Debug.Assert(tilemap != null, nameof(tilemap) + " != null");
//                this.usingTilemap = true;
//                instantiator = InstantiateTilemap;
                break;
        }
    }

    private void InstantiateSprite() {
        float spriteWidth = spriteRender.bounds.size.x;
        Vector3 position = spriteRender.transform.position;
        int copiesNeeded = Mathf.CeilToInt(screenBounds.x * 2 / spriteWidth);
        
        backgrounds.AddFirst(originalObject);
        for(int i = 1; i <= copiesNeeded; i++) {
            GameObject clone = Instantiate(originalObject, transform, true);
            clone.transform.position = new Vector3(position.x + spriteWidth * i, position.y, position.z);
            backgrounds.AddLast(clone);
        }
    }
    
    private void InstantiateTilemap() {
        float tilemapWidth = tilemapRenderer.bounds.size.x;
        Vector3 position = tilemapRenderer.transform.position;
        int copiesNeeded = Mathf.CeilToInt(screenBounds.x * 2 / tilemapWidth);
        
        backgrounds.AddFirst(originalObject);
        for(int i = 1; i <= copiesNeeded; i++) {
            GameObject clone = Instantiate(originalObject, transform, true);
            clone.transform.position = new Vector3(position.x + tilemapWidth * i, position.y, position.z);
            backgrounds.AddLast(clone);
        }
    }
    
    private void Update() {
        scroller.Invoke();
    }

    private void LateUpdate() {
        reposition.Invoke();
    }

    private void RepositionSprite() {
        Vector3 position = transform.position;
        float rightScreenBound = position.x + screenBounds.x;
        float leftScreenBound = position.x - screenBounds.x;
        
        GameObject first = backgrounds.First.Value;
        GameObject last = backgrounds.Last.Value;
        float halfObjWidth = last.GetComponent<SpriteRenderer>().bounds.extents.x;
        
        if(rightScreenBound > last.transform.position.x + halfObjWidth) {
            first.transform.position = new Vector3();
            backgrounds.SetFirstAsLast();
        } else if(leftScreenBound < first.transform.position.x - halfObjWidth) {
            last.transform.position = new Vector3();
            backgrounds.SetLastAsFirst();
        }
    }

    private void NormalScrolling() {  
        Vector3 position = main.transform.position;
        float hPara = (previousCamPos.x - position.x) * horizontalScrollRate;
        float vPara = (previousCamPos.y - position.y) * verticalScrollRate;
        
//        this.transform.position = new Vector3(position.x - hPara, position.y - vPara, 0);
        
        this.previousCamPos = position;
    }

    private void AutoScrolling() {
        // TODO
        Vector3 position = main.transform.position;
        this.previousCamPos = position;
    }

    private void NoScrolling() {
        this.transform.position = main.transform.position;
    }
}
