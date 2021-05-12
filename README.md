# AirStriker:Helicopter

Changes AirStriker:Helicopter.v1.1.1 May 12 2021:

- PROJECT NAME:
	- Modified project name from WonderPlane to AirStriker:Helicopter
- BUGS:
	- Fixed armament cooldown animation bug
	- Fixed player health bug
	- Fixed controls bug
	- Fixed player and enemy collision bug (some instances player and won't explode when collided)

- FOLDERS:
	- Added AttackDataScriptableObjects
	- Added Functions
	- Added GeneralCharacterFunctions under Functions
	- Remove(including all contents) PlayerAnimator_prefab under Player_prefab
	- Added MainMenu from Scenes
	- Added Testing from Scenes

- SCENES:
	- Added LoadingToMainMenu
	- Added MainMenu

- CHARACTERS:
	- Modified MiniBossLevel1 to EnemyPlaneLevel6
	- Modified MiniBossLevel2 to BossStage1
	- Modified MiniBossLevel3 to BossStage2
	- Added Attacks:
		- EnemyPlaneLevel2_GatlingGun
		- EnemyPlaneLevel2_StraightMissile
		- EnemyPlaneLevel2_DropBomb
		- EnemyPlaneLevel3_SelfDestruct
		- EnemyPlaneLevel3_DropBomb
		- EnemyHelicopterLevel1
		- EnemyHelicopterLevel2
		- EnemyHelicopterLevel3
	- Added Movements:
		- EnemyPlaneLevel2_GatlingGun
		- EnemyPlaneLevel3_SelfDestruct
		- EnemyPlaneLevel3_DropBomb

- 3D MODELS:
	- Modified Enemy Character
		- EnemyPlaneLevel1_blue
		- EnemyPlaneLevel2_green
		- EnemyPlaneLevel3_red
		- EnemyPlaneLevel2_gatlingGun
		- EnemyPlaneLevel3_straightMissile
		- EnemyPlaneLevel4_dropBomb
		- EnemyHelicopterLevel1
		- EnemyHelicopterLevel2
		- EnemyHelicopterLevel3

- SCRIPTS:
	- Modified Player UI Manager
	- Modified Player Attacks:
		- Player Automic
		- Player Drop Missile
		- Player Guided Missile
		- Player Missile
	- Added Game Manager
		- Modified Game Settings to Game Graphics
		- Added Game Controls
	- Modified DisplayFPS to DebuggerManager
		- Added Movement control selection
	- Modified AmamentData to AttackData as ScriptableObject
	- Modified OnTargetVerticalMovement
	- Modified EnemyCollider "Object will be destroy once exit's on ObjectEnabler Trigger"
	- Cleanup EnemyHandler
	- Cleanup PlayerManager
	- Added MovementCharacterRoll
	- Added MovementCharacterRotor
	- Added OnHitCharacter
	- Modified PathMovement
	- Remove PathMovementDistributor

Changes WonderPlane.v1.1.0 May 1 2021:

- IN GAME UI:
	- Hide Armaments Number
	- Added Target UI "When Guided Attack is selected"
- CHARACTERS:
	- Enemy will explode when collided to player, other enemy, ground or terrain
- FOLDERS:
	- Added Enemy Armament
- MATERIALS:
	- Added Destroy material
	- Added Hit material
- 3D MODELS:
	- Updated 3d models to optimized vertices of the game
		- Tank: TankEnemyLevel1, TankEnemyLevel2, TankEnemyLevel3
		- Artillery: ArtilleryEnemyLevel1, ArtilleryEnemyLevel2, ArtilleryEnemyLevel3, ArtilleryEnemyLevel4,
			ArtilleryEnemyLevel5
		- Armament: BasicArmament, DropBomb, StraightMissile, GuidedMissile, AutomicBomb
		- Coins
	- Added 3d destroy models
		- Tank: TankEnemyLevel1, TankEnemyLevel2, TankEnemyLevel3
		- Artillery: ArtilleryEnemyLevel1, ArtilleryEnemyLevel2, ArtilleryEnemyLevel3, ArtilleryEnemyLevel4,
			ArtilleryEnemyLevel5
- SCRIPTS:
	- Added #define directives in UNITY_EDITOR only. The following scripts are affected:
		- Camera Manager
		- Game Boundary Handler
		- Player Manager
		- Player UI Manager
		- Player Animator
		- Player Movement
	- Object Enabler
		- Enemy Group Handler: if statement added FixedPosition
	- Added Detection Handler
	- Change Attack Data:
		- change from: both enemy and player have attack data
		- change to: create a global attack data for both enemy and player
	- Guided Attack Target Finder:
		- change from: cant't find target will do the cross product until to explode
		- change to: can't find target will straight forward base on object rotation until to explode
	- EnemyData
		- Added enum field for EnemyTypes
	- Added Enemy Asset Destroy
	- Added Enemy Random Destroy
	- Added Enemy Guided Attack
	- Added Enemy Guided Find Target
	- Added Enemy Guided Trigger
	- Enemy Manager
		- Added: Instantiate enemy destroy assets
	- Enemy Collider Handler
		- Added hit
		- Modified collision (Player and Enemy)
	- Enemy Group Handler
		- Added: Fixed Position
	- (Modified Name) Enemy Artillery Target -> Enemy Armament Find Target
		- change turret rotation:
			- change from: turret max and min rotation is base on set values
			- change to: turret max and min rotation is base on artillery rotation, min and max rotation can't be change
	- (Modified Name) Enemy Artillery Attack -> Enemy Aramament Attack Target
	- Deleted Enemy On Guard Movement
	- Deleted Enemy Patrol Movement
	- Deleted Enemy March Movement
	- Deleted Enemy Random Movement
	- Added Enemy On Target Horizontal Movement (Replacement for On Guard Movement)
	- Added Enemy On Taret Vertical Movement (Replacement for On Guard Movement)
	- Added Path Movement (Replacement for Patrol Movement, March Movement and Random Movement)
	- Added Path Movement Distributor
	- Added Fly Off Movement
	- Player Movement
		- Bug fixed:
			- Player Movement increase
				- Bug: when player is asceding or descending while moving forward or backward it increase movement
				- Solution: we set a condition if the player is ascending or descending it will not increase the speed 
			- Player Character Roll
				- Bug: when player is on the ground character can roll and cause an collision to ground 
				- Solution: we add a comparison for roll statement, that character will not roll if on the ground
	- Player Attack Info:
		- added areaAffect field
	- Drop Attack:
		- added area affect trigger
			- trigger enemy
			- trigger enemy attacks:
				- enemy straight attack
				- enemy guided attack
		- change attack trigger:
			- change from: attack trigger when collided
			- change to: attack trigger base on area affect
		- explosion effects:
			- change from: create explosion effect for the collided enemy target only
			- change to: create both explosion effect for the collided enemy target and the armament
	- Guided Attack
		- create attack ui prefab
	- Game Manager
		- Added Low Graphics and High Graphics
	

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
- Artillery (enemy) AI:
	- Find target (Player) Detect when target is entered the attack range
	- Add Attack Artillery when the target is at range

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
