using System;
using KartRider.IO;
using ExcData;
using Set_Data;

namespace KartRider
{
	public class StartGameData
	{
		public static int StartTimeAttack_Unk1 = 0;
		public static int StartTimeAttack_Unk2 = 0;
		public static uint StartTimeAttack_Track = 0;
		public static byte StartTimeAttack_SpeedType = 0;
		public static byte StartTimeAttack_GameType = 0;
		public static short Kart_id = 0;
		public static short FlyingPet_id = 0;
		public static byte StartTimeAttack_StartType = 0;
		public static int StartTimeAttack_Unk3 = 0;
		public static int StartTimeAttack_Unk4 = 0;
		public static byte StartTimeAttack_Unk5 = 0;
		public static byte StartTimeAttack_RankingTimaAttackType = 0;
		public static byte StartTimeAttack_TimaAttackMpdeType = 0;
		public static int StartTimeAttack_TimaAttackMpde = 0;
		public static byte StartTimeAttack_RandomTrackGameType = 0;

		public static void Start_KartSpac()
		{
			Console.WriteLine("SpeedType: {0}, Kart_id: {1}, FlyingPet_id: {2}", StartGameData.StartTimeAttack_SpeedType, StartGameData.Kart_id, StartGameData.FlyingPet_id);
			if (GameType.StartType == 1)
			{
				Console.WriteLine("故事模式");
				StartGameData.PrKartSpec();
			}
			else if (GameType.StartType == 2)
			{
				Console.WriteLine("挑战者");
				StartGameData.PrchallengerKartSpec();
			}
			else if (GameType.StartType == 3)
			{
				Console.WriteLine("排行计时");
				if (StartGameData.StartTimeAttack_StartType == 0)
				{
					StartGameData.PrStartTimeAttack();
				}
				else
				{
					StartGameData.PrStartTimeAttack_QuestType();
				}
			}
			else
			{
				GameSupport.OnDisconnect();
			}
		}

		public static void PrStartTimeAttack()
		{
			using (OutPacket oPacket = new OutPacket("PrStartTimeAttack"))
			{
				oPacket.WriteInt(StartGameData.StartTimeAttack_Unk1);
				oPacket.WriteInt(0);
				//------------------------------------------------------------------------KartSpac Start
				GetKartSpac(oPacket);
				//------------------------------------------------------------------------KartSpac End
				oPacket.WriteByte(0);
				oPacket.WriteInt(0);
				oPacket.WriteInt(0);
				oPacket.WriteUInt(SetRider.Lucci);
				oPacket.WriteUInt(SetRider.Koin);
				oPacket.WriteUInt(StartGameData.StartTimeAttack_Track);
				RouterListener.MySession.Client.Send(oPacket);
			}
			StartGameData.KartSpecLog();
		}

		public static void PrchallengerKartSpec()
		{
			using (OutPacket oPacket = new OutPacket("PrchallengerKartSpec"))
			{
				oPacket.WriteByte(1);
				//------------------------------------------------------------------------KartSpac Start
				GetKartSpac(oPacket);
				//------------------------------------------------------------------------KartSpac End
				oPacket.WriteInt(0);
				oPacket.WriteByte(0);
				RouterListener.MySession.Client.Send(oPacket);
			}
			StartGameData.KartSpecLog();
		}

		public static void PrKartSpec()
		{
			using (OutPacket oPacket = new OutPacket("PrKartSpec"))
			{
				oPacket.WriteByte(1);
				//------------------------------------------------------------------------KartSpac Start
				GetDefaultSpac(oPacket);
				//------------------------------------------------------------------------KartSpac End
				oPacket.WriteByte(0);
				RouterListener.MySession.Client.Send(oPacket);
			}
		}

		public static void PrStartTimeAttack_QuestType()
		{
			using (OutPacket oPacket = new OutPacket("PrStartTimeAttack"))
			{
				oPacket.WriteInt(StartGameData.StartTimeAttack_Unk1);
				oPacket.WriteInt(0);
				//------------------------------------------------------------------------KartSpac Start
				GetDefaultSpac(oPacket);
				//------------------------------------------------------------------------KartSpac End
				oPacket.WriteByte(0);
				oPacket.WriteInt(0);
				oPacket.WriteInt(0);
				oPacket.WriteUInt(SetRider.Lucci);
				oPacket.WriteUInt(SetRider.Koin);
				oPacket.WriteUInt(StartGameData.StartTimeAttack_Track);
				RouterListener.MySession.Client.Send(oPacket);
			}
		}

		public static void GetKartSpac(OutPacket oPacket)
		{
			float DriftEscapeForce = FlyingPet.DriftEscapeForce + TuneSpec.Tune_DriftEscapeForce + TuneSpec.Plant45_DriftEscapeForce + TuneSpec.KartLevel_DriftEscapeForce + SpeedPatch.DriftEscapeForce;
			float NormalBoosterTime = FlyingPet.NormalBoosterTime + TuneSpec.Tune_NormalBoosterTime + TuneSpec.Plant46_NormalBoosterTime;
			float TransAccelFactor = TuneSpec.Tune_TransAccelFactor + TuneSpec.Plant43_TransAccelFactor + TuneSpec.KartLevel_TransAccelFactor + SpeedPatch.TransAccelFactor;
			//------------------------------------------------------------------------KartSpac Start
			oPacket.WriteEncFloat(Kart.draftMulAccelFactor);
			oPacket.WriteEncInt(Kart.draftTick);
			oPacket.WriteEncFloat(Kart.driftBoostMulAccelFactor);
			oPacket.WriteEncInt(Kart.driftBoostTick);
			oPacket.WriteEncFloat(Kart.chargeBoostBySpeed);
			oPacket.WriteEncByte((byte)(Kart.SpeedSlotCapacity + TuneSpec.Plant46_SpeedSlotCapacity));
			oPacket.WriteEncByte((byte)(Kart.ItemSlotCapacity + TuneSpec.Plant46_ItemSlotCapacity));
			oPacket.WriteEncByte(Kart.SpecialSlotCapacity);
			oPacket.WriteEncByte(Kart.UseTransformBooster);
			oPacket.WriteEncByte(Kart.motorcycleType);
			oPacket.WriteEncByte(Kart.BikeRearWheel);
			oPacket.WriteEncFloat(Kart.Mass);
			oPacket.WriteEncFloat(Kart.AirFriction);
			oPacket.WriteEncFloat(SpeedType.DragFactor + Kart.DragFactor + FlyingPet.DragFactor + SpeedPatch.DragFactor + TuneSpec.Tune_DragFactor + TuneSpec.Plant43_DragFactor + TuneSpec.Plant45_DragFactor + TuneSpec.KartLevel_DragFactor);
			oPacket.WriteEncFloat(SpeedType.ForwardAccelForce + Kart.ForwardAccelForce + FlyingPet.ForwardAccelForce + TuneSpec.Tune_ForwardAccel + TuneSpec.Plant43_ForwardAccel + TuneSpec.Plant46_ForwardAccel + TuneSpec.KartLevel_ForwardAccel + SpeedPatch.ForwardAccelForce);
			oPacket.WriteEncFloat(SpeedType.BackwardAccelForce + Kart.BackwardAccelForce);
			oPacket.WriteEncFloat(SpeedType.GripBrakeForce + Kart.GripBrakeForce + TuneSpec.Plant44_GripBrake + TuneSpec.Plant46_GripBrake);
			oPacket.WriteEncFloat(SpeedType.SlipBrakeForce + Kart.SlipBrakeForce + TuneSpec.Plant44_SlipBrake + TuneSpec.Plant45_SlipBrake + TuneSpec.Plant46_SlipBrake);
			oPacket.WriteEncFloat(Kart.MaxSteerAngle);
			if (TuneSpec.PartSpec_SteerConstraint == 0f)
			{
				oPacket.WriteEncFloat(SpeedType.SteerConstraint + Kart.SteerConstraint + TuneSpec.Plant44_SteerConstraint + TuneSpec.KartLevel_SteerConstraint);
			}
			else
			{
				oPacket.WriteEncFloat(TuneSpec.PartSpec_SteerConstraint + SpeedType.AddSpec_SteerConstraint + TuneSpec.Plant44_SteerConstraint + TuneSpec.KartLevel_SteerConstraint);
			}
			oPacket.WriteEncFloat(Kart.FrontGripFactor + TuneSpec.Plant44_FrontGripFactor);
			oPacket.WriteEncFloat(Kart.RearGripFactor + TuneSpec.Plant44_RearGripFactor);
			oPacket.WriteEncFloat(Kart.DriftTriggerFactor);
			oPacket.WriteEncFloat(Kart.DriftTriggerTime);
			oPacket.WriteEncFloat(Kart.DriftSlipFactor + TuneSpec.Plant46_DriftSlipFactor);
			if (TuneSpec.PartSpec_DriftEscapeForce == 0f)
			{
				oPacket.WriteEncFloat(SpeedType.DriftEscapeForce + Kart.DriftEscapeForce + DriftEscapeForce);
			}
			else
			{
				oPacket.WriteEncFloat(TuneSpec.PartSpec_DriftEscapeForce + SpeedType.AddSpec_DriftEscapeForce + DriftEscapeForce);
			}
			oPacket.WriteEncFloat(SpeedType.CornerDrawFactor + Kart.CornerDrawFactor + FlyingPet.CornerDrawFactor + TuneSpec.Tune_CornerDrawFactor + TuneSpec.Plant44_CornerDrawFactor + TuneSpec.Plant45_CornerDrawFactor + TuneSpec.KartLevel_CornerDrawFactor + SpeedPatch.CornerDrawFactor);
			oPacket.WriteEncFloat(Kart.DriftLeanFactor);
			oPacket.WriteEncFloat(Kart.SteerLeanFactor);
			if (StartGameData.StartTimeAttack_SpeedType == 4 || StartGameData.StartTimeAttack_SpeedType == 6)
			{
				oPacket.WriteEncFloat(GameType.S4_DriftMaxGauge);
			}
			else
			{
				oPacket.WriteEncFloat(SpeedType.DriftMaxGauge + Kart.DriftMaxGauge + TuneSpec.Tune_DriftMaxGauge + TuneSpec.Plant45_DriftMaxGauge + TuneSpec.Plant46_DriftMaxGauge + SpeedPatch.DriftMaxGauge);
			}
			if (StartGameData.StartTimeAttack_SpeedType == 6)
			{
				oPacket.WriteEncFloat(GameType.S6_BoosterTime);
			}
			else
			{
				if (TuneSpec.PartSpec_NormalBoosterTime == 0f)
				{
					oPacket.WriteEncFloat(Kart.NormalBoosterTime + NormalBoosterTime);
				}
				else
				{
					oPacket.WriteEncFloat(TuneSpec.PartSpec_NormalBoosterTime + NormalBoosterTime);
				}
			}
			oPacket.WriteEncFloat(Kart.ItemBoosterTime + FlyingPet.ItemBoosterTime);
			if (StartGameData.StartTimeAttack_SpeedType == 6)
			{
				oPacket.WriteEncFloat(GameType.S6_BoosterTime);
			}
			else
			{
				oPacket.WriteEncFloat(Kart.TeamBoosterTime + FlyingPet.TeamBoosterTime + TuneSpec.Tune_TeamBoosterTime + TuneSpec.Plant46_TeamBoosterTime);
			}
			oPacket.WriteEncFloat(Kart.AnimalBoosterTime + TuneSpec.Plant45_AnimalBoosterTime + TuneSpec.Plant46_AnimalBoosterTime);
			oPacket.WriteEncFloat(Kart.SuperBoosterTime);
			if (TuneSpec.PartSpec_TransAccelFactor == 0f)
			{
				oPacket.WriteEncFloat(SpeedType.TransAccelFactor + Kart.TransAccelFactor + TransAccelFactor);
			}
			else
			{
				oPacket.WriteEncFloat(TuneSpec.PartSpec_TransAccelFactor + SpeedType.AddSpec_TransAccelFactor + TransAccelFactor);
			}
			oPacket.WriteEncFloat(SpeedType.BoostAccelFactor + Kart.BoostAccelFactor + SpeedPatch.BoostAccelFactor);
			oPacket.WriteEncFloat(Kart.StartBoosterTimeItem + TuneSpec.KartLevel_StartBoosterTimeItem + TuneSpec.Plant46_StartBoosterTimeItem);
			oPacket.WriteEncFloat(Kart.StartBoosterTimeSpeed + TuneSpec.Tune_StartBoosterTimeSpeed + TuneSpec.Plant43_StartBoosterTimeSpeed + TuneSpec.Plant46_StartBoosterTimeSpeed + TuneSpec.KartLevel_StartBoosterTimeSpeed);
			oPacket.WriteEncFloat(SpeedType.StartForwardAccelForceItem + Kart.StartForwardAccelForceItem + FlyingPet.StartForwardAccelForceItem + SpeedPatch.StartForwardAccelForceItem + TuneSpec.Plant46_StartForwardAccelItem);
			oPacket.WriteEncFloat(SpeedType.StartForwardAccelForceSpeed + Kart.StartForwardAccelForceSpeed + FlyingPet.StartForwardAccelForceSpeed + SpeedPatch.StartForwardAccelForceSpeed + TuneSpec.Plant43_StartForwardAccelSpeed + TuneSpec.Plant46_StartForwardAccelSpeed);
			oPacket.WriteEncFloat(Kart.DriftGaguePreservePercent);
			oPacket.WriteEncByte(Kart.UseExtendedAfterBooster);
			oPacket.WriteEncFloat(Kart.BoostAccelFactorOnlyItem + TuneSpec.KartLevel_BoostAccelFactorOnlyItem);
			oPacket.WriteEncFloat(Kart.antiCollideBalance + TuneSpec.Plant45_AntiCollideBalance);
			oPacket.WriteEncByte(Kart.dualBoosterSetAuto);
			oPacket.WriteEncInt(Kart.dualBoosterTickMin);
			oPacket.WriteEncInt(Kart.dualBoosterTickMax);
			oPacket.WriteEncFloat(Kart.dualMulAccelFactor);
			oPacket.WriteEncFloat(Kart.dualTransLowSpeed);
			oPacket.WriteEncByte(Kart.PartsEngineLock);
			oPacket.WriteEncByte(Kart.PartsWheelLock);
			oPacket.WriteEncByte(Kart.PartsSteeringLock);
			oPacket.WriteEncByte(Kart.PartsBoosterLock);
			oPacket.WriteEncByte(Kart.PartsCoatingLock);
			oPacket.WriteEncByte(Kart.PartsTailLampLock);
			oPacket.WriteEncFloat(Kart.chargeInstAccelGaugeByBoost);
			oPacket.WriteEncFloat(Kart.chargeInstAccelGaugeByGrip);
			oPacket.WriteEncFloat(Kart.chargeInstAccelGaugeByWall);
			oPacket.WriteEncFloat(Kart.instAccelFactor);
			oPacket.WriteEncInt(Kart.instAccelGaugeCooldownTime);
			oPacket.WriteEncFloat(Kart.instAccelGaugeLength);
			oPacket.WriteEncFloat(Kart.instAccelGaugeMinUsable);
			oPacket.WriteEncFloat(Kart.instAccelGaugeMinVelBound);
			oPacket.WriteEncFloat(Kart.instAccelGaugeMinVelLoss);
			oPacket.WriteEncByte(Kart.useExtendedAfterBoosterMore);
			oPacket.WriteEncInt(Kart.wallCollGaugeCooldownTime);
			oPacket.WriteEncFloat(Kart.wallCollGaugeMaxVelLoss);
			oPacket.WriteEncFloat(Kart.wallCollGaugeMinVelBound);
			oPacket.WriteEncFloat(Kart.wallCollGaugeMinVelLoss);
			oPacket.WriteEncFloat(Kart.modelMaxX);
			oPacket.WriteEncFloat(Kart.modelMaxY);
			//------------------------------------------------------------------------KartSpac End
		}

		public static void GetDefaultSpac(OutPacket oPacket)
		{
			//------------------------------------------------------------------------KartSpac Start
			oPacket.WriteEncFloat(Kart.draftMulAccelFactor);
			oPacket.WriteEncInt(Kart.draftTick);
			oPacket.WriteEncFloat(Kart.driftBoostMulAccelFactor);
			oPacket.WriteEncInt(Kart.driftBoostTick);
			oPacket.WriteEncFloat(Kart.chargeBoostBySpeed);
			oPacket.WriteEncByte(Kart.SpeedSlotCapacity);
			oPacket.WriteEncByte(Kart.ItemSlotCapacity);
			oPacket.WriteEncByte(Kart.SpecialSlotCapacity);
			oPacket.WriteEncByte(Kart.UseTransformBooster);
			oPacket.WriteEncByte(Kart.motorcycleType);
			oPacket.WriteEncByte(Kart.BikeRearWheel);
			oPacket.WriteEncFloat(Kart.Mass);
			oPacket.WriteEncFloat(Kart.AirFriction);
			oPacket.WriteEncFloat(SpeedType.DragFactor + Kart.DragFactor + FlyingPet.DragFactor + SpeedPatch.DragFactor);
			oPacket.WriteEncFloat(SpeedType.ForwardAccelForce + Kart.ForwardAccelForce + FlyingPet.ForwardAccelForce + SpeedPatch.ForwardAccelForce);
			oPacket.WriteEncFloat(SpeedType.BackwardAccelForce + Kart.BackwardAccelForce);
			oPacket.WriteEncFloat(SpeedType.GripBrakeForce + Kart.GripBrakeForce);
			oPacket.WriteEncFloat(SpeedType.SlipBrakeForce + Kart.SlipBrakeForce);
			oPacket.WriteEncFloat(Kart.MaxSteerAngle);
			oPacket.WriteEncFloat(SpeedType.SteerConstraint + Kart.SteerConstraint);
			oPacket.WriteEncFloat(Kart.FrontGripFactor);
			oPacket.WriteEncFloat(Kart.RearGripFactor);
			oPacket.WriteEncFloat(Kart.DriftTriggerFactor);
			oPacket.WriteEncFloat(Kart.DriftTriggerTime);
			oPacket.WriteEncFloat(Kart.DriftSlipFactor);
			oPacket.WriteEncFloat(SpeedType.DriftEscapeForce + Kart.DriftEscapeForce + FlyingPet.DriftEscapeForce + SpeedPatch.DriftEscapeForce);
			oPacket.WriteEncFloat(SpeedType.CornerDrawFactor + Kart.CornerDrawFactor + FlyingPet.CornerDrawFactor + SpeedPatch.CornerDrawFactor);
			oPacket.WriteEncFloat(Kart.DriftLeanFactor);
			oPacket.WriteEncFloat(Kart.SteerLeanFactor);
			if (StartGameData.StartTimeAttack_SpeedType == 4 || StartGameData.StartTimeAttack_SpeedType == 6)
			{
				oPacket.WriteEncFloat(GameType.S4_DriftMaxGauge);
			}
			else
			{
				oPacket.WriteEncFloat(SpeedType.DriftMaxGauge + Kart.DriftMaxGauge + SpeedPatch.DriftMaxGauge);
			}
			if (StartGameData.StartTimeAttack_SpeedType == 6)
			{
				oPacket.WriteEncFloat(GameType.S6_BoosterTime);
			}
			else
			{
				oPacket.WriteEncFloat(Kart.NormalBoosterTime + FlyingPet.NormalBoosterTime);
			}
			oPacket.WriteEncFloat(Kart.ItemBoosterTime + FlyingPet.ItemBoosterTime);
			if (StartGameData.StartTimeAttack_SpeedType == 6)
			{
				oPacket.WriteEncFloat(GameType.S6_BoosterTime);
			}
			else
			{
				oPacket.WriteEncFloat(Kart.TeamBoosterTime + FlyingPet.TeamBoosterTime);
			}
			oPacket.WriteEncFloat(Kart.AnimalBoosterTime);
			oPacket.WriteEncFloat(Kart.SuperBoosterTime);
			oPacket.WriteEncFloat(SpeedType.TransAccelFactor + Kart.TransAccelFactor + SpeedPatch.TransAccelFactor);
			oPacket.WriteEncFloat(SpeedType.BoostAccelFactor + Kart.BoostAccelFactor + SpeedPatch.BoostAccelFactor);
			oPacket.WriteEncFloat(Kart.StartBoosterTimeItem);
			oPacket.WriteEncFloat(Kart.StartBoosterTimeSpeed);
			oPacket.WriteEncFloat(SpeedType.StartForwardAccelForceItem + Kart.StartForwardAccelForceItem + FlyingPet.StartForwardAccelForceItem + SpeedPatch.StartForwardAccelForceItem);
			oPacket.WriteEncFloat(SpeedType.StartForwardAccelForceSpeed + Kart.StartForwardAccelForceSpeed + FlyingPet.StartForwardAccelForceSpeed + SpeedPatch.StartForwardAccelForceSpeed);
			oPacket.WriteEncFloat(Kart.DriftGaguePreservePercent);
			oPacket.WriteEncByte(Kart.UseExtendedAfterBooster);
			oPacket.WriteEncFloat(Kart.BoostAccelFactorOnlyItem);
			oPacket.WriteEncFloat(Kart.antiCollideBalance);
			oPacket.WriteEncByte(Kart.dualBoosterSetAuto);
			oPacket.WriteEncInt(Kart.dualBoosterTickMin);
			oPacket.WriteEncInt(Kart.dualBoosterTickMax);
			oPacket.WriteEncFloat(Kart.dualMulAccelFactor);
			oPacket.WriteEncFloat(Kart.dualTransLowSpeed);
			oPacket.WriteEncByte(Kart.PartsEngineLock);
			oPacket.WriteEncByte(Kart.PartsWheelLock);
			oPacket.WriteEncByte(Kart.PartsSteeringLock);
			oPacket.WriteEncByte(Kart.PartsBoosterLock);
			oPacket.WriteEncByte(Kart.PartsCoatingLock);
			oPacket.WriteEncByte(Kart.PartsTailLampLock);
			oPacket.WriteEncFloat(Kart.chargeInstAccelGaugeByBoost);
			oPacket.WriteEncFloat(Kart.chargeInstAccelGaugeByGrip);
			oPacket.WriteEncFloat(Kart.chargeInstAccelGaugeByWall);
			oPacket.WriteEncFloat(Kart.instAccelFactor);
			oPacket.WriteEncInt(Kart.instAccelGaugeCooldownTime);
			oPacket.WriteEncFloat(Kart.instAccelGaugeLength);
			oPacket.WriteEncFloat(Kart.instAccelGaugeMinUsable);
			oPacket.WriteEncFloat(Kart.instAccelGaugeMinVelBound);
			oPacket.WriteEncFloat(Kart.instAccelGaugeMinVelLoss);
			oPacket.WriteEncByte(Kart.useExtendedAfterBoosterMore);
			oPacket.WriteEncInt(Kart.wallCollGaugeCooldownTime);
			oPacket.WriteEncFloat(Kart.wallCollGaugeMaxVelLoss);
			oPacket.WriteEncFloat(Kart.wallCollGaugeMinVelBound);
			oPacket.WriteEncFloat(Kart.wallCollGaugeMinVelLoss);
			oPacket.WriteEncFloat(Kart.modelMaxX);
			oPacket.WriteEncFloat(Kart.modelMaxY);
			//------------------------------------------------------------------------KartSpac End
		}

		public static void KartSpecLog()
		{
			InPacket iPacket;
			using (OutPacket oPacket = new OutPacket())
			{
				GetKartSpac(oPacket);
				iPacket = new InPacket(oPacket.ToArray());
			}
			Console.WriteLine($"-------------------------------------------------------------");
			Console.WriteLine($"draftMulAccelFactor:{iPacket.ReadEncodedFloat()}");
			Console.WriteLine($"draftTick:{iPacket.ReadEncodedInt()}");
			Console.WriteLine($"driftBoostMulAccelFactor:{iPacket.ReadEncodedFloat()}");
			Console.WriteLine($"driftBoostTick:{iPacket.ReadEncodedInt()}");
			Console.WriteLine($"chargeBoostBySpeed:{iPacket.ReadEncodedFloat()}");
			Console.WriteLine($"SpeedSlotCapacity:{iPacket.ReadEncodedByte()}");
			Console.WriteLine($"ItemSlotCapacity:{iPacket.ReadEncodedByte()}");
			Console.WriteLine($"SpecialSlotCapacity:{iPacket.ReadEncodedByte()}");
			Console.WriteLine($"UseTransformBooster:{iPacket.ReadEncodedByte()}");
			Console.WriteLine($"motorcycleType:{iPacket.ReadEncodedByte()}");
			Console.WriteLine($"BikeRearWheel:{iPacket.ReadEncodedByte()}");
			Console.WriteLine($"Mass:{iPacket.ReadEncodedFloat()}");
			Console.WriteLine($"AirFriction:{iPacket.ReadEncodedFloat()}");
			Console.WriteLine($"DragFactor:{iPacket.ReadEncodedFloat()}");
			Console.WriteLine($"ForwardAccelForce:{iPacket.ReadEncodedFloat()}");
			Console.WriteLine($"BackwardAccelForce:{iPacket.ReadEncodedFloat()}");
			Console.WriteLine($"GripBrakeForce:{iPacket.ReadEncodedFloat()}");
			Console.WriteLine($"SlipBrakeForce:{iPacket.ReadEncodedFloat()}");
			Console.WriteLine($"MaxSteerAngle:{iPacket.ReadEncodedFloat()}");
			Console.WriteLine($"SteerConstraint:{iPacket.ReadEncodedFloat()}");
			Console.WriteLine($"FrontGripFactor:{iPacket.ReadEncodedFloat()}");
			Console.WriteLine($"RearGripFactor:{iPacket.ReadEncodedFloat()}");
			Console.WriteLine($"DriftTriggerFactor:{iPacket.ReadEncodedFloat()}");
			Console.WriteLine($"DriftTriggerTime:{iPacket.ReadEncodedFloat()}");
			Console.WriteLine($"DriftSlipFactor:{iPacket.ReadEncodedFloat()}");
			Console.WriteLine($"DriftEscapeForce:{iPacket.ReadEncodedFloat()}");
			Console.WriteLine($"CornerDrawFactor:{iPacket.ReadEncodedFloat()}");
			Console.WriteLine($"DriftLeanFactor:{iPacket.ReadEncodedFloat()}");
			Console.WriteLine($"SteerLeanFactor:{iPacket.ReadEncodedFloat()}");
			Console.WriteLine($"DriftMaxGauge:{iPacket.ReadEncodedFloat()}");
			Console.WriteLine($"NormalBoosterTime:{iPacket.ReadEncodedFloat()}");
			Console.WriteLine($"ItemBoosterTime:{iPacket.ReadEncodedFloat()}");
			Console.WriteLine($"TeamBoosterTime:{iPacket.ReadEncodedFloat()}");
			Console.WriteLine($"AnimalBoosterTime:{iPacket.ReadEncodedFloat()}");
			Console.WriteLine($"SuperBoosterTime:{iPacket.ReadEncodedFloat()}");
			Console.WriteLine($"TransAccelFactor:{iPacket.ReadEncodedFloat()}");
			Console.WriteLine($"BoostAccelFactor:{iPacket.ReadEncodedFloat()}");
			Console.WriteLine($"StartBoosterTimeItem:{iPacket.ReadEncodedFloat()}");
			Console.WriteLine($"StartBoosterTimeSpeed:{iPacket.ReadEncodedFloat()}");
			Console.WriteLine($"StartForwardAccelForceItem:{iPacket.ReadEncodedFloat()}");
			Console.WriteLine($"StartForwardAccelForceSpeed:{iPacket.ReadEncodedFloat()}");
			Console.WriteLine($"DriftGaguePreservePercent:{iPacket.ReadEncodedFloat()}");
			Console.WriteLine($"UseExtendedAfterBooster:{iPacket.ReadEncodedByte()}");
			Console.WriteLine($"BoostAccelFactorOnlyItem:{iPacket.ReadEncodedFloat()}");
			Console.WriteLine($"antiCollideBalance:{iPacket.ReadEncodedFloat()}");
			Console.WriteLine($"dualBoosterSetAuto:{iPacket.ReadEncodedByte()}");
			Console.WriteLine($"dualBoosterTickMin:{iPacket.ReadEncodedInt()}");
			Console.WriteLine($"dualBoosterTickMax:{iPacket.ReadEncodedInt()}");
			Console.WriteLine($"dualMulAccelFactor:{iPacket.ReadEncodedFloat()}");
			Console.WriteLine($"dualTransLowSpeed:{iPacket.ReadEncodedFloat()}");
			Console.WriteLine($"PartsEngineLock:{iPacket.ReadEncodedByte()}");
			Console.WriteLine($"PartsWheelLock:{iPacket.ReadEncodedByte()}");
			Console.WriteLine($"PartsSteeringLock:{iPacket.ReadEncodedByte()}");
			Console.WriteLine($"PartsBoosterLock:{iPacket.ReadEncodedByte()}");
			Console.WriteLine($"PartsCoatingLock:{iPacket.ReadEncodedByte()}");
			Console.WriteLine($"PartsTailLampLock:{iPacket.ReadEncodedByte()}");
			Console.WriteLine($"chargeInstAccelGaugeByBoost:{iPacket.ReadEncodedFloat()}");
			Console.WriteLine($"chargeInstAccelGaugeByGrip:{iPacket.ReadEncodedFloat()}");
			Console.WriteLine($"chargeInstAccelGaugeByWall:{iPacket.ReadEncodedFloat()}");
			Console.WriteLine($"instAccelFactor:{iPacket.ReadEncodedFloat()}");
			Console.WriteLine($"instAccelGaugeCooldownTime:{iPacket.ReadEncodedInt()}");
			Console.WriteLine($"instAccelGaugeLength:{iPacket.ReadEncodedFloat()}");
			Console.WriteLine($"instAccelGaugeMinUsable:{iPacket.ReadEncodedFloat()}");
			Console.WriteLine($"instAccelGaugeMinVelBound:{iPacket.ReadEncodedFloat()}");
			Console.WriteLine($"instAccelGaugeMinVelLoss:{iPacket.ReadEncodedFloat()}");
			Console.WriteLine($"useExtendedAfterBoosterMore:{iPacket.ReadEncodedByte()}");
			Console.WriteLine($"wallCollGaugeCooldownTime:{iPacket.ReadEncodedInt()}");
			Console.WriteLine($"wallCollGaugeMaxVelLoss:{iPacket.ReadEncodedFloat()}");
			Console.WriteLine($"wallCollGaugeMinVelBound:{iPacket.ReadEncodedFloat()}");
			Console.WriteLine($"wallCollGaugeMinVelLoss:{iPacket.ReadEncodedFloat()}");
			Console.WriteLine($"modelMaxX:{iPacket.ReadEncodedFloat()}");
			Console.WriteLine($"modelMaxY:{iPacket.ReadEncodedFloat()}");
			Console.WriteLine($"-------------------------------------------------------------");
		}
	}
}