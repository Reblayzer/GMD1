# Blog 5 Development Updates 3

During these other weeks, I have worked more on the level UI. I have added a dynamic PauseMenu that changes its content based on 3 different scenarios:
If the user presses the escape button/left trigger
![image](https://github.com/user-attachments/assets/143f2c7a-d32c-458b-a64a-a39635ae70f1)
If the player falls into the void
![image](https://github.com/user-attachments/assets/4233903c-07d3-496f-b985-396a0c79c2ff)
Or when the player enters the portal and finishes the level.
![image](https://github.com/user-attachments/assets/bf44d6a6-8ef5-4631-a4d9-97858ebdf062)
In each version of the pause menu, I also have a section for the time that has passed from the start of the level and another section for the score.
The logic for it is not implemented yet, but it will be soon.
Additionally, I have changed the logic of how the player can finish the level.
Until now, the portal at the end of the level was active all the time, allowing the player to finish the level even with 0 collected shards, and if, during the next run, more shards were collected,
this counter in the levels menu would be updated with the highest collected number.
Now, I have decided that the player must collect all the shards to activate the portal, and the next level will be unlocked once the previous level is completed. This change will add an additional obstacle to the player,
forcing them to spend even more time on each level, exploring and collecting all the shards.
![image](https://github.com/user-attachments/assets/407c2655-5c44-4f0f-95f5-9983e6037235)
Moreover, I finished the layout of the first and second levels with the textures, enemies' behavior, and lighting.
![image](https://github.com/user-attachments/assets/6d3212a6-bc6b-46ef-83b9-f476770288a9)
![image](https://github.com/user-attachments/assets/79901a43-acf6-42df-bc49-bed6c77f90fa)
In the first level, there are two enemies placed on the sides of the central platform, waiting for the player to enter it.
Once Orbo steps on the platform, it will be detected by the enemies, who will face Orbo and try to shoot him.
If it gets shot, the pause menu will pop up, forcing the player to either try again or go to the levels menu.
![image](https://github.com/user-attachments/assets/c62a00aa-b615-41f6-a46f-164ec26a17c8)
In the second level, I have added moving platforms to make the game feel more dynamic and increased the difficulty compared to the first level.
![image](https://github.com/user-attachments/assets/1fc08220-5fe2-41bb-98fc-492b916f9df7)
Besides the moving platforms, I have also added a "following enemy" that will hit Orbo on contact.
This is accomplished by adding a NavMesh Surface component on the platform and a NavMesh Agent component on the enemy.
Once Orpo enters the platform, the enemy will start chasing it, and once the player exits the platform, the enemy will return to its starting position.
![image](https://github.com/user-attachments/assets/33d1c3c8-79ff-47e3-ac38-c97dcb124d14)
As you can see from the screenshot above, the final part consists of other 4 moving platforms, making the transition from one platform to another more difficult.

All in all, I am pretty satisfied with what I have implemented until now, even if I am creating this blog post with a 9-day delay.
Initially, I had in mind to create a 10 + 1 final level, which would contain the ending of the "story", but I concluded that 10 levels are too much for the remaining time.
Therefore, I've decided to have 5 + 1 final level, so that I can focus more on quality than quantity.
![Uploading image.png…]()

