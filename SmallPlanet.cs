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

		public int speed = 1;

		private Ellipse planet;

		private Vector targetPosition;

		private List<Vector> targetBoundary = new List<Vector>(2);
		private List<Vector> localBoundary = new List<Vector>(2);

		public SmallPlanet(Ellipse planet)
		{
			this.planet = planet;

			timer.Interval = new TimeSpan(0, 0, 0, 0, 25);
			timer.Tick += Timer_Tick;

			UpdateLocalBoundary();
		}

		private void UpdateLocalBoundary()
		{
			Vector leftTop = new Vector();
			Vector rightBottom = new Vector();

			leftTop.X = Canvas.GetLeft(planet);
			leftTop.Y = Canvas.GetTop(planet);
			rightBottom.X = Canvas.GetLeft(planet) + (planet.Width);
			rightBottom.Y = Canvas.GetTop(planet) + (planet.Height);

			this.localBoundary.Clear();
			this.localBoundary.Add(leftTop);
			this.localBoundary.Add(rightBottom);
		}

		private void Timer_Tick(object sender, EventArgs e)
		{
			UpdatePosition();
			UpdateLocalBoundary();

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
			temp.X = Canvas.GetLeft(ellipse) + (ellipse.Width / 2);
			temp.Y = Canvas.GetTop(ellipse) + (ellipse.Height / 2);
			return temp;
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
			this.targetPosition = GetPosition(targetPlanet);
		}

		public void CheckCollisionWith(Ellipse ellipse)
		{
			Vector leftTop = new Vector();
			Vector rightBottom = new Vector();

			leftTop.X = Canvas.GetLeft(ellipse);
			leftTop.Y = Canvas.GetTop(ellipse);
			rightBottom.X = Canvas.GetLeft(ellipse) + (ellipse.Width);
			rightBottom.Y = Canvas.GetTop(ellipse) + (ellipse.Height);

			this.targetBoundary.Clear();
			this.targetBoundary.Add(leftTop);
			this.targetBoundary.Add(rightBottom);
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

			if (localBoundary[0].X < targetBoundary[1].X && localBoundary[0].X > targetBoundary[0].X) // if smallPlanet left less than the left+width of bigPlanet and not smaller than left of bigPlanet
			{
				x_status = true;
			}
			else if (localBoundary[1].X > targetBoundary[0].X && localBoundary[1].X < targetBoundary[1].X) // or the smallPlanet left+width is greater than the left of bigPlanet and not greater than left+width of bigPlanet
			{
				x_status = true;
			}

			if (localBoundary[1].Y > targetBoundary[0].Y && localBoundary[1].Y < targetBoundary[1].Y) // if smallPlanet top+height is greater than the top of bigPlanet and not grater than top+height of bigPlanet
			{
				y_status = true;
			}
			else if (localBoundary[0].Y < targetBoundary[1].Y && localBoundary[0].Y > targetBoundary[0].Y) // or the top of the smallPlanet less than the top+height of bigPlanet and not less than top of bigPlanet
			{
				y_status = true;
			}

			return (x_status && y_status);
		}
	}
}
