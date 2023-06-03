﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GTA;
using GTA.Native;
using GTA.Math;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;

public class debug : Script
{
    private int compact_spawned = 0;
    private int x = 0;
    private float distance = 200.0f;
    private int all_coords;

    private Blip[] marker = new Blip[103];
    private Vector3[] coords = new Vector3[103];
    private GTA.Vehicle[] cars_hashes = new GTA.Vehicle[5];
    private List<int> models = new List<int>() { 15214558, 1429622905, 1644055914, 409049982, 1118611807, 931280609, 1549126457 };

    public debug()
    {
        KeyDown += onkeydown;

        coords[0] = new Vector3(-3260.979f, 3524.306f, 1.150f);

        all_coords = 5;
    }

    void onkeydown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Z)
        {
            var veh_model = new Model(VehicleHash.Cheburek);
            veh_model.Request(500);
            while (!veh_model.IsLoaded) Script.Wait(100);

            var position = Game.Player.Character.GetOffsetPosition(new Vector3(0, 5, 0)); // берем координаты игрока и прибавляем смещение 5 игровых метров от него
            var heading = Game.Player.Character.Heading - 90;
            var car = World.CreateVehicle(veh_model, position, heading);
            Function.Call(Hash.DECOR_SET_INT, car, "MPBitset", 0);
            GTA.Native.Function.Call(GTA.Native.Hash.SET_VEHICLE_DOORS_LOCKED, car, 7);
            GTA.Native.Function.Call(GTA.Native.Hash.SET_VEHICLE_ALARM, car, true);
            GTA.Native.Function.Call(GTA.Native.Hash.START_VEHICLE_ALARM, car);
            veh_model.MarkAsNoLongerNeeded();
        }
    }
}