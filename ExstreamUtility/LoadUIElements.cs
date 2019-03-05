using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Windows.Controls;
using System.Windows;
using System.IO;

namespace ExstreamUtility
{
    class LoadUIElements
    {
        public string[] RunTypeRadio;
        public string[] ApprovalStateRadio;
        public string[] ExstreamVersionRadio;
        public string KeyType1 = "RunTypeRadio";
        public string KeyType2 = "ApprovalStateRadio";
        public string KeyType3 = "ExstreamVersionsRadio";
        public string FileName = @"E:\ProjectFolder\ExstreamUtility\ExstreamUtility\AppandPubNames.csv";
        public List<String> AppNames = new List<string>();
        public List<String> PubNames = new List<string>();




        public void LoadRadioLabels() {
            RunTypeRadio = ConfigurationManager.AppSettings.Get(KeyType1).Split(',');
            ApprovalStateRadio = ConfigurationManager.AppSettings.Get(KeyType2).Split(',');
            ExstreamVersionRadio = ConfigurationManager.AppSettings.Get(KeyType3).Split(',');
        }
        public void LoadListItems()
        {
            var lines = File.ReadLines(FileName);
            foreach(var line in lines)
            {
                AppNames.Add(line.Split(',')[0].ToString());
                PubNames.Add(line.Split(',')[1].ToString());
            }

        }
    }
}
