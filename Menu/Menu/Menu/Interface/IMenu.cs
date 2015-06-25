using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Menu.Classes;
using Microsoft.Xna.Framework.GamerServices;

namespace Menu.Interface
{
    public interface IMenu:IMenuItems
    {
        Items menu { get; set; }
        void Next();
        void Before();
    }
}
