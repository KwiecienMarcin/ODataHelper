using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Xml;

namespace ConsoleApplication1
{
	public class DataHelper
	{
		static List<string> listOfData = new List<string>();
		static void Main(string[] args)
		{
			DataHelper.CreateObject();
			Menu();
		}

		private static void Menu()
		{
			Console.WriteLine("-If you want to show elements from service write 1\n" +
			                  "-If you want to sort data alphabetically write 2\n" +
			                  "-If you want to sort data from shortest to longest write 3\n" +
			                  "-If you want to filter data that was longer than your number write 4\n" +
			                  "-If you want to filter data that contains given letter write 5");

			int n = int.Parse(Console.ReadLine());
			switch (n)
			{
				case 1:
					foreach (var VARIABLE in listOfData)
					{
						Console.WriteLine(VARIABLE);
					}

					break;
				case 2:
					listOfData.Sort();
					foreach (var VARIABLE in listOfData)
					{
						Console.WriteLine(VARIABLE);
					}

					break;
				case 3:
					var lenghts = from element in listOfData orderby element.Length select element;
					foreach (var VARIABLE in lenghts)
					{
						Console.WriteLine(VARIABLE);
					}

					break;
				case 4:
					Console.WriteLine($"How long can be words? Write number. (min. 8)");
					n = int.Parse(Console.ReadLine());
					List<string> shortestList = listOfData.Where(x => x.Length < n).ToList();
					foreach (var VARIABLE in shortestList)
					{
						Console.WriteLine(VARIABLE);
					}

					break;
				case 5:
					Console.WriteLine(
						$"Words with which letter do you want to see? Remember that the size of letters is important.");
					string l = Console.ReadLine();
					List<string> filteredList = listOfData.Where(x => x.Contains(l)).ToList();
					foreach (var VARIABLE in filteredList)
					{
						Console.WriteLine(VARIABLE);
					}

					break;
				default:
					break;
			}
		}

		private const string URL = "https://services.odata.org/V3/(S(ej2oh1jixzvj3baomotuaaaw))/OData/OData.svc/";


		public static void CreateObject()
		{
			List<string> list = listOfData;
			var request = (HttpWebRequest)WebRequest.Create(URL);
			request.Method = "GET";
			request.ContentType = "application/xml";
			request.Accept = "application/xml";
			using (var response = request.GetResponse())
			{
				using (var stream = response.GetResponseStream())
				{
					var reader = new XmlTextReader(stream);
					int i = 0;
					while (reader.Read())
					{
						if (reader.Value != "")
						{
							list.Add(reader.Value);
							if (list[i] != null)
							{
								i++;
							}
						}
						else
						{
							continue;
						}
					}
				}
			}
		}
	}
}