
// TODO: See if the animation part of the derived weapons can be put into the Weapon base class.

// NOTE: When adding a bool, use "is" or "has" as a prefix.

// NOTE: When using editor extension you might need to add [CanEditMultipleObjects] to be able to edit more than one gameobjects with a custom editor extension.

/*
 * - v0.2 WorkTime: 2h 06/05 + 5h 06/06 + 10h 06/07 + 5h 06/08 + 8h 06/09 +7h 06/10 + 5h30m 06/11 +1h 06/13 +5h 06/14 +5h 06/15
 * +6h30m 06/16 + 1h30m 06/19 +3h30m 06/20 +5h 06/21
 * 
 * -> v0.2 TotalWorkTime: 37h.

 * - Change transform.Find to FindObjectOfType<T>()

 * - Check all the naming of my methods to see if I can improve them.

 * - Look for the best option to control pause for all the classes.

 * - Use enums instead of -1 and 1.

 * - Change localUpdate to isUpdateHandledLocally.

 * - Explain what this definitions mean: isUpdateHandledLocally, isLookAtMousePosition, isFlipAxis.

 * - Change LookAt2Dv2 isLookAtMousePosition and isLookAtTarget to enums and make methods.

 * - Talk about the option to have an AudioSource and SoundHandler attached to each object that will emit sfx.

 * - Make my public fields private and use [SerializeField] to expose them.

 * - Talk about the amount of comments in my scripts and how it would be a great idea to always try to make the functions self explanatory,
     maybe link to good source that goes deeper into that. Use the comments from reddit to highlight the basic idea of commenting.

 * - Refactor PlayerController Update into pieces for better readability.

 * - Talk about how I was using Editor extensions to add a label for a quick undestanding of the use of the Component, but it
     was hard to mantain and update, and how I found a better solution that allowed me to get rid of all the editor classes.

 * - Add animations to the class Weapon_ShootProjectileCharge.

 * - Created an InputManager called TadaInput.

 * - Talk about the SoundManager on the documentation and delete the big note from the class.

 * - Replace the generic Singleton on the documentation credits.

 * - Proyectiles now automatically set their travelDirection based on the current direction of the body.

 * - Removed BodyPartsOrder class that I mistakenly placed on the hands.

 * - Changed FireRate to UseRate and made it a Weapon only method, it's now handled by a switch that lets you change rates.

 * - Completelly reworked Weapon class to make it a base class and created new weapon classes.
 */

