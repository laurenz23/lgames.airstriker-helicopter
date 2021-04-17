# WonderPlane

Changes WonderPlane.v1.0.3 April 17 2021:
- Change the target Android API to API 30
- Change the Unity version from 2019.3.13 to 2021.1.3f1
- Added Layer called Character for game characters collision
- Project Settings:
	- Configuration:
		- Change Scripting Backend from mono to IL2CPP
		- Target Architectures now use both ARMv7 and ARM64
	- Physics:
		- Layer Collision Matrix uncheck everything except to default and characters
	- Physics 2D:
		- Layer Collision Matrix uncheck everything except to default and characters

Changes WonderPlane.v1.0.1 April 11 2021:
- Added health bar system
- Added energy bar system
- Added player points system
- Added popup text UI for health deduction, health added, energy added, added points, added coins


Changes WonderPlane.v1.0.0 April 11 2021:
- Added 3d Assets and 2d Assets
  - Added clouds, rocks, tress and grounds (3d assets)
  - Added visual representatoin and buttons for attacks (2d assets)
- Seperated the 1 level in to 4 levels
- Change the character design to helicopter
- Added enemies object such as planes, helicopters, mini-boss, artillery and ground enemy units
  - Added scripts for enemy planes such as enemy movements
- Added optimization system
