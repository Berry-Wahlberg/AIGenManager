using System;
using BerryAIGen.Common;
using System.Windows;

namespace BerryAIGen.Toolkit.MdStyles
{
    public class CustomStyles
    {

        private static ResourceDictionary LoadDictionary()
        {
            return (ResourceDictionary)Application.LoadComponent(new Uri("/MdStyles/Styles.xaml", UriKind.RelativeOrAbsolute));
        }

        private static Style LoadXaml(string name) => (Style)LoadDictionary()[(object)name];


        public static Style BetterGithub => LoadXaml("DocumentStyleBetterGithub");
    }
}







