using System;
using UnityEngine;

namespace SchloooLib.Temporal
{
    public class Timer
    {
        public float Duration { get; }
        public bool IsLooping { get; }
        public bool UsesGameTimescale { get; }
        public bool IsFinished { get; private set; }
        public bool IsPaused { get; private set; } 

        private Action onFinish;
        private Action<float> onTick;
        private float startupTime;
        private float lastUpdateTime;
        
        private float? elapsedTimeBeforePause;

        public static Timer ScheduleLoop(float delayPerIteration, Action onIterationFinish, Action<float> onTick = null, bool usesGameTimescale = false, bool runInstantly = true)
        {
            return new Timer(delayPerIteration, onIterationFinish, onTick, true, usesGameTimescale, runInstantly);
        }
        
        public static Timer ScheduleDelay(float delayDuration, Action onFinish, Action<float> onTick = null, bool usesGameTimescale = false, bool runInstantly = true)
        {
            return new Timer(delayDuration, onFinish, onTick, false, usesGameTimescale, runInstantly);
        }

        protected Timer(float duration, Action onFinish, Action<float> onTick = null, bool isLooping = false, bool usesGameTimescale = false, bool runInstantly = true)
        {
            Duration = duration;
            this.onFinish = onFinish;
            this.onTick = onTick;
            IsLooping = isLooping;
            UsesGameTimescale = usesGameTimescale;
            IsFinished = false;

            IsPaused = !runInstantly;
            RuntimeTimerConductor.Instance.AddTimer(this);
        }
        
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

        public float GetTimeSinceTimerStart()
        {
            if (IsFinished || GetGlobalTime() >= GetFinishTime()) return Duration;
            
            return elapsedTimeBeforePause ?? GetGlobalTime() - startupTime;
        }
        
        private float GetFinishTime()
        {
            return startupTime + Duration;
        }

        private void ProgressCycleFinish()
        {
            onFinish?.Invoke();
            
            if (IsLooping)
            {
                startupTime = GetGlobalTime();
            }
            else
            {
                IsFinished = true;
            }
        }
        
        public void Pause()
        {
            if (IsPaused || IsFinished) return;

            elapsedTimeBeforePause = GetTimeSinceTimerStart();
            IsPaused = true;
        }

        public void Unpause()
        {
            if (!IsPaused || IsFinished) return;

            elapsedTimeBeforePause = null;
            IsPaused = false;
        }

        public void Stop()
        {
            if (IsFinished) return;

            IsFinished = true;
        }
    }
}