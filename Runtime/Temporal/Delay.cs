using System;

namespace SchloooLib.Temporal
{
    /// <summary>
    /// Primitive <see cref="AbstractScheduler"/> implementation to invoke a delay.
    /// </summary>
    public class Delay : AbstractScheduler
    {
        /// <summary>
        /// Constructs a new <see cref="Delay"/> instance running as an one-time delay.
        /// </summary>
        /// <param name="delayDuration">The amount of seconds the delay will last.</param>
        /// <param name="onFinish"><see cref="Action"/> delegate called when the delay is finished.</param>
        /// <param name="onTick"><see cref="Action"/> called whenever the <see cref="RuntimeSchedulerConductor"/> casts an update cycle.</param>
        /// <param name="usesGameTimescale">Adjusts whether the Unity timescale will influence time measurement or not.</param>
        /// <param name="runInstantly">Adjusts whether the delay will run instantly or will be created in a paused state.</param>
        /// <returns>The resulting <see cref="Delay"/> instance.</returns>
        public static Delay ScheduleDelay(float delayDuration, Action onFinish, Action<float> onTick = null, bool usesGameTimescale = false, bool runInstantly = true)
        {
            return new Delay(delayDuration, onFinish, onTick, usesGameTimescale, runInstantly);
        }

        protected Delay(float delayDuration, Action onFinishDelayEnd, Action<float> onTick = null, bool usesGameTimescale = false, bool runInstantly = true) 
            : base(delayDuration, onFinishDelayEnd, onTick, false, usesGameTimescale, runInstantly) { }
    }
}