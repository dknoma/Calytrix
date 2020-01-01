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
