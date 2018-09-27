﻿#pragma warning disable 0649 // variable declared but not used.

using UnityEngine;

using com.spacepuppy.Utils;

namespace com.spacepuppy.Events
{

    public class i_TriggerRandom : AutoTriggerable
    {

        #region Fields

        [SerializeField()]
        [SPEvent.Config(Weighted = true)]
        private SPEvent _targets;

        [SerializeField()]
        private bool _passAlongTriggerArg;

        [SerializeField()]
        [TimeUnitsSelector()]
        private SPTimePeriod _delay = 0f;

        #endregion

        #region Properties

        public bool PassAlongTriggerArg
        {
            get { return _passAlongTriggerArg; }
            set { _passAlongTriggerArg = value; }
        }

        public SPTimePeriod Delay
        {
            get { return _delay; }
            set { _delay = value; }
        }

        #endregion
        
        #region ITriggerableMechanism Interface

        public override bool Trigger(object sender, object arg)
        {
            if (!this.CanTrigger) return false;

            if (_delay.Seconds > 0f)
            {
                this.InvokeGuaranteed(() =>
                {
                    if (this._passAlongTriggerArg)
                        _targets.ActivateRandomTrigger(this, arg, true);
                    else
                        _targets.ActivateRandomTrigger(this, null, true);
                }, _delay.Seconds, _delay.TimeSupplier);
            }
            else
            {
                if (this._passAlongTriggerArg)
                    _targets.ActivateRandomTrigger(this, arg, true);
                else
                    _targets.ActivateRandomTrigger(this, null, true);
            }

            return true;
        }

        #endregion

    }

}
