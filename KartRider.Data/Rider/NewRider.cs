using System;
using System.Collections.Generic;
using System.IO;
using KartRider.IO;
using KartRider;
using ExcData;
using Set_Data;
using System.Xml;
using System.Linq;

namespace RiderData
{
	public static class NewRider
	{
		public static void LoadItemData()
		{
			KartExcData.Tune_ExcData();
			KartExcData.Plant_ExcData();
			KartExcData.Level_ExcData();
			KartExcData.Parts_ExcData();
			NewRider.Pet();
			NewRider.FlyingPet();
			NewRider.Character();
			NewRider.BonusCard();
			NewRider.HandGearL();
			NewRider.HeadBand();
			NewRider.Goggle();
			NewRider.Balloon();
			NewRider.Tachometer();
			NewRider.SlotItem();
			NewRider.slotBg();
			NewRider.MyRoom();
			NewRider.InitialCard();
			NewRider.Card();
			NewRider.ReplayTicket();
			NewRider.Uniform();
			NewRider.Decal();
			NewRider.Plate();
			NewRider.RidColor();
			NewRider.SkidMark();
			NewRider.Aura();
			NewRider.Dye();
			NewRider.Paint();
			NewRider.tuneEnginePatch();
			NewRider.tuneHandle();
			NewRider.tuneWheel();
			NewRider.tuneSupportKit();
			NewRider.socket();
			NewRider.tune();
			NewRider.enchantProtect();
			NewRider.enchantProtect2();
			NewRider.resetSocket();
			NewRider.XUniquePartsData();
			NewRider.XLegendPartsData();
			NewRider.XRarePartsData();
			NewRider.XNormalPartsData();
			NewRider.V1UniquePartsData();
			NewRider.V1LegendPartsData();
			NewRider.V1RarePartsData();
			NewRider.V1NormalPartsData();
			NewRider.V1EffectData();
			NewRider.V1BoosterEffectData();
			NewRider.partsPiece();
			NewRider.upgradeKit();
			NewRider.Kart();
			NewRider.NewRiderData();//라이더 인식
		}

		public static void NewRiderData()
		{
			using (OutPacket oPacket = new OutPacket("PrGetRider"))
			{
				oPacket.WriteByte(1);
				oPacket.WriteByte(0);
				oPacket.WriteString(SetRider.Nickname);
				oPacket.WriteShort(0);
				oPacket.WriteShort(0);
				oPacket.WriteShort(SetRider.Emblem1);
				oPacket.WriteShort(SetRider.Emblem2);
				oPacket.WriteShort(0);
				oPacket.WriteShort(SetRiderItem.Set_Character);
				oPacket.WriteShort(SetRiderItem.Set_Paint);
				oPacket.WriteShort(SetRiderItem.Set_Kart);
				oPacket.WriteShort(SetRiderItem.Set_Plate);
				oPacket.WriteShort(SetRiderItem.Set_Goggle);
				oPacket.WriteShort(SetRiderItem.Set_Balloon);
				oPacket.WriteShort(0);
				oPacket.WriteShort(SetRiderItem.Set_HeadBand);
				oPacket.WriteShort(0);
				oPacket.WriteShort(SetRiderItem.Set_HandGearL);
				oPacket.WriteShort(0);
				oPacket.WriteShort(SetRiderItem.Set_Uniform);
				oPacket.WriteShort(SetRiderItem.Set_Decal);
				oPacket.WriteShort(SetRiderItem.Set_Pet);
				oPacket.WriteShort(SetRiderItem.Set_FlyingPet);
				oPacket.WriteShort(SetRiderItem.Set_Aura);
				oPacket.WriteShort(SetRiderItem.Set_SkidMark);
				oPacket.WriteShort(0);
				oPacket.WriteShort(SetRiderItem.Set_RidColor);
				oPacket.WriteShort(SetRiderItem.Set_BonusCard);
				oPacket.WriteShort(0);
				var PlantKartAndSN = new { Kart = SetRiderItem.Set_Kart, SN = SetRiderItem.Set_KartSN };
				var plantList = KartExcData.PlantList;
				var existingPlant = plantList.FirstOrDefault(list => list[0] == PlantKartAndSN.Kart && list[1] == PlantKartAndSN.SN);
				if (existingPlant != null)
				{
					oPacket.WriteShort(existingPlant[3]);
					oPacket.WriteShort(existingPlant[7]);
					oPacket.WriteShort(existingPlant[5]);
					oPacket.WriteShort(existingPlant[9]);
				}
				else
				{
					oPacket.WriteShort(0);
					oPacket.WriteShort(0);
					oPacket.WriteShort(0);
					oPacket.WriteShort(0);
				}
				oPacket.WriteShort(0);
				oPacket.WriteShort(0);
				oPacket.WriteShort(SetRiderItem.Set_Tachometer);
				oPacket.WriteShort(SetRiderItem.Set_Dye);
				oPacket.WriteShort(SetRiderItem.Set_KartSN);
				oPacket.WriteByte(0);
				var ExcKartAndSN = new { Kart = SetRiderItem.Set_Kart, SN = SetRiderItem.Set_KartSN };
				var partsList = KartExcData.PartsList;
				var existingParts = partsList.FirstOrDefault(list => list[0] == ExcKartAndSN.Kart && list[1] == ExcKartAndSN.SN);
				if (existingParts != null)
				{
					oPacket.WriteShort(existingParts[14]);
					oPacket.WriteShort(existingParts[15]);
				}
				else
				{
					var levelList = KartExcData.LevelList;
					var existingLevel = levelList.FirstOrDefault(list => list[0] == ExcKartAndSN.Kart && list[1] == ExcKartAndSN.SN);
					if (existingLevel != null)
					{
						oPacket.WriteShort(7);
						oPacket.WriteShort(0);
					}
					else
					{
						oPacket.WriteShort(0);
						oPacket.WriteShort(0);
					}
				}
				oPacket.WriteShort(SetRiderItem.Set_slotBg);
				oPacket.WriteString(SetRider.Card);
				oPacket.WriteUInt(SetRider.Lucci);
				oPacket.WriteUInt(SetRider.RP);
				oPacket.WriteBytes(new byte[100]);
				RouterListener.MySession.Client.Send(oPacket);
			}
		}

		public static void Kart()
		{
			if (File.Exists(@"Profile\NewKart.xml"))
			{
				XmlDocument doc = new XmlDocument();
				doc.Load(@"Profile\NewKart.xml");
				if (!(doc.GetElementsByTagName("Kart") == null))
				{
					XmlNodeList lis = doc.GetElementsByTagName("Kart");
					List<List<short>> item = new List<List<short>>();
					foreach (XmlNode xn in lis)
					{
						XmlElement xe = (XmlElement)xn;
						short i = short.Parse(xe.GetAttribute("id"));
						short sn = short.Parse(xe.GetAttribute("sn"));
						short num = 1;
						List<short> add = new List<short> { i, sn, num };
						item.Add(add);
					}
					LoRpGetRiderItemPacket(3, item);
				}
			}
		}

		public static void Paint()
		{
			XmlDocument doc = new XmlDocument();
			doc.Load(@"Profile\Item.xml");
			if (!(doc.GetElementsByTagName("Paint") == null))
			{
				XmlNodeList lis = doc.GetElementsByTagName("Paint");
				List<List<short>> item = new List<List<short>>();
				foreach (XmlNode xn in lis)
				{
					XmlElement xe = (XmlElement)xn;
					short i = short.Parse(xe.GetAttribute("id"));
					short sn = 0;
					short num = 1;
					List<short> add = new List<short> { i, sn, num };
					item.Add(add);
				}
				LoRpGetRiderItemPacket(2, item);
			}
		}

		public static void Dye()
		{
			XmlDocument doc = new XmlDocument();
			doc.Load(@"Profile\Item.xml");
			if (!(doc.GetElementsByTagName("Dye") == null))
			{
				XmlNodeList lis = doc.GetElementsByTagName("Dye");
				List<List<short>> item = new List<List<short>>();
				foreach (XmlNode xn in lis)
				{
					XmlElement xe = (XmlElement)xn;
					short i = short.Parse(xe.GetAttribute("id"));
					short sn = 0;
					short num = 1;
					List<short> add = new List<short> { i, sn, num };
					item.Add(add);
				}
				LoRpGetRiderItemPacket(70, item);
			}
		}

		public static void Character()
		{
			XmlDocument doc = new XmlDocument();
			doc.Load(@"Profile\Item.xml");
			if (!(doc.GetElementsByTagName("Character") == null))
			{
				XmlNodeList lis = doc.GetElementsByTagName("Character");
				List<List<short>> item = new List<List<short>>();
				foreach (XmlNode xn in lis)
				{
					XmlElement xe = (XmlElement)xn;
					short i = short.Parse(xe.GetAttribute("id"));
					short sn = 0;
					short num = 1;
					List<short> add = new List<short> { i, sn, num };
					item.Add(add);
				}
				LoRpGetRiderItemPacket(1, item);
			}
		}

		public static void Pet()
		{
			XmlDocument doc = new XmlDocument();
			doc.Load(@"Profile\Item.xml");
			if (!(doc.GetElementsByTagName("Pet") == null))
			{
				XmlNodeList lis = doc.GetElementsByTagName("Pet");
				List<List<short>> item = new List<List<short>>();
				foreach (XmlNode xn in lis)
				{
					XmlElement xe = (XmlElement)xn;
					short i = short.Parse(xe.GetAttribute("id"));
					short sn = 0;
					short num = 1;
					List<short> add = new List<short> { i, sn, num };
					item.Add(add);
				}
				LoRpGetRiderItemPacket(21, item);
			}
		}

		public static void FlyingPet()
		{
			XmlDocument doc = new XmlDocument();
			doc.Load(@"Profile\Item.xml");
			if (!(doc.GetElementsByTagName("FlyingPet") == null))
			{
				XmlNodeList lis = doc.GetElementsByTagName("FlyingPet");
				List<List<short>> item = new List<List<short>>();
				foreach (XmlNode xn in lis)
				{
					XmlElement xe = (XmlElement)xn;
					short i = short.Parse(xe.GetAttribute("id"));
					short sn = 0;
					short num = 1;
					List<short> add = new List<short> { i, sn, num };
					item.Add(add);
				}
				LoRpGetRiderItemPacket(52, item);
			}
		}

		public static void Uniform()
		{
			XmlDocument doc = new XmlDocument();
			doc.Load(@"Profile\Item.xml");
			if (!(doc.GetElementsByTagName("Uniform") == null))
			{
				XmlNodeList lis = doc.GetElementsByTagName("Uniform");
				List<List<short>> item = new List<List<short>>();
				foreach (XmlNode xn in lis)
				{
					XmlElement xe = (XmlElement)xn;
					short i = short.Parse(xe.GetAttribute("id"));
					short sn = 0;
					short num = 1;
					List<short> add = new List<short> { i, sn, num };
					item.Add(add);
				}
				LoRpGetRiderItemPacket(18, item);
			}
		}

		public static void Decal()
		{
			XmlDocument doc = new XmlDocument();
			doc.Load(@"Profile\Item.xml");
			if (!(doc.GetElementsByTagName("Decal") == null))
			{
				XmlNodeList lis = doc.GetElementsByTagName("Decal");
				List<List<short>> item = new List<List<short>>();
				foreach (XmlNode xn in lis)
				{
					XmlElement xe = (XmlElement)xn;
					short i = short.Parse(xe.GetAttribute("id"));
					short sn = 0;
					short num = 1;
					List<short> add = new List<short> { i, sn, num };
					item.Add(add);
				}
				LoRpGetRiderItemPacket(20, item);
			}
		}

		public static void Aura()
		{
			XmlDocument doc = new XmlDocument();
			doc.Load(@"Profile\Item.xml");
			if (!(doc.GetElementsByTagName("Aura") == null))
			{
				XmlNodeList lis = doc.GetElementsByTagName("Aura");
				List<List<short>> item = new List<List<short>>();
				foreach (XmlNode xn in lis)
				{
					XmlElement xe = (XmlElement)xn;
					short i = short.Parse(xe.GetAttribute("id"));
					short sn = 0;
					short num = 1;
					List<short> add = new List<short> { i, sn, num };
					item.Add(add);
				}
				LoRpGetRiderItemPacket(26, item);
			}
		}

		public static void SkidMark()
		{
			XmlDocument doc = new XmlDocument();
			doc.Load(@"Profile\Item.xml");
			if (!(doc.GetElementsByTagName("SkidMark") == null))
			{
				XmlNodeList lis = doc.GetElementsByTagName("SkidMark");
				List<List<short>> item = new List<List<short>>();
				foreach (XmlNode xn in lis)
				{
					XmlElement xe = (XmlElement)xn;
					short i = short.Parse(xe.GetAttribute("id"));
					short sn = 0;
					short num = 1;
					List<short> add = new List<short> { i, sn, num };
					item.Add(add);
				}
				LoRpGetRiderItemPacket(27, item);
			}
		}

		public static void Plate()
		{
			XmlDocument doc = new XmlDocument();
			doc.Load(@"Profile\Item.xml");
			if (!(doc.GetElementsByTagName("Plate") == null))
			{
				XmlNodeList lis = doc.GetElementsByTagName("Plate");
				List<List<short>> item = new List<List<short>>();
				foreach (XmlNode xn in lis)
				{
					XmlElement xe = (XmlElement)xn;
					short i = short.Parse(xe.GetAttribute("id"));
					short sn = 0;
					short num = 1;
					List<short> add = new List<short> { i, sn, num };
					item.Add(add);
				}
				LoRpGetRiderItemPacket(4, item);
			}
		}

		public static void Balloon()
		{
			XmlDocument doc = new XmlDocument();
			doc.Load(@"Profile\Item.xml");
			if (!(doc.GetElementsByTagName("Balloon") == null))
			{
				XmlNodeList lis = doc.GetElementsByTagName("Balloon");
				List<List<short>> item = new List<List<short>>();
				foreach (XmlNode xn in lis)
				{
					XmlElement xe = (XmlElement)xn;
					short i = short.Parse(xe.GetAttribute("id"));
					short sn = 0;
					short num = SetRider.SlotChanger;
					List<short> add = new List<short> { i, sn, num };
					item.Add(add);
				}
				LoRpGetRiderItemPacket(9, item);
			}
		}

		public static void Goggle()
		{
			XmlDocument doc = new XmlDocument();
			doc.Load(@"Profile\Item.xml");
			if (!(doc.GetElementsByTagName("Goggle") == null))
			{
				XmlNodeList lis = doc.GetElementsByTagName("Goggle");
				List<List<short>> item = new List<List<short>>();
				foreach (XmlNode xn in lis)
				{
					XmlElement xe = (XmlElement)xn;
					short i = short.Parse(xe.GetAttribute("id"));
					short sn = 0;
					short num = 1;
					List<short> add = new List<short> { i, sn, num };
					item.Add(add);
				}
				LoRpGetRiderItemPacket(8, item);
			}
		}

		public static void HeadBand()
		{
			XmlDocument doc = new XmlDocument();
			doc.Load(@"Profile\Item.xml");
			if (!(doc.GetElementsByTagName("HeadBand") == null))
			{
				XmlNodeList lis = doc.GetElementsByTagName("HeadBand");
				List<List<short>> item = new List<List<short>>();
				foreach (XmlNode xn in lis)
				{
					XmlElement xe = (XmlElement)xn;
					short i = short.Parse(xe.GetAttribute("id"));
					short sn = 0;
					short num = 1;
					List<short> add = new List<short> { i, sn, num };
					item.Add(add);
				}
				LoRpGetRiderItemPacket(11, item);
			}
		}

		public static void HandGearL()
		{
			XmlDocument doc = new XmlDocument();
			doc.Load(@"Profile\Item.xml");
			if (!(doc.GetElementsByTagName("HandGearL") == null))
			{
				XmlNodeList lis = doc.GetElementsByTagName("HandGearL");
				List<List<short>> item = new List<List<short>>();
				foreach (XmlNode xn in lis)
				{
					XmlElement xe = (XmlElement)xn;
					short i = short.Parse(xe.GetAttribute("id"));
					short sn = 0;
					short num = 1;
					List<short> add = new List<short> { i, sn, num };
					item.Add(add);
				}
				LoRpGetRiderItemPacket(16, item);
			}
		}

		public static void MyRoom()
		{
			XmlDocument doc = new XmlDocument();
			doc.Load(@"Profile\Item.xml");
			if (!(doc.GetElementsByTagName("MyRoom") == null))
			{
				XmlNodeList lis = doc.GetElementsByTagName("MyRoom");
				List<List<short>> item = new List<List<short>>();
				foreach (XmlNode xn in lis)
				{
					XmlElement xe = (XmlElement)xn;
					short i = short.Parse(xe.GetAttribute("id"));
					short sn = 0;
					short num = 1;
					List<short> add = new List<short> { i, sn, num };
					item.Add(add);
				}
				LoRpGetRiderItemPacket(28, item);
			}
		}

		public static void RidColor()
		{
			XmlDocument doc = new XmlDocument();
			doc.Load(@"Profile\Item.xml");
			if (!(doc.GetElementsByTagName("RidColor") == null))
			{
				XmlNodeList lis = doc.GetElementsByTagName("RidColor");
				List<List<short>> item = new List<List<short>>();
				foreach (XmlNode xn in lis)
				{
					XmlElement xe = (XmlElement)xn;
					short i = short.Parse(xe.GetAttribute("id"));
					short sn = 0;
					short num = 1;
					List<short> add = new List<short> { i, sn, num };
					item.Add(add);
				}
				LoRpGetRiderItemPacket(31, item);
			}
		}

		public static void BonusCard()
		{
			XmlDocument doc = new XmlDocument();
			doc.Load(@"Profile\Item.xml");
			if (!(doc.GetElementsByTagName("BonusCard") == null))
			{
				XmlNodeList lis = doc.GetElementsByTagName("BonusCard");
				List<List<short>> item = new List<List<short>>();
				foreach (XmlNode xn in lis)
				{
					XmlElement xe = (XmlElement)xn;
					short i = short.Parse(xe.GetAttribute("id"));
					short sn = 0;
					short num = 1;
					List<short> add = new List<short> { i, sn, num };
					item.Add(add);
				}
				LoRpGetRiderItemPacket(32, item);
			}
		}

		public static void SlotItem()
		{
			XmlDocument doc = new XmlDocument();
			doc.Load(@"Profile\Item.xml");
			if (!(doc.GetElementsByTagName("SlotItem") == null))
			{
				XmlNodeList lis = doc.GetElementsByTagName("SlotItem");
				List<List<short>> item = new List<List<short>>();
				foreach (XmlNode xn in lis)
				{
					XmlElement xe = (XmlElement)xn;
					short i = short.Parse(xe.GetAttribute("id"));
					short sn = 0;
					short num = SetRider.SlotChanger;
					if (i == 3 || i == 4)
					{
						num = 1;
					}
					List<short> add = new List<short> { i, sn, num };
					item.Add(add);
				}
				LoRpGetRiderItemPacket(7, item);
			}
		}

		public static void slotBg()
		{
			XmlDocument doc = new XmlDocument();
			doc.Load(@"Profile\Item.xml");
			if (!(doc.GetElementsByTagName("slotBg") == null))
			{
				XmlNodeList lis = doc.GetElementsByTagName("slotBg");
				List<List<short>> item = new List<List<short>>();
				foreach (XmlNode xn in lis)
				{
					XmlElement xe = (XmlElement)xn;
					short i = short.Parse(xe.GetAttribute("id"));
					short sn = 0;
					short num = 1;
					List<short> add = new List<short> { i, sn, num };
					item.Add(add);
				}
				LoRpGetRiderItemPacket(71, item);
			}
		}

		public static void InitialCard()
		{
			XmlDocument doc = new XmlDocument();
			doc.Load(@"Profile\Item.xml");
			if (!(doc.GetElementsByTagName("InitialCard") == null))
			{
				XmlNodeList lis = doc.GetElementsByTagName("InitialCard");
				List<List<short>> item = new List<List<short>>();
				foreach (XmlNode xn in lis)
				{
					XmlElement xe = (XmlElement)xn;
					short i = short.Parse(xe.GetAttribute("id"));
					short sn = 0;
					short num = SetRider.SlotChanger;
					List<short> add = new List<short> { i, sn, num };
					item.Add(add);
				}
				LoRpGetRiderItemPacket(22, item);
			}
		}

		public static void Card()
		{
			XmlDocument doc = new XmlDocument();
			doc.Load(@"Profile\Item.xml");
			if (!(doc.GetElementsByTagName("Card") == null))
			{
				XmlNodeList lis = doc.GetElementsByTagName("Card");
				List<List<short>> item = new List<List<short>>();
				foreach (XmlNode xn in lis)
				{
					XmlElement xe = (XmlElement)xn;
					short i = short.Parse(xe.GetAttribute("id"));
					short sn = 0;
					short num = SetRider.SlotChanger;
					List<short> add = new List<short> { i, sn, num };
					item.Add(add);
				}
				LoRpGetRiderItemPacket(23, item);
			}
		}

		public static void ReplayTicket()
		{
			XmlDocument doc = new XmlDocument();
			doc.Load(@"Profile\Item.xml");
			if (!(doc.GetElementsByTagName("ReplayTicket") == null))
			{
				XmlNodeList lis = doc.GetElementsByTagName("ReplayTicket");
				List<List<short>> item = new List<List<short>>();
				foreach (XmlNode xn in lis)
				{
					XmlElement xe = (XmlElement)xn;
					short i = short.Parse(xe.GetAttribute("id"));
					short sn = 0;
					short num = 1;
					if (i == 1)
						num = SetRider.SlotChanger;
					List<short> add = new List<short> { i, sn, num };
					item.Add(add);
				}
				LoRpGetRiderItemPacket(13, item);
			}
		}

		public static void Tachometer()
		{
			XmlDocument doc = new XmlDocument();
			doc.Load(@"Profile\Item.xml");
			if (!(doc.GetElementsByTagName("Tachometer") == null))
			{
				XmlNodeList lis = doc.GetElementsByTagName("Tachometer");
				List<List<short>> item = new List<List<short>>();
				foreach (XmlNode xn in lis)
				{
					XmlElement xe = (XmlElement)xn;
					short i = short.Parse(xe.GetAttribute("id"));
					short sn = 0;
					short num = 1;
					List<short> add = new List<short> { i, sn, num };
					item.Add(add);
				}
				LoRpGetRiderItemPacket(61, item);
			}
		}

		public static void tuneEnginePatch()
		{
			using (OutPacket oPacket = new OutPacket("LoRpGetRiderItemPacket"))
			{
				oPacket.WriteInt(1);
				oPacket.WriteInt(1);
				oPacket.WriteInt(23);
				for (short i = 1; i <= 23; i++)
				{
					oPacket.WriteShort(43);
					oPacket.WriteShort(i);
					oPacket.WriteShort(0);
					oPacket.WriteShort(SetRider.SlotChanger);
					oPacket.WriteByte(0);
					oPacket.WriteByte(0);
					oPacket.WriteShort(-1);
					oPacket.WriteShort(0);
					oPacket.WriteByte(0);
					oPacket.WriteByte(0);
					oPacket.WriteShort(0);
				}
				RouterListener.MySession.Client.Send(oPacket);
			}
		}

		public static void tuneHandle()
		{
			using (OutPacket oPacket = new OutPacket("LoRpGetRiderItemPacket"))
			{
				oPacket.WriteInt(1);
				oPacket.WriteInt(1);
				oPacket.WriteInt(15);
				for (short i = 1; i <= 15; i++)
				{
					oPacket.WriteShort(44);
					oPacket.WriteShort(i);
					oPacket.WriteShort(0);
					oPacket.WriteShort(SetRider.SlotChanger);
					oPacket.WriteByte(0);
					oPacket.WriteByte(0);
					oPacket.WriteShort(-1);
					oPacket.WriteShort(0);
					oPacket.WriteByte(0);
					oPacket.WriteByte(0);
					oPacket.WriteShort(0);
				}
				RouterListener.MySession.Client.Send(oPacket);
			}
		}

		public static void tuneWheel()
		{
			using (OutPacket oPacket = new OutPacket("LoRpGetRiderItemPacket"))
			{
				oPacket.WriteInt(1);
				oPacket.WriteInt(1);
				oPacket.WriteInt(23);
				for (short i = 1; i <= 23; i++)
				{
					oPacket.WriteShort(45);
					oPacket.WriteShort(i);
					oPacket.WriteShort(0);
					oPacket.WriteShort(SetRider.SlotChanger);
					oPacket.WriteByte(0);
					oPacket.WriteByte(0);
					oPacket.WriteShort(-1);
					oPacket.WriteShort(0);
					oPacket.WriteByte(0);
					oPacket.WriteByte(0);
					oPacket.WriteShort(0);
				}
				RouterListener.MySession.Client.Send(oPacket);
			}
		}

		public static void tuneSupportKit()
		{
			using (OutPacket oPacket = new OutPacket("LoRpGetRiderItemPacket"))
			{
				oPacket.WriteInt(1);
				oPacket.WriteInt(1);
				oPacket.WriteInt(30);
				for (short i = 1; i <= 30; i++)
				{
					oPacket.WriteShort(46);
					oPacket.WriteShort(i);
					oPacket.WriteShort(0);
					oPacket.WriteShort(SetRider.SlotChanger);
					oPacket.WriteByte(0);
					oPacket.WriteByte(0);
					oPacket.WriteShort(-1);
					oPacket.WriteShort(0);
					oPacket.WriteByte(0);
					oPacket.WriteByte(0);
					oPacket.WriteShort(0);
				}
				RouterListener.MySession.Client.Send(oPacket);
			}
		}

		public static void socket()
		{
			using (OutPacket oPacket = new OutPacket("LoRpGetRiderItemPacket"))
			{
				oPacket.WriteInt(1);
				oPacket.WriteInt(1);
				oPacket.WriteInt(1);
				oPacket.WriteShort(37);
				oPacket.WriteShort(1);
				oPacket.WriteShort(0);
				oPacket.WriteShort(SetRider.SlotChanger);
				oPacket.WriteByte(0);
				oPacket.WriteByte(0);
				oPacket.WriteShort(-1);
				oPacket.WriteShort(0);
				oPacket.WriteByte(0);
				oPacket.WriteByte(0);
				oPacket.WriteShort(0);
				RouterListener.MySession.Client.Send(oPacket);
			}
		}

		public static void tune()
		{
			using (OutPacket oPacket = new OutPacket("LoRpGetRiderItemPacket"))
			{
				oPacket.WriteInt(1);
				oPacket.WriteInt(1);
				oPacket.WriteInt(4);
				for (short i = 3; i <= 6; i++)
				{
					oPacket.WriteShort(38);
					oPacket.WriteShort(i);
					oPacket.WriteShort(0);
					oPacket.WriteShort(SetRider.SlotChanger);
					oPacket.WriteByte(0);
					oPacket.WriteByte(0);
					oPacket.WriteShort(-1);
					oPacket.WriteShort(0);
					oPacket.WriteByte(0);
					oPacket.WriteByte(0);
					oPacket.WriteShort(0);
				}
				RouterListener.MySession.Client.Send(oPacket);
			}
		}

		public static void resetSocket()
		{
			using (OutPacket oPacket = new OutPacket("LoRpGetRiderItemPacket"))
			{
				oPacket.WriteInt(1);
				oPacket.WriteInt(1);
				oPacket.WriteInt(1);
				oPacket.WriteShort(39);
				oPacket.WriteShort(1);
				oPacket.WriteShort(0);
				oPacket.WriteShort(SetRider.SlotChanger);
				oPacket.WriteByte(0);
				oPacket.WriteByte(0);
				oPacket.WriteShort(-1);
				oPacket.WriteShort(0);
				oPacket.WriteByte(0);
				oPacket.WriteByte(0);
				oPacket.WriteShort(0);
				RouterListener.MySession.Client.Send(oPacket);
			}
		}

		public static void enchantProtect()
		{
			using (OutPacket oPacket = new OutPacket("LoRpGetRiderItemPacket"))
			{
				oPacket.WriteInt(1);
				oPacket.WriteInt(1);
				oPacket.WriteInt(1);
				oPacket.WriteShort(49);
				oPacket.WriteShort(1);
				oPacket.WriteShort(0);
				oPacket.WriteShort(SetRider.SlotChanger);
				oPacket.WriteByte(0);
				oPacket.WriteByte(0);
				oPacket.WriteShort(-1);
				oPacket.WriteShort(0);
				oPacket.WriteByte(0);
				oPacket.WriteByte(0);
				oPacket.WriteShort(0);
				RouterListener.MySession.Client.Send(oPacket);
			}
		}

		public static void enchantProtect2()
		{
			using (OutPacket oPacket = new OutPacket("LoRpGetRiderItemPacket"))
			{
				oPacket.WriteInt(1);
				oPacket.WriteInt(1);
				oPacket.WriteInt(1);
				oPacket.WriteShort(53);
				oPacket.WriteShort(1);
				oPacket.WriteShort(0);
				oPacket.WriteShort(SetRider.SlotChanger);
				oPacket.WriteByte(0);
				oPacket.WriteByte(0);
				oPacket.WriteShort(-1);
				oPacket.WriteShort(0);
				oPacket.WriteByte(0);
				oPacket.WriteByte(0);
				oPacket.WriteShort(0);
				RouterListener.MySession.Client.Send(oPacket);
			}
		}

		public static void partsPiece()
		{
			using (OutPacket oPacket = new OutPacket("LoRpGetRiderItemPacket"))
			{
				oPacket.WriteInt(1);
				oPacket.WriteInt(1);
				oPacket.WriteInt(2);
				for (short i = 1; i <= 2; i++)
				{
					oPacket.WriteShort(67);
					oPacket.WriteShort(i);
					oPacket.WriteShort(0);
					oPacket.WriteShort(SetRider.SlotChanger);
					oPacket.WriteByte(0);
					oPacket.WriteByte(0);
					oPacket.WriteShort(-1);
					oPacket.WriteShort(0);
					oPacket.WriteByte(0);
					oPacket.WriteByte(0);
					oPacket.WriteShort(0);
				}
				RouterListener.MySession.Client.Send(oPacket);
			}
		}

		public static void XUniquePartsData()
		{
			using (OutPacket oPacket = new OutPacket("LoRpGetRiderItemPacket"))
			{
				oPacket.WriteInt(1);
				oPacket.WriteInt(1);
				oPacket.WriteInt(40);
				byte Grade = 1;
				//-----------------------------------------------------------------X 유니크 파츠
				for (short i = 1053; i <= 1080; i += 3)
				{
					oPacket.WriteShort(63);
					oPacket.WriteShort(1);
					oPacket.WriteShort(0);
					oPacket.WriteShort(SetRider.SlotChanger);
					oPacket.WriteByte(0);
					oPacket.WriteByte(0);
					oPacket.WriteShort(-1);
					oPacket.WriteShort(-1);
					oPacket.WriteByte(1);
					oPacket.WriteByte(Grade);
					oPacket.WriteShort(i);
				}
				for (short i = 1053; i <= 1080; i += 3)
				{
					oPacket.WriteShort(64);
					oPacket.WriteShort(1);
					oPacket.WriteShort(0);
					oPacket.WriteShort(SetRider.SlotChanger);
					oPacket.WriteByte(0);
					oPacket.WriteByte(0);
					oPacket.WriteShort(-1);
					oPacket.WriteShort(-1);
					oPacket.WriteByte(1);
					oPacket.WriteByte(Grade);
					oPacket.WriteShort(i);
				}
				for (short i = 1053; i <= 1080; i += 3)
				{
					oPacket.WriteShort(65);
					oPacket.WriteShort(1);
					oPacket.WriteShort(0);
					oPacket.WriteShort(SetRider.SlotChanger);
					oPacket.WriteByte(0);
					oPacket.WriteByte(0);
					oPacket.WriteShort(-1);
					oPacket.WriteShort(-1);
					oPacket.WriteByte(1);
					oPacket.WriteByte(Grade);
					oPacket.WriteShort(i);
				}
				for (short i = 1053; i <= 1080; i += 3)
				{
					oPacket.WriteShort(66);
					oPacket.WriteShort(1);
					oPacket.WriteShort(0);
					oPacket.WriteShort(SetRider.SlotChanger);
					oPacket.WriteByte(0);
					oPacket.WriteByte(0);
					oPacket.WriteShort(-1);
					oPacket.WriteShort(-1);
					oPacket.WriteByte(1);
					oPacket.WriteByte(Grade);
					oPacket.WriteShort(i);
				}
				RouterListener.MySession.Client.Send(oPacket);
			}
		}

		public static void XLegendPartsData()
		{
			using (OutPacket oPacket = new OutPacket("LoRpGetRiderItemPacket"))
			{
				oPacket.WriteInt(1);
				oPacket.WriteInt(1);
				oPacket.WriteInt(40);
				byte Grade = 2;
				//-----------------------------------------------------------------X 레전드 파츠
				for (short i = 1005; i <= 1050; i += 5)
				{
					oPacket.WriteShort(63);
					oPacket.WriteShort(1);
					oPacket.WriteShort(0);
					oPacket.WriteShort(SetRider.SlotChanger);
					oPacket.WriteByte(0);
					oPacket.WriteByte(0);
					oPacket.WriteShort(-1);
					oPacket.WriteShort(-1);
					oPacket.WriteByte(1);
					oPacket.WriteByte(Grade);
					oPacket.WriteShort(i);
				}
				for (short i = 1005; i <= 1050; i += 5)
				{
					oPacket.WriteShort(64);
					oPacket.WriteShort(1);
					oPacket.WriteShort(0);
					oPacket.WriteShort(SetRider.SlotChanger);
					oPacket.WriteByte(0);
					oPacket.WriteByte(0);
					oPacket.WriteShort(-1);
					oPacket.WriteShort(-1);
					oPacket.WriteByte(1);
					oPacket.WriteByte(Grade);
					oPacket.WriteShort(i);
				}
				for (short i = 1005; i <= 1050; i += 5)
				{
					oPacket.WriteShort(65);
					oPacket.WriteShort(1);
					oPacket.WriteShort(0);
					oPacket.WriteShort(SetRider.SlotChanger);
					oPacket.WriteByte(0);
					oPacket.WriteByte(0);
					oPacket.WriteShort(-1);
					oPacket.WriteShort(-1);
					oPacket.WriteByte(1);
					oPacket.WriteByte(Grade);
					oPacket.WriteShort(i);
				}
				for (short i = 1005; i <= 1050; i += 5)
				{
					oPacket.WriteShort(66);
					oPacket.WriteShort(1);
					oPacket.WriteShort(0);
					oPacket.WriteShort(SetRider.SlotChanger);
					oPacket.WriteByte(0);
					oPacket.WriteByte(0);
					oPacket.WriteShort(-1);
					oPacket.WriteShort(-1);
					oPacket.WriteByte(1);
					oPacket.WriteByte(Grade);
					oPacket.WriteShort(i);
				}
				RouterListener.MySession.Client.Send(oPacket);
			}
		}

		public static void XRarePartsData()
		{
			using (OutPacket oPacket = new OutPacket("LoRpGetRiderItemPacket"))
			{
				oPacket.WriteInt(1);
				oPacket.WriteInt(1);
				oPacket.WriteInt(40);
				byte Grade = 3;
				//-----------------------------------------------------------------X 레어 파츠
				for (short i = 910; i <= 1000; i += 10)
				{
					oPacket.WriteShort(63);
					oPacket.WriteShort(1);
					oPacket.WriteShort(0);
					oPacket.WriteShort(SetRider.SlotChanger);
					oPacket.WriteByte(0);
					oPacket.WriteByte(0);
					oPacket.WriteShort(-1);
					oPacket.WriteShort(-1);
					oPacket.WriteByte(1);
					oPacket.WriteByte(Grade);
					oPacket.WriteShort(i);
				}
				for (short i = 910; i <= 1000; i += 10)
				{
					oPacket.WriteShort(64);
					oPacket.WriteShort(1);
					oPacket.WriteShort(0);
					oPacket.WriteShort(SetRider.SlotChanger);
					oPacket.WriteByte(0);
					oPacket.WriteByte(0);
					oPacket.WriteShort(-1);
					oPacket.WriteShort(-1);
					oPacket.WriteByte(1);
					oPacket.WriteByte(Grade);
					oPacket.WriteShort(i);
				}
				for (short i = 910; i <= 1000; i += 10)
				{
					oPacket.WriteShort(65);
					oPacket.WriteShort(1);
					oPacket.WriteShort(0);
					oPacket.WriteShort(SetRider.SlotChanger);
					oPacket.WriteByte(0);
					oPacket.WriteByte(0);
					oPacket.WriteShort(-1);
					oPacket.WriteShort(-1);
					oPacket.WriteByte(1);
					oPacket.WriteByte(Grade);
					oPacket.WriteShort(i);
				}
				for (short i = 910; i <= 1000; i += 10)
				{
					oPacket.WriteShort(66);
					oPacket.WriteShort(1);
					oPacket.WriteShort(0);
					oPacket.WriteShort(SetRider.SlotChanger);
					oPacket.WriteByte(0);
					oPacket.WriteByte(0);
					oPacket.WriteShort(-1);
					oPacket.WriteShort(-1);
					oPacket.WriteByte(1);
					oPacket.WriteByte(Grade);
					oPacket.WriteShort(i);
				}
				RouterListener.MySession.Client.Send(oPacket);
			}
		}

		public static void XNormalPartsData()
		{
			using (OutPacket oPacket = new OutPacket("LoRpGetRiderItemPacket"))
			{
				oPacket.WriteInt(1);
				oPacket.WriteInt(1);
				oPacket.WriteInt(40);
				byte Grade = 4;
				//-----------------------------------------------------------------X 일반 파츠
				for (short i = 810; i <= 900; i += 10)
				{
					oPacket.WriteShort(63);
					oPacket.WriteShort(1);
					oPacket.WriteShort(0);
					oPacket.WriteShort(SetRider.SlotChanger);
					oPacket.WriteByte(0);
					oPacket.WriteByte(0);
					oPacket.WriteShort(-1);
					oPacket.WriteShort(-1);
					oPacket.WriteByte(1);
					oPacket.WriteByte(Grade);
					oPacket.WriteShort(i);
				}
				for (short i = 810; i <= 900; i += 10)
				{
					oPacket.WriteShort(64);
					oPacket.WriteShort(1);
					oPacket.WriteShort(0);
					oPacket.WriteShort(SetRider.SlotChanger);
					oPacket.WriteByte(0);
					oPacket.WriteByte(0);
					oPacket.WriteShort(-1);
					oPacket.WriteShort(-1);
					oPacket.WriteByte(1);
					oPacket.WriteByte(Grade);
					oPacket.WriteShort(i);
				}
				for (short i = 810; i <= 900; i += 10)
				{
					oPacket.WriteShort(65);
					oPacket.WriteShort(1);
					oPacket.WriteShort(0);
					oPacket.WriteShort(SetRider.SlotChanger);
					oPacket.WriteByte(0);
					oPacket.WriteByte(0);
					oPacket.WriteShort(-1);
					oPacket.WriteShort(-1);
					oPacket.WriteByte(1);
					oPacket.WriteByte(Grade);
					oPacket.WriteShort(i);
				}
				for (short i = 810; i <= 900; i += 10)
				{
					oPacket.WriteShort(66);
					oPacket.WriteShort(1);
					oPacket.WriteShort(0);
					oPacket.WriteShort(SetRider.SlotChanger);
					oPacket.WriteByte(0);
					oPacket.WriteByte(0);
					oPacket.WriteShort(-1);
					oPacket.WriteShort(-1);
					oPacket.WriteByte(1);
					oPacket.WriteByte(Grade);
					oPacket.WriteShort(i);
				}
				RouterListener.MySession.Client.Send(oPacket);
			}
		}

		//-----------------------------------------------------------------------------------------------V1 파츠 관련
		public static void V1UniquePartsData()
		{
			using (OutPacket oPacket = new OutPacket("LoRpGetRiderItemPacket"))
			{
				oPacket.WriteInt(1);
				oPacket.WriteInt(1);
				oPacket.WriteInt(40);
				byte Grade = 1;
				//-----------------------------------------------------------------V1 유니크 파츠
				for (short i = 1153; i <= 1180; i += 3)
				{
					oPacket.WriteShort(63);
					oPacket.WriteShort(2);
					oPacket.WriteShort(0);
					oPacket.WriteShort(SetRider.SlotChanger);
					oPacket.WriteByte(0);
					oPacket.WriteByte(0);
					oPacket.WriteShort(-1);
					oPacket.WriteShort(-1);
					oPacket.WriteByte(1);
					oPacket.WriteByte(Grade);
					oPacket.WriteShort(i);
				}
				for (short i = 1053; i <= 1080; i += 3)
				{
					oPacket.WriteShort(64);
					oPacket.WriteShort(2);
					oPacket.WriteShort(0);
					oPacket.WriteShort(SetRider.SlotChanger);
					oPacket.WriteByte(0);
					oPacket.WriteByte(0);
					oPacket.WriteShort(-1);
					oPacket.WriteShort(-1);
					oPacket.WriteByte(1);
					oPacket.WriteByte(Grade);
					oPacket.WriteShort(i);
				}
				for (short i = 1153; i <= 1180; i += 3)
				{
					oPacket.WriteShort(65);
					oPacket.WriteShort(2);
					oPacket.WriteShort(0);
					oPacket.WriteShort(SetRider.SlotChanger);
					oPacket.WriteByte(0);
					oPacket.WriteByte(0);
					oPacket.WriteShort(-1);
					oPacket.WriteShort(-1);
					oPacket.WriteByte(1);
					oPacket.WriteByte(Grade);
					oPacket.WriteShort(i);
				}
				for (short i = 1053; i <= 1080; i += 3)
				{
					oPacket.WriteShort(66);
					oPacket.WriteShort(2);
					oPacket.WriteShort(0);
					oPacket.WriteShort(SetRider.SlotChanger);
					oPacket.WriteByte(0);
					oPacket.WriteByte(0);
					oPacket.WriteShort(-1);
					oPacket.WriteShort(-1);
					oPacket.WriteByte(1);
					oPacket.WriteByte(Grade);
					oPacket.WriteShort(i);
				}
				RouterListener.MySession.Client.Send(oPacket);
			}
		}

		public static void V1LegendPartsData()
		{
			using (OutPacket oPacket = new OutPacket("LoRpGetRiderItemPacket"))
			{
				oPacket.WriteInt(1);
				oPacket.WriteInt(1);
				oPacket.WriteInt(40);
				byte Grade = 2;
				//-----------------------------------------------------------------V1 레전드 파츠
				for (short i = 1105; i <= 1150; i += 5)
				{
					oPacket.WriteShort(63);
					oPacket.WriteShort(2);
					oPacket.WriteShort(0);
					oPacket.WriteShort(SetRider.SlotChanger);
					oPacket.WriteByte(0);
					oPacket.WriteByte(0);
					oPacket.WriteShort(-1);
					oPacket.WriteShort(-1);
					oPacket.WriteByte(1);
					oPacket.WriteByte(Grade);
					oPacket.WriteShort(i);
				}
				for (short i = 1005; i <= 1050; i += 5)
				{
					oPacket.WriteShort(64);
					oPacket.WriteShort(2);
					oPacket.WriteShort(0);
					oPacket.WriteShort(SetRider.SlotChanger);
					oPacket.WriteByte(0);
					oPacket.WriteByte(0);
					oPacket.WriteShort(-1);
					oPacket.WriteShort(-1);
					oPacket.WriteByte(1);
					oPacket.WriteByte(Grade);
					oPacket.WriteShort(i);
				}
				for (short i = 1105; i <= 1150; i += 5)
				{
					oPacket.WriteShort(65);
					oPacket.WriteShort(2);
					oPacket.WriteShort(0);
					oPacket.WriteShort(SetRider.SlotChanger);
					oPacket.WriteByte(0);
					oPacket.WriteByte(0);
					oPacket.WriteShort(-1);
					oPacket.WriteShort(-1);
					oPacket.WriteByte(1);
					oPacket.WriteByte(Grade);
					oPacket.WriteShort(i);
				}
				for (short i = 1005; i <= 1050; i += 5)
				{
					oPacket.WriteShort(66);
					oPacket.WriteShort(2);
					oPacket.WriteShort(0);
					oPacket.WriteShort(SetRider.SlotChanger);
					oPacket.WriteByte(0);
					oPacket.WriteByte(0);
					oPacket.WriteShort(-1);
					oPacket.WriteShort(-1);
					oPacket.WriteByte(1);
					oPacket.WriteByte(Grade);
					oPacket.WriteShort(i);
				}
				RouterListener.MySession.Client.Send(oPacket);
			}
		}

		public static void V1RarePartsData()
		{
			using (OutPacket oPacket = new OutPacket("LoRpGetRiderItemPacket"))
			{
				oPacket.WriteInt(1);
				oPacket.WriteInt(1);
				oPacket.WriteInt(40);
				byte Grade = 3;
				//-----------------------------------------------------------------V1 레어 파츠
				for (short i = 1010; i <= 1100; i += 10)
				{
					oPacket.WriteShort(63);
					oPacket.WriteShort(2);
					oPacket.WriteShort(0);
					oPacket.WriteShort(SetRider.SlotChanger);
					oPacket.WriteByte(0);
					oPacket.WriteByte(0);
					oPacket.WriteShort(-1);
					oPacket.WriteShort(-1);
					oPacket.WriteByte(1);
					oPacket.WriteByte(Grade);
					oPacket.WriteShort(i);
				}
				for (short i = 910; i <= 1000; i += 10)
				{
					oPacket.WriteShort(64);
					oPacket.WriteShort(2);
					oPacket.WriteShort(0);
					oPacket.WriteShort(SetRider.SlotChanger);
					oPacket.WriteByte(0);
					oPacket.WriteByte(0);
					oPacket.WriteShort(-1);
					oPacket.WriteShort(-1);
					oPacket.WriteByte(1);
					oPacket.WriteByte(Grade);
					oPacket.WriteShort(i);
				}
				for (short i = 1010; i <= 1100; i += 10)
				{
					oPacket.WriteShort(65);
					oPacket.WriteShort(2);
					oPacket.WriteShort(0);
					oPacket.WriteShort(SetRider.SlotChanger);
					oPacket.WriteByte(0);
					oPacket.WriteByte(0);
					oPacket.WriteShort(-1);
					oPacket.WriteShort(-1);
					oPacket.WriteByte(1);
					oPacket.WriteByte(Grade);
					oPacket.WriteShort(i);
				}
				for (short i = 910; i <= 1000; i += 10)
				{
					oPacket.WriteShort(66);
					oPacket.WriteShort(2);
					oPacket.WriteShort(0);
					oPacket.WriteShort(SetRider.SlotChanger);
					oPacket.WriteByte(0);
					oPacket.WriteByte(0);
					oPacket.WriteShort(-1);
					oPacket.WriteShort(-1);
					oPacket.WriteByte(1);
					oPacket.WriteByte(Grade);
					oPacket.WriteShort(i);
				}
				RouterListener.MySession.Client.Send(oPacket);
			}
		}

		public static void V1NormalPartsData()
		{
			using (OutPacket oPacket = new OutPacket("LoRpGetRiderItemPacket"))
			{
				oPacket.WriteInt(1);
				oPacket.WriteInt(1);
				oPacket.WriteInt(40);
				byte Grade = 4;
				//-----------------------------------------------------------------V1 일반 파츠
				for (short i = 910; i <= 1000; i += 10)
				{
					oPacket.WriteShort(63);
					oPacket.WriteShort(2);
					oPacket.WriteShort(0);
					oPacket.WriteShort(SetRider.SlotChanger);
					oPacket.WriteByte(0);
					oPacket.WriteByte(0);
					oPacket.WriteShort(-1);
					oPacket.WriteShort(-1);
					oPacket.WriteByte(1);
					oPacket.WriteByte(Grade);
					oPacket.WriteShort(i);
				}
				for (short i = 810; i <= 900; i += 10)
				{
					oPacket.WriteShort(64);
					oPacket.WriteShort(2);
					oPacket.WriteShort(0);
					oPacket.WriteShort(SetRider.SlotChanger);
					oPacket.WriteByte(0);
					oPacket.WriteByte(0);
					oPacket.WriteShort(-1);
					oPacket.WriteShort(-1);
					oPacket.WriteByte(1);
					oPacket.WriteByte(Grade);
					oPacket.WriteShort(i);
				}
				for (short i = 910; i <= 1000; i += 10)
				{
					oPacket.WriteShort(65);
					oPacket.WriteShort(2);
					oPacket.WriteShort(0);
					oPacket.WriteShort(SetRider.SlotChanger);
					oPacket.WriteByte(0);
					oPacket.WriteByte(0);
					oPacket.WriteShort(-1);
					oPacket.WriteShort(-1);
					oPacket.WriteByte(1);
					oPacket.WriteByte(Grade);
					oPacket.WriteShort(i);
				}
				for (short i = 810; i <= 900; i += 10)
				{
					oPacket.WriteShort(66);
					oPacket.WriteShort(2);
					oPacket.WriteShort(0);
					oPacket.WriteShort(SetRider.SlotChanger);
					oPacket.WriteByte(0);
					oPacket.WriteByte(0);
					oPacket.WriteShort(-1);
					oPacket.WriteShort(-1);
					oPacket.WriteByte(1);
					oPacket.WriteByte(Grade);
					oPacket.WriteShort(i);
				}
				RouterListener.MySession.Client.Send(oPacket);
			}
		}

		public static void V1EffectData()
		{
			XmlDocument doc = new XmlDocument();
			doc.Load(@"Profile\Item.xml");
			if (!(doc.GetElementsByTagName("V1EffectData") == null))
			{
				XmlNodeList lis = doc.GetElementsByTagName("V1EffectData");
				List<List<short>> item = new List<List<short>>();
				foreach (XmlNode xn in lis)
				{
					XmlElement xe = (XmlElement)xn;
					short i = short.Parse(xe.GetAttribute("id"));
					short sn = 0;
					short num = SetRider.SlotChanger;
					List<short> add = new List<short> { i, sn, num };
					item.Add(add);
				}
				LoRpGetRiderItemPacket(68, item);
			}
		}

		public static void V1BoosterEffectData()
		{
			XmlDocument doc = new XmlDocument();
			doc.Load(@"Profile\Item.xml");
			if (!(doc.GetElementsByTagName("V1BoosterEffectData") == null))
			{
				XmlNodeList lis = doc.GetElementsByTagName("V1BoosterEffectData");
				List<List<short>> item = new List<List<short>>();
				foreach (XmlNode xn in lis)
				{
					XmlElement xe = (XmlElement)xn;
					short i = short.Parse(xe.GetAttribute("id"));
					short sn = 0;
					short num = SetRider.SlotChanger;
					List<short> add = new List<short> { i, sn, num };
					item.Add(add);
				}
				LoRpGetRiderItemPacket(69, item);
			}
		}

		public static void upgradeKit()
		{
			XmlDocument doc = new XmlDocument();
			doc.Load(@"Profile\Item.xml");
			if (!(doc.GetElementsByTagName("upgradeKit") == null))
			{
				XmlNodeList lis = doc.GetElementsByTagName("upgradeKit");
				List<List<short>> item = new List<List<short>>();
				foreach (XmlNode xn in lis)
				{
					XmlElement xe = (XmlElement)xn;
					short i = short.Parse(xe.GetAttribute("id"));
					short sn = 0;
					short num = 1;
					List<short> add = new List<short> { i, sn, num };
					item.Add(add);
				}
				LoRpGetRiderItemPacket(14, item);
			}
		}

		public static void LoRpGetRiderItemPacket(short itemCat, List<List<short>> item)
		{
			int range = 100;//分批次数
			int times = item.Count / range + (item.Count % range > 0 ? 1 : 0);
			for (int i = 0; i < times; i++)
			{
				var tempList = item.GetRange(i * range, (i + 1) * range > item.Count ? (item.Count - i * range) : range);
				using (OutPacket oPacket = new OutPacket("LoRpGetRiderItemPacket"))
				{
					oPacket.WriteInt(1);
					oPacket.WriteInt(1);
					oPacket.WriteInt(tempList.Count);
					for (int f = 0; f < tempList.Count; f++)
					{
						oPacket.WriteShort(itemCat);
						oPacket.WriteShort(tempList[f][0]);
						oPacket.WriteShort(tempList[f][1]);
						oPacket.WriteShort(tempList[f][2]);
						oPacket.WriteByte((byte)((Program.PreventItem ? 1 : 0)));
						oPacket.WriteByte(0);
						oPacket.WriteShort(-1);
						oPacket.WriteShort(0);
						oPacket.WriteByte(0);
						oPacket.WriteByte(0);
						oPacket.WriteShort(0);
					}
					RouterListener.MySession.Client.Send(oPacket);
				}
			}
		}
	}
}
