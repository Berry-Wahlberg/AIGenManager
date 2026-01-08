using System.Windows;
using BerryAIGen.Common;
using System.Windows.Input;
using BerryAIGen.Toolkit.Common;
using BerryAIGen.Toolkit.MdStyles;
using BerryAIGen.Toolkit.Models;

namespace BerryAIGen.Toolkit
{
    public class TipsModel : BaseNotify
    {
        public string Markdown
        {
            get;
            set => SetField(ref field, value);
        }

        public Style Style
        {
            get;
            set => SetField(ref field, value);
        }

        public ICommand Escape
        {
            get;
            set => SetField(ref field, value);
        }
    }


    /// <summary>
    /// Interaction logic for Tips.xaml
    /// </summary>
    public partial class TipsWindow : Window
    {

        public TipsWindow()
        {
            InitializeComponent();
            var tips = new TipsModel
            {
                Markdown = ResourceHelper.GetString("BerryAIGen.Toolkit.Tips.md"),
                Style = CustomStyles.BetterGithub,
                Escape = new RelayCommand<object>(o => Close())
            };

            //Markdown engine = new Markdown();
            //engine.DocumentStyle = CustomStyles.BetterGithub;
            //FlowDocument document = engine.Transform(markdown);
            //RichTextBox.Document = document;
            DataContext = tips;
        }


        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("explorer", "https://github.com/Berry-Wahlberg/AIGenManager/blob/master/BerryAIGen.Toolkit/Tips.md");
        }
    }
}







