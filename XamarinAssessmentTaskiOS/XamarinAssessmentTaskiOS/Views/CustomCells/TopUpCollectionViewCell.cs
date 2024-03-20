using System;
using Foundation;
using UIKit;

namespace XamarinAssessmentTaskiOS.Views.CustomCells
{
	public partial class TopUpCollectionViewCell : UICollectionViewCell
	{
		public static readonly NSString Key = new NSString ("TopUpCollectionViewCell");
		public static readonly UINib Nib;

		static TopUpCollectionViewCell ()
		{
			Nib = UINib.FromName ("TopUpCollectionViewCell", NSBundle.MainBundle);
		}

		protected TopUpCollectionViewCell (IntPtr handle) : base (handle)
		{
			// Note: this .ctor should not contain any initialization logic.
		}

		public void SetData(string topUpAmount) {
			ContentView.Layer.CornerRadius = 8;
			AmountLabel.Text = topUpAmount ?? "";
		}

		public void SelectedCell(bool isSelected) {
			AmountLabel.TextColor = isSelected ? UIColor.White : UIColor.Black;
			AEDLabel.TextColor = isSelected ? UIColor.White : UIColor.Black;
            ContentView.BackgroundColor = isSelected ? UIColor.Link : UIColor.White;
		}
	}
}