﻿using System;

namespace StreamKontrolUnit.EventHandlers
{
    public class CameraControlEventArgs : EventArgs
    {
        public string Photo { get; set; }

        public CameraControlEventArgs(string photo)
        {
            Photo = photo;
        }
    }
}
