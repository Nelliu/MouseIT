using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
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

namespace MouseIt
{
    

    

    public partial class MainWindow : Window
    {


        bool FileExists = false;

        List<Profile> profiles = new List<Profile>();
        double val;
        double val2;
        double val3;

        

        public MainWindow()
        {


            val = Convert.ToDouble(MouseOptions.GetMouseSpeed());
            val2 = Convert.ToDouble(MouseOptions.GetDoubleClick());
            val3 = Convert.ToDouble(MouseOptions.GetScrollSpeed());


            if (File.Exists("profiles.json"))
            {
                FileExists = true;
            }


            InitializeComponent();
            speed.Value = val;
            clickSpeed.Value = val2;
            scrollSpeed.Value = val3;
           
            if (FileExists)
            {
                profilePre.SelectedIndex = 1;

                profiles = SaveSystem.ReadFromJsonFile<List<Profile>>("profiles.json");

                speed.Value = profiles[0].scrollSpeed;
                clickSpeed.Value = profiles[0].doubleClick;
                scrollSpeed.Value = profiles[0].scrollSpeed;

            }
            else // file doesnt exist
            {

                int uuid = SaveSystem.GetuID();
                
                


                profilePre.SelectedIndex = 1;
                Profile prof1 = new Profile();
                prof1.uID = uuid;
                prof1.name = "profile1";
                prof1.mouseSpeed = Convert.ToInt32(val);
                prof1.scrollSpeed = Convert.ToInt32(val3);
                prof1.doubleClick = Convert.ToInt32(val2);



                Profile prof2 = new Profile();
                prof2.uID = uuid;
                prof2.name = "profile2";
                prof2.mouseSpeed = Convert.ToInt32(val);
                prof2.scrollSpeed = Convert.ToInt32(val3);
                prof2.doubleClick = Convert.ToInt32(val2);

                profiles.Add(prof1);
                profiles.Add(prof2);

                SaveSystem.Save(profiles);
          

            }


            GetProfs(profiles);



            
            


        }
        private async void GetProfs(List<Profile> profs)
        {
            profiles.Clear();
            profs = await Api.Ask();
        }



        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e) // mouse speed change slider action
        {
            MouseOptions.SetMouseSpeed(Convert.ToInt32(speed.Value));
        }

        private void Grid_MouseMove(object sender, MouseEventArgs e)
        {
            //speedPre.Text = e.MouseDevice
            
        }

        private void clickSpeed_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            MouseOptions.SetDoubleClickSpeed(clickSpeed.Value);

            
        }

        private void scrollSpeed_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            MouseOptions.SetScrollSpeed(scrollSpeed.Value);
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {

            if (profilePre.SelectedIndex == 1 || profilePre.SelectedIndex == 2)
            {
                if (FileExists)
                {
                    File.Delete("profiles.json");
                    if (profilePre.SelectedIndex == 1)
                    {
                        profiles[0].scrollSpeed = Convert.ToInt32(scrollSpeed.Value);
                        profiles[0].doubleClick = Convert.ToInt32(clickSpeed.Value);
                        profiles[0].mouseSpeed = Convert.ToInt32(speed.Value);
                    }
                    else
                    {
                        profiles[1].scrollSpeed = Convert.ToInt32(scrollSpeed.Value);
                        profiles[1].doubleClick = Convert.ToInt32(clickSpeed.Value);
                        profiles[1].mouseSpeed = Convert.ToInt32(speed.Value);
                    }
                    SaveSystem.Save(profiles);
                    string a = JsonConvert.SerializeObject(profiles);
                    await Api.Send(JsonConvert.SerializeObject(profiles));
                }
            }

            
        }

        private void profilePre_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (profiles.Count != 0)
            {
                if (profilePre.SelectedIndex == 1)
                {
                    speed.Value = profiles[0].mouseSpeed;
                    scrollSpeed.Value = profiles[0].scrollSpeed;
                    clickSpeed.Value = profiles[0].doubleClick;

                    MouseOptions.SetMouse(clickSpeed.Value, speed.Value, scrollSpeed.Value);


                }
                else if (profilePre.SelectedIndex == 2)
                {
                    speed.Value = profiles[1].mouseSpeed;
                    scrollSpeed.Value = profiles[1].scrollSpeed;
                    clickSpeed.Value = profiles[1].doubleClick;
                    MouseOptions.SetMouse(clickSpeed.Value, speed.Value, scrollSpeed.Value);
                }
            }
            
        }

        private void Button_Click_1(object sender, RoutedEventArgs e) // adding button click
        {

        }
    }
}
