# AirStriker:Helicopter

Changes AirStriker:Helicopter.v1.1.3 Aug 29 2021:

SUMMARY
	- In this update we change our old input system and integrate new unity input system
	- We organize each scene hierarchy and project structure or project tree
	- Updated Project Settings: Script Execute Order to fix some bugs and errors
	- Added DataManager to save and load data using JSON data: 
		GameUnit 
		GameWeapons 
		PlayerProfileData 
		PlayerProfileUnit 
		PlayerProfileScore 
		PlayerProfileTokens
		And we remove also the scriptable objects that transfer data from one scene to another
	- Update Main Menu Panel:
		- Improve main menu background 
		- Added some tilt effect in main menu background
		- Hide unresearch weapons and display the researched only
	- Updated Armory Panel:
		- When selecting the unresearch weapon it will display the weapons but with unresearch effect in object
		- Can Research and Upgrade the weapon and save the data
	- Updated Mission Map Panel (Player cannot play the game when insuficient deployment capsule)
	- Updated Gameplay UI by loading the style of ui from game settings data
	- Gameplay:
		- Added Pooling System to improve game performance
		- Added Muzzle Flashes effects
		- Improved and updated Hit Effect, Explosion Effects, Attack Projectiles
		- Deleted the old low poly sky to 2d sky, and added a parallax effect
	- Updated Tokens:
		- We added a function for tokens where player now can add diamonds, coins, and deployment capsule by pressing the add icon.

PACKAGE:
	- Added New Input System
	- Added 2D Sprite Editor
	- Added Epic_Toon_FX [Imported_Assets]

2D ARTS:
	- Modified Texture 2D settings: Max Size: 512, Compression: None
	- Added bulletCircleProjectileBlueLvl1
	- Added bulletCircleProjectileGreenLvl1
	- Added bulletCircleProjectileRedLvl1
	- Added bulletCircleProjectileVioletLvl1
	- Added bulletCircleProjectileYellowLvl1
	- Added bulletProjectileBlueLvl1
	- Added bulletProjectileBlueLvl2
	- Added bulletProjectileDarkBlueLvl1
	- Added bulletProjectileGoldLvl1
	- Added bulletProjectileGreenLvl1
	- Added bulletProjectilePinkLvl1
	- Added bulletProjectileRedLvl1
	- Added bulletProjectileRedLvl2
	- Added bulletProjectileVioletLvl1
	- Added bulletProjectileVioletLvl2
	- Added dropBomb
	- Added guidedMissile1
	- Added missileProjectile1
	- Added atomic
	- Added sparkle
	- Added sparkleRed
	- Added redMuzzleEffect1
	- Added forestBackground
	- Added parallax_mountain1
	- Added parallax_mountain2
	- Added parallax_mountain3
	- Added parallax_mountain4
	- Added parallax_mountain5
	- Added parallax_mountain6
	- Added flashBlue
	- Added flashDeepBlue
	- Added flashGreen
	- Added flashRed
	- Added flashViolet
	- Added flashWhite
	- Added flashYellow
	- Added squareTech
	- Added 2d_cloud_1
	- Added 2d_cloud_2
	- Added 2d_cloud_3
	- Added 2d_cloud_4
	- Added 2d_cloud_5
	- Added 2d_cloud_6
	- Added 2d_cloud_7
	- Added 2d_cloud_8
	- Added 2d_cloud_9
	- Added 2d_cloud_10
	- Added sprite_muzzleFlash_blue
	- Added sprite_muzzleFlash_deepBlue
	- Added sprite_muzzleFlash_green
	- Added sprite_muzzleFlash_red
	- Added sprite_muzzleFlash_violet
	- Added sprite_muzzleFlashBig_blue
	- Added sprite_muzzleFlashBig_deepBlue
	- Added sprite_muzzleFlashBig_green
	- Added sprite_muzzleFlashBig_red
	- Added sprite_muzzleFlashBig_violet
	- Remove explosionEffect

3D ARTS:
	- Added cp1_defaultMaterial_transparent
	- Remove basicBullet
	- Remove Player_BiPlane
	- Remove EnergyParticle
	- Remove 3d_arts_energy folder including contents

ANIMATIONS:
	- Added animations_boss1 folder
	- Added Boss1_animator
	- Added boss1_entry
	- Added boss1_zRotation
	- Added animations_muzzleFlash folder
	- Added muzzleFlash_blue animation
	- Added muzzleFlash_blue animator controller
	- Added muzzleFlash_deepBlue animation
	- Added muzzleFlash_deepBlue animator controller
	- Added muzzleFlash_green animation
	- Added muzzleFlash_green animator controller
	- Added muzzleFlash_red animation
	- Added muzzleFlash_red animator controller
	- Added muzzleFlash_violet animation
	- Added muzzleFlash_violet animator controller
	- Added muzzleFlashBig_blue animation
	- Added muzzleFlashBig_blue animator controller
	- Added muzzleFlashBig_deepBlue animation
	- Added muzzleFlashBig_deepBlue animator controller
	- Added muzzleFlashBig_green animation
	- Added muzzleFlashBig_green animator controller
	- Added muzzleFlashBig_red animation
	- Added muzzleFlashBig_red animator controller
	- Added muzzleFlashBig_violet animation
	- Added muzzleFlashBig_violet animator controller
	- Added animations_mainMenu_popup_text folder
	- Added Popup_text_animator
	- Added Popup_text_entry_anim

PREFABS:
	- Modified Boss1DoubleBullet_cannon1
	- Modified GatlingProjectiles 
	- Modified EnemyAttackBullet to EnemyBullet
	- Modified hg_explosionEffect
	- Modified hg_explosionOrbEffect
	- Modified hg_largeExplosionEffect
	- Modified hg_mushroomExplosion
	- Modified hg_squaredExplosionEffect
	- Added hg_bulletHitEffect_blue
	- Added hg_bulletHitEffect_deepBlue
	- Added hg_bulletHitEffect_green
	- Added hg_bulletHitEffect_red
	- Added hg_bulletHitEffect_violet
	- Added hg_bulletHitEffect_yellow
	- Added CharacterSelectedInGame
	- Added DataManager
	- Added ProfilePlayerDataManager
	- Modified Unit1 (InGame)
	- Added Unit1 (MainMenu)
	- Added Unit2 (InGame)
	- Added ArmoryItem
	- Remove EnergyPrefab
	- Remove EnergyParticleEffect

SCRIPTS:
	- Modified InputManager
	- Modified PlatformRotator
	- Modified MainMenuSwitchesHandler
	- Modified EffectHandler to EffectPrefabManager
	- Modified EnemyArmamentFindTarget
		- From: Armament wait to find the target before attacking
		- To: Armament will automatically attack 
	- Added scripts_gameUnitData_scriptableObjects folder
	- Added scripts_gameWeaponData_scriptableObjects folder
	- Added scripts_objectPooledData_scriptableObjects folder
	- Added GameUnitData
	- Added GameWeaponData
	- Added ObjectPooledData
	- Added ProfilePlayerData
	- Added ProfileScoreData
	- Added ProfileSelectedData
	- Added ProfileTokensData
	- Added ProfileUnitWrapper
	- Added UnitPassiveData
	- Added WeaponData
	- Added Utils
	- Added AccelerometerPosEffect
		- For Android Device that have accelerometer in it.
		- Added effect for main menu grid and square tech background
	- Added Clouds
	- Added MuzzleFlash
	- Added BossHealth
	- Added CharacterSelectedManager
	- Added DataManager
	- Added PoolingManager
	- Added ProfilePlayerDataManager
	- Added UnitArmoryManager
	- Added ArmoryItem
	- Remove KeyboardInput
	- Remove ScreenInput
	- Remove ScreenActionInput
	- Remove SwipeRotate
	- Remove BackgroundManager
	- Remove ResearchItem
	- Remove UpgradeItem

UI:
	- Added tech_grid
	- Added invalid_start_btn
	- Added invalid_start_btn_click
	- Added maxLvL_btn_click
	- Added play_btn_invalid_effect
	- Added weapon_maxLvL_item
	- Added weapon_maxLvL_item_click

Changes AirStriker:Helicopter.v1.1.2 June 16 2021:

SUMMARY: 
	- In version 1.1.2 we added a main menu scene where player can navigate to other scene and panels
	- Added Player Name and Level in MainMenu at top center of the screen
	- Added Player diamonds and coins in MainMenu at top right and top left of the screen
	- Added Player current helicopter at the center of MainMenu
	- Added Helicopter description and stats at the left and right center of MainMenu
	- Added Buttons for Settings, Mission and Armory
	- Added Settings panel where player can change the game graphics, game helicopter movement controls, and game music and sound fx
	- Added Armory panel where player can upgrade and research the helicopter
	- Added Mission panel where player can see the world map and enter to selected or current stage level and start the game mission
	- Added Updated InGame UI
	- Added Pause panel, GameOver panel and Level Complete panel
	- Added Control And UI representation options
	- Added SoundManager for Music and SoundFX
	- Added Game Icons

PROJECT SETTINGS:
	- Modified Version Control -> Mode from Visible Meta Files to Hidden Meta Files

FOLDERS:
	- Modified Scripts -> Data -> AttackDataScriptableObjects To Scripts -> Data -> AttackData_ScriptableObjects
	- Modified Settings_prefab -> Manager_prefab
	- Modified Sounds -> Music
	- Added SoundFX 
	- Added MainMenuUI
	- Added InGameUI
	- Added Animations
	- Added Animations -> MainMenu
	- Added Animations -> MainMenu -> MainMenu_panel_transition
	- Added Animations -> MainMenu -> MainMenu_play_pointer
	- Added Animations -> MainMenu -> MainMenu_scene_objects
	- Added Scenes -> MainMenu
	- Added Scenes -> Stage1
	- Added Scenes -> Testing
	- Added Scripts -> UIscripts -> InGameUI
	- Added Scripts -> UIscripts -> MainMenuUI
	- Added 2d_Arts -> maps
	- Added Scripts -> Data -> GameSettingsData_ScriptableObjects
	- Added Scripts -> Enemy -> EnemyBoss
	- Added Animations -> Boss1
	- Added Music
	- Added SoundFX

IMPORTS:
	- Library TextMeshPro

2D ARTS:
	- Added mapBackground
	- Added map_stage1_lock
	- Added map_stage1
	- Added map_stage2_lock
	- Added map_stage2
	- Added map_stage3_lock
	- Added map_stage4_lock
	- Added map_stage4
	- Added map_stage5_lock
	- Added map_stage5
	- Added map_stage6_lock
	- Added map_stage6
	- Added map_stage7&8_lock
	- Added map_stage7&8
	- Added map_stage9_lock
	- Added map_stage9
	- Added map_stage10_lock
	- Added map_stage10
	- Added PlayerHQ
	- Added GameIcons

3D ARTS:
	- Modified BigPlane3
	- Added PlayerHelicopter_MainMenu

ANIMATOR:
	- Added Boss1_animator

ANIMATIONS:
	- Added boss1_entry
	- Added boss1_zRotation

MUSIC:
	- Added Boss_Insane_Gameplay_Looping
	- Added InGame_Endless_Cyber_Runner_2
	- Added InGame_Quirky_Action
	- Added InGame_Quirky_Action2
	- Added InGame_Techno_Caper_Looping
	- Added MainMenu_Digital_Reality
	- Added MainMenu_Techno_Gameplay_Looping

SOUNDFX:
	- Added Alert_Warning1
	- Added Alert_Warning2
	- Added Armory1
	- Added Armory2
	- Added Bomb_Drop1
	- Added Collect_Coin1
	- Added Collect_Coin2
	- Added Collect_Coin3
	- Added Collect_Coin4
	- Added Explode1
	- Added Explode2
	- Added Explode3
	- Added Explode4
	- Added Explode5
	- Added Explode6
	- Added Explode7
	- Added Explode8
	- Added Explode_Big1
	- Added Explode_SciFi1
	- Added Explode_SciFi2
	- Added Hit_Metal1
	- Added Hit_Metal2
	- Added Hit_Metal3
	- Added Hit_Metal4
	- Added Other_Mission_Start1
	- Added Other_Recieve_Coins1
	- Added Other_Rotor_Lossing_Rotation1
	- Added Other_Use_Energy_Capsule
	- Added Shoot1
	- Added Shoot2
	- Added Shoot3
	- Added Shoot_Laser1
	- Added Shoot_Laser2
	- Added Shoot_Laser3
	- Added Shoot_Laser4
	- Added Shoot_Missile1
	- Added Shoot_Missile2
	- Added Shoot_Missile3
	- Added Shoot_Missile4
	- Added Shoot_Missile5
	- Added Shoot_Missile6
	- Added Shoot_Missile7
	- Added Shoot_Missile8
	- Added UI_Click1
	- Added UI_Click2
	- Added UI_Click3
	- Added UI_Transition1
	- Added UI_Transition2

UI:
	- Added 2D_others -> tech_grid
	- Added 2D_others -> pause_header
	- Added 2D_others -> gameover_header
	- Added 2D_others -> levelComplete_header
	- Added 2D_others -> health_1bar
	- Added 2D_others -> health_10bar
	- Added 2D_others -> transparent_passive_straightMissile
	- Added 2D_others -> transparent_passive_dropBomb
	- Added 2D_others -> transparent_passive_guidedMissile
	- Added 2D_others -> round_passive_straightMissile
	- Added 2D_others -> round_passive_dropBomb
	- Added 2D_others -> round_passive_guidedMissile
	- Added 2D_others -> square_green_bar
	- Added 2D_others -> square_health_bar
	- Added 2D_others -> square_passive_straightMissile
	- Added 2D_others -> square_passive_dropBomb
	- Added 2D_others -> square_passive_guidedMissile
	- Added Buttons -> armory_btn
	- Added Buttons -> armory_btn_click
	- Added Buttons -> back_btn
	- Added Buttons -> back_btn_click
	- Added Buttons -> main_menu_btn
	- Added Buttons -> main_menu_btn_click
	- Added Buttons -> map_btn
	- Added Buttons -> map_btn_click
	- Added Buttons -> map50_btn
	- Added Buttons -> map50_btn_click
	- Added Buttons -> mission_btn
	- Added Buttons -> mission_btn_click
	- Added Buttons -> objective_btn
	- Added Buttons -> objective_btn_click
	- Added Buttons -> play_btn_click_main_menu
	- Added Buttons -> play_btn_main_menu
	- Added Buttons -> research_btn
	- Added Buttons -> research_btn_click
	- Added Buttons -> settings_btn
	- Added Buttons -> settings_btn_click
	- Added Buttons -> start_btn
	- Added Buttons -> start_btn_click
	- Added Buttons -> toggle_background
	- Added Buttons -> toggle_foreground
	- Added Buttons -> top_right_btn_add
	- Added Buttons -> top_right_btn_add_click
	- Added Buttons -> upgrade_btn
	- Added Buttons -> upgrade_btn_click
	- Added Buttons -> watch_ads_btn
	- Added Buttons -> watch_ads_btn_click
	- Added Buttons -> x_btn
	- Added Buttons -> x_btn_click
	- Added Buttons -> pause_worldMap_btn
	- Added Buttons -> pause_worldMap_btn_click
	- Added Buttons -> pause_continue_btn
	- Added Buttons -> pause_continue_btn_click
	- Added Buttons -> nextLevel_btn
	- Added Buttons -> nextLevel_btn_click
	- Added Buttons -> x2Reward_btn
	- Added Buttons -> x2Reward_btn_click
	- Added Buttons -> gameOver_worldMap_btn
	- Added Buttons -> gameOver_worldMap_btn_click
	- Added Buttons -> gameOver_retry_btn
	- Added Buttons -> gameOver_retry_btn_click
	- Added Buttons -> pause_settings_btn
	- Added Buttons -> pause_settings_btn_click
	- Added Buttons -> transparent_joystick_area
	- Added Buttons -> transparent_rightMovement_btn
	- Added Buttons -> transparent_upMovement_btn
	- Added Buttons -> transparent_leftMovement_btn
	- Added Buttons -> transparent_downMovement_btn
	- Added Buttons -> transparent_attack_btn
	- Added Buttons -> transparent_atomicBomb_btn
	- Added Buttons -> round_joystick_area
	- Added Buttons -> round_rightMovement_btn
	- Added Buttons -> round_upMovement_btn
	- Added Buttons -> round_leftMovement_btn
	- Added Buttons -> round_downMovement_btn
	- Added Buttons -> round_attack_btn
	- Added Buttons -> round_atomicBomb_btn
	- Added Buttons -> square_joystick_area
	- Added Buttons -> square_rightMovement_btn
	- Added Buttons -> square_upMovement_btn
	- Added Buttons -> square_leftMovement_btn
	- Added Buttons -> square_downMovement_btn
	- Added Buttons -> square_attack_btn
	- Added Buttons -> square_atomicBomb_btn
	- Added Buttons -> square_pause_btn
	- Added Buttons -> square_pause_btn_click
	- Added Effect_UI -> play_btn_effects
	- Added Fonts -> Squarely
	- Added Icons -> blast_icon
	- Added Icons -> boss_icon
	- Added Icons -> coin_icon
	- Added Icons -> damage_icon
	- Added Icons -> diamonds_icon
	- Added Icons -> energy_capsule_icon
	- Added Icons -> firerate_icon
	- Added Icons -> health_icon
	- Added Icons -> lockMap_icon
	- Added Icons -> more_coins_icon
	- Added Icons -> more_diamonds_icon
	- Added Icons -> passive_icon
	- Added Icons -> speed_icon
	- Added Icons -> adaptive_background_icon_81x81
	- Added Icons -> adaptive_background_Icon_108x108
	- Added Icons -> adaptive_background_Icon_162x162
	- Added Icons -> adaptive_background_Icon_216x216
	- Added Icons -> adaptive_background_Icon_324x324
	- Added Icons -> adaptive_background_Icon_432x432
	- Added Icons -> adaptive_foreground_Icon_81x81
	- Added Icons -> adaptive_foreground_Icon_108x108
	- Added Icons -> adaptive_foreground_Icon_162x162
	- Added Icons -> adaptive_foreground_Icon_216x216
	- Added Icons -> adaptive_foreground_Icon_324x324
	- Added Icons -> adaptive_foreground_Icon_432x432
	- Added Icons -> adaptive_Icon_81x81
	- Added Icons -> adaptive_Icon_108x108
	- Added Icons -> adaptive_Icon_162x162
	- Added Icons -> adaptive_Icon_216x216
	- Added Icons -> adaptive_Icon_324x324
	- Added Icons -> adaptive_Icon_432x432
	- Added Icons -> legacy_Icon_36x36
	- Added Icons -> legacy_Icon_48x48
	- Added Icons -> legacy_Icon_72x72
	- Added Icons -> legacy_Icon_96x96
	- Added Icons -> legacy_Icon_144x144
	- Added Icons -> legacy_Icon_192x192
	- Added Icons -> round_Icon_36x36
	- Added Icons -> round_Icon_48x48
	- Added Icons -> round_Icon_72x72
	- Added Icons -> round_Icon_96x96
	- Added Icons -> round_Icon_144x144
	- Added Icons -> round_Icon_192x192
	- Added Items -> weapon_research_item
	- Added Items -> weapon_research_item_click
	- Added Items -> weapon_upgrade_item
	- Added Items -> weapon_upgrade_item_click
	- Added Panel -> ads_panel
	- Added Panel -> armory_header
	- Added Panel -> blue_panel_info
	- Added Panel -> main_menu_top_panel
	- Added Panel -> objective_panel
	- Added Panel -> settings_panel
	- Added Panel -> top_header_main_menu
	- Added Panel -> top_right_panel_main_menu
	- Added Panel -> transparent_title_panel
	- Added Panel -> weapon_info_panel
	- Added Panel -> weapon_panel

ANIMATORS:
	- MainMenuUI_animator
	- MainMenu_play_pointer_animator
	- StageRotator_animator

ANIMATIONS:
	- ArmoryToMainMenu
	- MainMenu_entry
	- MainMenuToArmory
	- MainMenuToMap
	- MapToMainMenu
	- MainMenu_play_pointer_animation
	- StageRotator_ArmoryToMainMenu
	- StageRotator_entry
	- StageRotator_MainMenuToArmory

PREFABS:
	- Modified Name to Character_Player (InGame)
	- Added Character_Player (MainMenu)

SCRIPTS:
	- Modified GameManager
	- Modified Location PlayerUIManager from Scripts -> Player to Scripts -> UIScripts -> InGameUI
	- Modified AttackData -> ArmamentAttackData as GlobalFunction
	- Modified PlayerAutomic -> PlayerAttackActive1
	- Modified PlayerDropMissile -> PlayerAttackPassive2
	- Modified PlayerGatlingGun -> PlayerAttackBasic
	- Modified PlayerGuidedMissile -> PlayerAttackPassive3
	- Modified PlayerMissile -> PlayerAttackPassive1
	- Added StageRotator
	- Added ChangeScene
	- Added SwipeRotator
	- Added GraphicsManager
	- Added StyleInGameUIManager
	- Added ControlsManager
	- Added SoundManager
	- Added TokensManager
	- Added AdsPanel
	- Added RewardsPanel
	- Added ArmoryPanel
	- Added SettingsPanel
	- Added InGameLevelComplete
	- Added InGameOver
	- Added InGamePause
	- Added InGameUIDesign
	- Added ArmoryUIManager
	- Added SettingsUIManaager
	- Added TurretRotation
	- Added ArmamentAttackData
	- Added GameSettingsData
	- Added SoundData
	- Added Boss1Handler
	- Added Boss2Handler
	- Added AttackHandler
	- Remove EnemyArmamentAttackTarget
	- Remove EnemyArmamentAutoAttack

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
