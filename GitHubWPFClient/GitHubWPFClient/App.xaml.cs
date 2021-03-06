﻿using GitHubApiWrapper;
using GitHubWPFClient.ViewModels;
using GitHubWPFClient.Views;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace GitHubWPFClient
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            MainWindow mw = new MainWindow();
            mw.DataContext = new MainWindowViewModel(new GitHubWrapper());
            mw.Show();
        }
    }
}
