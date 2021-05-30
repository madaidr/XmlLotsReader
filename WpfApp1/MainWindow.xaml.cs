using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Xml;


namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string nnn;
        public List<int> nums = new List<int>();
        private List<XmlNode> xmlN = new List<XmlNode>();
        private int snum;
        private int qqq = 0;
        private XmlDocument xDoc;
        public MainWindow()
        {
            InitializeComponent();
            xDoc = new XmlDocument();
            xDoc.Load("lots.xml");
            xmlN.Add(xDoc.DocumentElement);
            for (int i = 0; i < xmlN[0].ChildNodes.Count; i++)
            {
                Button btn = new Button();
                btn.Click += Button_Click;
                btn.Name += "_" + (tControl.Items.Count - 1).ToString() + i.ToString();
                btn.Content = xmlN[0].ChildNodes.Item(i).Attributes.GetNamedItem("name").Value;
                layoutStackPanel.Children.Add(btn);
            }

            //Search();


        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            RemoveI();
            qqq = tControl.SelectedIndex;
            Button selectedButton = (Button)e.Source;
            nnn = selectedButton.Name.Substring(2);
            //MessageBox.Show(nnn);  
            TabItem titem = new TabItem();
            Grid gSecond = new Grid();
            ScrollViewer sSecond = new ScrollViewer();
            StackPanel pSecond = new StackPanel();
            sSecond.Content = pSecond;
            gSecond.Children.Add(sSecond);
            titem.Content = gSecond;
            titem.Header = selectedButton.Content;
            tControl.Items.Add(titem);
            tControl.SelectedIndex = tControl.Items.Count - 1;
            Search(pSecond);

        }
        void Search(StackPanel pSecond)
        {
            //ШАГ ПЕРВЫЙ
            //string nnn = Console.ReadLine();
            snum = Convert.ToInt32(nnn);
                nums.Add(snum);
                xmlN.Add(xmlN[qqq].ChildNodes.Item(nums[qqq]));
                ReSearch(pSecond);
   
        }
        void ReSearch(StackPanel pSecond)
        {
            //обход по дочерним
            for (int i = 0; i < xmlN[qqq].ChildNodes.Count; i++)
            {
                if (i == nums.Last())//тут
                {
                    for (int j = 0; j < xmlN.Last().ChildNodes.Count; j++)
                    {
                        if (xmlN.Last().ChildNodes.Item(j).Name == "content")
                        {
                            RadioButton rbt = new RadioButton();
                            rbt.GroupName = "radbut";
                            rbt.Name += "_" + (tControl.Items.Count - 1).ToString() + j.ToString();
                            rbt.Content = xmlN.Last().ChildNodes.Item(j).InnerText + " id: " + xmlN.Last().ChildNodes.Item(j).Attributes.GetNamedItem("id").Value;
                            pSecond.Children.Add(rbt);
                        }

                        if (xmlN.Last().ChildNodes.Item(j).Name == "lot")
                        {
                            Button btnn = new Button();
                            btnn.Click += Button_Click;
                            btnn.Name += "_" + (tControl.Items.Count - 1).ToString() + j.ToString();
                            btnn.Content = xmlN.Last().ChildNodes.Item(j).Attributes.GetNamedItem("name").Value;
                            pSecond.Children.Add(btnn);
                        }
                        /*if (j == xmlN.Last().ChildNodes.Count - 1)
                        {
                            qqq++;
                        }*/
                        /*
                            www = www + "    ";
                            if (qqq == xmlN.Count - 1)
                            {
                                Search(pSecond);
                            }
                            else
                            {
                                ReSearch(pSecond);
                            }
                        }*/
                    }
                }


            }
        }


        void RemoveI()
        {
            if(tControl.Items.Count!= tControl.SelectedIndex+1)
            {
                tControl.Items.RemoveAt(tControl.Items.Count - 1);
                nums.RemoveAt(nums.Count - 1);
                xmlN.RemoveAt(xmlN.Count - 1);
                RemoveI();

            }
        }
        }
}
