# Blog 6 Show-Off

In the Main Menu Scene, there is a MainMenuManager with the MainMenu.cs and the CursorLock.cs attached.
![image](https://github.com/user-attachments/assets/07f81182-cfd6-4513-8a2f-a70941ce2624)
<br><br>
The MainMenu.cs is responsible for the navigation of the 3 buttons Play, Options and Quit, while the CursorLock.cs makes sure that the mouse cursor is locked and hidden.
The play button will navigate to the Levels Menu Scene; the Options button will navigate to the Options Scene and the Quit button will quit the game.
Additionally, there is also a MusicPlayer object with an audio source and the PersistentMusic.cs attached.
The audio source plays the background sound and the PersistentMusic.cs makes sure the background sound is played through all the scenes.
There is also an EventSystem, making sure that the first selected button/item is the Play button and the Actions Asset files has the ImputActions tailored for keyboard and gamepad movement.
![image](https://github.com/user-attachments/assets/9311385b-f713-4bfb-86a4-5664662886c5)
<br><br>
Lastly there is also an object called "Persistent" with the ShardPersistenManager.cs attached to it that keeps track of the shards that will be collected during the levels, if a level is completed or not and the best score for each level.
<br><br>
Moving to the Options Menu Scene, there is a slider for the volume and a mute toggle button to mute and unmute the sounds.
![image](https://github.com/user-attachments/assets/7bb71cf8-5ee5-4c90-9241-a92db018a57b)
<br><br>
The OptionsMenuManager has the same responsibility as the MainMenuManager, handling navigation of the back button (once clicked the Main Menu Scene will be loaded) and hiding the mouse cursor.
The AudioSettingsManager object has the AudioSettingsManager.cs attached to it, allowing to manipulate the sound's volume.
<br><br>
Next on the list is the Levels Menu Scene with a button to go back to the Main Menu Scene, a button for each level (level 1 to 4 also have a text for the score number) and on the top right there is a counter for the collected shards.
![image](https://github.com/user-attachments/assets/66d1c752-fb7f-4c73-9002-388f9cc8efd6)
<br><br>
The LevelsMenuManager has one additional script, compared with the MainMenuManager/OptionsMenuManager, which is the LevelUnlocker.cs that hides all the levels buttons and score texts. If the user enters the Levels Menu Scene for the first time, they will see just the Level 1 button without the score text and the shard counter at the top right corner showing 0 with the rotating shard. They can then select the level 1 and complete it. Once completed and loaded the Levels Menu Scene again, the user will see the Level 1 button with the calculated score of the previous run, the Level 2 button without any score text, since they didn't complete it yet and the shard counter increased to 10. The Shard animation in the Shard Counter is possible thanks to the ShardRenderCam, which is a secondary camera positioned far away from the canvas position.
<br>
![image](https://github.com/user-attachments/assets/e687645b-33a8-43b5-a984-43b3b8759a54)
<br><br>
It has an orthographic projection, and it outputs a texture that is used in the Shard Counter's icon.
![image](https://github.com/user-attachments/assets/266dc5ee-03ae-4d2f-81f8-e2a33a12bb5e)
<br><br>
Each Level has its own scene, and they have a few things in common.
All of them have:
<ul>
  <li> a GameManager with the CursorLocker.cs
  <li> a LevelUIManager with the LevelUIManager.cs responsible for showing the content of the PauseMenu in different situations (fell into the void, shot by a projectile, hit by a cuboid, level completed, pause, etc.)
  <li> a playable character Orbo, with the MoveJumpAbility.cs (responsible for left, right, forward and backward movement and jumping), the Player Input, the VoidDetector.cs (responsible for detecting if Orbo fell into the void), the PlayerHitReceiver.cs (responsible for detecting collisions with objects tagged as Projectile and Enemy) the CollisionManager.cs (responsible for detecting collisions with objects tagged as Shard and Portal) and the Audio source playing the jump sound.
  <li> a Shard Counter for the shards in the current level
  <li> a Layout object containig multiple platforms, enemies, shards and a portal
</ul>
The Layout Object is in each level, but its content is different in each level. <br>
The first level has just two enemies with a gun that will start to shoot at Orbo as soon as it enters the big square platform. <br>

![image](https://github.com/user-attachments/assets/2b0dacfc-0034-4f3c-ae49-b6859c6b099e)
<br><br>
In the second level there are moving platforms, an enemy with a gun and an enemy that will start following Orbo as soon as it enters the enemy's platform. <br>

![image](https://github.com/user-attachments/assets/0d2f5957-0469-4283-8c4f-541ce47754f3)
<br><br>
In the third level there are rotating platforms with horizontal cylinders attached to the central pillar. the platforms rotate in one direction and the cylinders rotate in the opposite direction. <br>

![image](https://github.com/user-attachments/assets/c981109a-e437-4976-8c4f-753a37109486)
<br><br>
In the fourth level, collectable keys are introduced to unlock parts of the level to reach the portal. Orbo must collect a key, and the next part of the level will appear. <br>

![image](https://github.com/user-attachments/assets/1889f872-4751-4a9c-85c4-6818fd6172ef)
<br><br>

In the final level, Orbo reaches the shattered core, returning all the collected shards, concluding the game with a short animation. <br>

![image](https://github.com/user-attachments/assets/1a599699-dd4b-415b-b038-196b26011e94)

