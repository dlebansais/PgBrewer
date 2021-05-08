namespace PgBrewer
{
    using System.Windows;
    using System.Windows.Media;
    using Contracts;

    public class Tools
    {
        public static bool FindFirstControl<TControl>(FrameworkElement ctrl, out TControl firstControl)
            where TControl : FrameworkElement
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(ctrl); i++)
                if (VisualTreeHelper.GetChild(ctrl, i) is FrameworkElement Child)
                    if (Child is TControl AsControl)
                    {
                        firstControl = AsControl;
                        return true;
                    }
                    else if (FindFirstControl(Child, out firstControl))
                        return true;

            Contract.Unused(out firstControl);
            return false;
        }
    }
}
