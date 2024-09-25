namespace Twig
{
    public static partial class TwigUtils
    {
        public static bool WithinBounds(this int index, int cap) => index >= 0 && index < cap;
    }
}

