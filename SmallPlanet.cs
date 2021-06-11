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
	public class SmallPlanet
	{
		public event Notify OnCollision;

		private DispatcherTimer timer = new DispatcherTimer();

		private const int speed = 1;

		private Ellipse planet;

		private Vector targetPosition;

		private Ellipse targetPlanet;

		public SmallPlanet(Ellipse planet)
		{
			this.planet = planet;

			timer.Interval = new TimeSpan(0, 0, 0, 0, 25);
			timer.Tick += Timer_Tick;
		}

		private void Timer_Tick(object sender, EventArgs e)
		{
			UpdatePosition();
		}

		private Vector LocalPosition
		{
			get
			{
				Vector temp = new Vector();
				temp.X = Canvas.GetLeft(this.planet) + (this.planet.Width / 2);
				temp.Y = Canvas.GetTop(this.planet) + (this.planet.Height / 2);
				return temp;
			}
			set
			{
				Canvas.SetLeft(this.planet, (value.X - (this.planet.Width / 2)));
				Canvas.SetTop(this.planet, (value.Y - (this.planet.Height / 2)));
			}
		}

		private void UpdatePosition()
		{
			Vector temp = new Vector();
			temp = LocalPosition;
			temp.X = temp.X > this.targetPosition.X ? temp.X - speed : temp.X;
			temp.X = temp.X < this.targetPosition.X ? temp.X + speed : temp.X;
			temp.Y = temp.Y > this.targetPosition.Y ? temp.Y - speed : temp.Y;
			temp.Y = temp.Y < this.targetPosition.Y ? temp.Y + speed : temp.Y;

			LocalPosition = temp;
		}

		public void SetTarget(Ellipse targetPlanet)
		{
			this.targetPosition.X = Canvas.GetLeft(targetPlanet) + (targetPlanet.Width / 2);
			this.targetPosition.Y = Canvas.GetTop(targetPlanet) + (targetPlanet.Height / 2);

			this.targetPlanet = targetPlanet;
		}

		public void Move()
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
