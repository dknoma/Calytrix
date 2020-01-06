# Calytrix
> #plantgang

Some sort of new fangled platformer thing.

# Terminology
> Some terminology that will be used consistently throughout the document in case you're not familiar with them.

- `key/value pair`: A `key` is the `name or type` assigned to a value while the `value` is the actual data itself. `Key/value` pairs (also written as `<key, value>`) are used to conveniently store data by a name or type so that we can use that name or type to grab that value later on.
  - For example, `"genre": "rock"`. `genre` would be the `key` here and `rock` would be the value. 

# Tiled
## Custom Properties
> Custom properties are used in Tiled to allow us to specify variables we otherwise would not be able to use in Tiled AND Unity.
For example, `layer_name` allows us to tell Unity which `Layer` this tilemap layer should belong to (which is used for physics, etc).

### Layer Names
> NOTE: All names, keys, and values are case sensitive. So capitalization or lack thereof matters! DO NOT FORGET OR THE GOBLINS WILL GET YOU.

> Labels are the keys (names) that tell Unity which custom property is being used
- Labels
  - `layer_name`: the key that tells Unity which Unity `Layer` this Tiled layer is a part of.
> The actual Unity `Layer` name
- Names
  - `Ground`: The default layer for ground tiles which the player can interact with.
  - `Decoration`: Tiles that the player should not interact with.
  - `Hazards`: Tiles that are player hazards.
  - `Special`: Not implemented yet, but will be used in conjunction with another custom property to see what the special interactions should be.
  
### Order in Layer
- `order_in_layer`
  - allows Unity to determine the order of each tilemap in the layer. This is so the tilemaps are rendered in the correct order (in front of the player, behind the player, behind another layer, etc)
  
# Wwise
### Importing audio
- Project -> Import Audio
  - From here, you can select `actor-mixer hierarchy` (SFX, etc) or `interactive music hierarchy` (music files).
    - Press `Import`
  - Create an event which when called, will play the SFX or soundtrack.
    - Drag the audio file/playlist into the event so that the event will actually play the audio
    - In order for music to be looped, it must be placed into its own `sequence playlist container`
      - From the playlist editor you can change the loop count to `infinite` for songs you want looped.
  - Create a soundbank you would like the place the music into
    - this will be placed on the object that you want to play the audio from
  - Depending on how you want the audio to be played:
    - In a script attached to the object you want to play audio from, you can do something as simple as `AkSoundEngine.PostEvent("EventNameFromWwise", gameObject);` and it will play the specified `Event` and subsequently any audio attached to the event


# Version History

### 1.0.1
> Wwise audio integration
#### AudioKinetic: Wwise
- Wwise allows us to smoothly integrate audio files into our system.
  - This includes SFX, music tracks, stingers, etc.
- It is relatively easy to add audio from Wwise into Unity:
  - To set it up the first time, use the Wwise launcher to inject the Wwise 
  - Import the audio into Wwise 
  - Create an event that will play the audio
  - Put it into a soundbank then generate the soundbank
  - Depending on how you want the audio to be played:
    - In a script attached to the object you want to play audio from, you can do something as simple as `AkSoundEngine.PostEvent("EventNameFromWwise", gameObject);` and it will play the specified `Event` and subsequently any audio attached to the event

### 1.0.0
> Initial Version

#### Tiled Importer Script
- Takes tilemap.json and tileset.json files, parses the data to create objects that contain the same data that Unity can understand
- Can slice spritesheets/tileset sheets instead of manually having to do that
- When importing the level tilemap into Unity
  - it will create the proper Tile assets (if necessary, otherwise will grab existing ones)
  - will create the Unity Grid object which holds tilemaps (and set the cell size via the importers settings which we set in the Unity inspector. The cell size is how big the cells are that tiles go into)
  - Create the Unity Tilemap object for each layer (which becomes a child of the Grid)
    - The tilemap will be given a Renderer (so that the tiles actually render lol)
    - Its order in layer is assigned via the Tiled custom property: order_in_layer
    - Its Unity Layer is assigned via the custom property: layer_name
      - Layer names are specified in: https://github.com/dknoma/Calytrix
  - each tilemap layer is then used to set the Tile objects in Unity into each tilemap Layer with the right properties
