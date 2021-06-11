using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;
using System.Windows.Shapes;
using System.Windows.Controls;
using System.Windows;

namespace Small_Big_Planet
{
	public class BigPlanet
	{
		public event Notify OnCollision;

		private DispatcherTimer timer = new DispatcherTimer();

		private Ellipse planet;

		private Ellipse targetPlanet;

		public BigPlanet(Ellipse planet)
		{
			this.planet = planet;

			timer.Interval = new TimeSpan(0, 0, 0, 0, 25);
			timer.Tick += Timer_Tick;
		}

		private void Timer_Tick(object sender, EventArgs e)
		{
			if (IsCollided())
			{
				OnCollision?.Invoke();
			}
		}

		private Vector LocalPosition
		{
			get
			{
				return GetPosition(this.planet);
			}
			set
			{
				Canvas.SetLeft(this.planet, (value.X - (this.planet.Width / 2)));
				Canvas.SetTop(this.planet, (value.Y - (this.planet.Height / 2)));
			}
		}

		private Vector GetPosition(Ellipse ellipse)
		{
			Vector temp = new Vector();
			temp.X = Canvas.GetLeft(this.planet) + (this.planet.Width / 2);
			temp.Y = Canvas.GetTop(this.planet) + (this.planet.Height / 2);
			return temp;
		}

		public void CheckCollisionWith(Ellipse target)
		{
			this.targetPlanet = planet;
		}

		public void Pull()
		{
			if (timer.IsEnabled)
			{
				timer.Stop();
			}

			timer.Start();
		}

		public void Stop()
		{
			if (timer.IsEnabled)
			{
				timer.Stop();
			}
		}

		private bool IsCollided()
		{
			bool x_status = false;
			bool y_status = false;

			//if smallPlanet left less than the left+width of bigPlanet and not smaller than left of bigPlanet
			//or the smallPlanet left+width is greater than the left of bigPlanet and not greater than left+width of bigPlanet

			//if smallPlanet top+height is greater than the top of bigPlanet and not grater than top+height of bigPlanet
			//or the top of the smallPlanet less than the top+height of bigPlanet and not less than top of bigPlanet

			return (x_status && y_status);
		}
	}
}
