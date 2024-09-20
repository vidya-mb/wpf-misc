using OpenAI.Chat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Documents;
using System.Windows.Input;
using static EnhancedTextApp.TextSuggestionCommands;

namespace EnhancedTextApp
{
    public static class TextSuggestionHelper
    {

        static TextSuggestionHelper()
        {
            CommandManager.RegisterClassCommandBinding(typeof(TextBoxBase), new CommandBinding(TextSuggestionCommands.CustomRewrite, OnCustomRewrite));
            CommandManager.RegisterClassCommandBinding(typeof(TextBoxBase), new CommandBinding(TextSuggestionCommands.FriendlyRewrite, OnFriendlyRewrite));
            CommandManager.RegisterClassCommandBinding(typeof(TextBoxBase), new CommandBinding(TextSuggestionCommands.ProfessionalRewrite, OnProfessionalRewrite));

            LLMClientManager.DeploymentName = "wpf-ai";
        }

        #region AttachedProperties

        public static readonly DependencyProperty TextSuggestionsEnabledProperty =
            DependencyProperty.RegisterAttached(
                "TextSuggestionsEnabled",
                typeof(bool),
                typeof(TextSuggestionHelper),
                new FrameworkPropertyMetadata(false, OnTextSuggestionsEnabled));

        public static bool GetTextSuggestionsEnabled(Control control)
        {
            return (bool)control.GetValue(TextSuggestionsEnabledProperty);
        }

        public static void SetTextSuggestionsEnabled(Control control, bool value)
        {
            control.SetValue(TextSuggestionsEnabledProperty, value);
        }

        private static void OnTextSuggestionsEnabled(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if(d is TextBoxBase tbb && e.NewValue is bool showAdorner && showAdorner)
            {
                UIElement uie = (UIElement)d;
                var layer = AdornerLayer.GetAdornerLayer(uie);
                if (layer != null)
                {
                    var adorner = new TextBoxAdorner(tbb);
                    adorner.AIButtonClicked += Adorner_AIButtonClicked;
                    layer.Add(adorner);
                }
            }
        }

        private static void Adorner_AIButtonClicked(object sender, RoutedEventArgs e)
        {
            Popup suggestionPopup = new Popup();
            suggestionPopup.PlacementTarget = sender as UIElement;
            suggestionPopup.Placement = PlacementMode.Right;

            var textSuggestionBox = new TextSuggestionsDialogBox();
            textSuggestionBox.SuggestionTarget = sender as UIElement;
            suggestionPopup.Child = textSuggestionBox;
            
            suggestionPopup.StaysOpen = true;
            suggestionPopup.IsOpen = true;
        }

        #endregion

        #region Command Handlers

        private static void OnProfessionalRewrite(object sender, ExecutedRoutedEventArgs e)
        {
            _PerformRewrite(sender, TextSuggestionCommandId.ProfessionalRewrite);
        }

        private static void OnFriendlyRewrite(object sender, ExecutedRoutedEventArgs e)
        {
            _PerformRewrite(sender, TextSuggestionCommandId.FriendlyRewrite);
        }

        private static void OnCustomRewrite(object sender, ExecutedRoutedEventArgs e)
        {
            _PerformRewrite(sender, TextSuggestionCommandId.CustomRewrite);
        }

        private static void _PerformRewrite(object sender, TextSuggestionCommandId commandId)
        {
            if (!(sender is TextBoxBase)) return;

            bool? hasSelection = GetSelectedText(sender as TextBoxBase, out string selectedText, out string completeText);

            if (hasSelection is null) return;

            var chatClient = LLMClientManager.GetChatClient(sender as DependencyObject);
            if (chatClient != null)
            {
                var messages = new List<ChatMessage>();

                messages.Add(ChatMessage.CreateSystemMessage(
                                TextSuggestionCommands.GetDefaultSystemMessage(commandId)));
                messages.Add(CreateUserChatMessage(commandId, selectedText, completeText));

                ChatCompletion completion = chatClient.CompleteChat(messages);
                SetSuggestion(sender as TextBoxBase, completion, hasSelection);
            }
        }


        #endregion

        #region Helper Methods

        private static void SetSuggestion(TextBoxBase textBoxBase, ChatCompletion completion, bool? hasSelection)
        {
            if(textBoxBase is TextBox tb)
            {
                if(hasSelection == true)
                {
                    tb.SelectedText = completion.Content[0].Text;
                }
                else if(hasSelection == false)
                {
                    tb.Text = completion.Content[0].Text;
                }
            }

            if(textBoxBase is RichTextBox rtb)
            {
                if (hasSelection == true)
                {
                    rtb.Selection.Text = completion.Content[0].Text;
                }
                else if (hasSelection == false)
                {
                    rtb.Document.Blocks.Clear();
                    rtb.Document.Blocks.Add(new Paragraph(new Run(completion.Content[0].Text)));
                }
            }
        }

        private static Nullable<bool> GetSelectedText(TextBoxBase sender, out string selectedText, out string completeText)
        {

            selectedText = string.Empty;
            completeText = string.Empty;
            
            if (!(sender is TextBox) && !(sender is RichTextBox)) return null;

            if (sender is TextBox tb)
            {
                selectedText = tb.SelectedText;
                completeText = tb.Text;
            }

            if (sender is RichTextBox rtb)
            {
                selectedText = rtb.Selection.Text;
                TextRange tr = new TextRange(rtb.Document.ContentStart, rtb.Document.ContentEnd);
                completeText = tr.Text;
            }

            if (string.IsNullOrEmpty(selectedText))
            {
                if (!string.IsNullOrEmpty(completeText))
                {
                    selectedText = completeText;
                    completeText = string.Empty;
                }
                else
                {
                    // there is nothing to do here
                    return null;
                }

                return false;
            }

            return true;
        }

        private static ChatMessage CreateUserChatMessage(TextSuggestionCommandId commandId, string selectedText, string completeText = "")
        {
            StringBuilder sb = new StringBuilder();

            if(!string.IsNullOrEmpty(completeText))
            {
                sb.AppendLine("Here is the complete text for context : ");
                sb.AppendLine("-----------------------------");
                sb.AppendLine(completeText);
            }

            sb.AppendLine(TextSuggestionCommands.GetDefaultInstruction(commandId));
            sb.AppendLine("-----------------------------");
            sb.AppendLine(selectedText);
            sb.AppendLine("");
            sb.AppendLine("");

            sb.AppendLine("In output, do not provide anything else the rewritten text");

            return new UserChatMessage(sb.ToString());
        }


        #endregion

    }
}
