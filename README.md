# 2D Top Down Isometric Shooter Study by Tadadosi
#### Made with Unity 2019.3.10f1

[![](https://i.imgur.com/5w5mh7r.gif)]()
> This is the result of challenging myself to make a prototype of a 2D isometric shooter with a sci-fi vibe in the shortest amount of time I could. 

> Also the idea to create an open source little project that could help someone in the future :)

---

#### Result after 19 hours of work (approximately):

- Lots of fun shooting lasers.
- Sprites for the player and the environment.
- Basic movement animations.
- Added shooting and impact SFXs and Music.
- Post processing using URP to achieve a cool sci-fi aesthetic. 
- A bunch of scripts to control all the behaviours.
- Lots of learning and more resources for my future projects.
- A cool video game prototype project to share for free.

[![](https://i.imgur.com/d0emTav.png)]()
[![](https://i.imgur.com/Bpkg4dB.gif)]()
[![](https://i.imgur.com/5e88KTG.gif)]()
[![](https://i.imgur.com/YVZvC2Q.gif)]()

---

### What will you get from this repo?
- A free game prototype project with an MIT License that grants you permission to use it for free for any purpose`*`
- Pixel art sprites: player, walls, groung and laser bullet.`**`
- A well thought player setup, with idle and walking animations and a behaviour that allows it to walk while shooting in 360°. It can also switch between 2 fire rates, 2 types of bullets and to do a satisfying charged shot.
- A simple example of how to create an appealing aesthetic with minimal pixel art and the use of  <a href="https://docs.unity3d.com/Packages/com.unity.render-pipelines.universal@7.1/manual/integration-with-post-processing.html" target="_blank">Unity's URP Post Processing System</a> 
- 9 edited sounds fxs and 1 sci-fi music track (names and authors on the credits section).
- 16 Monobehaviour scripts used to create the prototype (Most of them are generic and can be easily reused on other projects).
- Hopefully a good resource to learn or just to have fun messing around with it.

`* The MIT License only applies to the code tagged with "by Tadadosi" and the Unity project setup in this repo, it does not include the sprites and the audio files.`

`** Pixel art sprites are free for personal use.`

---

### How do you use this repo?
- This was made using Unity 2019.3.10f1, so I recommend using that version to avoid any possible issue, it's up to you to decide.
- Use this link to clone the repo: https://github.com/tadadosii/2DTopDownIsometricShooterStudy.git (I use <a href="https://www.sourcetreeapp.com/" target="_blank">Sourcetree</a> to control my repos)
- You can alternatively click the green button **Clone or download** to get the link or download a zip file with all the content.
- Or simply <a href="https://github.com/tadadosii/2DTopDownIsometricShooterStudy/archive/master.zip">click here</a> to download the zip file.

---

### So, how does this system works?
The main feature of this project is the **player controller** and all the **classes that combined together result in the correct movement and the 360° rotation of its arms and head**. To achieve this, I created on the scene a body structure using the body parts that I previously draw and sliced from the sprite sheet called ``char_main_skin_001`` . After having this setup ready, I started playing with it by moving the pivots. This gave me a clear view of how I needed to code the movement to make it work.

[![](https://i.imgur.com/fT7ta0H.gif)]()

Having my mind set on the behaviour idea, I ended up using ``transform.up or transform.right = direction;`` to create a **lookAt 2D method** to always point the main arm holding the weapon towards the mouse position and the opposite forearm and hand towards two points that smoothly move based on and input calculated with the current position of the mouse in relation to the position of the player. The opposite shoulder has a script that follows the rotation of the main shoulder and also adds an offset angle based on the same input and there is also a script called ``BodyPartsOrder`` that switches the head and hands Sprite Renderer ``Orden in Layer`` using it as well. And lastly the head simply rotates towards the mouse position.

[![](https://i.imgur.com/uGrCOvS.gif)]()

``A really important note here is that I used two sets of arms, one for when the player is poiting the mouse to the right and the other when pointing to the left. I made it this way because I was having trouble with the lookAt behaviour when rotating from one side to the other and also inverting the sprites scale. I'm sure that it can be done using only one set of arms, but I needed to advance, so I ended up creating conditions that would correctly switch between sets.``

Once I got all this figured out, I made **2 basic animation loops** using Unity's animation system, one for the player stop state ``Idle`` and one for when it's moving ``WalkForward``. Right after that I wrote a simple movement behaviour using transform.Translate:
```c#
Vector3 moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
transform.Translate(moveInput * Time.deltaTime * moveSpeed);
```
Then I used again ``moveInput`` along with the Vector2 ``direction player -> mouse position`` to add a bunch of conditions to check the **player's movement direction** and where is it pointing at to determine which animation should be played. When I made this, I realized that I needed a WalkBackwards animation loop for when the player was pointing forwards but moving backwards. To easily create it, I just duplicated the WalkForward animation, added it to the Animation Component, went to the Animation tab, wrote 80 (AnimFrameAmount x2) on the frame inputfield and simply reversed the animation by dragging the group of frames from the start to the new frame. And finally moved all the frames to the left to make the loop start from frame 0.

`I want to point out that although I used transform.Translate for the movement, this was only made like that because I needed my player to simply move around without messing with physics and collisions, what was important for me in this project was the 360° rotation + shooting actions. It would be a good choice to use Rigidbody2D to add movement and behaviours that interact with Unity's physics engine.`

[![](https://i.imgur.com/G2SEH2E.gif)]()

Next, after making my character move and correctly use it's animations, it was time to create last and most entertaining part, the **Shooting Behaviour**. The system consist of 2 scripts. The first one called ``Weapon`` is attached to the GameObjects called `RightDirection_Gun` and `LeftDirection_Gun`. The purpose of this class is to `Instantiate()*` a `GameObject` that has a `Projectile` class attached to it. This spawning method happens after a timer reaches a float called `fireRate`. When a projectile is ready, the bool `canFire` is set to true allowing other classes to call the method `FireBasic()` which also calls the method `Fire()` from the projectile class. There is also a method called `FireCharged()` which immediately spawns its own projectile and shoot it right away (used to bypass the stored basic projectile and shoot a charged shot right away). 

The **Projectile** class is quite simple, is has 3 custom methods. `SetActive(bool value)` to enable or disable a GameObject SpriteRenderer property, `Fire()` to initiate the launch action, `Travel()` to use transform.Translate on Update() to move the object towards its local x axis. To check for collision it uses `OnTriggerEnter2D(Collider2D collision)` and compares the tag "Wall", if that tag is found, it spawns a particle fx and destroys itself.

`It's important to note that using Instantiate() to shoot projectiles is a great choice for rapid prototyping and testing, but its a bad move when it comes to actually creating a good system, constantly creating new GameObjects and destroying them would do no good to the garbage collection system. A good choice would be creating a pool system that has pre-cloned prefabs which can be used without destroying them, they just get enabled when called and disabled when they are "destroyed".`

Once the projectile was done, it was time to start making **particle effects (pfx)** and to create a simple level to actually test the laser shots. The pfx I used are really simple, I used Unity's Particle System which can be found by right clicking on an empty spot of the hierarchy tab and then selecting `Effects > Particle System`. 

[![](https://i.imgur.com/M4us80W.gif)]()

**Wall impact pfx setup:**

[![](https://i.imgur.com/KhUUtVV.png)]()

The particle basic colors doesn't include a **HDR intensity value** that's needed to make effects glow while using a post processing effect called `Bloom`, so I also made a custom shader for the pfx using `Shader Graph`.

**Shader node tree:**

[![](https://i.imgur.com/5Z74Ddz.png)]()

**The level is made out of three sprites** that I created for this project, `Ground_Tiles`, `Wall_Front` and `Wall_Side`. They are basically pieces that fit together and could be used to create a much bigger level (although it would be quite simple considering the lack of variety). To actually use them, you just drag a sprite on the Hierarchy tab and it will be placed on the (0,0,0) position. 

After carefully placing the level sprites in the right spot, I added to the top and bottom walls a Box Collider 2D component marked as `Is Trigger` to make sure that my projectiles would find them while using `OnTriggerEnter2D()`. The walls on the sides are made with a 45° angle that cannot be easily matched by using Box Collider 2D, so I used a collider called `Polygon Collider 2D` which lets me easily move points around to make them match any shape I want`*`.

`* I'm not sure if this type of colliders would become later a performance issue, I recommend doing tests or searching online to see if they can safely be used.`

[![](https://i.imgur.com/kcr8oR0.gif)]()

`This setup is not exactly the type that you would use to create the whole game, it was made like that only to test the projectile vs wall behaviour.`

Once I had my player's core mechanics ready and working, I made a **centralized sound system** to play all the audio from two sources, one for the music and one for the sound fx. Then I edited the audio files that I had previously downloaded (see credits) to match the idea that I had in mind. For this task I used <a href="https://www.audacityteam.org/" target="_blank">Audacity</a>, a really neat and free open source audio software that comes with a bunch of options to easily modify audio files. Then I created a class called `SoundFXHandler` that simply calls the `SoundManager` class to play an audio from an array or even play all the audio files on that array at once. I added the new sound class to the player GameObject, the bullet prefabs and the pfx impact prefabs, and also added code to link the other scripts with this one.

Now that I had almost everything set, with cool sfx and music, I used <a href="https://docs.unity3d.com/Packages/com.unity.render-pipelines.universal@7.1/manual/integration-with-post-processing.html" target="_blank">Unity's URP Post Processing System</a> to create a volume to add post processing effects. The ones I used were `Bloom`, `Vignette`, `Chromatic Aberration`, `Motion Blur` and `Lens Distortion`. **Using this really makes everything pop**, specially the bloom, which makes any color with a high HDR intensity glow, creating the `"...illusion of an extremely bright light overwhelming the Camera."`(<a href="https://docs.unity3d.com/Manual/PostProcessing-Bloom.html" target="_blank">Unity Documentation - Bloom</a>).

[![](https://i.imgur.com/5e88KTG.gif)]()
> Notice how without bloom the laser shots doesn't add much impact to the graphics of the game. 

That's how I made this system and how it works. I hope you find it useful and fun to mess around with.

---

### Support
If you need any help or you found an issue that would like to talk about, reach out to me at one of the following places!
- <a href="https://twitter.com/tadadosi" target="_blank">Twitter @tadadosi </a>
- <a href="https://www.reddit.com/user/tadadosi" target="_blank">Reddit u/tadadosi </a>

---

<p align="center">  

[![](https://i.imgur.com/N4Dnnje.gif)]()

<p/>

---

<p align="center">  
I believe that knowledge should be free and easy to find. Not so long ago I was having a hard time figuring all this stuff out, so I started making this type of projects hoping that it will be useful to you or to anyone else who finds it.
<p/>
<p align="center"> 
If you would like to support my work, consider doing any of the following:
<p/>
<p align="center"> 
<a href="https://twitter.com/tadadosi?ref_src=twsrc%5Etfw"><img width="150" height="33" src="https://i.imgur.com/upEtqCa.png" title="Follow me on Twitter" alt="Follow me on Twitter @tadadosi"></a>
<p/>
<p align="center"> 
<a href="https://www.reddit.com/user/tadadosi"><img width="150" height="33" src="https://i.imgur.com/PrMqwGF.png" title="Follow me on Reddit" alt="Follow me on Reddit u/tadadosi"></a>
<p/>
<p align="center"> 
<a href="https://ko-fi.com/tadadosi"><img src="https://i.imgur.com/6DSw7x4.png" title="Buy me a Coffee on ko-fi.com" alt="Buy me a Coffee on ko-fi.com"></a>
<p/>

---

### Many thanks to
- My awesome wife that encourages me every day.
- Unity for existing and being free!
- All the people who gives me great feedback on <a href="https://www.reddit.com/user/tadadosi" target="_blank">Reddit</a> and <a href="https://twitter.com/tadadosi" target="_blank">Twitter</a>.
- All the artist / programmers who are constantly making free knowlegde available.
- And anyone who's reading this :)

---

### Credits

#### Scripts
- <a href="https://gist.github.com/ftvs/5822103" target="_blank">CameraShake</a> by ftvs on Github
- <a href="http://wiki.unity3d.com/index.php/Singleton" target="_blank">Singleton pattern</a> wiki.unity3d.com

#### Sound FX (<a href="https://freesound.org" target="_blank">Freesound.org</a>)
- Short Laser Shots by  <a href="https://freesound.org/people/Emanuele_Correani/sounds/260155/" target="_blank">Emanuele_Correani</a> - CC-BY-3.0 
-  Sci-Fi Force Field Impact 15 by  <a href="https://freesound.org/people/StormwaveAudio/sounds/330629/" target="_blank">StormwaveAudio</a> - CC-BY-3.0 
- Sci_FI_Weapon_01 by  <a href="https://freesound.org/people/ST303/sounds/338783/" target="_blank">ST303</a> - CC0 1.0
- SciFi Gun - Mega Charge Cannon by  <a href="https://freesound.org/people/dpren/sounds/440147/" target="_blank">dpren</a> - CC0 1.0

#### Music (<a href="https://freemusicarchive.org/" target="_blank">Freemusicarchive.org</a>)
- Azimutez by  <a href="https://freemusicarchive.org/music/Sci_Fi_Industries/Blame_the_Lord/01_sci_fi_industries_-_azimutez" target="_blank">Sci Fi Industries</a> - CC BY-NC-SA 3.0

#### Repo readme
- sampleREADME.md by <a href="https://gist.github.com/fvcproductions/1bfc2d4aecb01a834b46" target="_blank"> fvcproductions</a> on Github
-  <a href="https://help.github.com/en/github/writing-on-github/creating-and-highlighting-code-blocks" target="_blank"> Github - Creating and highlighting code blocks</a>

---

### License
- <a href="https://opensource.org/licenses/mit-license.php" target="_blank">MIT license</a>
- The MIT License only applies to the code tagged with "by Tadadosi" and the Unity project setup in this repo, it does not include the sprites and the audio files.
- Pixel art sprites are free for personal use.