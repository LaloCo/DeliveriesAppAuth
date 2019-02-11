﻿using DeliveriesApp.Model;
using Foundation;
using System;
using System.Collections.Generic;
using UIKit;

namespace DeliveriesApp.iOS
{
    public partial class DeliveriesViewController : UITableViewController
    {
        List<Delivery> deliveries;
        public DeliveriesViewController (IntPtr handle) : base (handle)
        {
            deliveries = new List<Delivery>();
        }

        public override async void ViewDidLoad()
        {
            base.ViewDidLoad();

            deliveries = await Delivery.GetDeliveries();
            TableView.ReloadData();
        }

        public override nint RowsInSection(UITableView tableView, nint section)
        {
            return deliveries.Count;
        }

        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            var cell = tableView.DequeueReusableCell("deliveryCell") as DeliveryTableViewCell;

            var delivery = deliveries[indexPath.Row];
            cell.nameLabel.Text = delivery.Name;
            cell.coordinatesLabel.Text = $"{delivery.DestinationLatitude}, {delivery.DestinationLongitude}";
            switch(delivery.Status)
            {
                case 0:
                    cell.statusLabel.Text = "Waiting delivery person";
                    break;
                case 1:
                    cell.statusLabel.Text = "In delivery";
                    break;
                case 2:
                    cell.statusLabel.Text = "Delivered";
                    break;
            }

            return cell;
        }

        public override nfloat GetHeightForRow(UITableView tableView, NSIndexPath indexPath)
        {
            return 60;
        }
    }
}