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
using TravelAgency.Util;
using TravelAgency.ViewModels;

namespace TravelAgency.Views
{
    /// <summary>
    /// Interaction logic for AdminPackageView.xaml
    /// </summary>
    public partial class AdminPackageView : UserControl
    {
        public AdminPackageView()
        {
            InitializeComponent();
            DataContext = new PackageViewModel();
        }

    }
}
