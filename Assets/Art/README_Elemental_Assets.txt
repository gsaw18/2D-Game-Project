
Elemental (Original Assets Pack)

Files:
- ElementalTiles.png       : 256x256 tileset (16x16 tiles). Slice 16x16.
- CollisionTiles.png       : 64x64 collision tiles (4x4). Slice 16x16.
- ElementalPlayer.png      : 12-frame sheet (Right/Up/Left/Down × [WalkA,WalkB,Attack]). Slice 16x16, grid 12x1.
- Slime_Fire.png, Slime_Water.png : 2-frame enemy sheets (slice 16x16).
- DelverCollisions.txt     : 256-character collision mapping. Characters used: '_', 'S', 'A', 'D', 'W'.
- ElementalLevel_Hub.txt   : Example visual map (11x16 tiles) using two-hex indices (.. means 00).

Unity setup:
1) Put PNGs into Assets/Resources/ (or your art folder).
2) Import settings for all PNGs:
   - Sprite (Multiple), Pixels Per Unit = 16, Filter Mode = Point, Compression = None.
3) Slice:
   - ElementalTiles.png: Grid 16x16 (256 sprites).
   - CollisionTiles.png: Grid 16x16 (16 sprites; order maps indices 0..15).
   - ElementalPlayer.png: Grid by Cell Size 16x16 (12 sprites). Animator states: Dray_Walk_0/1/2/3 and Dray_Attack_0/1/2/3 (use the matching frames).
   - Slime_*.png: Grid 16x16 (2 sprites).
4) Tilemaps:
   - Visual: drag slices from ElementalTiles.png into a Tile Palette.
   - Collision: drag CollisionTiles.png slices into a separate Tile Palette if you want to preview, or hide the Renderer later.
5) Scripts:
   - Assign TextAssets:
       MapInfo.delverLevel      -> ElementalLevel_Hub.txt
       MapInfo.delverCollisions -> DelverCollisions.txt
   - Ensure TilemapManager has references to Tilemap_Visual and Tilemap_Collision.
6) Camera:
   - MSAA Off, Orthographic, attach CamFollowDray. Set InRoom.keepInRoom on Camera = false.
7) Layers/Sorting: Ground (visual), Player above, Collision tilemap can render or be hidden.

Collision tiles index meaning (by order in CollisionTiles.png):
 0: '_' none, 1: 'S' solid, 2: 'A' half-left, 3: 'D' half-right, 4: 'W' top-half
 5..15: duplicates of solid for convenience (you can reassign later).

Notes:
- You can extend DelverCollisions.txt by editing the 256 characters to match your new tiles.
- ElementalLevel_Hub.txt is a single room you can load immediately; expand by concatenating rooms per your loader.
- All art is original and free for your project submission.
