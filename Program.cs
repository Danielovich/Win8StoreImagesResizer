using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;

namespace ImageResizer
{
	class Program
	{
		private static string[] imagesSizes = new string[] { "30x30|smalllogo.png", "50x50|storelogo.png", "150x150|logo.png", "620x300|splashscreen.png" };

		static void Main(string[] args) {
			Console.WriteLine("full name of the image...");

			var imageName = Console.ReadLine();

			Image img = null;
			try {
				 img = Image.FromFile(Environment.CurrentDirectory + Path.DirectorySeparatorChar + imageName);
			} catch {
				throw;
			}
			
			if(!Directory.Exists("scaled"))
				Directory.CreateDirectory("scaled");

			foreach (var size in imagesSizes) {
				try {
					var width = size.Substring(0, size.IndexOf("x"));
					var height = size.Substring(size.IndexOf("x") + 1, (size.IndexOf("|") - size.IndexOf("x")) - 1);
					var newName = size.Substring(size.IndexOf("|") + 1);

					var scaledImage = ImageUtilities.ResizeImage(img, int.Parse(width), int.Parse(height));
					ImageUtilities.SaveJpeg(Environment.CurrentDirectory + Path.DirectorySeparatorChar + "scaled" + Path.DirectorySeparatorChar + newName, scaledImage, 80);
				} catch  {
					if (!Directory.Exists("scaled"))
						Directory.Delete("scaled");

					throw;
				}
			}
		}
	}
}
