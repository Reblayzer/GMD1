# Blog 3 Development Updates

During these weeks, I made some progress, but I was all over the place and didn't know where to start, so I decided to start with the characters.
There is Orbo, the main character, who is a light blue glowing sphere that can move and jump around.
There is the shard, which will be present in each level, and Orbo has to collect all of them in order to get to the next level.
A red version of the shard is the projectile model, which the gun will shoot, that the enemy cuboids will possess.
Additionally, the model of the cuboids is present, and these cuboids will have different sizes.
The last one is the model of the Core with missing shards, which Orbo will return to.
These models are not final, but I needed something in order to progress with the entire development.
![Screenshot 2025-04-02 164107](https://github.com/user-attachments/assets/d0533e91-d2f3-4ce2-afb8-98452a815caf)
<hr>
After creating the first version of the models, I started implementing a few scripts. 
One for Orbo that contains the movement and jump mechanic, and one for collecting the shards. Orbo's model has also the PlayerInput with InputSyste_actions making sure that Orbo can be moved on the arcade machine.
One for the shard containing the rotating animation;
One for the gun containing the shooting mechanic;
One for the projectile containing the mechanism of dealing damage.
These scripts still need to be perfected.
<hr>
Lastly, I created a scene for the main menu containing a picture as a background, the title, the play, the options, and the quit buttons.
The MainMenuManager has a script containing the methods that will be executed once the buttons are clicked.

![Screenshot 2025-04-02 163235](https://github.com/user-attachments/assets/810d2ad3-8462-4ce7-8cb0-3d755e82c1e5)
<hr>
These other two scenes have just a title and a back button for now.
Each of these scenes has a manager for the back button, loading the main menu scene once clicked.
From the assets store I got the sprite for the buttons and a font for the text.

![Screenshot 2025-04-02 163249](https://github.com/user-attachments/assets/0984a842-b227-4c48-b29c-78ab58ee0630)
![Screenshot 2025-04-02 163313](https://github.com/user-attachments/assets/c87a522a-1722-4eff-93be-78c4e83c7b11)
