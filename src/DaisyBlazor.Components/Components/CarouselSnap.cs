namespace DaisyBlazor;

/// <summary>Scroll-snap alignment applied to a daisyUI carousel.</summary>
public enum CarouselSnap
{
    /// <summary>No explicit snap class; browser default snap behaviour.</summary>
    None,

    /// <summary>Snaps items to the centre of the scroll container (<c>carousel-center</c>).</summary>
    Center,

    /// <summary>Snaps items to the end of the scroll container (<c>carousel-end</c>).</summary>
    End
}
