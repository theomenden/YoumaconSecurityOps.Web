using Microsoft.AspNetCore.Components;

namespace YoumaconSecurityOps.Web.Client.Toast.Core
{
    public class ToastSettings
    {
        public ToastSettings(
            string heading,
            RenderFragment childContent,
            string baseClass,
            string additionalClasses,
            string icon,
            bool showProgressBar)
        {
            Heading = heading;

            ChildContent = childContent;

            BaseClass = baseClass;

            AdditionalClasses = additionalClasses;

            Icon = icon;

            ShowProgressBar = showProgressBar;

        }

        public string Heading { get; set; }

        public RenderFragment ChildContent { get; set; }

        public string BaseClass { get; set; }

        public string AdditionalClasses { get; set; }

        public string Icon { get; set; }

        public bool ShowProgressBar { get; set; }
    }
}
