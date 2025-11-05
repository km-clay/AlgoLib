# AlgoLib

I ended up using a lot of the same algorithms across all of my mods and thought that re-implementing them every time was annoying, so I made this library. Contains useful algorithms and helpers for gathering information in 2D space.

This mod doesn't do anything on its own, it exists solely to expose useful tools for myself and other modders.

## Highlights
- Graph search iterators that traverse grid coordinates:
	- Breadth-first search for radial searches that expand from a specific point
	- Bresenham's Line Algorithm for raycasting

- A nice way to get tiles safely without having to remember to check WorldGen.InWorld every time
- A dozen different functions for getting information about two points, such as distance, angle, direction, etc

The implementation of the point iterator algorithms in particular is quite optimized, and both run comfortably in hot loops.

I highly recommend it for cutting down on boilerplate in mods that are heavy on analyzing tiles. It was a big help when writing both [AutoTorch+](https://steamcommunity.com/sharedfiles/filedetails/?id=3595057258) and [Smart Cursor Tweaks](https://steamcommunity.com/sharedfiles/filedetails/?id=3598787459)
