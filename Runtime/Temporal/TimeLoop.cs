using System;

namespace SchloooLib.Temporal
{
    /// <summary>
    /// Primitive <see cref="AbstractScheduler"/> implementation to invoke a loop.
    /// </summary>
    public class TimeLoop : AbstractScheduler
    {
        /// <summary>
        /// Constructs a new <see cref="TimeLoop"/> instance running as an one-time delay.
        /// </summary>
        /// <param name="iterationDelayDuration">The amount of seconds one iteration will last.</param>
        /// <param name="onIterationFinish"><see cref="Action"/> delegate called whenever one iteration is finished.</param>
        /// <param name="onFinishScheduler"><see cref="Action"/> delegate called when the loop is stopped</param>
        /// <param name="onTick"><see cref="Action"/> called whenever the <see cref="RuntimeSchedulerConductor"/> casts an update cycle.</param>
        /// <param name="usesGameTimescale">Adjusts whether the Unity timescale will influence time measurement or not.</param>
        /// <param name="runInstantly">Adjusts whether the delay will run instantly or will be created in a paused state.</param>
        /// <returns>The resulting <see cref="TimeLoop"/> instance.</returns>
        public static TimeLoop ScheduleLoop(float iterationDelayDuration, Action onIterationFinish, Action onFinishScheduler = null, Action<float> onTick = null, 
            bool usesGameTimescale = false, bool runInstantly = true)
        {
            return new TimeLoop(iterationDelayDuration, onIterationFinish, onFinishScheduler, onTick, usesGameTimescale, runInstantly);
        }
        
        protected TimeLoop(float iterationDelayDuration, Action onFinishIteration, Action onFinishScheduler = null, Action<float> onTick = null, bool usesGameTimescale = false, bool runInstantly = true) 
            : base(iterationDelayDuration, onFinishIteration, onTick, true, usesGameTimescale, runInstantly) { }
    }
}