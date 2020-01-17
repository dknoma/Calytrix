﻿using System;
using System.Collections.Generic;
using Backgrounds;
using UnityEngine;
using Utility;

public class BackgroundScroll : MonoBehaviour {
    [SerializeField] private int horizontalScrollRate;
    [SerializeField] private int verticalScrollRate;
    [SerializeField] private ScrollType.Type scrollType;
    [SerializeField] private ScrollType.ScrollDirection scrollDirection;
    
    private const float PADDING_THRESHOLD = 2f;

    private GameObject originalObject;
    private Camera main;
    private Renderer renderer;

    private Action scroller;
    private Vector3 previousCamPos;
    private Vector2 screenBounds;
    
    private bool usingTilemap;
    private float halfBackgroundWidth;
    
    private readonly LinkedList<GameObject> backgrounds = new LinkedList<GameObject>();

    private void Awake() {
        // If no scrolling, then background does not move; disable script
        switch(scrollType) {
            case ScrollType.Type.NORMAL:
                this.scroller = NormalScrolling;
                
//                if(horizontalScrollRate == 0 && verticalScrollRate == 0) {
//                    this.enabled = false;
//                }
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
        this.halfBackgroundWidth = renderer.bounds.extents.x;
        InstantiateCopies();
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
    
    private void Update() {
        scroller.Invoke();
    }

    private void LateUpdate() {
        RepositionBackground();
    }

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
    
    public void Initialize(int horizontalScrollRate, int verticalScrollRate, 
                           ScrollType.Type scrollType, ScrollType.ScrollDirection direction) {
        this.horizontalScrollRate = horizontalScrollRate;
        this.verticalScrollRate = verticalScrollRate;
        this.scrollType = scrollType;
        this.scrollDirection = direction;
    }
}