using System;
using System.Windows.Input;
using System.Windows.Media.Media3D;
using System.Windows.Threading;

namespace Shelfinator.Creator
{
	partial class ModelWindow
	{
		int x, y, z, speed = 5;
		public ModelWindow()
		{
			InitializeComponent();
			var timer = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(100) };
			timer.Tick += Timer_Tick;
			timer.Start();
		}

		bool shiftDown => Keyboard.Modifiers.HasFlag(ModifierKeys.Shift);

		int GetValue(int value)
		{
			var newValue = 1 * (shiftDown ? -1 : 1);
			return value == newValue ? 0 : newValue;
		}

		protected override void OnKeyDown(KeyEventArgs e)
		{
			e.Handled = true;
			switch (e.Key)
			{
				case Key.Escape: speed = 5; x = y = z = 0; rotatex.Angle = rotatey.Angle = rotatez.Angle = 0; break;
				case Key.X: x = GetValue(x); break;
				case Key.Y: y = GetValue(y); break;
				case Key.Z: z = GetValue(z); break;
				case Key.Space: x = y = z = 0; break;
				case Key.Up: ++speed; break;
				case Key.Down: speed = Math.Max(1, speed - 1); break;
				case Key.D0: top.Positions = Point3DCollection.Parse("-14.9746538974535,-7.2578929097252,17 -10.6068870864941,-7.2578929097252,17 11.3751573193393,3.3963315769919,17 11.3751573193393,5.5132942784795,17 -14.9746538974535,-7.2578929097252,-17 -10.6068870864941,-7.2578929097252,-17 11.3751573193393,3.3963315769919,-17 11.3751573193393,5.5132942784795,-17 14.9746538974535,5.1409302082376,5.66666666666667 14.9746538974535,7.2578929097252,5.66666666666667 14.9746538974535,5.1409302082376,-5.66666666666667 14.9746538974535,7.2578929097252,-5.66666666666667 11.3751573193393,3.3963315769919,0 11.3751573193393,5.5132942784795,0"); break;
				case Key.D1: top.Positions = Point3DCollection.Parse("-14.9746538974535,-7.2578929097252,17 -10.6068870864941,-7.2578929097252,17 11.3751573193393,3.3963315769919,17 11.3751573193393,5.5132942784795,17 -14.9746538974535,-7.2578929097252,-17 -10.6068870864941,-7.2578929097252,-17 11.3751573193393,3.3963315769919,-17 11.3751573193393,5.5132942784795,-17 14.9746538974535,5.1409302082376,4 14.9746538974535,7.2578929097252,4 14.9746538974535,5.1409302082376,-4 14.9746538974535,7.2578929097252,-4 11.3751573193393,3.3963315769919,0 11.3751573193393,5.5132942784795,0"); break;
				default: e.Handled = false; break;
			}
			base.OnKeyDown(e);
		}

		void Timer_Tick(object sender, EventArgs e)
		{
			rotatex.Angle += x * speed;
			rotatey.Angle += y * speed;
			rotatez.Angle += z * speed;
		}
	}
}
