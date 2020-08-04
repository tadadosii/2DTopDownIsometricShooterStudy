/// <summary>
/// Contains methods to handle arrays.
/// </summary>
public static class ArraysHandler
{
    // --------------------------------------
    // ----- 2D Isometric Shooter Study -----
    // ----------- by Tadadosi --------------
    // --------------------------------------
    // ---- Support my work by following ----
    // ---- https://twitter.com/tadadosi ----
    // --------------------------------------

    public static int GetNextIndex (int currentIndex, int arrayLength)
    {
        return (currentIndex + 1) % arrayLength;
    }

    public static int GetPreviousIndex(int currentIndex, int arrayLength)
    {
        return ((currentIndex - 1) + arrayLength) % arrayLength;
    }
}
