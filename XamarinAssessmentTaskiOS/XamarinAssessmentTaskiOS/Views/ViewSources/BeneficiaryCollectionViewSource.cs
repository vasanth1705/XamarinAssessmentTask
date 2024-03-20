using System;
using System.Collections.Generic;
using Foundation;
using UIKit;
using XamarinAssessmentTaskiOS.Models;

namespace XamarinAssessmentTaskiOS.Views.CustomCells
{
	public class BeneficiaryCollectionViewSource: UICollectionViewSource
	{
        private IReadOnlyList<BeneficiariesModel> _beneficiaryList;
        public event EventHandler<BeneficiariesModel> SelectedItem;

        public BeneficiaryCollectionViewSource(IReadOnlyList<BeneficiariesModel> beneficiaryList)
		{
            _beneficiaryList = beneficiaryList;
        }

        public override UICollectionViewCell GetCell(UICollectionView collectionView, NSIndexPath indexPath)
        {
            var cell = collectionView.DequeueReusableCell(BeneficiaryCell.Key, indexPath) as BeneficiaryCell;
            if (_beneficiaryList.Count > 0) {
                cell?.SetData(_beneficiaryList[indexPath.Row]);
            }
            return cell;
        }

        public override nint GetItemsCount(UICollectionView collectionView, nint section)
        {
            return _beneficiaryList?.Count ?? 0;
        }

        public override void ItemSelected(UICollectionView collectionView, NSIndexPath indexPath)
        {
            SelectedItem?.Invoke(this, _beneficiaryList[indexPath.Row]);
        }

        public void UpdateBeneficiaries(IReadOnlyList<BeneficiariesModel> beneficiaries)
        {
            _beneficiaryList = beneficiaries;
        }
    }
}