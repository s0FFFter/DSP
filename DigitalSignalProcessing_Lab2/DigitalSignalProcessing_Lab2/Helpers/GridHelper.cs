using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using OxyPlot.Wpf;

namespace DigitalSignalProcessing_Lab2.Helpers
{
    public static class GridHelper
    {
        public static void SetGrid2x2(Grid grid, PlotView[] plots)
        {
            grid.Children.Clear();

            grid.RowDefinitions.Clear();
            grid.ColumnDefinitions.Clear();

            grid.RowDefinitions.Add(new RowDefinition());
            grid.RowDefinitions.Add(new RowDefinition());

            grid.ColumnDefinitions.Add(new ColumnDefinition());
            grid.ColumnDefinitions.Add(new ColumnDefinition());

            Grid.SetRow(plots[0], 0);
            Grid.SetColumn(plots[0], 0);

            Grid.SetRow(plots[1], 0);
            Grid.SetColumn(plots[1], 1);

            Grid.SetRow(plots[2], 1);
            Grid.SetColumn(plots[2], 0);

            Grid.SetRow(plots[3], 1);
            Grid.SetColumn(plots[3], 1);

            grid.Children.Add(plots[0]);
            grid.Children.Add(plots[1]);
            grid.Children.Add(plots[2]);
            grid.Children.Add(plots[3]);
        }

        public static void SetGrid2x3(Grid grid, PlotView[] plots)
        {
            grid.Children.Clear();

            grid.RowDefinitions.Clear();
            grid.ColumnDefinitions.Clear();

            grid.RowDefinitions.Add(new RowDefinition());
            grid.RowDefinitions.Add(new RowDefinition());

            grid.ColumnDefinitions.Add(new ColumnDefinition());
            grid.ColumnDefinitions.Add(new ColumnDefinition());
            grid.ColumnDefinitions.Add(new ColumnDefinition());

            Grid.SetRow(plots[0], 0);
            Grid.SetColumn(plots[0], 0);

            Grid.SetRow(plots[1], 0);
            Grid.SetColumn(plots[1], 1);

            Grid.SetRow(plots[2], 0);
            Grid.SetColumn(plots[2], 2);

            Grid.SetRow(plots[3], 1);
            Grid.SetColumn(plots[3], 0);

            Grid.SetRow(plots[4], 1);
            Grid.SetColumn(plots[5], 1);

            Grid.SetRow(plots[5], 1);
            Grid.SetColumn(plots[6], 2);

            grid.Children.Add(plots[0]);
            grid.Children.Add(plots[1]);
            grid.Children.Add(plots[2]);
            grid.Children.Add(plots[3]);
            grid.Children.Add(plots[4]);
            grid.Children.Add(plots[5]);
        }
    }
}
