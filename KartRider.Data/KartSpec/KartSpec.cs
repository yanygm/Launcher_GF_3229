using System;
using System.Xml;
using KartRider;

namespace KartSpec
{
	public class Kart_Spec
	{
		public static void KartAll()
		{
			XmlDocument doc = new XmlDocument();
			doc.Load(@"Profile\KartSpec.xml");
			if (!(doc.GetElementsByTagName("id" + StartGameData.Kart_id.ToString()) == null))
			{
				XmlNodeList lis = doc.GetElementsByTagName("id" + StartGameData.Kart_id.ToString());
				foreach (XmlNode xn in lis)
				{
					XmlElement xe = (XmlElement)xn;
					Kart.draftMulAccelFactor = float.Parse(xe.GetAttribute("draftMulAccelFactor"));
					Kart.draftTick = int.Parse(xe.GetAttribute("draftTick"));
					Kart.driftBoostMulAccelFactor = float.Parse(xe.GetAttribute("driftBoostMulAccelFactor"));
					Kart.driftBoostTick = int.Parse(xe.GetAttribute("driftBoostTick"));
					Kart.chargeBoostBySpeed = float.Parse(xe.GetAttribute("chargeBoostBySpeed"));
					Kart.SpeedSlotCapacity = byte.Parse(xe.GetAttribute("SpeedSlotCapacity"));
					Kart.ItemSlotCapacity = byte.Parse(xe.GetAttribute("ItemSlotCapacity"));
					Kart.SpecialSlotCapacity = byte.Parse(xe.GetAttribute("SpecialSlotCapacity"));
					Kart.UseTransformBooster = byte.Parse(xe.GetAttribute("UseTransformBooster"));
					Kart.motorcycleType = byte.Parse(xe.GetAttribute("motorcycleType"));
					Kart.BikeRearWheel = byte.Parse(xe.GetAttribute("BikeRearWheel"));
					Kart.Mass = float.Parse(xe.GetAttribute("Mass"));
					Kart.AirFriction = float.Parse(xe.GetAttribute("AirFriction"));
					Kart.DragFactor = float.Parse(xe.GetAttribute("DragFactor"));
					Kart.ForwardAccelForce = float.Parse(xe.GetAttribute("ForwardAccelForce"));
					Kart.BackwardAccelForce = float.Parse(xe.GetAttribute("BackwardAccelForce"));
					Kart.GripBrakeForce = float.Parse(xe.GetAttribute("GripBrakeForce"));
					Kart.SlipBrakeForce = float.Parse(xe.GetAttribute("SlipBrakeForce"));
					Kart.MaxSteerAngle = float.Parse(xe.GetAttribute("MaxSteerAngle"));
					Kart.SteerConstraint = float.Parse(xe.GetAttribute("SteerConstraint"));
					Kart.FrontGripFactor = float.Parse(xe.GetAttribute("FrontGripFactor"));
					Kart.RearGripFactor = float.Parse(xe.GetAttribute("RearGripFactor"));
					Kart.DriftTriggerFactor = float.Parse(xe.GetAttribute("DriftTriggerFactor"));
					Kart.DriftTriggerTime = float.Parse(xe.GetAttribute("DriftTriggerTime"));
					Kart.DriftSlipFactor = float.Parse(xe.GetAttribute("DriftSlipFactor"));
					Kart.DriftEscapeForce = float.Parse(xe.GetAttribute("DriftEscapeForce"));
					Kart.CornerDrawFactor = float.Parse(xe.GetAttribute("CornerDrawFactor"));
					Kart.DriftLeanFactor = float.Parse(xe.GetAttribute("DriftLeanFactor"));
					Kart.SteerLeanFactor = float.Parse(xe.GetAttribute("SteerLeanFactor"));
					Kart.DriftMaxGauge = float.Parse(xe.GetAttribute("DriftMaxGauge"));
					Kart.NormalBoosterTime = float.Parse(xe.GetAttribute("NormalBoosterTime"));
					Kart.ItemBoosterTime = float.Parse(xe.GetAttribute("ItemBoosterTime"));
					Kart.TeamBoosterTime = float.Parse(xe.GetAttribute("TeamBoosterTime"));
					Kart.AnimalBoosterTime = float.Parse(xe.GetAttribute("AnimalBoosterTime"));
					Kart.SuperBoosterTime = float.Parse(xe.GetAttribute("SuperBoosterTime"));
					Kart.TransAccelFactor = float.Parse(xe.GetAttribute("TransAccelFactor"));
					Kart.BoostAccelFactor = float.Parse(xe.GetAttribute("BoostAccelFactor"));
					Kart.StartBoosterTimeItem = float.Parse(xe.GetAttribute("StartBoosterTimeItem"));
					Kart.StartBoosterTimeSpeed = float.Parse(xe.GetAttribute("StartBoosterTimeSpeed"));
					Kart.StartForwardAccelForceItem = float.Parse(xe.GetAttribute("StartForwardAccelForceItem"));
					Kart.StartForwardAccelForceSpeed = float.Parse(xe.GetAttribute("StartForwardAccelForceSpeed"));
					Kart.DriftGaguePreservePercent = float.Parse(xe.GetAttribute("DriftGaguePreservePercent"));
					Kart.UseExtendedAfterBooster = byte.Parse(xe.GetAttribute("UseExtendedAfterBooster"));
					Kart.BoostAccelFactorOnlyItem = float.Parse(xe.GetAttribute("BoostAccelFactorOnlyItem"));
					Kart.antiCollideBalance = float.Parse(xe.GetAttribute("antiCollideBalance"));
					Kart.dualBoosterSetAuto = byte.Parse(xe.GetAttribute("dualBoosterSetAuto"));
					Kart.dualBoosterTickMin = int.Parse(xe.GetAttribute("dualBoosterTickMin"));
					Kart.dualBoosterTickMax = int.Parse(xe.GetAttribute("dualBoosterTickMax"));
					Kart.dualMulAccelFactor = float.Parse(xe.GetAttribute("dualMulAccelFactor"));
					Kart.dualTransLowSpeed = float.Parse(xe.GetAttribute("dualTransLowSpeed"));
					Kart.PartsEngineLock = byte.Parse(xe.GetAttribute("PartsEngineLock"));
					Kart.PartsWheelLock = byte.Parse(xe.GetAttribute("PartsWheelLock"));
					Kart.PartsSteeringLock = byte.Parse(xe.GetAttribute("PartsSteeringLock"));
					Kart.PartsBoosterLock = byte.Parse(xe.GetAttribute("PartsBoosterLock"));
					Kart.PartsCoatingLock = byte.Parse(xe.GetAttribute("PartsCoatingLock"));
					Kart.PartsTailLampLock = byte.Parse(xe.GetAttribute("PartsTailLampLock"));
					Kart.chargeInstAccelGaugeByBoost = float.Parse(xe.GetAttribute("chargeInstAccelGaugeByBoost"));
					Kart.chargeInstAccelGaugeByGrip = float.Parse(xe.GetAttribute("chargeInstAccelGaugeByGrip"));
					Kart.chargeInstAccelGaugeByWall = float.Parse(xe.GetAttribute("chargeInstAccelGaugeByWall"));
					Kart.instAccelFactor = float.Parse(xe.GetAttribute("instAccelFactor"));
					Kart.instAccelGaugeCooldownTime = int.Parse(xe.GetAttribute("instAccelGaugeCooldownTime"));
					Kart.instAccelGaugeLength = float.Parse(xe.GetAttribute("instAccelGaugeLength"));
					Kart.instAccelGaugeMinUsable = float.Parse(xe.GetAttribute("instAccelGaugeMinUsable"));
					Kart.instAccelGaugeMinVelBound = float.Parse(xe.GetAttribute("instAccelGaugeMinVelBound"));
					Kart.instAccelGaugeMinVelLoss = float.Parse(xe.GetAttribute("instAccelGaugeMinVelLoss"));
					Kart.useExtendedAfterBoosterMore = byte.Parse(xe.GetAttribute("useExtendedAfterBoosterMore"));
					Kart.wallCollGaugeCooldownTime = int.Parse(xe.GetAttribute("wallCollGaugeCooldownTime"));
					Kart.wallCollGaugeMaxVelLoss = float.Parse(xe.GetAttribute("wallCollGaugeMaxVelLoss"));
					Kart.wallCollGaugeMinVelBound = float.Parse(xe.GetAttribute("wallCollGaugeMinVelBound"));
					Kart.wallCollGaugeMinVelLoss = float.Parse(xe.GetAttribute("wallCollGaugeMinVelLoss"));
					Kart.modelMaxX = float.Parse(xe.GetAttribute("modelMaxX"));
					Kart.modelMaxY = float.Parse(xe.GetAttribute("modelMaxY"));
				}
			}
			else
			{
				GameType.StartType = 0;
			}
			StartGameData.Start_KartSpac();
		}
	}
}
