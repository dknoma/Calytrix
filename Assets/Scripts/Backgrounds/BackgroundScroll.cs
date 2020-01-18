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

    private const float BASE_HORIZONTAL_MOVE_SPEED = 0.0625f;
    private const float BASE_VERTICAL_MOVE_SPEED = 0.0625f;
    private const float PADDING_THRESHOLD = 2f;
    private static readonly Action NO_OP = () => {};

    private GameObject originalObject;
    private Camera main;
    private Renderer renderer;

    private Tilemap tilemap;

    private Action scroller;
    private Action repositionBg;
    
    private Vector3 previousPos;
    private Vector3 originalBgPos;
    private Vector2 screenBounds;
    
    private bool usingTilemap;
    private float halfBackgroundWidth;
    private float verticalPadding;

    private float hScroll;
    private float vScroll;
    
    private readonly LinkedList<GameObject> backgrounds = new LinkedList<GameObject>();

    private void Awake() {
//        player = GameObject.FindWithTag("Player").GetComponent<PlayerController>();    // Get the main player character

        this.main = Camera.main;
        Debug.Assert(main != null, nameof(main) + " != null");

        Transform mainCameraTransform = main.transform;
        Vector3 camPos = mainCameraTransform.position;
        this.previousPos = camPos;
        
        this.screenBounds =
            main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, camPos.z));
        
        this.originalObject = transform.GetChild(0).gameObject;
        this.renderer = originalObject.GetComponent<Renderer>();
        Bounds bounds = renderer.bounds;
        this.halfBackgroundWidth = bounds.extents.x;

        this.verticalPadding = camPos.y - bounds.extents.y;
        // If no scrolling, then background does not move; disable script
        switch(scrollType) {
            case ScrollType.Type.NORMAL:
                this.scroller = NormalScrolling;
                InstantiateCopies();
                repositionBg = RepositionBackground;
                
//                if(horizontalScrollRate == 0 && verticalScrollRate == 0) {
//                    this.enabled = false;
//                }
                break;
            case ScrollType.Type.AUTO:
                this.scroller = AutoScrolling;
                InstantiateCopies();
                repositionBg = RepositionBackground;
                break;
            case ScrollType.Type.NONE:
                this.scroller = NoScrolling;
                repositionBg = NO_OP;
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        this.hScroll = BASE_HORIZONTAL_MOVE_SPEED * horizontalScrollRate;
        this.vScroll = BASE_VERTICAL_MOVE_SPEED * verticalPadding;
    }

    private void Update() {
        scroller.Invoke();
    }
    
    private void LateUpdate() {
        repositionBg.Invoke();
    }
    
    // Initialization
    public void Initialize(int horizontalScrollRate, int verticalScrollRate, 
                           ScrollType.Type scrollType, ScrollType.ScrollDirection direction) {
        this.horizontalScrollRate = horizontalScrollRate;
        this.verticalScrollRate = verticalScrollRate;
        this.scrollType = scrollType;
        this.scrollDirection = direction;
    }
    
    public void Initialize(BackgroundSettings settings) {
        this.horizontalScrollRate = settings.HorizontalScrollRate;
        this.verticalScrollRate = settings.VerticalScrollRate;
        this.scrollType = settings.Type;
        this.scrollDirection = settings.Direction;
    }

    private void InstantiateCopies() {
        float renderWidth = renderer.bounds.size.x;
        Vector3 position = renderer.transform.position;
        int copiesNeeded = Mathf.CeilToInt(screenBounds.x * 2 / renderWidth) + 1;
        
        backgrounds.AddFirst(originalObject);
        for(int i = 1; i <= copiesNeeded; i++) {
            GameObject clone = Instantiate(originalObject, transform, true);
            Vector3 clonePos = new Vector3(position.x + renderWidth * i, position.y, position.z);
            
            Debug.Log($"{name} - position={position}, clonePos={clonePos}");
            
            clone.transform.position = clonePos;
            backgrounds.AddLast(clone);
        }
    }

    // Looping
    private void RepositionBackground() {
        // Get bounds of the screen
        Vector3 position = main.transform.position;
        float rightScreenBound = position.x + screenBounds.x;
        float leftScreenBound = position.x - screenBounds.x;
        
        GameObject first = backgrounds.First.Value;
        GameObject last = backgrounds.Last.Value;
        
        // If screen bound is moving past the threshold of an edge, move backgrounds accordingly
        if(rightScreenBound > last.transform.position.x + halfBackgroundWidth - PADDING_THRESHOLD) {
            Vector3 lastBgPos = last.transform.position;
            first.transform.position = new Vector3(lastBgPos.x + halfBackgroundWidth * 2, lastBgPos.y, lastBgPos.z);
            backgrounds.SetFirstAsLast();
        } else if(leftScreenBound < first.transform.position.x - halfBackgroundWidth + PADDING_THRESHOLD) {
            Vector3 firstBgPos = first.transform.position;
            last.transform.position = new Vector3(firstBgPos.x - halfBackgroundWidth * 2, firstBgPos.y, firstBgPos.z);
            backgrounds.SetLastAsFirst();
        }
    }

    // Scrolling
    private void NormalScrolling() {
        Vector3 camPos = main.transform.position;
        float hPara = camPos.x - previousPos.x * BASE_HORIZONTAL_MOVE_SPEED * horizontalScrollRate;
        float vPara = camPos.y - previousPos.y * BASE_VERTICAL_MOVE_SPEED * verticalScrollRate;
        
        this.previousPos = camPos;

        Transform thisTransform = transform;
        thisTransform.position = new Vector3(camPos.x - hPara, camPos.y - vPara, thisTransform.position.z);
    }

    private void AutoScrolling() {
        Vector3 camPos = main.transform.position;
        Transform thisTransform = transform;
        Vector3 pos = thisTransform.position;
        
        float hPara = BASE_HORIZONTAL_MOVE_SPEED * horizontalScrollRate;
        float vPara = BASE_VERTICAL_MOVE_SPEED * verticalScrollRate;
        

        switch(scrollDirection){
            case ScrollType.ScrollDirection.LEFT:
                thisTransform.position = new Vector3(pos.x - hPara, camPos.y - vPara, thisTransform.position.z);
                break;
            case ScrollType.ScrollDirection.RIGHT:
                thisTransform.position = new Vector3(pos.x + hPara, camPos.y - vPara, thisTransform.position.z);
                break;
            case ScrollType.ScrollDirection.UP:
                thisTransform.position = new Vector3(camPos.x - hPara, pos.y - vPara, thisTransform.position.z);
                break;
            case ScrollType.ScrollDirection.DOWN:
                thisTransform.position = new Vector3(camPos.x - hPara, pos.y + vPara, thisTransform.position.z);
                break;
            case ScrollType.ScrollDirection.NONE:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private void NoScrolling() {
        Vector3 position = main.transform.position;
        this.transform.position = new Vector3(position.x, position.y, transform.position.z);
    }
}
