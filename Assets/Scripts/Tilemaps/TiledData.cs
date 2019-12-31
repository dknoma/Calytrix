using Tilemaps;
using static Tilemaps.TiledRenderOrder;

public class TiledData {
    private TiledTilemapInfo _tiledTilemapInfo;

    [DisableInspectorEdit] 
    private RenderOrder renderOrder;

    public TiledData(TiledTilemapInfo tiledTilemapInfo) {
        this._tiledTilemapInfo = tiledTilemapInfo;
        this.renderOrder = GetRenderOrder(tiledTilemapInfo.renderorder);
    }
}
