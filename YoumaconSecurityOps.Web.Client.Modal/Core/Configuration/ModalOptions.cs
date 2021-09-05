namespace YoumaconSecurityOps.Web.Client.Modal.Core.Configuration
{
    public class ModalOptions
    {
        public ModalPosition? Position { get; set; }

        public string Class { get; set; }

        public string DialogClass { get; set; }

        public bool? IsBackgroundDisabled { get; set; }

        public bool? IsHeaderHidden { get; set; }

        public bool? IsCloseButtonHidden { get; set; }

        public bool? IsKeyboardAllowedToClose { get; set; }
    }
}
