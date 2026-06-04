namespace DaisyBlazor;

/// <summary>Animation modifier applied to the daisyUI swap component.</summary>
public enum SwapAnimation
{
    /// <summary>No animation; the swap content changes without transition.</summary>
    None,

    /// <summary>Applies the <c>swap-rotate</c> class for a rotation transition.</summary>
    Rotate,

    /// <summary>Applies the <c>swap-flip</c> class for a flip transition.</summary>
    Flip
}
