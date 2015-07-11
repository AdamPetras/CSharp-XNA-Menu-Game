using Menu.MenuFolder.Classes;

namespace Menu.MenuFolder.Interface
{
    public interface IMenu
    {
        Items selected { get; set; }
        void Next();
        void Before();
    }
}
