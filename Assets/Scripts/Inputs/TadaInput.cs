using UnityEngine;

/// <summary>
/// This class have public static input variables and methods that can be easily accessed by any other class.
/// </summary>
public class TadaInput : MonoBehaviour
{
    // --------------------------------------
    // ----- 2D Isometric Shooter Study -----
    // ----------- by Tadadosi --------------
    // --------------------------------------
    // ---- Support my work by following ----
    // ---- https://twitter.com/tadadosi ----
    // --------------------------------------

    [TextArea(2, 10)]
    public string notes = "This class have public static input variables and methods that can be easily accessed by any other class.";

    private readonly static bool debug = false;

    #region ------------------------------------------ PROPERTIES

    #region ------------------------------------------ MOUSE PROPERTIES

    public static bool IsMouseActive { get { return _isMouseActive; } private set { _isMouseActive = value; } }
    private static bool _isMouseActive;

    /// <summary>
    /// Mouse input from (-1,-1) to (1,1).
    /// </summary>
    public static Vector3 MouseInput { get { return _MouseInput; } private set { _MouseInput = value; } }
    private static Vector3 _MouseInput;

    /// <summary>
    /// Mouse position in pixel coordinates.
    /// </summary>
    public static Vector3 MousePixelPos { get { return _MousePixelPos; } private set { _MousePixelPos = value; } }
    private static Vector3 _MousePixelPos;

    /// <summary>
    /// Mouse position in world coordinates.
    /// </summary>
    public static Vector3 MouseWorldPos { get { return _MouseWorldPos; } private set { _MouseWorldPos = value; } }
    private static Vector3 _MouseWorldPos;

    #endregion

    #region ------------------------------------------ KEYBOARD AND JOYSTICK PROPERTIES

    /// <summary>
    /// These keys map to one or several KeyCodes from Unity's Input System and also map joystick axes as Keys.
    /// </summary>
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

    /// <summary>
    /// Keyboard WASD or Joystick Left Stick with smooth filtering.
    /// </summary>
    public static Vector2 MoveAxisSmoothInput { get { return _MoveAxisSmoothInput; } private set { _MoveAxisSmoothInput = value; } }
    private static Vector2 _MoveAxisSmoothInput;

    /// <summary>
    /// Keyboard WASD or Joystick Left Stick without smooth filtering.
    /// </summary>
    public static Vector2 MoveAxisRawInput { get { return _MoveAxisRawInput; } private set { _MoveAxisRawInput = value; } }
    private static Vector2 _MoveAxisRawInput;

    /// <summary>
    /// Joystick Right Stick with smooth filtering.
    /// </summary>
    public static Vector2 AimAxisSmoothInput { get { return _AimAxisSmoothInput; } private set { _AimAxisSmoothInput = value; } }
    private static Vector2 _AimAxisSmoothInput;

    /// <summary>
    /// Joystick Right Stick without smooth filtering.
    /// </summary>
    public static Vector2 AimAxisRawInput { get { return _AimAxisRawInput; } private set { _AimAxisRawInput = value; } }
    private static Vector2 _AimAxisRawInput;

    // To stop the mouse scrollwheel from infinitely registering KeyUp.
    private bool isScrollWheelActive;

    #endregion

    private Camera cam;

    #endregion

    private void Awake()
    {
        cam = Camera.main;
        InitializeInputArrays();
    }

    private void Update()
    {
        #region ------------------------------------------ STORE MOUSE PROPERTIES

        if (_MouseInput.sqrMagnitude > 0)
            if (!_isMouseActive)
                _isMouseActive = true;

        if (_AimAxisRawInput.sqrMagnitude > 0)
            if (_isMouseActive)
                _isMouseActive = false;

        _MouseInput = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        if (_MouseInput.magnitude > 1)
            _MouseInput *= (100f / _MouseInput.magnitude) / 100f;

        _MousePixelPos = Input.mousePosition;
        _MousePixelPos.z = 20f;
        _MouseWorldPos = cam.ScreenToWorldPoint(_MousePixelPos);
        _MouseWorldPos.z = 0f;

        #endregion

        #region ------------------------------------------ KEYBOARD AND JOYSTICK INPUTS

        _MoveAxisSmoothInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        // NOTE: I should add this axis clamping to a Utility class.
        // Clamp axis magnitude to have a value that doesn't go higher than 1 if it's a diagonal vector.
        if (_MoveAxisSmoothInput.magnitude > 1)
            _MoveAxisSmoothInput *= (100f / _MoveAxisSmoothInput.magnitude)/100f;

        _MoveAxisRawInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        // Clamp axis magnitude to have a value that doesn't go higher than 1 if it's a diagonal vector.
        if (_MoveAxisRawInput.magnitude > 1)
            _MoveAxisRawInput *= (100f / _MoveAxisRawInput.magnitude) / 100f;

        #region ------------------------------------------ CUSTOM INPUTS ADDED TO UNITY'S INPUT MANAGER

        // To support all controllers, use these names and add new inputs to Unity's Input Manager.
        // At the moment it only support Xbox controllers
        _AimAxisSmoothInput = new Vector2(Input.GetAxis("HorizontalAim"), Input.GetAxis("VerticalAim"));

        // Clamp axis magnitude to have a value that doesn't go higher than 1 if it's a diagonal vector.
        if (_AimAxisSmoothInput.magnitude > 1)
            _AimAxisSmoothInput *= (100f / _AimAxisSmoothInput.magnitude) / 100f;

        _AimAxisRawInput = new Vector2(Input.GetAxisRaw("HorizontalAim"), Input.GetAxisRaw("VerticalAim"));

        // Clamp axis magnitude to have a value that doesn't go higher than 1 if it's a diagonal vector.
        if (_AimAxisRawInput.magnitude > 1)
            _AimAxisRawInput *= (100f / _AimAxisRawInput.magnitude) / 100f;

        #region ------------------------------------------ STORE CURRENT AXIS AS KEYS

        // Xbox 360 Inputs. I'm not exactly sure, but I think this works for Xbox One controllers too.
        StoreCurrentAxisAsKeyType(ThisKey.Xbox360RightTrigger, Input.GetAxis("Xbox360RightTrigger"));
        StoreCurrentAxisAsKeyType(ThisKey.Xbox360LeftTrigger, Input.GetAxis("Xbox360LeftTrigger"));

        #endregion

        #endregion

        #region ------------------------------------------ STORE CURRENT KEY

        if (Input.GetKey(KeyCode.A))
            StoreCurrentKey(ThisKey.MoveLeft);

        if (Input.GetKey(KeyCode.D))
            StoreCurrentKey(ThisKey.MoveRight);

        if (Input.GetKey(KeyCode.W))
            StoreCurrentKey(ThisKey.MoveUp);

        if (Input.GetKey(KeyCode.S))
            StoreCurrentKey(ThisKey.MoveDown);

        if (Input.GetMouseButton(0) || GetKey(ThisKey.Xbox360RightTrigger))
            StoreCurrentKey(ThisKey.PrimaryAction);

        if (Input.GetMouseButton(1) || GetKey(ThisKey.Xbox360LeftTrigger))
            StoreCurrentKey(ThisKey.SecondaryAction);

        #endregion

        #region ------------------------------------------ STORE CURRENT KEY DOWN

        if (Input.GetKeyDown(KeyCode.A))
            StoreCurrentKeyDown(ThisKey.MoveLeft);

        if (Input.GetKeyDown(KeyCode.D))
            StoreCurrentKeyDown(ThisKey.MoveRight);

        if (Input.GetKeyDown(KeyCode.W))
            StoreCurrentKeyDown(ThisKey.MoveUp);

        if (Input.GetKeyDown(KeyCode.S))
            StoreCurrentKeyDown(ThisKey.MoveDown);

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Joystick1Button5))
            StoreCurrentKeyDown(ThisKey.Dash);

        if (Input.GetKeyDown(KeyCode.C))
            StoreCurrentKeyDown(ThisKey.PreviousUseRate);

        if (Input.GetKeyDown(KeyCode.V) || Input.GetKeyDown(KeyCode.Joystick1Button9))
            StoreCurrentKeyDown(ThisKey.NextUseRate);

        if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Joystick1Button1))
            StoreCurrentKeyDown(ThisKey.NextWeapon);

        if (Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.Joystick1Button0))
            StoreCurrentKeyDown(ThisKey.PreviousWeapon);

        if (Input.GetMouseButtonDown(0) || GetKeyDown(ThisKey.Xbox360RightTrigger))
            StoreCurrentKeyDown(ThisKey.PrimaryAction);

        if (Input.GetMouseButtonDown(1) || GetKeyDown(ThisKey.Xbox360LeftTrigger))
            StoreCurrentKeyDown(ThisKey.SecondaryAction);

        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Joystick1Button7))
            StoreCurrentKeyDown(ThisKey.Pause);

        if (Input.mouseScrollDelta.y > 0)
        {
            isScrollWheelActive = true;
            StoreCurrentKeyDown(ThisKey.NextWeapon);
        }

        if (Input.mouseScrollDelta.y < 0)
        {
            isScrollWheelActive = true;
            StoreCurrentKeyDown(ThisKey.PreviousWeapon);
        }

        #endregion

        #region ------------------------------------------ STORE CURRENT KEY UP

        if (Input.GetKeyUp(KeyCode.A))
            StoreCurrentKeyUp(ThisKey.MoveLeft);

        if (Input.GetKeyUp(KeyCode.D))
            StoreCurrentKeyUp(ThisKey.MoveRight);

        if (Input.GetKeyUp(KeyCode.W))
            StoreCurrentKeyUp(ThisKey.MoveUp);

        if (Input.GetKeyUp(KeyCode.S))
            StoreCurrentKeyUp(ThisKey.MoveDown);

        if (Input.GetKeyUp(KeyCode.Space) || Input.GetKeyUp(KeyCode.Joystick1Button5))
            StoreCurrentKeyUp(ThisKey.Dash);

        if (Input.GetKeyUp(KeyCode.C))
            StoreCurrentKeyUp(ThisKey.PreviousUseRate);

        if (Input.GetKeyUp(KeyCode.V))
            StoreCurrentKeyUp(ThisKey.NextUseRate);

        if (Input.GetKeyUp(KeyCode.V) || Input.GetKeyUp(KeyCode.Joystick1Button9))
            StoreCurrentKeyUp(ThisKey.NextUseRate);

        if (Input.GetKeyUp(KeyCode.E) || Input.GetKeyUp(KeyCode.Joystick1Button1))
            StoreCurrentKeyUp(ThisKey.NextWeapon);

        if (Input.GetKeyUp(KeyCode.Q) || Input.GetKeyUp(KeyCode.Joystick1Button0))
            StoreCurrentKeyUp(ThisKey.PreviousWeapon);

        if (Input.GetMouseButtonUp(0) || GetKeyUp(ThisKey.Xbox360RightTrigger))
            StoreCurrentKeyUp(ThisKey.PrimaryAction);

        if (Input.GetMouseButtonUp(1) || GetKeyUp(ThisKey.Xbox360LeftTrigger))
            StoreCurrentKeyUp(ThisKey.SecondaryAction);

        if (Input.GetKeyUp(KeyCode.Escape) || Input.GetKeyUp(KeyCode.Joystick1Button7))
            StoreCurrentKeyUp(ThisKey.Pause);

        // TODO: Improve mouseScrollDelta input conditions.
        if (Input.mouseScrollDelta.y == 0 && isScrollWheelActive)
        {
            StoreCurrentKeyUp(ThisKey.NextWeapon);
            StoreCurrentKeyUp(ThisKey.PreviousWeapon);
            isScrollWheelActive = false;
        }

        #endregion

        #endregion
    }

    /// <summary>
    /// Returns true while the user is holding the key identified by the key ThisKey enum parameter.
    /// </summary>
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

    /// <summary>
    /// Returns true during the frame the user start pressing down the key identified by the key ThisKey enum parameter.
    /// </summary>
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

    /// <summary>
    /// Returns true during the frame the user releases the key identified by the key ThisKey enum parameter.
    /// </summary>
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
}
