using System;
using System.Collections.Generic;
using Foundation;
using UIKit;

namespace XamarinAssessmentTaskiOS.Views.CustomCells
{
	public class TopUpCollectionViewSource: UICollectionViewSource
	{
        private readonly IReadOnlyList<int> _amounts;
        public event EventHandler<int> SelectedItem;
        public event EventHandler<int> DeSelectedItem;

        public TopUpCollectionViewSource(IReadOnlyList<int> amounts)
		{
            _amounts = amounts;
        }

        public override UICollectionViewCell GetCell(UICollectionView collectionView, NSIndexPath indexPath)
        {
            var cell = collectionView.DequeueReusableCell(TopUpCollectionViewCell.Key, indexPath) as TopUpCollectionViewCell;
            if (_amounts.Count > 0)
            {
                cell?.SetData(_amounts[indexPath.Row].ToString());
            }
            return cell;
        }

        public override nint GetItemsCount(UICollectionView collectionView, nint section)
        {
            return _amounts?.Count ?? 0;
        }

        public override void ItemSelected(UICollectionView collectionView, NSIndexPath indexPath)
        {
            var cell = collectionView.CellForItem(indexPath) as TopUpCollectionViewCell;
            cell?.SelectedCell(true);
            SelectedItem?.Invoke(this, _amounts[indexPath.Row]);
        }

        public override void ItemDeselected(UICollectionView collectionView, NSIndexPath indexPath)
        {
            var cell = collectionView.CellForItem(indexPath) as TopUpCollectionViewCell;
            cell?.SelectedCell(false);
            DeSelectedItem?.Invoke(this, 0);
        }
    }
}