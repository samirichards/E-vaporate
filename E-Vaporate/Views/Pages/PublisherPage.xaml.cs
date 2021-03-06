﻿using System;
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
using System.Windows.Media.Animation;
using E_Vaporate.Model;

namespace E_Vaporate.Views.Pages
{
    /// <summary>
    /// Interaction logic for PublisherPage.xaml
    /// </summary>
    public partial class PublisherPage : Page
    {
        public User LoggedInUser { get; set; }
        public PublisherPage(User user)
        {
            InitializeComponent();
            LoggedInUser = user;
            Refresh();
        }

        private void Btn_AddNewGame_Click(object sender, RoutedEventArgs e)
        {
            //Open the upload game window if there are no ither upload game windows already open
            if (!Application.Current.Windows.OfType<MahApps.Metro.Controls.MetroWindow>().Any(w=> w.GetType().Equals(typeof(UploadGame))))
            {
                UploadGame upload = new UploadGame(LoggedInUser);
                upload.Show();
            }
        }

        public void Refresh()
        {
            if (Frm_GamePubPage.Content != null)
            {
                ((PublisherGameItem)Frm_GamePubPage.Content).Dispose();
            }
            Frm_GamePubPage.Content = null;
            List<Game> games = new List<Game>();
            using (var context = new EVaporateModel())
            {
                games.AddRange(context.Games.Where(p => p.Publisher == LoggedInUser.UserID).ToList());
            }
            Lst_PublishedGames.ItemsSource = games;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Refresh();
        }

        private void Lst_PublishedGames_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Frm_GamePubPage.Content != null)
            {
                ((PublisherGameItem)Frm_GamePubPage.Content).Dispose();
            }
            Frm_GamePubPage.Content = new PublisherGameItem((Game)Lst_PublishedGames.SelectedItem);
            GC.Collect(4);
        }
    }
}
