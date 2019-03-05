using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ExstreamUtility
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Pubbing pub = new Pubbing();
        //string RunType;
        string ApprovalState1;
        string VesionSelected;
        public MainWindow()
        {
            InitializeComponent();
            LoadUIElements loadUI = new LoadUIElements();
            loadUI.LoadRadioLabels();
            loadUI.LoadListItems();
            BuildRadioButtons(loadUI.RunTypeRadio, loadUI.KeyType1);
            BuildRadioButtons(loadUI.ApprovalStateRadio, loadUI.KeyType2);
            BuildRadioButtons(loadUI.ExstreamVersionRadio, loadUI.KeyType3);
            BuildListItems(loadUI.AppNames);

        }

        private void BuildRadioButtons(string[] ButtonNames, string keytype)
        {
            foreach (string runTypeRadio in ButtonNames)
            {
                RadioButton radioButton = new RadioButton();
                radioButton.Content = runTypeRadio;
                radioButton.MinHeight = 20;
                radioButton.Margin = new Thickness(5, 10, 10, 5);
                radioButton.GroupName = keytype;
                radioButton.FontWeight = FontWeights.Normal;
                if (keytype.Equals("RunTypeRadio")) {
                    RunType.Children.Add(radioButton);
                }
                if (keytype.Equals("ApprovalStateRadio"))
                {
                    ApprovalState.Children.Add(radioButton);
                }
                if (keytype.Equals("ExstreamVersionsRadio"))
                {
                    ExVersion.Children.Add(radioButton);
                }
            }

        }
        private void BuildListItems(List<String> Names)
        {
            foreach(var item in Names)
            {
                ListBoxItem listItem = new ListBoxItem();
                listItem.Content = item.ToString();
                AppName.Items.Add(listItem);


            }
        }

        //private void RadioPubbing_Click(object sender, RoutedEventArgs e)
        //{
        //    RunType = "Pubbing";
        //}

        //private void RadioCompare_Click(object sender, RoutedEventArgs e)
        //{
        //    RunType = "Compare";
        //}

        //private void RadioPubbingCompare_Click(object sender, RoutedEventArgs e)
        //{
        //    RunType = "PubbingCompare";
        //}

        private void RadioWIP_Click(object sender, RoutedEventArgs e)
        {
            ApprovalState1 = "WIP";
        }
        private void RadioPeer_Click(object sender, RoutedEventArgs e)
        {
            ApprovalState1 = "PEER";
        }
        private void RadioIntg_Click(object sender, RoutedEventArgs e)
        {
            ApprovalState1 = "INTG";
        }

        private void RadioUser_Click(object sender, RoutedEventArgs e)
        {
            ApprovalState1 = "USER";
        }

        private void RadioFnst_Click(object sender, RoutedEventArgs e)
        {
            ApprovalState1 = "FNST";
        }

        private void RadioApproved_Click(object sender, RoutedEventArgs e)
        {
            ApprovalState1 = "APPROVED";
        }

        private void RadioVersion8_Click(object sender, RoutedEventArgs e)
        {
            VesionSelected = "Exstream 8.0.330";
        }

        private void RadioVersion86_Click(object sender, RoutedEventArgs e)
        {
            VesionSelected = "Exstream 8.6.107";
        }

        private void RadioVersion9_Click(object sender, RoutedEventArgs e)
        {
            VesionSelected = "Exstream 9.5.201";
        }

        private void RadioVersion95_Click(object sender, RoutedEventArgs e)
        {
            VesionSelected = "Exstream 9.5.305";
        }

        private void RadioVersion16_Click(object sender, RoutedEventArgs e)
        {
            VesionSelected = "Exstream 16.4.5";
        }
        private void RadioDefaultPath_Click(object sender, RoutedEventArgs e)
        {

        }
        private void RadioCustomPath_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ListBoxAppName_Click(object sender, RoutedEventArgs e)
        {
            //List<int> selectedItemIndexes = (from object o in AppNames.SelectedItems select AppNames.Items.IndexOf(o)).ToList();


            ////AppNames.UnselectAll();

            //foreach (int rowIndex in selectedItemIndexes)
            //{

            //    // listBox.SelectedIndex = rowIndex;  // Tried this as well
            //    AppNames.SelectedItem = AppNames.Items.GetItemAt(rowIndex);

            //}
        }
        public void CreateWorkFlow()
        {
            
            pub.AppName = "";
        }

        private void RadioButton_SourceUpdated(object sender, DataTransferEventArgs e)
        {

        }
    }
}
