﻿using HealthWear.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Web.Mvc;

namespace AplicatieMedici.Controllers
{
	public class GraficeController : Controller
	{
		public GraficeController()
		{

		}

		// GET: Home
		public ActionResult Test()
		{
			//List<DataPoint> dataPoints = new List<DataPoint>();

			//dataPoints.Add(new DataPoint("NXP", 14));
			//dataPoints.Add(new DataPoint("Infineon", 10));
			//dataPoints.Add(new DataPoint("Renesas", 9));
			//dataPoints.Add(new DataPoint("STMicroelectronics", 8));
			//dataPoints.Add(new DataPoint("Texas Instruments", 7));
			//dataPoints.Add(new DataPoint("Bosch", 5));
			//dataPoints.Add(new DataPoint("ON Semiconductor", 4));
			//dataPoints.Add(new DataPoint("Toshiba", 3));
			//dataPoints.Add(new DataPoint("Micron", 3));
			//dataPoints.Add(new DataPoint("Osram", 2));
			//dataPoints.Add(new DataPoint("Others", 35));

			//ViewBag.DataPoints = JsonConvert.SerializeObject(dataPoints);

			return View();
		}
	}
}