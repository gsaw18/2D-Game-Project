
Elemental 3-Room Tilemap (Unity 6.2)

Files created:
- elemental_tilesheet.png        (tilesheet: 8x8 tiles, 16x16 px)
- visual_map.csv                 (tile indices for visuals)
- collision_map.csv              (1 = wall collider, 0 = empty/passable)

Tile indices:
0 = floor (checker light gray)
1 = wall (dark, bordered)
2 = door (gold) – visual only (no collision by default here)
3 = pillar (decorative visual)
4 = alt floor
5 = accent (water/blue)

Map layout:
- 3 rectangular rooms, width 16 x height 12 tiles each, placed side-by-side
- Doors centered on the shared walls between rooms (row 6, columns 15 and 31)
- CSV size: 48 x 12 tiles

Unity setup (quick):
1) Import elemental_tilesheet.png. In Inspector:
   - Texture Type: Sprite (2D and UI)
   - Sprite Mode: Multiple
   - Pixels Per Unit (PPU): 16 (or your preference, but be consistent)
   - Filter Mode: Point (no filter)
   - Sprite Editor → Grid By Cell Size: 16 x 16 → Slice → Apply.

2) Create a Grid with two child Tilemaps:
   - Tilemap_Visual (Tilemap + TilemapRenderer)
   - Tilemap_Collision (Tilemap + TilemapCollider2D [Used By Composite]=On)
     Add CompositeCollider2D + Rigidbody2D(Static) on the parent that holds Tilemap_Collision.

3) Use the CsvTilemapLoader script below (paste into Assets/Scripts) and set in the Inspector:
   - Visual CSV (TextAsset) → visual_map.csv
   - Collision CSV (TextAsset) → collision_map.csv
   - Sprites[] → drag all sliced sprites from elemental_tilesheet.png (ensure index 0..5 as above)
   - VisualTilemap → Tilemap_Visual
   - CollisionTilemap → Tilemap_Collision

4) After pressing Play or calling LoadNow() in the Editor (via a context menu), the map will be painted.
   You can add Door trigger prefabs at the doorway positions and Keys as GameObjects.

Door positions (tile coords):
- Between Room0↔Room1:  (x=15, y=6)
- Between Room1↔Room2:  (x=31, y=6)

NOTE: Keys and Doors should be GameObjects (prefabs) with your DoorToRoom and KeyPickup scripts;
      the door tile here is visual only.
