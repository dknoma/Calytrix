using System;
using Backgrounds;
using UnityEngine;

public class BackgroundScroll : MonoBehaviour {
    [SerializeField] private int horizontalScrollRate;
    [SerializeField] private int verticalScrollRate;
    [SerializeField] private ScrollType.Type scrollType;
    [SerializeField] private ScrollType.ScrollDirection scrollDirection;

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

    private Vector3 previousCamPos;

    private Action scroller;

    private void Awake() {
        // If no scrolling, then background does not move; disable script
        switch(scrollType) {
            case ScrollType.Type.NORMAL:
                scroller = NormalScrolling;
                
                if(horizontalScrollRate == 0 && verticalScrollRate == 0) {
                    this.enabled = false;
                }
                break;
            case ScrollType.Type.AUTO:
                scroller = AutoScrolling;
                break;
            case ScrollType.Type.NONE:
                scroller = NoScrolling;
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
        
        this.main = Camera.main;
        Debug.Assert(main != null, nameof(main) + " != null");
        this.previousCamPos = main.transform.position;
    }
    
    private void Update() {
        scroller.Invoke();
    }

    private void NormalScrolling() {  
        Vector3 position = main.transform.position;
        float hPara = (previousCamPos.x - position.x) * horizontalScrollRate;
        float vPara = (previousCamPos.y - position.y) * verticalScrollRate;

        
        this.transform.position = new Vector3(position.x - hPara, position.y - vPara, 0);
        
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
