// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace XamarinAssessmentTaskiOS
{
	[Register ("HistoryViewController")]
	partial class HistoryViewController
	{
		[Outlet]
		UIKit.UITableView HistoryTableView { get; set; }

		[Outlet]
		UIKit.UILabel NoHistoryFoundLabel { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (HistoryTableView != null) {
				HistoryTableView.Dispose ();
				HistoryTableView = null;
			}

			if (NoHistoryFoundLabel != null) {
				NoHistoryFoundLabel.Dispose ();
				NoHistoryFoundLabel = null;
			}
		}
	}
}
