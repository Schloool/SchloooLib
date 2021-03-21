using System;

namespace SchloooLib.Temporal
{
    /// <summary>
    /// <see cref="TimeLoop"/> implementation to invoke a loop that runs for a specified amount of times until it stops automatically.
    /// </summary>
    public class CountingTimeLoop : TimeLoop
    {
        /// <summary>
        /// The amount of iterations the <see cref="CountingTimeLoop"/> has already run through.
        /// </summary>
        public int FinishedIterations { get; private set; }

        private Action<int> OnFinishIteration;
        private readonly int maximumIterations;
        
        /// <summary>
        /// Constructs a new <see cref="Delay"/> instance running as an one-time delay.
        /// </summary>
        /// <param name="iterations">The amount of iterations the <see cref="CountingTimeLoop"/> will run through.</param>
        /// <param name="iterationDelayDuration">The amount of seconds one iteration will last.</param>
        /// <param name="onIterationFinish"><see cref="Action"/> delegate called with the currently finished iteration-count whenever one iteration is finished.</param>
        /// <param name="onFinishScheduler"><see cref="Action"/> delegate called when the loop is stopped</param>
        /// <param name="onTick"><see cref="Action"/> called whenever the <see cref="RuntimeSchedulerConductor"/> casts an update cycle.</param>
        /// <param name="usesGameTimescale">Adjusts whether the Unity timescale will influence time measurement or not.</param>
        /// <param name="runInstantly">Adjusts whether the delay will run instantly or will be created in a paused state.</param>
        /// <returns>The resulting <see cref="TimeLoop"/> instance.</returns>
        public static CountingTimeLoop ScheduleCountingLoop(int iterations, float iterationDelayDuration, Action<int> onIterationFinish, Action onFinishScheduler = null, Action<float> onTick = null, bool usesGameTimescale = false, bool runInstantly = true)
        {
            return new CountingTimeLoop(iterations, iterationDelayDuration, onIterationFinish, onFinishScheduler, onTick, usesGameTimescale, runInstantly);
        }
        
        /// <summary>
        /// Constructs a new <see cref="Delay"/> instance running as an one-time delay.
        /// </summary>
        /// <param name="iterations">The amount of iterations the <see cref="CountingTimeLoop"/> will run through.</param>
        /// <param name="iterationDelayDuration">The amount of seconds one iteration will last.</param>
        /// <param name="onIterationFinish"><see cref="Action"/> delegate called whenever one iteration is finished.</param>
        /// <param name="onFinishScheduler"><see cref="Action"/> delegate called when the loop is stopped</param>
        /// <param name="onTick"><see cref="Action"/> called whenever the <see cref="RuntimeSchedulerConductor"/> casts an update cycle.</param>
        /// <param name="usesGameTimescale">Adjusts whether the Unity timescale will influence time measurement or not.</param>
        /// <param name="runInstantly">Adjusts whether the delay will run instantly or will be created in a paused state.</param>
        /// <returns>The resulting <see cref="TimeLoop"/> instance.</returns>
        public static CountingTimeLoop ScheduleCountingLoop(int iterations, float iterationDelayDuration, Action onIterationFinish, Action onFinishScheduler = null, Action<float> onTick = null, bool usesGameTimescale = false, bool runInstantly = true)
        {
            return new CountingTimeLoop(iterations, iterationDelayDuration, onIterationFinish, onFinishScheduler, onTick, usesGameTimescale, runInstantly);
        }

        protected CountingTimeLoop(int iterations, float iterationDelayDuration, Action<int> onFinishIteration, Action onFinishScheduler = null, Action<float> onTick = null, bool usesGameTimescale = false, bool runInstantly = true) 
            : base(iterationDelayDuration, null, onFinishScheduler, onTick, usesGameTimescale, runInstantly)
        {
            OnFinishIteration = onFinishIteration;
            maximumIterations = iterations;
            onFinishSchedulerRun += HandleIterationEnd;
        }
        
        protected CountingTimeLoop(int iterations, float iterationDelayDuration, Action onFinishIteration, Action onFinishScheduler = null, Action<float> onTick = null, bool usesGameTimescale = false, bool runInstantly = true) 
            : base(iterationDelayDuration, onFinishIteration, onFinishScheduler, onTick, usesGameTimescale, runInstantly)
        {
            maximumIterations = iterations;
            onFinishSchedulerRun += HandleIterationEnd;
        }

        private void HandleIterationEnd()
        {
            FinishedIterations++;
            OnFinishIteration?.Invoke(FinishedIterations);

            if (FinishedIterations >= maximumIterations)
            {
                Stop();
            }
        }

        /// <summary>
        /// Calculates the amount of iteration that is left until the loop will stop.
        /// </summary>
        /// <returns>The remaining amount of iterations.</returns>
        public int GetRemainingIterations()
        {
            return maximumIterations - FinishedIterations;
        }
    }
}