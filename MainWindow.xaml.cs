using System;
using System.Speech.Recognition;
using System.Windows;
using System.Windows.Media;

namespace NekoCom
{
    public partial class MainWindow : Window
    {
        private SpeechRecognitionEngine recognizer;
        
        public MainWindow()
        {
            InitializeComponent();
            InitializeVoiceRecognition();
        }

        private void InitializeVoiceRecognition()
        {
            try
            {
                recognizer = new SpeechRecognitionEngine();
                var commands = new Choices("hey neko", "open discord", "open chrome", "tell me a joke", "what time is it");
                var grammar = new Grammar(new GrammarBuilder(commands));
                
                recognizer.LoadGrammar(grammar);
                recognizer.SpeechRecognized += OnSpeechRecognized;
                recognizer.SetInputToDefaultAudioDevice();
                recognizer.RecognizeAsync(RecognizeMode.Multiple);
                
                StatusText.Text = "🎤 Listening... Say 'Hey Neko'!";
            }
            catch (Exception ex)
            {
                StatusText.Text = $"❌ Mic error: {ex.Message}";
            }
        }

        private void OnSpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            string command = e.Result.Text.ToLower();
            
            Dispatcher.Invoke(() => {
                // Change emoji based on command!
                if (command.Contains("hey neko"))
                {
                    AvatarEmoji.Text = "😺";
                    StatusText.Text = "Yes? Listening for command...";
                }
                else if (command.Contains("open discord"))
                {
                    AvatarEmoji.Text = "🚀";
                    StatusText.Text = "Opening Discord!";
                    System.Diagnostics.Process.Start("discord://");
                }
                else if (command.Contains("open chrome"))
                {
                    AvatarEmoji.Text = "🌐";
                    StatusText.Text = "Opening Chrome!";
                    System.Diagnostics.Process.Start("chrome.exe");
                }
                else if (command.Contains("tell me a joke"))
                {
                    AvatarEmoji.Text = "😹";
                    StatusText.Text = "Why do catboys make great programmers? We're always purr-sistent!";
                }
                else if (command.Contains("what time is it"))
                {
                    AvatarEmoji.Text = "🕒";
                    StatusText.Text = $"It's {DateTime.Now:T} nya~!";
                }
            });
        }
    }
}
