using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace EnhancedTextApp
{
    public class TextSuggestionCommands
    {
        public static RoutedUICommand AutoComplete => _EnsureCommand(TextSuggestionCommandId.AutoComplete);
        public static RoutedUICommand CustomRewrite => _EnsureCommand(TextSuggestionCommandId.CustomRewrite);
        public static RoutedUICommand ProfessionalRewrite => _EnsureCommand(TextSuggestionCommandId.ProfessionalRewrite);
        public static RoutedUICommand FriendlyRewrite => _EnsureCommand(TextSuggestionCommandId.FriendlyRewrite);
        public static RoutedUICommand ConciseRewrite => _EnsureCommand(TextSuggestionCommandId.ConciseRewrite);
        public static RoutedUICommand ElaborateRewrite => _EnsureCommand(TextSuggestionCommandId.ElaborateRewrite);


        internal static string GetDefaultInstruction(TextSuggestionCommandId commandId)
        {
            switch (commandId)
            {
                case TextSuggestionCommandId.AutoComplete:
                    return "Autocomplete the following text in 2-3 words: ";
                case TextSuggestionCommandId.CustomRewrite:
                    return "Please rewrite the following text in a simple style: ";
                case TextSuggestionCommandId.ProfessionalRewrite:
                    return "Please rewrite the following text in a professional style:";
                case TextSuggestionCommandId.FriendlyRewrite:
                    return "Please rewrite the following text in a friendly style :";
                case TextSuggestionCommandId.ConciseRewrite:
                    return "Please rewrite the following text in a concise style:";
                case TextSuggestionCommandId.ElaborateRewrite:
                    return "Please rewrite and elaborate the points in the text:";
                default:
                    return string.Empty;
            }
        }

        internal static string GetDefaultSystemMessage(TextSuggestionCommandId commandId)
        {
            return @"You are a helpful English language assistant. Given a text you can rewrite the text in different tones like friendly, angry, professional or in a concise manner.
                                                        The text message can be a part of larger text and in that case you will be provided the complete text as well to help you understand the context,
                                                        so that your responses are better.";
        }

        #region Private Methods
        private static RoutedUICommand _EnsureCommand(TextSuggestionCommandId idCommand)
        {
            if (idCommand >= 0 && idCommand < TextSuggestionCommandId.Last)
            {
                lock (_internalCommands.SyncRoot)
                {
                    if (_internalCommands[(int)idCommand] == null)
                    {
                        _internalCommands[(int)idCommand] = new RoutedUICommand(GetPropertyName(idCommand), GetPropertyName(idCommand), typeof(TextSuggestionCommands));
                    }
                }
                return _internalCommands[(int)idCommand];
            }
            return null;
        }

        private static string GetPropertyName(TextSuggestionCommandId commandId)
        {
            string propertyName = String.Empty;

            switch (commandId)
            {
                case TextSuggestionCommandId.AutoComplete: propertyName = "AutoComplete"; break;
                case TextSuggestionCommandId.CustomRewrite: propertyName = "SimpleRewrite"; break;
                case TextSuggestionCommandId.ProfessionalRewrite: propertyName = "ProfessionalRewrite"; break;
                case TextSuggestionCommandId.FriendlyRewrite: propertyName = "FriendlyRewrite"; break;
                case TextSuggestionCommandId.ConciseRewrite: propertyName = "ConciseRewrite"; break;
                case TextSuggestionCommandId.ElaborateRewrite: propertyName = "ElaborateRewrite"; break;
                case TextSuggestionCommandId.Last: propertyName = "Last"; break;

            }
            return propertyName;
        }

        #endregion

        internal enum TextSuggestionCommandId : byte
        {
            None = 0,
            AutoComplete = 1,
            CustomRewrite = 2,
            ProfessionalRewrite = 3,
            FriendlyRewrite = 4,
            ConciseRewrite = 5,
            ElaborateRewrite = 6,
            Last = 7
        }

        private static RoutedUICommand[] _internalCommands = new RoutedUICommand[(int)TextSuggestionCommandId.Last];

    }
}
