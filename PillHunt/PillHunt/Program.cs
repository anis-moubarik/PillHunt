﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PillHunt
{

    static class Program
    {
        static void Main(string[] args)
        {
            using (Game game = new Game())
                game.Run();
        }
    }
}
