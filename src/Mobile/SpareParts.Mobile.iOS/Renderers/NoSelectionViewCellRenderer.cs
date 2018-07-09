using SpareParts.Mobile.iOS.Renderers;
using System;
using System.Collections.Generic;
using System.Text;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(ViewCell), typeof(NoSelectionViewCellRenderer))]
namespace SpareParts.Mobile.iOS.Renderers
{
    public class NoSelectionViewCellRenderer : ViewCellRenderer
    {
        public override UIKit.UITableViewCell GetCell(Cell item, UITableViewCell reusableCell, UITableView tv)
        {
            var cell = base.GetCell(item, reusableCell, tv);
            if (cell != null)
            {
                cell.SelectionStyle = UITableViewCellSelectionStyle.None;
                //cell.SelectedBackgroundView = new UIView
                //{
                //    BackgroundColor = UIColor.DarkGray,
                //};
            }

            return cell;
        }
    }
}
