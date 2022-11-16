# Readme

Within the Unity menu, choose Component -> Tilemap -> Tilemap to add a new tilemap
and parent grid object to the scene. To get the best results, we're going to want
to layer multiple tilemaps on our scene. Right click on the Grid object in the
scene and choose 2D Object -> Tilemap.

The next step is to open the Tile Palette window within Unity.
From the menu, choose Window -> 2D -> Tile Palette. The palette will be empty to
start, but you'll want to drag your images either one at a time or multiple at a
time into the window.

Select the tilemap that represents our walls or boundaries and
choose to Add Component in the inspector. You'll want to add both a Tilemap Collider 2D
as well as a Rigidbody 2D. The Body Type of the Rigidbody 2D should be static so that
gravity and other physics-related events are not applied.
