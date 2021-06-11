using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;
using System.Windows.Shapes;
using System.Windows.Controls;

namespace Small_Big_Planet
{
	public class BigPlanet
	{
		public event Notify OnCollision;

		private DispatcherTimer timer = new DispatcherTimer();

		private Canvas canvas;

		private Ellipse planet;

		public BigPlanet(Ellipse planet, Canvas canvas)
		{
			timer.Interval = new TimeSpan(0, 0, 0, 0, 250);
			this.planet = planet;
			this.canvas = canvas;
		}
	}
}
