using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Windows.Controls;
using System.Windows;

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


        public void LoadRadioLabels() {
            RunTypeRadio = ConfigurationManager.AppSettings.Get(KeyType1).Split(',');
            ApprovalStateRadio = ConfigurationManager.AppSettings.Get(KeyType2).Split(',');
            ExstreamVersionRadio = ConfigurationManager.AppSettings.Get(KeyType3).Split(',');
        }
    }
}
