using System.Collections;
using SchloooLib.Temporal;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.TestTools;

namespace SchloooLib.Tests
{
    public class TimerTests
    {
        [UnityTest]
        public IEnumerator TimerGetsMarkedAsFinished()
        {
            Timer timer = Timer.Schedule(1f, null);
            yield return new WaitForSeconds(1f);
            Assert.IsTrue(timer.IsFinished);
        }
        
        [UnityTest]
        public IEnumerator TimerIsNotMarkedAsFinishedEarly()
        {
            Timer timer = Timer.Schedule(10f, null);
            yield return new WaitForSeconds(1f);
            Assert.IsFalse(timer.IsFinished);
            
            timer.Stop();
        }
        
        [UnityTest]
        public IEnumerator LoopingTimerIsNotMarkedAsFinished()
        {
            Timer timer = Timer.Schedule(1f, null, isLooping: true, runInstantly: false);
            
            yield return new WaitForSeconds(2f);
            Assert.IsFalse(timer.IsFinished);
            
            timer.Stop();
        }
        
        [UnityTest]
        public IEnumerator TimescaleEffectsScaledTimer()
        {
            Timer timer = Timer.Schedule(1.5f, null, usesGameTimescale: true, runInstantly: false);
            Time.timeScale = 0.5f;
            yield return new WaitForSecondsRealtime(1f);
            
            timer.Unpause();
            yield return new WaitForSecondsRealtime(2f);
            Assert.IsFalse(timer.IsFinished);
            
            timer.Stop();
            Time.timeScale = 1f;
        }
        
        [UnityTest]
        public IEnumerator TimescaleDoesNotEffectUnscaledTimer()
        {
            Timer timer = Timer.Schedule(1.5f, null, runInstantly: false);
            Time.timeScale = 0.5f;
            yield return new WaitForSecondsRealtime(1f);
            
            timer.Unpause();
            yield return new WaitForSecondsRealtime(2f);
            Assert.IsTrue(timer.IsFinished);
            
            Time.timeScale = 1f;
        }

        [UnityTest]
        public IEnumerator StartPauseGetsSet()
        {
            Timer timer = Timer.Schedule(1f, null, runInstantly: false);
            yield return null;

            Assert.IsTrue(timer.IsPaused);
            Assert.IsFalse(timer.IsFinished);
        }
        
        [UnityTest]
        public IEnumerator PauseAndUnpauseStateGetsSet()
        {
            Timer timer = Timer.Schedule(10f, null);
            yield return null;

            timer.Pause();
            Assert.IsTrue(timer.IsPaused);
            
            timer.Unpause();
            Assert.IsFalse(timer.IsPaused);
            
            timer.Stop();
        }
        
        [UnityTest]
        public IEnumerator PauseStateEffectsTimer()
        {
            Timer timer = Timer.Schedule(10f, null);
            timer.Pause();
            float timeBeforePause = timer.GetTimeSinceTimerStart();
            
            yield return new WaitForSeconds(1f);
            Assert.AreEqual(timeBeforePause, timer.GetTimeSinceTimerStart());
        }
    }
}
