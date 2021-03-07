using System.Collections.Generic;
using SchloooLib.Core;

namespace SchloooLib.Temporal
{
    /// <summary>
    /// Central controller to register and update <see cref="Timer"/> instances.
    /// </summary>
    public sealed class RuntimeTimerConductor : Singleton<RuntimeTimerConductor>
    {
        private List<Timer> timers = new List<Timer>();
        private List<Timer> timerBacklog = new List<Timer>();

        private void Update()
        {
            ProgressBacklog();
            TickTimers();
        }
        
        private void ProgressBacklog()
        {
            if (timerBacklog.Count <= 0) return;
            
            timerBacklog.ForEach(backlogTimer => timers.Add(backlogTimer));
            timerBacklog.Clear();
        }

        private void TickTimers()
        {
            timers.ForEach(timer => timer.Tick());
            timers.RemoveAll(timer => timer.IsFinished);
        }

        /// <summary>
        /// Registers a new <see cref="Timer"/> that will be registered to be updated.
        /// </summary>
        /// <param name="timer">The new <see cref="Timer"/> instance.</param>
        public void AddTimer(Timer timer)
        {
            timerBacklog.Add(timer);
        }

        /// <summary>
        /// Stops all <see cref="Timer"/>s controlled by this <see cref="RuntimeTimerConductor"/> instance.
        /// </summary>
        public void StopAllTimers()
        {
            timers.ForEach(timer => timer.Stop());
            timers.Clear();
        }
    }
}