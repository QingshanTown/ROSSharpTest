﻿
//using System.Diagnostics;
/*
© Siemens AG, 2018-2019
Author: Berkay Alp Cakal (berkay_alp.cakal.ct@siemens.com)

Licensed under the Apache License, Version 2.0 (the "License");
you may not use this file except in compliance with the License.
You may obtain a copy of the License at
<http://www.apache.org/licenses/LICENSE-2.0>.
Unless required by applicable law or agreed to in writing, software
distributed under the License is distributed on an "AS IS" BASIS,
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
See the License for the specific language governing permissions and
limitations under the License.
*/

using UnityEngine;

namespace RosSharp.RosBridgeClient
{
    public class LaserScanWriter : MonoBehaviour
    {
        private bool isReceived = false;
        private float range_max;
        private float range_min;
        private float[] ranges;
        private Vector3[] directions;
        private LaserScanVisualizer[] laserScanVisualizers;

        private void Update()
        {
            laserScanVisualizers = GetComponents<LaserScanVisualizer>();
            if (isReceived)
                if (laserScanVisualizers != null)
                    foreach (LaserScanVisualizer laserScanVisualizer in laserScanVisualizers)
                        laserScanVisualizer.SetSensorData(gameObject.transform, directions, ranges, range_min, range_max);

            isReceived = false;
        }

        public void Write(MessageTypes.Sensor.LaserScan laserScanMessage)
        {
            ranges = new float[laserScanMessage.ranges.Length];
            directions = new Vector3[laserScanMessage.ranges.Length];
            range_max = laserScanMessage.range_max;
            range_min = laserScanMessage.range_min;

            for (int i = 0; i < laserScanMessage.ranges.Length; i++)
            {
                ranges[i] = laserScanMessage.ranges[i];
                // directions[i] = new Vector3(Mathf.Cos(laserScanMessage.angle_min + laserScanMessage.angle_increment * i), Mathf.Sin(laserScanMessage.angle_min + laserScanMessage.angle_increment * i), 0).Ros2Unity();
                directions[i] = new Vector3(-Mathf.Cos(laserScanMessage.angle_min + laserScanMessage.angle_increment * i), -Mathf.Sin(laserScanMessage.angle_min + laserScanMessage.angle_increment * i), 0).Ros2Unity();
            }
            isReceived = true;
        }

    }
}