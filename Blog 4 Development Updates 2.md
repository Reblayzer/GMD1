# Blog 4 Development Updates

During these 2 weeks I have been working on further extending the options menu.
I have implemented a volume slider for the background music and a mute toggle for it as well.
The volume stars at 100% and mute is off.
![image](https://github.com/user-attachments/assets/1509bdf4-497c-429f-ab59-e56d09d647cd)
As soon as you enable mute, the volume slider is not interactable anymore.
![image](https://github.com/user-attachments/assets/30e63106-4a50-4730-93de-04dd88268669)
The volume is controlled by an AudioSettingsManager that has access to the audio mixer.
![image](https://github.com/user-attachments/assets/39f2baae-ff61-499c-acf5-0bd371a87dbd)
In the main menu scene, to play the background music, I have added a MusicPlayer that contains an audio source.
The audio resource is not final yet; it is used as place holder to test all teh controls from the AudioSettingsManager.
![image](https://github.com/user-attachments/assets/f04df04d-ae3b-444d-87e9-fc1fed49f545)
Additionally, I finished the layout of the first level. My idea is that every level will contain 10 shards that can be picked up
and at the end of the level there is a portal that brings you back to the levels menu.
![image](https://github.com/user-attachments/assets/4d12687d-5e5e-439f-859a-465f9322bb2b)
Moreover, I have also implemented a counter showing how many shards have been picked up in this run.
![image](https://github.com/user-attachments/assets/0335215f-55e9-45fb-9104-368138e8f20e)
To make the counter looks better, I am rendering a shard using another camera with orthographic projection.
![image](https://github.com/user-attachments/assets/64355d76-606d-40f6-a611-c24328c9a0b7)
As you can see, textures and lighting are not set up yet.






