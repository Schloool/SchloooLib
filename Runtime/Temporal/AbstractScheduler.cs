using System;
using UnityEngine;

namespace SchloooLib.Temporal
{
    /// <summary>
    /// Main class to conduct delayed or time-based looped activities.
    /// </summary>
    public abstract class AbstractScheduler
    {
        /// <summary>
        /// The amount of seconds it will take to run one cycle.
        /// </summary>
        public float DelayDuration { get; }
        /// <summary>
        /// Indicates whether the instance will loop or not.
        /// </summary>
        public bool IsLooping { get; }
        /// <summary>
        /// Indicates whether the instance uses the Unity timescale or real time.
        /// </summary>
        public bool UsesGameTimescale { get; }
        /// <summary>
        /// Indicates whether the instance has already finished or not.
        /// A <see cref="Delay"/> is set as finished when running through a not looping delay or when stopped manually.
        /// </summary>
        public bool IsFinished { get; private set; }
        /// <summary>
        /// Indicates if the instance is currently paused.
        /// The pause flag gets also set when the <see cref="Delay"/> is not run instantly.
        /// </summary>
        public bool IsPaused { get; private set; }
        
        protected Action onFinishSchedulerRun;
        protected Action<float> onTick;
        
        private float startupTime;
        private float lastUpdateTime;
        
        private float? elapsedTimeBeforePause;

        protected AbstractScheduler(float delayDuration, Action onFinishSchedulerRun, Action<float> onTick = null, bool isLooping = false, bool usesGameTimescale = false, bool runInstantly = true)
        {
            DelayDuration = delayDuration;
            this.onFinishSchedulerRun = onFinishSchedulerRun;
            this.onTick = onTick;
            IsLooping = isLooping;
            UsesGameTimescale = usesGameTimescale;
            IsFinished = false;

            IsPaused = !runInstantly;
            RuntimeSchedulerConductor.Instance.AddTimer(this);
        }

        /// <summary>
        /// Processes the time variables differentiating between running, paused and finished states.
        /// </summary>
        public void Tick()
        {
            if (IsFinished) return;

            if (IsPaused)
            {
                ProgressPauseState();
                return;
            }

            lastUpdateTime = GetGlobalTime();
            onTick?.Invoke(GetTimeSinceTimerStart());

            if (GetGlobalTime() >= GetFinishTime())
            {
                ProgressCycleFinish();
            }
            
        }
        
        private void ProgressPauseState()
        {
            startupTime += GetTickDeltaTime();
            lastUpdateTime = GetGlobalTime();
        }
        
        private float GetTickDeltaTime()
        {
            return GetGlobalTime() - lastUpdateTime;
        }
        
        private float GetGlobalTime()
        {
            return UsesGameTimescale ? Time.time : Time.realtimeSinceStartup;
        }

        /// <summary>
        /// Calculates the time that has passed since the current cycle has started.
        /// </summary>
        /// <returns></returns>
        public float GetTimeSinceTimerStart()
        {
            if (IsFinished || GetGlobalTime() >= GetFinishTime()) return DelayDuration;
            
            return elapsedTimeBeforePause ?? GetGlobalTime() - startupTime;
        }
        
        private float GetFinishTime()
        {
            return startupTime + DelayDuration;
        }

        private void ProgressCycleFinish()
        {
            if (IsLooping)
            {
                startupTime = GetGlobalTime();
            }
            else
            {
                IsFinished = true;
            }
            
            onFinishSchedulerRun?.Invoke();
        }
        
        /// <summary>
        /// Pauses the <see cref="Delay"/>.
        /// Does nothing when the instance is paused already or has finished.
        /// </summary>
        public void Pause()
        {
            if (IsPaused || IsFinished) return;

            elapsedTimeBeforePause = GetTimeSinceTimerStart();
            IsPaused = true;
        }

        /// <summary>
        /// Unpauses the <see cref="AbstractScheduler"/>.
        /// Does nothing when the instance is unpaused already or has finished.
        /// </summary>
        public void Unpause()
        {
            if (!IsPaused || IsFinished) return;

            elapsedTimeBeforePause = null;
            IsPaused = false;
        }

        /// <summary>
        /// Stops the <see cref="AbstractScheduler"/>.
        /// A stopped instance can not be resumed later.
        /// </summary>
        public void Stop()
        {
            if (IsFinished) return;

            IsFinished = true;
        }
    }
}