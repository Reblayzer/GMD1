# Roll-A-Ball

After going through the tutorial and learning the basics, I wanted to extend the Roll-A-Ball tutorial into a challenging parkour game.
The Player starts on a small platform that is connected to the main platform by a thin plank. On the main platform, there is a red cube that starts following the player as soon as the player touches the main platform, and there is also a big tower.
![Screenshot 2025-03-10 125230](https://github.com/user-attachments/assets/733bbdb2-42cf-4a0b-a9ef-5206c117b8eb)
![Screenshot 2025-03-10 125257](https://github.com/user-attachments/assets/0861318a-9fef-4659-a7ca-66f12a7c2fb4)
![Screenshot 2025-03-10 125312](https://github.com/user-attachments/assets/b88eb016-75a1-463a-8bf7-eed76f23f6bb)

To avoid getting killed by the enemy, the player should start climbing on the small platforms connected to the tower and collect all 7 little green cubes to win the game.
One of the green cubes is hidden, so good luck finding it.
To allow the user better exploration of the map, I also implemented the ability to move the camera by moving the mouse.
Moreover, I also focused on the user experience. If I must put myself in the shoes of the player, I would like to have a main menu when I launch the game, where I can start the game or quit the game. 
![Screenshot 2025-03-10 125118](https://github.com/user-attachments/assets/18f8de05-2439-4ab1-bd44-9c246079decb)

Additionally, after I start the level, I would also like to access the option menu by pressing the escape button. Let's suppose I fall from the platform while I am playing. If this happened, the player would fall forever, and he would have to close the application and restart it. Thanks to the option menu, the user can restart the level, resume the level, or quit the game.
![Screenshot 2025-03-10 125317](https://github.com/user-attachments/assets/29c009a9-3b69-4f49-9e6e-5778121a9611)

Allowing the player to jump was a very interesting experience. I assigned the tag "Ground" to the objects that the player can stand on, and initially, the player had a Boolean checking for collisions with these objects. Thoug,h there were some circumstances where the player would collide with 2 different "Ground" objects at the same time, and this would update the Boolean checking for the collisions properly. The solution to that was to change the Boolean to a counter, so if the player is not touching any ground objec,t the counter would be 0 and if this counter is higher than 0 then the player is allowed to jump.
