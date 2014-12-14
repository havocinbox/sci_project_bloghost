namespace Bloghost.UI.Web.Admin
{
    public class MenuNavigation
    {
        private static string[] _activeMenu = new string[1] { "" };

        public static string[] CurrentActiveMenu
        {
            get
            {
                if (_activeMenu == null)
                    return new string[1] { "" };
                return _activeMenu;
            }
            set { _activeMenu = value; }
        }

        public static bool IsActiveMenuItem(string item)
        {
            if (string.IsNullOrEmpty(item))
                return false;
            if (_activeMenu.Length >= 1 && _activeMenu[0] == item)
                return true;
            return false;
        }

        public static bool IsActiveMenuItem(string item, string subitem)
        {
            if (string.IsNullOrEmpty(item) || string.IsNullOrEmpty(subitem))
                return false;
            if (_activeMenu.Length >= 2 && _activeMenu[0] == item && _activeMenu[1] == subitem)
                return true;
            return false;
        }

        public static bool IsActiveMenuItem(string[] items)
        {
            if (items == null)
                return false;
            if (_activeMenu.Length < items.Length)
                return false;
            for (var i = 0; i < items.Length; i++)
            {
                if (string.IsNullOrEmpty(items[i]))
                    return false;
                if (_activeMenu[i] != items[i])
                    return false;
            }
            return true;
        }
    }
}