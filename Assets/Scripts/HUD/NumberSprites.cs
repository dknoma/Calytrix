using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Tilemaps;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "NumberSprites", menuName = "ScriptableObjects/NumberSprites", order = 1)]
public class NumberSprites : ScriptableObject {
    [SerializeField] private Sprite zero;
    [SerializeField] private Sprite one;
    [SerializeField] private Sprite two;
    [SerializeField] private Sprite three;
    [SerializeField] private Sprite four;
    [SerializeField] private Sprite five;
    [SerializeField] private Sprite six;
    [SerializeField] private Sprite seven;
    [SerializeField] private Sprite eight;
    [SerializeField] private Sprite nine;
    
    private const string ASSETS_PATH = "Assets/Sprites/HUD/";
    private const string CLEO_CURRENCY_FONT_FILE = "Assets/Sprites/HUD/cleo_currency_font.png";

    [SerializeField] private string filename;
    [SerializeField] private bool overrideFilename;
    [SerializeField] private bool reAddSprites;
    
    private static readonly IDictionary<int, Sprite> SPRITE_BY_INT = new Dictionary<int, Sprite>();

    private void Awake() {
        Init();
    }

    public Sprite GetSprite(int num) {
        InitDictionaryIfNecessary();
        return SPRITE_BY_INT.GetOrDefault(num, zero);
    }

    public void Init() {
        InitSprites();
        InitDictionary();
    }

    private void InitSprites() {
        string file = overrideFilename ? $"{ASSETS_PATH}{filename}" : CLEO_CURRENCY_FONT_FILE;

        if(File.Exists(file) && reAddSprites) {
            SpriteSlicer.SliceSprite(file, 9, 11);

            var sprites = AssetDatabase.LoadAllAssetsAtPath(file)
                                       .OfType<Sprite>()
                                       .ToArray();

            for(int i = 0; i < sprites.Length; i++) {
                Sprite sprite = sprites[i];
                switch(i) {
                    case 0:
                        this.zero = sprite;
                        break;
                    case 1:
                        this.one = sprite;
                        break;
                    case 2:
                        this.two = sprite;
                        break;
                    case 3:
                        this.three = sprite;
                        break;
                    case 4:
                        this.four = sprite;
                        break;
                    case 5:
                        this.five = sprite;
                        break;
                    case 6:
                        this.six = sprite;
                        break;
                    case 7:
                        this.seven = sprite;
                        break;
                    case 8:
                        this.eight = sprite;
                        break;
                    case 9:
                        this.nine = sprite;
                        break;
                }
            }
        }
    }

    private void InitDictionaryIfNecessary() {
        if(SPRITE_BY_INT.Count == 0) {
            InitDictionary();
        }
    }

    private void InitDictionary() {
        var fieldValues = this.GetType()
                              .GetRuntimeFields()
                              .Select(field => field.GetValue(this))
                              .ToList();
        
        Debug.Log($"fieldValues={string.Join(", ", fieldValues)}");

        for(int i = 0; i < 10; i++) {
            SPRITE_BY_INT.Add(i, (Sprite) fieldValues[i]);
        }
    }
}
