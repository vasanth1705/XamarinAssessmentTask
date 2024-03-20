using System;
using System.Collections.Generic;
using Foundation;
using UIKit;
using XamarinAssessmentTaskiOS.Models;

namespace XamarinAssessmentTaskiOS.Views.CustomCells
{
	public class HistoryTableViewSource: UITableViewSource
	{
        private readonly IReadOnlyList<PaymentModel> _paymentList;

        public HistoryTableViewSource(IReadOnlyList<PaymentModel> paymentList)
		{
            _paymentList = paymentList;
		}

        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            var cell = tableView.DequeueReusableCell(HistoryTableViewCell.Key, indexPath) as HistoryTableViewCell;
            cell?.ConfigureCell(_paymentList[indexPath.Row]);
            return cell;
        }

        public override nint RowsInSection(UITableView tableview, nint section)
        {
            return _paymentList?.Count ?? 0;
        }

        public override nfloat GetHeightForRow(UITableView tableView, NSIndexPath indexPath)
        {
            return 190f;
        }
    }
}