# SV TopDown
Programming Assesment for Starvania Studio

Unity Version: 2022.3.4f1

Systems Description:

-IDamageable (interface): Defines the TakeDamage function

-PlayerData scriptable object, stores the following data:
	-Max Health
-Current Health
	-Max Mana 
-Current Mana
-Mana regen rate
-Attack Cooldown

-PlayerController:
	-Refrences PlayerData scriptable object
	-Implements IDamagable
-Tracks health and send death event
-Takes care of refilling the mana

-PlayerHUD:
	-Refrences PlayerData scriptable object
	-Updates health and mana bars
	-Updates current key count 

-Finite State Machine
    -States:
        -Idle State: waits a random amount of time between a given min and max
        -Patrol State: picks a random position within a certain range of spawn, and moves towards it
        -Pulled State: moves to towards player position at a given speed
        -Player Detected State: Orients towards player position and fires a projectile

-Entity: Enemy AI controller. 
-Implements IDamageable
-Defines player detection functions used by diffrent states

-LevelEventChannel: 
    	-Scriptable object channel that allows communication between scripts via events
	-Defined events: OnPlayerDeath, OnKeyPickedUp and OnPlayerRespawn

-Character Movement: moves rigidbody given a movement direction. 
    Variables: 
        	-Move Speed 
        	-Velocity smoothing
	-Knockback speed

-Projectille Controller: spawns the referenced projectile prefab

-Projectile: 
Variables: 
-Speed
-Damage

-Respawn Controller: listens to OnPlayerRespawn event, and repawns player at a given location

-Pickup base class: defines a virtual pickup method

-Key Pickup (implements pickup base): Refrences a KeyData scriptable object. On pickup sets the HasBeenPickedUp value of its KeyData object to true

-Health Pickup (implements pickup base): On pickup heals the player. 
-Variables: Heal amount

-Door: Refrences a KeyData scriptable object. Allows player to open the door if the HasBeenPickedUp value of its KeyData object is set to true.
