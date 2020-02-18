using System;
using System.Collections.Generic;
using Backgrounds;
using UnityEngine;
using UnityEngine.Tilemaps;
using Utility;
using static Utility.Tags;

public class BackgroundScroll : MonoBehaviour {
    [SerializeField] [Range(-16, 16)] private float horizontalScrollRate;
    [SerializeField] [Range(-16, 16)] private float verticalScrollRate;
    [SerializeField] private ScrollType.Type scrollType;
    [SerializeField] private ScrollType.ScrollDirection scrollDirection;

    private const float BASE_MOVE_SPEED = 0.0625f;
    private const float PADDING_THRESHOLD = 2f;
    private static readonly Action NO_OP = () => {};

    private GameObject originalObject;
    private Camera bgCam;
    private Transform bgCamTransform;
    private Renderer renderer;
    private Transform player;

    private Tilemap tilemap;

    private Action scroller;
    private Action repositionBg;
    
    private Vector3 previousPos;
    private Vector3 originalBgPos;
    private Vector2 screenBounds;
    
    private bool usingTilemap;
    private float halfBackgroundWidth;
//    private float verticalPadding;
    private float verticalOffset;

    private float hScroll;
    private float vScroll;
    
    private readonly LinkedList<GameObject> backgrounds = new LinkedList<GameObject>();

    private void Awake() {
        Transform bgCamTransform = transform.parent;
        if(bgCamTransform == null || !bgCamTransform.CompareTag(BACKGROUND_CAMERA_TAG)) {
            GameObject cam = GameObject.FindWithTag(BACKGROUND_CAMERA_TAG);
//            if(cam != null) {
//                transform.SetParent(cam.transform);
                bgCamTransform = cam.transform;
//            }
        }
        
        
//        Debug.Assert(bgCamTransform != null, "parent != null");
        GameObject p = GameObject.FindWithTag(PLAYER);
        this.player = p.transform;

        this.bgCam = bgCamTransform.GetComponent<Camera>();
        Debug.Assert(bgCam != null, nameof(bgCam) + " != null");
        this.bgCamTransform = bgCam.transform;
        Vector3 camPos = bgCamTransform.position;

        this.verticalOffset = transform.position.y;

        this.previousPos = transform.position;
        
        this.screenBounds =
            bgCam.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, camPos.z));
        
        this.originalObject = transform.GetChild(0).gameObject;
        this.renderer = originalObject.GetComponent<Renderer>();
        Bounds bounds = renderer.bounds;
        this.halfBackgroundWidth = bounds.extents.x;

//        this.verticalPadding = camPos.y - bounds.extents.y;
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

        this.hScroll = BASE_MOVE_SPEED * horizontalScrollRate;
        this.vScroll = BASE_MOVE_SPEED * verticalScrollRate;
    }
    
    private void LateUpdate() {
        scroller.Invoke();
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
            
//            Debug.Log($"{name} - position={position}, clonePos={clonePos}");
            
            clone.transform.position = clonePos;
            backgrounds.AddLast(clone);
        }
    }

    // Looping
    private void RepositionBackground() {
        // Get bounds of the screen
        Vector3 position = bgCamTransform.position;
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
        Vector3 camPos = bgCamTransform.position;
        float hPara = camPos.x - previousPos.x * hScroll;
        float vPara = camPos.y - previousPos.y * vScroll;
//        Debug.Log($"{name}: camPos.y - vPara + verticalOffset = {camPos.y - vPara + verticalOffset}");
//        Debug.Log($"{name}: camPos.y - vPara = {Mathf.Abs(camPos.y - vPara) < Mathf.Epsilon}");
        
//        Debug.Log($"{name} - camPos={camPos}, vpara={vPara}, camPos.y - vPara = {camPos.y - vPara}");
//        hPara = Math.Abs(hPara % 0.0625f) < float.Epsilon ? hPara : 0;
        float x = Mathf.Round((camPos.x - hPara) / BASE_MOVE_SPEED) * BASE_MOVE_SPEED;
        float y = camPos.y - vPara + verticalOffset;

        Transform thisTransform = transform;
//        thisTransform.position = new Vector3(camPos.x - hPara, camPos.y - vPara + verticalOffset, thisTransform.position.z);
        thisTransform.position = new Vector3(x, y, thisTransform.position.z);
//        Debug.Log($"{name}: thisTransform.position = {thisTransform.position}");
        this.previousPos = camPos;
    }

    private void AutoScrolling() {
        Vector3 camPos = bgCamTransform.position;
        Transform thisTransform = transform;
        Vector3 pos = thisTransform.position;
        
        float hPara = BASE_MOVE_SPEED * horizontalScrollRate;
        float vPara = BASE_MOVE_SPEED * verticalScrollRate;

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
        Vector3 position = bgCamTransform.position;
        Transform transform = this.transform;
        transform.position = new Vector3(position.x, position.y, transform.position.z);
    }
}
