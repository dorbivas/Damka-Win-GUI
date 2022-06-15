
namespace GUIWindows
{
    using System;
    public class MessageBoxYesNoEvent : EventArgs
    {
        private readonly bool r_MessageBoxClickedYes;

        public MessageBoxYesNoEvent(bool i_IsMessageBoxClickedYes)
        {
            r_MessageBoxClickedYes = i_IsMessageBoxClickedYes;
        }

        public bool IsMessageBoxClickedYes
        {
            get => r_MessageBoxClickedYes;
        }
    }
}
