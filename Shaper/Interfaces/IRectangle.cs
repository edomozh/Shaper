using Shaper.Structs;

namespace Shaper.Interfaces
{
    internal interface IRectangle
    {
        Point TopLeft { get; }
        double Width { get; }
        double Height { get; }
    }
}
