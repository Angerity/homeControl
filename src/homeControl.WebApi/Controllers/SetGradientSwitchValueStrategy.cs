﻿using System;
using homeControl.Configuration.Switches;
using homeControl.Events.Switches;
using homeControl.WebApi.Dto;

namespace homeControl.WebApi.Controllers
{
    internal sealed class SetGradientSwitchValueStrategy : ISetSwitchValueStrategy
    {
        public bool CanHandle(SwitchKind switchKind, object value)
        {
            if (switchKind == SwitchKind.GradientSwitch && value is double)
            {
                var d = (double)value;
                if (d >= SetPowerEvent.MinPower && d <= SetPowerEvent.MaxPower)
                    return true;
            }

            return false;
        }


        public SetPowerEvent CreateSetPowerEvent(Guid id, object value)
        {
            return new SetPowerEvent(new SwitchId(id), (double)value);
        }

        public AbstractSwitchEvent CreateControlEvent(Guid id, object value)
        {
            var d = (double)value;
            if (AreEqual(d, SetPowerEvent.MinPower))
            {
                return new TurnOffEvent(new SwitchId(id));
            }

            return new TurnOnEvent(new SwitchId(id));
        }

        private static bool AreEqual(double a, double b)
        {
            return Math.Abs(a - b) < double.Epsilon;
        }
    }
}