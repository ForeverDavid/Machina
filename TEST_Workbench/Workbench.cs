﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BRobot;

namespace TEST_Workbench
{
    class Workbench
    {
        static void Main(string[] args)
        {
            //int inc = 50;
            Robot arm = new Robot();
            //arm.Mode("offline");
            arm.Mode("stream");
            arm.Connect();
            arm.Start();

            //TestRelativeMovementAxes(arm);
            //TestRelativeRotations(arm);
            TestStaticActions(arm);

            //arm.Export(@"C:\test.mod");

            Console.WriteLine("Press any key to EXIT...");
            Console.ReadKey();

            arm.Disconnect();
        }

        static void TestStaticActions(Robot arm)
        {
            BRobot.Action start = BRobot.Action.MoveTo(new Point(302, 0, 558));
            BRobot.Action x100 = BRobot.Action.Move(new Point(100, 0, 0));

            arm.Do(start);
            arm.Do(x100);
            arm.Do(start);
        }

        static void TestRelativeMovementAxes(Robot arm)
        {
            arm.SpeedTo(200);
            arm.JointsTo(0, 0, 0, 0, 90, 0);
            arm.TransformTo(new Point(302, 0, 558), Rotation.FlippedAroundY);
            arm.MoveTo(300, 300, 200);

            arm.ZoneTo(2);
            arm.Move(100, 0);

            arm.Motion("joint");
            arm.Move(0, -100);
            arm.Rotate(0, 1, 0, -90);

            arm.SpeedTo(20);
            arm.Coordinates("local");
            arm.Move(50, 0);        // not working either
            arm.Move(-50, 0);
            arm.Move(0, 50);        // working
            arm.Move(0, -50);
            arm.Move(0, 0, 50);     // nor working
            arm.Move(0, 0, -50);

            arm.Coordinates("global");
            arm.Rotate(0, 0, 1, 90);

            arm.Coordinates("local");
            arm.Move(50, 0);
            arm.Move(-50, 0);
            arm.Move(0, 50);
            arm.Move(0, -50);
            arm.Move(0, 0, 50);
            arm.Move(0, 0, -50);
        }

        static void TestRelativeRotations(Robot arm)
        {
            arm.SpeedTo(200);

            arm.JointsTo(0, 0, 0, 0, 90, 0);
            arm.TransformTo(new Point(302, 0, 558), Rotation.FlippedAroundY);
            arm.MoveTo(100, -400, 300);

            //// Works!
            //arm.Coordinates("local");
            //arm.SpeedTo(30);
            //arm.Rotate(1, 0, 0, 90);
            //arm.Rotate(0, 1, 0, 90);
            //arm.Rotate(0, 0, 1, 90);
            //arm.Wait(2000);           // this one doesn't!
            //arm.Rotate(0, 0, 1, -90);
            //arm.Rotate(0, 1, 0, -90);
            //arm.Rotate(1, 0, 0, -90);

            // Works too!
            double inc = 30;
            arm.Coordinates("local");
            arm.SpeedTo(15);
            arm.Rotate(1, 0, 0, inc);
            arm.Rotate(0, 1, 0, inc);
            arm.Rotate(0, 0, 1, inc);
            arm.Rotate(1, 0, 0, inc);
            arm.Rotate(0, 1, 0, inc);
            arm.Rotate(0, 0, 1, inc);
            arm.Wait(5000);
            arm.Rotate(0, 0, 1, -inc);
            arm.Rotate(0, 1, 0, -inc);
            arm.Rotate(1, 0, 0, -inc);
            arm.Rotate(0, 0, 1, -inc);
            arm.Rotate(0, 1, 0, -inc);
            arm.Rotate(1, 0, 0, -inc);


        }
    }
}
