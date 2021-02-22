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
using System.Windows.Shapes;

using System.Windows.Forms;
using Exam_MusicalShop.Models;

namespace Exam_MusicalShop
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        MusicalContext db = null;
        public LoginWindow()
        {
            db = new MusicalContext();
            InitializeComponent();
        }

        private void BtnEnter_Click(object sender, RoutedEventArgs e)
        {
            string login = BoxLogin.Text;
            string password = BoxPassword.Text;

           // if(db.Users.FirstOrDefault(u=> u.Login == login && u.Password==password) != null)
            //{
                MainWindow mw = new MainWindow();
                mw.ShowDialog();
           // }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
