using System;

namespace PillHunt
{

    public interface IScreenFactory
    {
        GameScreen CreateScreen(Type screenType);
    }
}
