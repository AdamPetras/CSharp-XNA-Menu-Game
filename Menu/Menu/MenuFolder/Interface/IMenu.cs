using Menu.MenuFolder.Classes;

namespace Menu.MenuFolder.Interface
{
    public interface IMenu:IMenuItems
    {
        Items menu { get; set; }
        void Next();
        void Before();
    }
}
