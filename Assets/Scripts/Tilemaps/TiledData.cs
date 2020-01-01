using Tilemaps;
using static Tilemaps.TiledRenderOrder;

public class TiledData {
    private TiledTilemapJsonInfo tiledTilemapJsonInfo;

    [DisableInspectorEdit] 
    private RenderOrder renderOrder;

    public TiledData(TiledTilemapJsonInfo tiledTilemapJsonInfo) {
        this.tiledTilemapJsonInfo = tiledTilemapJsonInfo;
        this.renderOrder = GetRenderOrder(tiledTilemapJsonInfo.renderorder);
    }
}
