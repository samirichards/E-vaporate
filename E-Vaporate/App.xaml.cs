﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace E_Vaporate
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            try
            {
                System.Data.SqlClient.SqlConnection connection = new System.Data.SqlClient.SqlConnection
                {
                    ConnectionString = ConfigurationManager.ConnectionStrings["ServerDB"].ConnectionString
                };
                connection.Open();
                connection.Close();
                var accountView = new Views.AccountVerification();
                accountView.Show();
            }
            catch (Exception a)
            {
                MessageBox.Show("Unable to connect to server" + Environment.NewLine + a.InnerException);
                Shutdown();
            }
        }
    }
}
