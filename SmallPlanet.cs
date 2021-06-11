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

		private const int speed = 3;

		private Canvas canvas;

		private Ellipse planet;

		private Vector targetPosition;

		private Vector localPosition;

		private Ellipse targetPlanet;

		public SmallPlanet(Ellipse planet, Canvas canvas)
		{
			this.planet = planet;
			this.canvas = canvas;

			this.localPosition.X = Canvas.GetLeft(planet);
			this.localPosition.Y = Canvas.GetTop(planet);

			timer.Interval = new TimeSpan(0, 0, 0, 0, 10);
			timer.Tick += Timer_Tick;
		}

		private void Timer_Tick(object sender, EventArgs e)
		{
			this.localPosition.X = this.localPosition.X > this.targetPosition.X ? this.localPosition.X - speed : this.localPosition.X;
			this.localPosition.X = this.localPosition.X < this.targetPosition.X ? this.localPosition.X + speed : this.localPosition.X;
			this.localPosition.Y = this.localPosition.Y > this.targetPosition.Y ? this.localPosition.Y - speed : this.localPosition.Y;
			this.localPosition.Y = this.localPosition.Y < this.targetPosition.Y ? this.localPosition.Y + speed : this.localPosition.Y;

			Canvas.SetLeft(planet, this.localPosition.X);
			Canvas.SetTop(planet, this.localPosition.Y);
		}

		public void SetTarget(Ellipse targetPlanet)
		{
			this.targetPosition.X = Canvas.GetLeft(targetPlanet);
			this.targetPosition.Y = Canvas.GetTop(targetPlanet);

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
