# 2D Isometric Shooter Study by Tadadosi v0.2
#### Game prototype with a sci-fi vibe made with Unity 2019.4.0f1

![Big 25mb Gameplay Animated GIF](https://github.com/tadadosii/ImageStorage/blob/master/Isometric_Shooter_Study_v0.2_Gameplay.gif)

This upgrade is the result of taking all the awesome feedback I got from my reddit posts (<a href="https://www.reddit.com/r/Unity2D/comments/gvwbbv/almost_ready_to_share_the_source_code_and_the/" target="_blank">Post 1</a> , <a href="https://www.reddit.com/r/Unity2D/comments/gwlzvf/without_further_ado_here_is_the_link_to_the/" target="_blank">Post 2</a>), along with a lot of researching, to completely rework my code in an attempt to really improve this project. 

And is also my great effort to provide new devs with a really well thought game prototype that could help them in their journey as game developers.

`Note: V0.1 is still available through this link` <a href="https://github.com/tadadosii/2DTopDownIsometricShooterStudy_v0.1" target="_blank">2D Top Down Isometric Shooter Study by Tadadosi v0.1</a>.`I stored it because there are important parts of my project explained in there and also to let people see how everything drastically changed from one project to the other.` 

---

### Table of Contents (ToC) <a name="home"></a>
- [Features](#features)
- [What will you get?](#whatyouget)
- [Installation](#installation)
- [How does it works?](#howdoesitwork)
  - [Animations](#animations)
  - [Audio](#audio)
  - [Camera](#camera)
  - [Crosshair](#crosshair)
  - [Debug](#debug)
  - [Environment](#environment)
  - [FX - Dash After Images](#afterimages)
  - [Pause Controller](#pausecontroller)
  - [TadaInput (Custom Input Manager)](#tadainput)
  - [Physics](#physics)
  - [Player (9 classes)](#player)
  - [Utilities](#utilities)
  - [Weapons](#weapons)
  - [Projectiles](#projectiles)
  - [Shaders](#shaders)
- [Support](#support)
- [Credits](#credits)
- [License](#license)

---

### Result after 19 (v0.1) + 70 (v0.2) hours of work (approximately): <a name="features"></a>

- New **base class Weapon** that has two actions (Primary, Secondary) which can be overriden (by a derived class) to add a behaviour that can be called using the base actions methods.
- Two really fun to use **laser weapons** made using the base class Weapon.
- A **satisfying dash skill** that has a cool after images effect which is automatically handled by a neat system.
- An awesome **LookAt2Dv2** class that **entirely fixed my 2D look at problems** and allowed me to use a simple set of arms to make my aim rotation behaviour, which in turn fixed a bunch of problems that arised when using duplicated arms and weapons.
- All the actions previously handled in the **PlayerController** are now handled by several classes like **PlayerPhysics**, **PlayerSkills**, **PlayerAnimations**, etc.
- The function of the **PlayerController** class is now to only call other classes by getting inputs from the new input manager called **TadaInput**.
- This new input manager works just like Unity's default input manager. For example, to get a key you simply write **TadaInput.GetKey(ThisKey.KeyName)**. You can also store an axis into a key and use it as a bool to do any action. It currently supports inputs from the keyboard, mouse and xbox controllers.
- New environment assets with colliders and behaviours to let the player correctly interact with them.
- Basic movement animations.
- A **SoundManager** class that is used to **easily play global sounds**.
- A **local Sound Handler** to **quickly add AudioSources to gameobjects** and to provide a set of methods to play one or more sounds at the same time.
- A camera that follow the player and offsets its position based on the player's crosshair position.
- Post processing effects using the **Universal Render Pipeline (URP)** to achieve a **cool sci-fi aesthetic**.
- Lots of learning and more resources for my future projects.
- A cool open source video game prototype project to share for free.

[![](https://i.imgur.com/OfmTyZ6.png)]()
[![](https://i.imgur.com/Bpkg4dB.gif)]()
[![](https://i.imgur.com/DjfttAc.gif)]()
[![](https://i.imgur.com/96w8CFs.gif)]()

### What will you get from this repo? <a name="whatyouget"></a> ([ToC↑](#home))
- A **free game prototype project** with an MIT License that grants you permission to use it for free for any purpose`*`
- **Pixel art sprites**: player, walls, groung, weapons and laser bullet.`**`
- A **well thought player setup**, with idle and walking animations and a behaviour that allows it to walk while shooting in 360°. It can also switch between fire rates, 2 types of weapons, can do a satisfying charged shot and a dash skill with a cool after images effect.
- A simple example of how to create an appealing aesthetic with minimal pixel art and the use of  <a href="https://docs.unity3d.com/Packages/com.unity.render-pipelines.universal@7.1/manual/integration-with-post-processing.html" target="_blank">Unity's URP Post Processing System</a> 
- 10 edited sounds fxs and 1 sci-fi music track (names and authors on the credits section).
- **45 scripts** (classes) used to create the prototype (Most of them are generic and can be easily reused on other projects).
- A great example of **how to organize the folders of a Unity project** to have all things under control.
- What I believe is a well structured system that allows the user to quickly add more behaviours using base classes.
- Hopefully a **good resource to learn** or just **to have fun messing around with** it.

`* The MIT License only applies to the code tagged with "by Tadadosi" and the Unity project setup in this repo, it does not include the sprites and the audio files.`

`** Pixel art sprites are free for personal use.`

---

### How do you use this repo? <a name="installation"></a> ([ToC↑](#home))
- This was made using Unity 2019.4.0f1, so I recommend using that version to avoid any possible issue, it's up to you to decide.
- Use this link to clone the repo: https://github.com/tadadosii/2DTopDownIsometricShooterStudy.git (I use <a href="https://www.sourcetreeapp.com/" target="_blank">Sourcetree</a> to control my repos)
- You can alternatively click the green button **Clone or download** to get the link or download a zip file with all the content.
- Or simply <a href="https://github.com/tadadosii/2DTopDownIsometricShooterStudy/archive/master.zip">click here</a> to download the zip file.

---

### So, how does this system works? <a name="howdoesitwork"></a> ([ToC↑](#home))

After creating the first prototype and reading all the feedback I got from reddit's awesome users, I sat down and completelly reworked my code. A reddit user suggested me to use the <a href="https://unity3d.college/2017/01/10/unity3d-architecture-srp/">Single Responsibility Principle (SRP)</a> which states that a class `"...should have responsibility over a single part of the functionality provided by the software, and that responsibility should be entirely encapsulated by the class."` and I think I succeded in separating responsabilities and having a great system that can scale really well, but I didn't exactly followed the SRP, there is still a lot of dependency between classes which I don't see as a problem, maybe someone more experienced could give me great feedback in this regard.

I will no go through all the components that makes my system work.

### Animations <a name="animations"></a> ([ToC↑](#home))

I made all my animations using Unity's Legacy Animation System. I believe it's the best choice when dealing with simple 2D actions that doesn't necessarily require a blending transition like the mecanim system provides, it's a good practice for optimization and it's quite easy to implement and use.

When dealing with legacy animations, you might get a message that says something like `Your animation must be marked as Legacy`, to fix this issue, select your animation in the project tab, click the three dots button on the top right corner of the inspector tab, hit Debug and you will find a checkbox named Legacy, check that box and voila, you got a legacy animation now.

![](https://i.imgur.com/R7nYPay.gif)

`Tip: Create an empty animation, mark it as legacy and simply duplicate it every time you need to create a new one, that way you don't have to mark every single new animation you create.`

To control de animations via code I created a class called `AnimationHandler` which has two basic methods, `SetAnimationSpeed(string name, float speed)` and `PlayAnimation(string name)`. Then I used it as base class to create a derived class for the player called `PlayerAnimations` which I then used to write new methods that along with the base behaviour create the intended final actions that are used by the `PlayerController` to handle the animations based on the player's inputs.

---

### Audio <a name="audio"></a> ([ToC↑](#home))

I believe that a really important aspect of a game is how it handles its sounds and how good they relate to what the user sees. Also in the case that a user have limited sight or even no sight at all, the sound system becomes the most important aspect of the game. In my first prototype, I made a `SoundManager` using a `Singleton Pattern` and after posting about it on <a href="https://www.reddit.com/r/Unity2D/comments/gwlzvf/without_further_ado_here_is_the_link_to_the/" target="_blank">Reddit</a> I got really great feedback which I used to learn much more about it and to create another system.

If you are not familiar with the term, **A Singleton pattern is a way to ensure a class has only a single globally accessible instance available at all times. Behaving much like a regular static class but with some advantages** (wiki.unity3d.com). What does this mean? it means that by using this pattern you can have a unique class tagged with `DontDestroyOnLoad`, it will be accesible across multiple scenes, if you load a new scene it will remain intact and could still be called by using `<ClassName>.Instance`.
 
**Using Singleton patterns is always a debated topic**, one side says that it's a bad practice and should totally be avoided, that it could lead to issues later on because it makes all the classes that use its global properties / methods dependent of a single global instance, and if you ever need to refactor things later on because your requirements somehow changed in a unexpected way, doing it could be complicated depending of how everything was build around said Singleton.

The other side says that it's not that they are evil, it's that `"they hard to do it right"` (which in my opinion is quite right, it's a concept that's confusing and not so easy to grasp). They also say that Singletons improve readability, improve maintenance and also memory and performance. 

As you can see, it's a really controversial topic, there are so many different views and it makes it really difficult to decide whether it's actually good or bad to use a Singleton pattern. My take on this is that we should simply test it and see for ourself how this pattern works and how it could helps us (or not), and also **when we are learning we cannot limit ourself, we just need to do stuff over and over until we eventually grasp everything**. In my particular case, I find it appropriate to create a SoundManager class that's a single instance and can be accessed globally, I like the idea of a centralized set of AudioSources that I can call from anywhere to simple play global sounds and I'll stick with it to play music tracks and sound fx that doesn't necessarily need to be positioned in space (e.g. UI Buttons).

As for the SoundManager itself, I created the class `SoundEmitter` which adds an `AudioSource` to any gameobject that has it and also stores this AudioSource in a protected property called `_Source`. Then I used this class to create three derived classes which I called `SoundFXEmitter`, `MusicEmitter` and `UnstoppableSoundEmitter`. The methods I wrote are really simple, the only part that I think could be seen as "interesting" is the `Random.Range(minPitch, maxPitch)` which I use to change the pitch of the sound based on those two arguments, and also the last derived class that I use when I need to play a sound that shouldn't be stopped after it started playing.

After I had this system ready, I had to create a class to control it, so I made the class called `SoundHandler` which I later renamed as `SoundHandlerGlobal`. At first this class had the public arrays `AudioClip[]`, `volumes[]`, `minPitch[]` and `maxPitch[]`, which looked like a mess in the Inspector tab. Then I found an awesome solution online to use a class to store multiple properties and expose those variables in a single array of a new class that I called `Sound`.

This is how the Sound code looks like:

```c#
using UnityEngine;

[System.Serializable]
public class Sound
{
    // NOTE: To show this kind of classes in the Inspector, make sure you add 
    // [System.Serializable] and also remove the Monobehaviour type.

    public string notes;
    public AudioClip clip;

    [Range(0f, 1f)]
    [SerializeField] private float _Volume = 1f;
    public float Volume { get { return _Volume; } private set { _Volume = value; } }

    [Range(0f, 1f)]
    [SerializeField] private float _MinPitch = 1f;
    public float MinPitch { get { return _MinPitch; } private set { _MinPitch = value; } }

    [Range(0f, 1f)]
    [SerializeField] private float _MaxPitch = 1f;
    public float MaxPitch { get { return _MaxPitch; } private set { _MaxPitch = value; } }
}
```

This is how simple it is to add the array of sounds:
```c#
public Sound[] sounds;
```

And this is how it looks like in the Inspector tab now (Cool isn't it? 🤩)

![](https://i.imgur.com/fThOYWO.png)

`When I made the first prototype I had no intentions of creating a local system for sounds as I was reluctant to add an AudioSource to all the objects, however, after the reddit's users feedback and testing some things out, the idea of adding a local system became a really great step towards having a great sound system.` 

To control the local sounds I made the class `SoundHandlerLocal` (reason why I renamed the other as SoundHandlerGlobal) which is a derived class of `SoundEmitter` (to make it have a stored AudioSource) and copied the basic methods from the SoundManager class to this new one. Then I simply added this class to all the gameobjects that needed local sounds, added the properties and wrote some lines of code to get this component from my other classes and play the sounds when needed.

As important as this system are, they would be meaningless without audio files to play, that's why I dedicated a whole day to just look for free sounds fx and music tracks to see which ones I could fit into this project. I found a lot of cool sounds in <a href="https://freesound.org/" target="_blank">Freesound</a> (a collaborative database of Creative Commons Licensed sounds), then I tested a bunch of them and selected the ones that I believed could sound better (audio files authors and links in the [credits](#credits) section), and after a little editing using a free and open source audio editor called <a href="https://www.audacityteam.org/" target="_blank">Audacity</a>, I ended up with the cool audio files that I'm using in this project.

---

### Camera <a name="camera"></a> ([ToC↑](#home))

In the first prototype the class `CameraBehaviour` had a simple follow action that made the camera move towards `target.position` using `Vector3.Lerp`. In the version 0.2, I added an offset float to stop the camera from centering the player and to shift the focus to the position where the mouse or the joystick are pointing at (crosshair), I like how this gives to the player more control over the camera.

I also edited the class `CameraShake` to stop it from using `Camera.main.transform` as the Transform to update. I'm now using the local position of the gameobject that has this class attached, I'm also storing a `defaultPosition` in the Awake method and using that position to reset the localPosition when the shake ends. I made this changes so I could make the camera a child of gameobject called `CameraPivot` that could shake and comeback to a default position without being affected by the movement of the camera.

---

### Crosshair <a name="crosshair"></a> ([ToC↑](#home))

Making this behaviour was a little tricky, at first I had a single class where I coded the intended behaviour, it sort of worked and it was hard to improve, so I started all over and took into account the SR Principle. The first thing I made was the base class `Crosshair` that has this basic code:
```c#
using UnityEngine;

public class Crosshair : MonoBehaviour
{
    [Tooltip("The gameobject that will be used to visually represent the crosshair.")]
    public GameObject crosshair;

    protected virtual void Awake()
    {
        if (crosshair == null)
        {
            Debug.LogError(gameObject.name + ": Missing crosshair!");
            Debug.Break();
        }
    }

    public virtual void UpdateCrosshair() { }

    /// <summary>
    /// To get or set the active state of the gameobject used to visually represent the crosshair.
    /// </summary>
    public bool IsActive
    {
        get { return _IsActive; }
        set { _IsActive = value; crosshair.SetActive(value); }
    }
    private bool _IsActive;
}
```
Then I used this as base class to create two derived classes which I called `CrosshairMouse` and `CrosshairJoystick`. The first one simply follows the Mouse World Position from my custom Input Manager called `TadaInput`. The second one is a little more trickier because the joystick input has a direction vector but it doesn't have any position in the screen, so I had to look for a way to use that vector to correctly update the position of the joystick crosshair. 

After a little thinking, what I did was use that vector direction to control the rotation of a gameobject and then I snapped it to the position of the player's main shoulder (via code), I also added a condition that stops the rotation if the player releases the stick. 

Here is the code:
```c#
  private Transform player;
  private Transform pointToFollow;
  private Quaternion targetRotation;
  private Vector3 upwardAxis;

  private bool isReady;

  private const float TURN_RATE = 16f;

  /// <summary>
  /// The position of the actual crosshair in world space.
  /// </summary>
  public static Vector3 CrosshairPosition
  {
      get { return _CrosshairPosition; }
      private set { _CrosshairPosition = value; }
  }
  private static Vector3 _CrosshairPosition;

  /// <summary>
  /// Vector that goes from player position to crosshair world position.
  /// </summary>
  public static Vector3 AimDirection
  {
      get { return _AimDirection; }
      private set { _AimDirection = value; }
  }
  private static Vector3 _AimDirection;

  protected override void Awake()
  {
      base.Awake();
      player = FindObjectOfType<PlayerController>().transform;
      pointToFollow = FindObjectOfType<PlayerShoulderMain>().transform;
  }

  public override void UpdateCrosshair()
  {
      base.UpdateCrosshair();

      if (!isReady)
      {
          if (pointToFollow == null)
              return;
          isReady = true;
      }

      // Multiply by AimAxis to create a vector that points towards that AimAxis Direction.
      upwardAxis = Quaternion.Euler(0, 0, 90) * TadaInput.AimAxisSmoothInput;
      targetRotation = Quaternion.LookRotation(forward: Vector3.forward, upwards: upwardAxis);

      // New Vector3 to zero out the Z axis.
      Vector3 positionToFollow = pointToFollow.position;
      positionToFollow.z = 0f;

      // Snap this gameobject position to the mainShoulder position.
      transform.position = positionToFollow;

      // Set crosshair world position.
      _CrosshairPosition = crosshair.transform.position;
      _CrosshairPosition.z = 0f;

      // Vector that goes from player to crosshair world position.
      _AimDirection = (_CrosshairPosition - player.position).normalized;
      _AimDirection.z = 0f;

      // Stop rotating if there is no AimAxis input.
      if (TadaInput.AimAxisRawInput.sqrMagnitude <= 0)
          return;

      // Smooth rotation.
      transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, TURN_RATE * Time.deltaTime);
  }
```

Once I had that behaviour, I added a child gameobject called Crosshair with the sprites to visually represent it and added a positive value on the X position axis to offset it from the player's position.

![](https://i.imgur.com/OsL9Jsb.gif)

And lastly I created the class `CrosshairHandler` which checks the bool `isMouseActive` from TadaInput to decide which crosshair should be enabled and updated. 

![](https://github.com/tadadosii/ImageStorage/blob/master/Isometric_Shooter_Study_v0.2_CrosshairHandler.gif)

`Note: The odd position of the mouse cursor vs crosshair is due to the screen capture process.`

---

### Debug <a name="debug"></a> ([ToC↑](#home))

When I get an error or I'm trying to write new code, I usually use the method `Debug.log` to get messages on the console which helps me get an idea of how everything is working. If I think that I could need those messages in the future, I also add a private bool called `debug` that I use to enable and disable the logs. I also use `Debug.LogWarning` or `Debug.LogError` to log messages when important components are missing, this is really important, to me is the best way to have everything under control and to quickly detect issues/bugs.

```c#
if (mouseCrosshair == null || joystickCrosshair == null)
{
    Debug.LogError(gameObject.name + ": mouse or/and joystick crosshair missing!");
    return;
}
```
Along with the logs I have two simple classes that I use to show information on the screen. The first one is `GizmosHandler` which I use to draw wireframe spheres for the mouse position and the joystick crosshair position. And the second one is `GUIHandler` that I use to write useful information on the screen.

![](https://i.imgur.com/ck4FeSD.png)

---

### Environment <a name="environment"></a> ([ToC↑](#home))

The current level has `9 interconnected rooms in a 3x3 arrangement`. The rooms are made out of wall prefabs. There are two types of walls, `Frontal` and `Side` and each of those walls has two child gameobjects with different sprites and colliders, one for when the player is on one side and one for when it's on the other side.

![](https://i.imgur.com/RSMXgq3.png)

I had to do it like this due to the isometric view of the game, I couldn't simply add a collider to stop the player from going through the wall, I had to add two different colliders and then create the class `EnvironmentObject` to enable those child gameobjects when needed. I also created the class `EnvironmentObjectTrigger` which can hold arrays of Environment Objects that get enabled/disabled when the player goes through a collider marked as `isTrigger`.

![](https://i.imgur.com/e4cS4Ec.gif)

With this system I was able to create a simple level with walls and doorways with triggers to dinamically set the walls types when the players goes through them. This is how the whole level looks like:

<img src="https://github.com/tadadosii/ImageStorage/blob/master/Isometric_Shooter_Study_v0.2_Level.gif" width="700" height="240"/>

When the time to position the assets came, I first had to painstakingly move them in relation to one another (this was a slow process that took quite some time). After I had a bunch of walls placed that I could use to add gameobjects that could act as `SnapPoints` (this points can be seen in the hierarchy image added above), I was able to speed the process up and I could just drop a prefab inside a point, reset its transform values and then remove it from the point and place it in its corresponding place in the hierarchy.

Painstakingly positioning process:

![](https://i.imgur.com/irJRtkW.gif)

Positioning with SnapPoints:

![](https://i.imgur.com/tYhBZHB.gif)

I think that a great choice to further speed this process could be creating a tool to quickly place the prefabs and I will definitely try it when I get back to this project.

Once I had everything setup, I started moving my player around the walls and I quickly realized that I needed to add rounded courners to my walls in order to improve my player's movement while hitting the colliders. I made it by using `Polygon Collider 2D` which allows the creation of custom colliders in any form we like, we can just drag points around and quickly get any desired shape.

`Tip: Use Polygon Collider 2D component to create custom colliders and add rounded corners to improve your player's movement while hitting those colliders.`

<img src="https://i.imgur.com/mpK64Xy.png" width="800"/>

The last detail that I had to add to the walls was an extra gameobject with a collider marked as isTrigger and also tagged as `Wall`, this due to the fact that since the version 0.1 the projectiles are using the collision method `OnTriggerEnter2D` and are also comparing the tag `Wall` to check if they hit one.

`Tip: Make sure to create prefabs of your assets from the beginning, if you ever need to make changes and already have a lot of assets placed everywhere, you will thank yourself when you simply change a prefab and all of your duplicated prefabs change as well.`

---

### FX - Dash Skill After Images <a name="afterimages"></a> ([ToC↑](#home))

![](https://i.imgur.com/vuzLA4X.gif)

I had this effect in mind since I started making this project, and after I had the dash skill ready, everything was set to start learning how to achieve it.

From playing lots of games and seeing some video tutorials with the after images effect, I got a clear view of how to do it, however, there was a little problem... all of the references I saw were using a single sprite for the whole character and my player had a bunch of sprites linked together to make the full body behaviour. 

At first I thought about painstakingly adding 3 after images sprites (with a different material for each one) to each part of the player's body, but that would take me like forever and if I needed to make a change it would take me forever². In order to avoid adding the sprites manually, what I ended up doing was a series of classes that would add them for me at runtime and group them as childs of gameobjects that would also be child of a main gameobject called `AfterImages`.

![](https://i.imgur.com/vCKaVFg.png)

To make this small system, I wrote 5 classes. The first class, that's directly attached to the player's sprites, is called `AfterImageGenerator`, and the process that starts on this class Awake method and goes all the way to the PlayerController, goes as follows:

0. The AfterImageGenerator first finds the second class `AfterImageMaterials` and stores it.
1. Then it creates a gameobject named `GameobjectName_AfterImage_Group` that comes with the third class `AfterImageGroup` attached to it.
2. Then this same class instantiates N amount of the fourth class `AfterImage`, it also sets properties (e.g. a material from the AfterImageMaterials class) for the instantiated classes and adds them to the AfterImage[] array inside the `AfterImageGroup` class.
3. When an `AfterImageGroup` is created, it immediatly finds the fifth class `AfterImageHandler` and adds itself to a list of `AfterImageGroup` inside the class.
4. And all this ends in having a simple method in the fifth class which I called `SetActiveAfterImages()`, and this method is called by the class `PlayerSkills` when the `Dash` skill is called by the `PlayerController`.

`Note: There are lots of comments on each part of the process to give you an idea of what's happening inside the code.`

### Pause Controller <a name="pausecontroller"></a> ([ToC↑](#home))

In the first version I was handling the pause action inside the `PlayerController`, that was not a good choice, but I did it like that because I simply wanted to stop everything to be able to take screenshots. 

Now I handle it with the class `PauseController` that has a public static bool called `isGamePaused` which I set active by checking if the key `Pause` from `TadaInput` was pressed. This class also has two public `UnityEvent` to quickly set actions in the Inspector tab (e.g. adding an events to enable/disable a paused menu).

This is the whole code:

```c#
using UnityEngine;
using UnityEngine.Events;

public class PauseController : MonoBehaviour
{
    public UnityEvent onGamePause;
    public UnityEvent onGameResume;
    public static bool isGamePaused;

    private void Update()
    {
        if (TadaInput.GetKeyDown(TadaInput.ThisKey.Pause))
        {
            if (!isGamePaused)
            {
                isGamePaused = true;
                OnGamePause();
                return;
            }
            isGamePaused = false;
            OnGameResume();
        }
    }

    private void OnGamePause()
    {
        Time.timeScale = 0;

        // Pause audio
        AudioListener.pause = true;

        // If there is at least one event added in the Inspector tab, Invoke it.
        if (onGamePause != null)
            onGamePause.Invoke();
    }

    private void OnGameResume()
    {
        Time.timeScale = 1;

        // Resume audio
        AudioListener.pause = false;
        
        // If there is at least one event added in the Inspector tab, Invoke it.
        if (onGameResume != null)
            onGameResume.Invoke();
    }
}
```
---

### TadaInput (Custom Input Manager) <a name="tadainput"></a> ([ToC↑](#home))

Since I started making games I always wanted to create my own Input Manager that could be used in the same way as Unity's `Input` class (e.g. Input.GetkeyDown(Keycode.AnyKey)) and I think that I managed to create a system that works great for what I needed in this version.

The basic idea of this class is quite simple, first I have an enum with keys `ThisKey`, then I have methods to store the inputs from Unity's `Input` as `ThisKey` in three different arrays (one for Key, one for KeyDown and one for KeyUp) and then I use three methods (GetKey, GetKeyDown and GetKeyUp) to compare a given `ThisKey` against those arrays and at the end they return true or false. The actual implementation of the idea took a while, I had a bunch of bugs at the beggining, but after some trials and editing I ended up with a behaviour that works just like I wanted it to.

Now I'll talk about the actual code:

- This is how the basic properties are:

```C#
  public enum ThisKey
  { 
      None, MoveLeft, MoveRight, MoveUp, MoveDown, PrimaryAction, SecondaryAction,
      PreviousWeapon, NextWeapon, PreviousUseRate, NextUseRate, Xbox360RightTrigger,
      Xbox360LeftTrigger, MouseAnyMovement, Dash, Pause, Count
  }
  private static ThisKey[] currentKeys;
  private static ThisKey[] currentKeysDown;
  private static ThisKey[] currentKeysUp;
  private static bool[] currentAxisDown;
```
> The values in `ThisKey` enum can be on any order, the system will work without any issue.

- The first thing that's called on Awake() is the method `InitializeInputArrays()` which uses `ThisKey.Count + 1` as the length of the arrays and adds a `None` key to each of the elements of the arrays.

```c#
  private static void InitializeInputArrays()
  {
      int length = (int)ThisKey.Count + 1;
      currentKeys = new ThisKey[length];
      currentKeysDown = new ThisKey[length];
      currentKeysUp = new ThisKey[length];
      currentAxisDown = new bool[length];

      for (int i = 0; i < length; i++)
      {
          currentKeys[i] = ThisKey.None;
          currentKeysDown[i] = ThisKey.None;
          currentKeysUp[i] = ThisKey.None;
          currentAxisDown[i] = false;
      }
  }
```
- Next we have the three methods to store the keys:

```c#
  private static void StoreCurrentKey(ThisKey key)
  {
      currentKeys[(int)key] = key;
  }

  private static void StoreCurrentKeyDown(ThisKey key)
  {
      currentKeysDown[(int)key] = key;
  }

  private static void StoreCurrentKeyUp(ThisKey key)
  {
      currentKeysUp[(int)key] = key;
      currentKeysDown[(int)key] = ThisKey.None;
      currentKeys[(int)key] = ThisKey.None;
  }
```
> Notices how I use (int)key to convert the enum ThisKey key into an int and use that as the Index, this is how I make this system work without worrying about the enum values order, each one of the values have their own reserved space inside the arrays.

Sample of how a sigle key is stored:

```c#
  // Down
  if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Joystick1Button5))
      StoreCurrentKeyDown(ThisKey.Dash);
  // Up
  if (Input.GetKeyUp(KeyCode.Space) || Input.GetKeyUp(KeyCode.Joystick1Button5))
  StoreCurrentKeyUp(ThisKey.Dash);
```

- Next we have a method to store axis inputs as simple keys. This is something that is missing from Unity's `Input` system and I needed it to use my xbox controller trigger as a simple button (This one was tricky to make, but I eventually got it working).

```c#
  private static void StoreCurrentAxisAsKeyType(ThisKey key, float rawAxisValue)
  {
      int index = (int)key;
      if (rawAxisValue > 0) 
      {
          if (!currentAxisDown[index]) // DOWN
          {
              currentAxisDown[index] = true;
              StoreCurrentKeyDown(key);
          }
          StoreCurrentKey(key); // HOLD
      }
      if (rawAxisValue == 0 && currentAxisDown[index]) // UP
      {
          currentAxisDown[index] = false;
          StoreCurrentKeyUp(key);
      }
  }
```
- Next we have three methods that works just like Unity's `Input` and can be used from any other class to actually check if the keys are being used.

**GetKey**
```c#
  public static bool GetKey(ThisKey key)
  {
      int index = (int)key;

      if (currentKeys[index] == key)
      {
          if (debug)
              Debug.Log("Key: " + key.ToString());
          return true;
      }
      return false;
  }
```
**GetKeyDown**
```c#
  public static bool GetKeyDown(ThisKey key)
  {
      int index = (int)key;
      if (currentKeysDown[index] == key)
      {
          currentKeysDown[index] = ThisKey.None;
          if (debug)
              Debug.Log("KeyDown: " + key.ToString());
          return true;
      }
      return false;
  }
```
**GetKeyUp**
```c#
  public static bool GetKeyUp(ThisKey key)
  {
      int index = (int)key;
      if (currentKeysUp[index] == key)
      {
          currentKeysUp[index] = ThisKey.None;
          if (debug)
              Debug.Log("KeyUp: " + key.ToString());
          return true;
      }
      return false;
  }
```
Sample of how this methods are used by other classes:

```c#
  if (TadaInput.GetKey(TadaInput.ThisKey.PrimaryAction))
      _WeaponHandler.UseWeapon(WeaponHandler.ActionType.Primary);
```
- Apart from all the methods mentioned above, `TadaInput` also stores inputs like mouse pixel position, mouse world position and some other properties.

`Note: Right now this class works just great for what I had intended and it was a great learning experiment, but in the future I will need to consider and implement a lot of things like remapping keys at runtime or supporting other types of controllers.`

---

### Physics <a name="physics"></a> ([ToC↑](#home))

To control Unity's 2D physics I made the class `PhysicsHandler`. This class stores a `Rigidbody2D` on `Awake()` to use it on public methods for handling velocity and force, it also stores a `Collider2D` to enable/disable it on a public method. The idea of this class is to use it as base class to create derived classes that inherit the base methods, and then add more behaviours using those inherited methods.

So far I've only used this class to create the class `PlayerPhysics` which handles the movement of the player by using the method `SetVelocity`.

Here is the whole code:
```c#

using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Physics2DHandler : MonoBehaviour
{
    protected Rigidbody2D _RigidBody;
    protected Collider2D _Collider;

    protected virtual void Awake()
    {
        TryGetComponent(out _RigidBody);
        TryGetComponent(out _Collider);
    }

    public void SetActiveCollider(bool value)
    {
        if (_Collider != null)
            _Collider.enabled = value;
    }

    public void SetRigidbodyType(RigidbodyType2D type)
    {
        if (_RigidBody != null)
            _RigidBody.bodyType = type;
    }

    /// <summary>
    /// Rigidbody2D velocity.
    /// </summary>
    public Vector2 Velocity { get { return _RigidBody.velocity; } private set { _RigidBody.velocity = value; } } 

    public virtual void SetVelocity(Vector2 newVelocity)
    {
        if (_RigidBody != null)
            Velocity = newVelocity;
    }

    public virtual void SetVelocity(Vector2 input, float speed)
    {
        if (_RigidBody != null)
            Velocity = input * speed;
    }

    public virtual void AddVelocity(Vector2 value)
    {
        if (_RigidBody != null)
            Velocity += value;
    }

    public virtual void AddVelocity(Vector2 direction, float value)
    {
        if (_RigidBody != null)
            Velocity += new Vector2 (direction.x + value, direction.y + value);
    }

    public virtual void AddForce(Vector2 direction, float force, ForceMode2D mode)
    {
        if (_RigidBody != null)
            _RigidBody.AddForce(direction * force, mode);
    }
}
```
---

### Player <a name="player"></a> ([ToC↑](#home))

In the version 0.1 the `PlayerController` had a lot of responsabilities that shouldn't have had, it was handling animations, flipping the body, updating rotations, handling the charging behaviour of the gun, and lots of other things. I made it like that because my objective at the time was to build a prototype as quick as possible and not actually caring much about separating behaviours. 

Now, in this version 0.2, I made 9 classes that act together to create the player's behaviour. I really like how they turned out and I'll now try to make a basic description of what each one does:

- **PlayerAnimations:** Inherits from the class `AnimationHandler`, it has the method `SetAnimationSpeed(AnimName name, float value)` and the method `PlayMoveAnimationsByMoveInputAndLookDirection(Vector3 moveInput)` which is the one responsible for correctly playing the walking animation depending on the moving direction and the position of the crosshair in relation to the player's position.

![](https://i.imgur.com/LraQHaF.gif)

- **PlayerBodyPartsHandler:** This class controls the behaviour of the player's body based on the position of the crosshair in relation to the player's position. Basically what it does is flip the x axis scale of the hips and the upperbody to make them point at the correct side and it also dynamically changes the layer order of the head and the non-trigger hand.

![](https://i.imgur.com/WhnevGy.gif)

- **PlayerHandTargetToLookAt:** This class uses the `AimDirection` of the `Crosshair` to move the local position of a gameobject that's used as a look at target by the non-trigger hand.

<img src="https://i.imgur.com/uGrCOvS.gif)" width="466"/>

- **PlayerShoulderMain:** This class only exists to act as a realiable way to let other classes find it (e.g. CrosshairJoystick). The actual behaviour of the main shoulder is handled by the rotation utility `LookAt2Dv2` which makes it point its x axis towards the crosshair position.

<img src="https://i.imgur.com/xVIw7CI.gif)" width="466"/>
> Debug line that goes from the main shoulder to the crosshair position.

- **PlayerShoulderSecondary:** This class uses the `Crosshair AimDirection` and other properties like rate, min offset angle and max offset angle to determine an offset angle that will be added to the secondary shoulder as an extra rotation. This is an important part of the behaviour that controls the rotation of the shoulders, with the correct values both shoulders match their rotation in relation to the weapon that's being held by the character.

- **PlayerMaterials:** This class will be used to control all the behaviours related to the player's materials. At the moment is used to control the property `Color Intensity` from the custom shader `Unlit_Sprite_HDR_ColorIntensity` which I created using `Shader Graph` to make the player glow for a short duration of time (used when dashing).

![](https://i.imgur.com/9sSdV0i.gif)
> In this capture the dash is disabled to showcase the highlight without any movement.

- **PlayerPhysics:** This class is a derived class of `PhysicsHandler`. At the moment it does three functions, the first one is to use the base method `SetVelocity (vector2 direction, float speed)` to move the player to the direction `TadaInput.MoveAxisRawInput` by a float called `_MoveSpeed` and the second is using the method `OnTriggerEnter2D` to find collisions when enterring colliders marked as `isTrigger` and to compare their tags to check if they are named `EnvironmentTrigger` (with the purpose of setting active the environment objects if a `EnvironmentObjectTrigger` is found). The third function is providing the `PhysicsHandler` methods to create skills inside the class called `PlayerSkills`.

- **PlayerSkills:** I use this class along with the classes `PlayerPhysics`, `PlayerMaterials`, `AfterImageHandler` and `SoundHandlerGlobal`, and also an array of `TrailRenderer`, to create the `Dash` skill. 

The way it works is really simple and I'll just add the code here with comments so you can have an idea of how it works:

```c#
using System.Collections;
using UnityEngine;

public class PlayerSkills : MonoBehaviour
{
    public TrailRenderer[] dashTrails;
    [SerializeField] private float dashForce = 5f;
    public SoundHandlerGlobal dashSFXHandler;

    private PlayerPhysics _PlayerPhysics;
    private PlayerMaterials _PlayerMaterials;
    private AfterImageHandler _AfterImageHandler;

    private bool canDash;

    private const float DASH_DURATION = 0.2f;

    private void Awake()
    {
        _AfterImageHandler = FindObjectOfType<AfterImageHandler>();
        TryGetComponent(out _PlayerPhysics);
        TryGetComponent(out _PlayerMaterials);

        // We can dash from the start, this should be handled by other behaviour that grants the player
        // the ability to dash after completing a task. 
        canDash = true;

        // Starts with disabled trails.
        SetActiveTrails(false);
    }

    public void Dash()
    {
        if (canDash)
            StartCoroutine(CO_Dash());
    }

    // Used a coroutine to have a WaitForSeconds method to set canDash to true after a given time.
    private IEnumerator CO_Dash()
    {
        canDash = false;

        // Enable trails.
        SetActiveTrails(true);

        _AfterImageHandler.SetActiveAfterImages();

        // SetVelocity is being used, if we don't stop it for the duration of the dash,
        // AddForce won't have any effect because the velocity will always be set to whatever
        // the TadaInput.MoveAxisRawInput * _MoveSpeed calculation value is.
        _PlayerPhysics.CanMove = false;

        ActualDash();
        yield return new WaitForSeconds(DASH_DURATION);

        // Disable trails.
        SetActiveTrails(false);

        // We can set the velocity again to be handler by the player's movement.
        _PlayerPhysics.CanMove = true;

        // We can dash again. This could be after another WaitForSeconds to add a little delay after a dash.
        canDash = true;
    }

    public void ActualDash()
    {
        // Play SFX
        if (dashSFXHandler != null)
            dashSFXHandler.PlaySound();

        // Activate body highlight effect
        _PlayerMaterials.SetActiveHighlightBody(DASH_DURATION, intensity: 1.25f);

        // Zero out rigidbody velocity to have a consistent dash
        _PlayerPhysics.SetVelocity(Vector2.zero);

        // AddForce towards move direction
        _PlayerPhysics.AddForce(TadaInput.MoveAxisRawInput.normalized, dashForce, ForceMode2D.Impulse);
    }

    private void SetActiveTrails(bool value)
    {
        for (int i = 0; i < dashTrails.Length; i++)
        {
            if (dashTrails != null)
            {
                dashTrails[i].emitting = value;
            }
        }
    }
}
```

- **PlayerController:** This class checks for the player's inputs using `TadaInput` and calls methods from others classes based on those inputs. 

```c#
  // This is how the skill Dash is called.
  if (TadaInput.GetKeyDown(TadaInput.ThisKey.Dash) && _PlayerPhysics.Velocity.sqrMagnitude > 0)
      _PlayerSkills.Dash();
```
---

### Utilities <a name="utilities"></a> ([ToC↑](#home))

A utility class is one that has one or many functions that are totally independent and can be reused as is on any other project (e.g. UnityEngine.Mathf which has lots of methods that we use as is on our projects).

In this version (0.2) I've four utility classes, one that handles arrays and three to handle rotations. The first I called it `ArraysHandler` and at the moment I use it to find the next or previous index of any array.

Here is the code:
```c#
/// <summary>
/// Contains methods to handle arrays.
/// </summary>
public static class ArraysHandler
{
    public static int GetNextIndex (int currentIndex, int arrayLength)
    {
        return (currentIndex + 1) % arrayLength;
    }

    public static int GetPreviousIndex(int currentIndex, int arrayLength)
    {
        return ((currentIndex - 1) + arrayLength) % arrayLength;
    }
}
```
And from the three that I use for rotations, the most important one to me is the one I called `LookAt2Dv2`, which is an improved version of the one I had on my v0.1 study and the main reason why I'm now able to use a single set or arms and no duplicated weapons.

**I'll add the code here if you simply need a great 2D LookAt class and you are feeling lazy to get it from the repo.** The only thing that is worth noting from this class is that it's not entirely a utility class that you can use as is, it will try to look for `TadaInput.MouseWorldPos` if the use of a mouse as a target is selected, but that's something that can be edited quite easily (I'll make sure to convert this class into a full independent utility later on).
```c#
using UnityEngine;

/// <summary>
/// New v2. The gameobject that has this component attached will instantly rotate to make its x or y axis look 
/// towards the assigned target or towards mouse world position if a exposed enum is selected. The direction can be
/// inverted by checking isFlipAxis. Also there is an option to disable local update if a linked control is 
/// needed. It can also use a smooth rotation by enabling isSmoothRotationEnable.
/// </summary>
public class LookAt2Dv2 : MonoBehaviour
{
    // --------------------------------------
    // ----- 2D Isometric Shooter Study -----
    // ----------- by Tadadosi --------------
    // --------------------------------------
    // ---- Support my work by following ----
    // ---- https://twitter.com/tadadosi ----
    // --------------------------------------

    [TextArea(4, 10)]
    public string notes = "New v2. The gameobject that has this component attached will instantly rotate to make its x or y axis look " +
        "towards the assigned target or towards mouse world position if a exposed enum is selected. The direction can be inverted by " +
        "checking isFlipAxis. Also there is an option to disable local update if a linked control is needed. It can also use a " +
        "smooth rotation by enabling isSmoothRotationEnable.";

    // TargetTransform: Look at the gameobject Transform from the public variable targetTransform.
    // MouseWorldPosition: Look at the mouse world position stored by the TadaInput class.
    public enum LookAtTarget { TargetTransform, MouseWorldPosition }
    [SerializeField] private LookAtTarget lookAtTarget = LookAtTarget.TargetTransform;

    [Tooltip("If you are using a Transform, select TargetTransform from lookAtTarget dropdown list.")]
    public Transform targetTransform;

    private enum Axis { X, Y }
    [SerializeField] private Axis axis = Axis.Y;

    [Tooltip("Used when isSmoothRotationEnable is true.")]
    [SerializeField] private float turnRate = 10f;

    [Tooltip("Use to set an initial offset angle or use SetOffsetAngle method to do it via code.")]
    [SerializeField] private float offsetLookAtAngle = 0f;

    [Tooltip("e.g. writing 30 will make the axis have a range of -30 to 30 degrees.")]
    [SerializeField] private float maxAngle = 360f;

    [Tooltip("Check to let this behaviour be run by the local Update() method and Uncheck if you want to call it from any other class by using UpdateLookAt().")]
    [SerializeField] private bool isUpdateCalledLocally = false;

    [Tooltip("Check to smoothly rotate towards target rotation using turnRate as variable.")]
    public bool isSmoothRotationEnable = false;

    [Tooltip("Check to flip the axis and use the negative side to look at")]
    public bool isFlipAxis = false;

    [Header("Debug")]
    [SerializeField] private Color debugColor = Color.green;
    [SerializeField] private bool debug = false;

    private Vector3 targetPosition;
    private Vector3 direction;
    private Vector3 upwardAxis; 

    private void Update()
    {
        if (!isUpdateCalledLocally)
            return;
        UpdateLookAt();
    }

    public void UpdateLookAt()
    {
        Vector3 myPosition = transform.position;

        if (lookAtTarget == LookAtTarget.MouseWorldPosition)
            targetPosition = TadaInput.MouseWorldPos;
        else if ((lookAtTarget == LookAtTarget.TargetTransform))
        {
            if (targetTransform == null)
            {
                Debug.LogError(gameObject.name + " target missing!");
                return;
            }
            targetPosition = targetTransform.position;
        }

        // Ensure there is no 3D rotation by aligning Z position
        targetPosition.z = myPosition.z;

        // Vector from this object towards the target position
        direction = (targetPosition - myPosition).normalized;

        switch (axis)
        {
            case Axis.X:

                if (!isFlipAxis)
                {
                    // Rotate direction by 90 degrees around the Z axis
                    upwardAxis = Quaternion.Euler(0, 0, 90 + offsetLookAtAngle) * direction;
                }
                else
                {
                    // Rotate direction by -90 degrees around the Z axis
                    upwardAxis = Quaternion.Euler(0, 0, -90 + offsetLookAtAngle) * direction;
                }
                break;

            case Axis.Y:

                if (!isFlipAxis)
                    upwardAxis = direction;
                else
                    upwardAxis = -direction;
                break;

            default:
                break;
        }

        // Get the rotation that points the Z axis forward, and the X or Y axis 90° away from the target
        // (resulting in the Y or X axis facing the target).
        Quaternion targetRotation = Quaternion.LookRotation(forward: Vector3.forward, upwards: upwardAxis);

        if (debug)
            Debug.DrawLine(transform.position, targetPosition, debugColor);

        if (!isSmoothRotationEnable)
        {
            // Update the rotation if it's inside the maxAngle limits.
            if (Quaternion.Angle(Quaternion.identity, targetRotation) < maxAngle)
                transform.rotation = targetRotation;
            return;
        }

        // Smooth rotation.
        Quaternion rotation = Quaternion.Lerp(transform.rotation, targetRotation, turnRate * Time.deltaTime);

        // Update the rotation if it's inside the maxAngle limits.
        if (Quaternion.Angle(Quaternion.identity, rotation) < maxAngle)
            transform.rotation = rotation;
    }

    public void SwitchToTarget(LookAtTarget target)
    {
        lookAtTarget = target;
    }

    public void SetOffsetAngle(float value)
    {
        offsetLookAtAngle = value;
    }
}
```
---

### Weapons <a name="weapons"></a> ([ToC↑](#home))

The weapon system has two main classes, the first one is called `Weapon` and it has four virtual methods that can be overriden in a new derived class to create any kind of weapons. It also has a bool called `canUse` which is used along with a float called `_UseRate` to control the speed in which the player can call an action on the current weapon. Having a base class like this one is great, it lets you have N amount of derived weapons that can be used by simply storing the class `Weapon` in a property and calling its base methods.

In order to call the base methods and handle N number of derived weapons, I made the second class called `WeaponHandler`. The way it works is that it has a `Weapon[]` array which is used throughout the class to do actions like `SwitchWeapon`, `SwitchUseRate` and `UseWeapon`.

> I believe that these two classes turned out great and can be reused on other projects with just a little editing or by also importing the file `ArraysHandler` because some of the methods need that utility to work.

To make the two weapons that are currently on this project, I created the derived class called `Weapon_ShootProjectileCanCharge` which has a primary action to shoot projectiles and a secondary action with a timer that after reaching its duration shoots a secondary projectile. This is what this weapon basically does:

1. When it gets enabled it spawns two projectiles (it uses instantiate) which get stored on two variables (primaryProjectile and secondaryProjectile) and also get disabled as soon as they are created.
2. If the player presses the primary action button the current weapon `PrimaryAction` method is called and does the following:

```c#
public override void PrimaryAction(bool value)
{
  base.PrimaryAction(value);

  // Can be executed only if there is a projectile available and canUse is true.
  if (primaryProjectile != null && canUse)
  {
      // Play the basic animation if WeaponAnim_ShootProjectileCanCharge is available.
      if (anim != null)
          anim.PlayAnimation(WeaponAnim_ShootProjectileCanCharge.Animation.BasicShot);

      // Make the camera Shake.
      CameraShake.Shake(duration: 0.075f, shakeAmount: 0.1f, decreaseFactor: 3f);

      // Enable the primary projectile.
      primaryProjectile.SetActive(true);

      // Call the method Fire on the primary projectile to launch it towards the crosshair direction.
      primaryProjectile.Fire();

      // We make it null to give room to a new instantiated projectile.
      primaryProjectile = null;

      // We make it false to execute the base Update actions which makes it true again after UseRate duration is reached,
      // which then calls the method OnCanUse() that's used to spawn new projectiles and to call to return to the Idle anim.
      canUse = false;
  }
}
```
<br><br/>
3. If the player presses the secondary action button the current weapon `SecondaryAction` method is called and does the following:

```c#
public override void SecondaryAction(bool value)
{
    base.SecondaryAction(value);

    // The purpose of this action is to let the player hold the secondary action button to make the bool
    // isReceivingInput true, which in turn enables a timer and a series of actions to ultimately launch the
    // secondary projectile.

    // After firing the projectile, canUse is set to false and because the player can continuously call this method,
    // we use this to stop isReceivingInput from getting a true value.
    if (!canUse)
    {
        // Cancel inputs after use.
        isReceivingInput = false;
        return;
    } 

    // We stop the code here if one of the needed variables is missing.
    if ((secondaryProjectile == null || chargingPFX == null || chargingSFX == null))
    {
        Debug.LogWarning(gameObject.name + ": missing prefabs!");
        return;
    }

    // We make it true if the player is pressing the secondary action button or false if not.
    // When it's true, it activates the actions on the Update method of this class.
    isReceivingInput = value;
}
```
<br><br/>
   3.1. When `isReceivingInput` is `true` a timer is enabled and a sequence of actions are called if certain conditions are met.

```c#
if (isReceivingInput)
{
    // Execute the initial actions that take place in the first frame after isReceivingInput is set to true.
    OnChargingStart();

    // Timer: Increase the value of chargingTime by adding Time.deltaTime on each frame.
    chargingTime += Time.deltaTime;

    // Update OnCharging actions and pass chargingTime as argument.
    OnCharging(chargingTime);

    // If charging time is equal or greater than the constant charge duration, execute the last actions.
    if (chargingTime >= CHARGE_DURATION)
        OnChargingEnd();
}
```
<br><br/>
   3.2. The first action that gets called is `OnChargingStart` and it's used to do the following actions:

```c#
private void OnChargingStart()
{
    // This actions can only be executed if isCharging is false.
    if (!isCharging)
    {
        // Play the charging animation if WeaponAnim_ShootProjectileCanCharge is available.
        if (anim != null)
            anim.PlayAnimation(WeaponAnim_ShootProjectileCanCharge.Animation.Charging);
            
        // We set it to true to avoid calling this method more than once.
        isCharging = true;

        // Make the camera Shake.
        CameraShake.Shake(duration: CHARGE_DURATION, shakeAmount: 0.065f, decreaseFactor: 1f);

        // Enable the charging visual effects.
        chargingPFX.SetActive(true);

        // Play the first sound of SoundHandlerLocal.
        chargingSFX.PlaySound();
    }
}
```

   3.3. Then the `OnCharging` action which is simply scaling the visual effects is called and updated each frame until `chargingTime` reaches `CHARGE_DURATION`.

```c#
private void OnCharging(float t)
{
    // Increase the size of the charging fx to enhance it with a feeling a anticipation.
    chargingPFX.transform.localScale = Vector2.one * t;
}
```
<br><br/>
   3.4. If `chargingTime` reaches `CHARGE_DURATION`, the last method `OnChargingEnd` is called and the secondary projectile is fired. These are the actions that take place when this method is called:

```c#
private void OnChargingEnd()
{
    // Play the charged shot animation if WeaponAnim_ShootProjectileCanCharge is available.
    if (anim != null)
        anim.PlayAnimation(WeaponAnim_ShootProjectileCanCharge.Animation.ChargedShot);

    // Set it to false to allow OnChargingStart to be called again.
    isCharging = false;

    // Set it to false to stop the player from making isReceivingInput true if it's holding the secondary action button.
    canUse = false;

    // Reset the timer to allow correctly restarting the charging action.
    chargingTime = 0.0f;

    // Make the camera Shake by a greater value.
    CameraShake.Shake(duration: 0.2f, shakeAmount: 1f, decreaseFactor: 3f);

    // Enable the projectile.
    secondaryProjectile.SetActive(true);

    // Call the method Fire on the projectile to launch it towards the crosshair direction.
    secondaryProjectile.Fire();

    // Make it null to give room to a new instantiated projectile.
    secondaryProjectile = null;

    // Reset the scale of the charging visual fx.
    chargingPFX.transform.localScale = Vector2.one;

    // Disable the charge visual fx.
    chargingPFX.SetActive(false);
}
```
<br><br/>
   3.5. Lastly if the secondary action is charging and the player releases the corresponding button, the method `OnChargeCancel` gets called and does the following actions to reset the weapon, allowing the player to use the charging action again right from the start:

```c#
private void OnChargeCancel()
{
    // Return to Idle animation if WeaponAnim_ShootProjectileCanCharge is available.
    if (anim != null)
        anim.PlayAnimation(WeaponAnim_ShootProjectileCanCharge.Animation.Idle);

    // Is set to false to allow OnChargingStart to be called again.
    isCharging = false;

    // Is set to zero to reset the timer so that it can correctly count the time again.
    chargingTime = 0.0f;

    // Stop the camera from shaking.
    CameraShake.Shake(0f, 0f, 0f);

    // Stop the charging SoundHandlerLocal sounds.
    chargingSFX.StopSound();

    // Reset the scale of the charging visual fx.
    chargingPFX.transform.localScale = Vector2.one;

    // Disable the charging visual fx.
    chargingPFX.SetActive(false);
}
```
> It's important to note that using Instantiate() to shoot projectiles is a great choice for rapid prototyping and testing, but its a bad move when it comes to actually creating a good system, constantly creating new GameObjects and destroying them would do no good to the garbage collection system. A good choice would be creating a pool system that has pre-cloned prefabs which can be used without destroying them, they just get enabled when called and disabled when they are "destroyed". I had my focus on all other parts of this project that I wanted to improve, so at the I didn't have enough time to implement a method like a pooling system.

> Also it's worth noting that the charging action can be reused right after OnChargingEnd is called, which I think is not the best desirable behaviour, in the future I'll introduce a short delay after use.

---

### Projectiles <a name="projectiles"></a> ([ToC↑](#home))

This class is really simple, it has three custom methods. `SetActive(bool value)` to enable/disable the `SpriteRenderer` and the `BoxCollider2D` of the projectile. `Fire()` to launch the projectile towards a Vector3 called `travelDirection` which also detaches it from its parent (weapon) and calls Destroy on itself with a specified delay time. And lastly the method `Travel()` to actually move the projectile towards `travelDirection` multiplied by a `Speed` variable.

The projectile also uses the method `OnTriggerEnter2D (Collider2D collision)` to check if there was an impact, and if one is found, the projectile gets destroyed after instantiating a visual effect.

> The projectiles get destroyed because I'm using instantiate to spawn them, if the pooling system is made, this should be changed to simply hide the projectiles and also reset them after a certain amount of time traveling or after an impact happens.

### Shaders <a name="shaders"></a> ([ToC↑](#home))

There are five simple shaders made with Shader Graph, the most elaborated one is the one used for the particle effects. At first I thought I could use a simple shader like this one to add to a particle system:

![]("https://i.imgur.com/eRfhxIr.png")

It works great for simple sprites, but for a particle system it doesn't. After searching for a while, I found that **particles uses the vector color of the object to actually change its color**, so without adding that part to the shader, I wouldn't be able to change the color by using the system options. After a little adjustments, this is the shader that I'm currently using for the particles:

![]("https://i.imgur.com/Ot3wpNW.png")

It's basically the same, but now it adds the vertex color in the calculation and it works great with the particle systems, it also has `HDR Color` which I use to increase its intensity and make the particles glow by using the post processing fx `Bloom`.

---

That's it for this version of the project. I hope you find it useful and fun to mess around with.

---

### Support <a name="support"></a> ([ToC↑](#home))
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
  
**If you would like to support my work, consider doing any of the following:**

<p/>

<p align="center">
Click the image to go to itch.io and try my new game demo.
<p/>

<p align="center">
<a href="https://twitter.com/tadadosi?ref_src=twsrc%5Etfw"><img src="https://i.imgur.com/Y9xKLSj.gif" title="Try HairLab 2D Free Demo" alt="Try HairLab 2D Free Demo">
<p/>
  
<p align="center">
(if you like it, please remember to rate it, it's a big help).
<p/>
  
<p align="center"> 
<a href="https://twitter.com/tadadosi?ref_src=twsrc%5Etfw"><img width="150" height="33" src="https://i.imgur.com/upEtqCa.png" title="Follow me on Twitter" alt="Follow me on Twitter @tadadosi"></a>
<p/>

<p align="center"> 
<a href="https://www.reddit.com/user/tadadosi"><img width="150" height="33" src="https://i.imgur.com/PrMqwGF.png" title="Follow me on Reddit" alt="Follow me on Reddit u/tadadosi"></a>
  
---

### Many thanks to
- My awesome wife that encourages me every day.
- Unity for existing and being free!
- All the people who has given me great feedback and motivation.
- All the artist / programmers who are constantly making free knowlegde available.
- And anyone who's reading this :)
- P.S.: **Extra thanks if you follow my twitter account and play my game :D!**

---

### Credits <a name="credits"></a> ([ToC↑](#home))

#### Scripts
- <a href="https://gist.github.com/ftvs/5822103" target="_blank">CameraShake</a> by ftvs on Github
- <a href="https://github.com/UnityCommunity/UnitySingleton" target="_blank">Singleton pattern</a> MIT Licence @ Unity Community

#### Sound FX (<a href="https://freesound.org" target="_blank">Freesound.org</a>)
- Short Laser Shots by  <a href="https://freesound.org/people/Emanuele_Correani/sounds/260155/" target="_blank">Emanuele_Correani</a> - CC-BY-3.0 
- Sci-Fi Force Field Impact 15 by  <a href="https://freesound.org/people/StormwaveAudio/sounds/330629/" target="_blank">StormwaveAudio</a> - CC-BY-3.0 
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