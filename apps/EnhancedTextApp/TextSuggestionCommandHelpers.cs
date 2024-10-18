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
    public static class TextSuggestionCommandHelpers
    {
        public static void ExecuteCustomRewrite(object sender, ExecutedRoutedEventArgs e)
        {
            if (sender == null) return;

            if (sender is TextBoxBase)
            {
                ExecuteRewrite(sender as TextBoxBase, e, TextSuggestionCommandId.CustomRewrite);
            }
            else if (sender is TextSuggestionsDialogBox suggestionDialogBox)
            {
                TextBoxBase? tbb = suggestionDialogBox.SuggestionTarget as TextBoxBase;
                
                if(tbb is not null)
                {
                    suggestionDialogBox.ProgressIndicatorBox.Visibility = Visibility.Visible;
                    ExecuteRewrite(tbb, e, TextSuggestionCommandId.CustomRewrite);
                    suggestionDialogBox.ProgressIndicatorBox.Visibility = Visibility.Hidden;
                }

            }
        }

        public static void ExecuteProfessionalRewrite(object sender, ExecutedRoutedEventArgs e)
        {
            if (sender == null) return;

            if (sender is TextBoxBase)
            {
                ExecuteRewrite(sender as TextBoxBase, e, TextSuggestionCommandId.ProfessionalRewrite);
            }
            else if (sender is TextSuggestionsDialogBox suggestionDialogBox)
            {
                TextBoxBase? tbb = suggestionDialogBox.SuggestionTarget as TextBoxBase;

                if (tbb is not null)
                {
                    suggestionDialogBox.ProgressIndicatorBox.Visibility = Visibility.Visible;
                    Thread.Sleep(1000);
                    ExecuteRewrite(tbb, e, TextSuggestionCommandId.ProfessionalRewrite);
                    //suggestionDialogBox.ProgressIndicatorBox.Visibility = Visibility.Hidden;
                }
            }
        }

        public static void ExecuteFriendlyRewrite(object sender, ExecutedRoutedEventArgs e)
        {
            if (sender == null) return;

            if (sender is TextBoxBase)
            {
                ExecuteRewrite(sender as TextBoxBase, e, TextSuggestionCommandId.FriendlyRewrite);
            }
            else if (sender is TextSuggestionsDialogBox suggestionDialogBox)
            {
                TextBoxBase? tbb = suggestionDialogBox.SuggestionTarget as TextBoxBase;

                if (tbb is not null)
                {
                    suggestionDialogBox.ProgressIndicatorBox.Visibility = Visibility.Visible;
                    ExecuteRewrite(tbb, e, TextSuggestionCommandId.FriendlyRewrite);
                    suggestionDialogBox.ProgressIndicatorBox.Visibility = Visibility.Hidden;
                }
            }

        }

        public static void ExecuteConciseRewrite(object sender, ExecutedRoutedEventArgs e)
        {
            if (sender == null) return;

            if (sender is TextBoxBase)
            {
                ExecuteRewrite(sender as TextBoxBase, e, TextSuggestionCommandId.ConciseRewrite);
            }
            else if (sender is TextSuggestionsDialogBox suggestionDialogBox)
            {
                TextBoxBase? tbb = suggestionDialogBox.SuggestionTarget as TextBoxBase;

                if (tbb is not null)
                {
                    suggestionDialogBox.ProgressIndicatorBox.Visibility = Visibility.Visible;
                    ExecuteRewrite(tbb, e, TextSuggestionCommandId.ConciseRewrite);
                    suggestionDialogBox.ProgressIndicatorBox.Visibility = Visibility.Hidden;
                }
            }
        }

        public static void ExecuteElaborateRewrite(object sender, ExecutedRoutedEventArgs e)
        {
            if (sender == null) return;

            if (sender is TextBoxBase)
            {
                ExecuteRewrite(sender as TextBoxBase, e, TextSuggestionCommandId.ElaborateRewrite);
            }
            else if (sender is TextSuggestionsDialogBox suggestionDialogBox)
            {
                TextBoxBase? tbb = suggestionDialogBox.SuggestionTarget as TextBoxBase;

                if (tbb is not null)
                {
                    suggestionDialogBox.ProgressIndicatorBox.Visibility = Visibility.Visible;
                    ExecuteRewrite(tbb, e, TextSuggestionCommandId.ElaborateRewrite);
                    suggestionDialogBox.ProgressIndicatorBox.Visibility = Visibility.Hidden;
                }
            }
        }

        private static void ExecuteRewrite(TextBoxBase? textBoxBase, ExecutedRoutedEventArgs e, TextSuggestionCommandId commandId)
        {
            if (textBoxBase == null) return;

            string selectedText = GetSelectedText(textBoxBase);
            string completeText = GetCompleteText(textBoxBase);

            if (string.IsNullOrEmpty(completeText)) return;

            string customInstruction = e.Parameter?.ToString() ?? "";

            //var chatClient = LLMClientManager.GetChatClient(textBoxBase as DependencyObject);

            if (string.IsNullOrEmpty(selectedText) || selectedText == completeText)
            {
                //string rewrittenText = GetRewriteResults(chatClient, commandId, RewriteMode.Complete, completeText, "", customInstruction);
                string rewrittenText = "Rewrote";
                SetRewrittenText(RewriteMode.Complete, textBoxBase, rewrittenText);
            }
            else
            {
                //string rewrittenText = GetRewriteResults(chatClient, commandId, RewriteMode.Selection, completeText, selectedText, customInstruction);
                string rewrittenText = "Rewrote";
                SetRewrittenText(RewriteMode.Selection, textBoxBase, rewrittenText);
            }
        }


        #region Helper Methods
        
        private static void SetRewrittenText(RewriteMode rewriteMode, TextBoxBase textBoxBase, string rewrittenText)
        {
            if(textBoxBase == null) return;

            if(textBoxBase is TextBox tb)
            {
                switch(rewriteMode)
                {
                    case RewriteMode.Complete:
                        tb.Text = rewrittenText;
                        break;
                    case RewriteMode.Selection:
                        tb.SelectedText = rewrittenText;
                        break;
                }
            }

            if (textBoxBase is RichTextBox rtb)
            {
                switch (rewriteMode)
                {
                    case RewriteMode.Complete:
                        rtb.Document.Blocks.Clear();
                        rtb.Document.Blocks.Add(new Paragraph(new Run(rewrittenText)));
                        break;
                    case RewriteMode.Selection:
                        rtb.Selection.Text = rewrittenText;
                        break;
                }
            }
        }

        private static string GetRewriteResults(ChatClient? chatClient, TextSuggestionCommandId commandId, RewriteMode rewriteMode, 
                                                    string completeText, string selectedText = "", string customInstruction = "")
        {
            if (chatClient == null) return "";

            var messages = new List<ChatMessage>();
            messages.Add(ChatMessage.CreateSystemMessage(GetDefaultSystemMessage(commandId)));

            string promptFormat = GetUserMessagePromptFormat(rewriteMode);


            string instruction = GetDefaultInstruction(commandId);
            if(string.IsNullOrEmpty(customInstruction))
            {
                instruction += customInstruction;
            }
            
            messages.Add(ChatMessage.CreateUserMessage(string.Format(promptFormat, 
                                                                        instruction, 
                                                                        completeText, 
                                                                        selectedText)));

            ChatCompletion completion = chatClient.CompleteChat(messages);
            return completion.Content[0].Text;
        }

        private static string GetUserMessagePromptFormat(RewriteMode rewriteMode)
        {
            StringBuilder sb = new StringBuilder();

            if (rewriteMode == RewriteMode.Complete)
            {
                sb.AppendLine("Here is the complete text for context : ");
                sb.AppendLine("-----------------------------");
                sb.AppendLine("{1}");
                sb.AppendLine("");
                sb.AppendLine("{0}");
                sb.AppendLine("-----------------------------");
                sb.AppendLine("{2}");
                sb.AppendLine("");
                sb.AppendLine("");
                sb.AppendLine("In output, do not provide anything else the rewritten text");
            }
            else if (rewriteMode == RewriteMode.Selection)
            {
                sb.AppendLine("{0}");
                sb.AppendLine("-----------------------------");
                sb.AppendLine("{1}");
                sb.AppendLine("");
                sb.AppendLine("");
                sb.AppendLine("In output, do not provide anything else the rewritten text");
            }
            else
            {
                sb.AppendLine("{0}");
                sb.AppendLine("-----------------------------");
                sb.AppendLine("{1}");
                sb.AppendLine("");
                sb.AppendLine("");
                sb.AppendLine("In output, do not provide anything else but the generated text");
            }

            return sb.ToString();
        }

        private static string GetCompleteText(TextBoxBase textBoxBase)
        {
            if (!(textBoxBase is TextBox) && !(textBoxBase is RichTextBox)) return "";

            string completeText = "";
            
            if (textBoxBase is TextBox tb)
            {
                completeText = tb.Text;
            }

            if (textBoxBase is RichTextBox rtb)
            {
                TextRange tr = new TextRange(rtb.Document.ContentStart, rtb.Document.ContentEnd);
                completeText = tr.Text;
            }

            return completeText;
        }

        private static string GetSelectedText(TextBoxBase textBoxBase)
        {
            if (!(textBoxBase is TextBox) && !(textBoxBase is RichTextBox)) return "";

            string selectedText = "";

            if (textBoxBase is TextBox tb)
            {
                selectedText = tb.SelectedText;
            }
            
            if (textBoxBase is RichTextBox rtb)
            {
                selectedText = rtb.Selection.Text;
            }

            return selectedText;
        }

        private enum RewriteMode : int
        {
            Generation,
            Selection,
            Complete
        }

        #endregion
    }
}
