using UnityEngine;
using System.Collections.Generic;
using System;
using System.Threading;
using System.Linq;

namespace CraftSharp
{
    /// <summary>
    /// Util script for executing code on Unity main thread.
    /// Should be prioritized in script execution order for the timer to work.
    /// </summary>
    public class Loom : MonoBehaviour
    {
        public static int maxThreads = 8;
        static int numThreads;
        const float MAX_FRAMETIME = 0.01F;
        private float _currentFrameStart = 0F;

        private static Loom _current;
        public static Loom Current
        {
            get
            {
                Initialize();
                return _current;
            }
        }

        static bool initialized;

        public static void Initialize()
        {
            if (!initialized)
            {
                if (!Application.isPlaying)
                    return;
                initialized = true;
                var g = new GameObject("Loom");
                // Don't destroy this object
                DontDestroyOnLoad(g);
                _current = g.AddComponent<Loom>();
            }
        }

        private readonly List<Action> _actions = new();
        private readonly Queue<Action> _minorActions = new();
        public struct DelayedQueueItem
        {
            public float time;
            public Action action;
        }
        private readonly List<DelayedQueueItem> _delayed = new();

        private readonly List<DelayedQueueItem> _currentDelayed = new();

        public static void QueueOnMainThread(Action action)
        {
            QueueOnMainThread(action, 0f);
        }
        
        public static void QueueOnMainThread(Action action, float time)
        {
            if (time != 0)
            {
                if (Current != null)
                {
                    lock (Current._delayed)
                    {
                        Current._delayed.Add(new DelayedQueueItem { time = Time.time + time, action = action });
                    }
                }
            }
            else
            {
                if (Current != null)
                {
                    lock (Current._actions)
                    {
                        Current._actions.Add(action);
                    }
                }
            }
        }

        public static void QueueOnMainThreadMinor(Action action)
        {
            if (Current != null)
            {
                lock (Current._minorActions)
                {
                    Current._minorActions.Enqueue(action);
                }
            }
        }

        public static Thread RunAsync(Action a)
        {
            Initialize();
            while (numThreads >= maxThreads)
            {
                Thread.Sleep(1);
            }
            Interlocked.Increment(ref numThreads);
            ThreadPool.QueueUserWorkItem(RunAction, a);
            return null;
        }

        private static void RunAction(object action)
        {
            try
            {
                ((Action)action)();
            }
            catch
            {
            }
            finally
            {
                Interlocked.Decrement(ref numThreads);
            }

        }

        void OnDisable()
        {
            if (_current == this)
            {

                _current = null;
            }
        }

        private readonly List<Action> _currentActions = new();

        void Update()
        {
            // Update time since frame start, note that script execution order of this
            // script should be set to a negative value to prioritize its execution
            _currentFrameStart = Time.realtimeSinceStartup;
            
            lock (_actions)
            {
                _currentActions.Clear();
                _currentActions.AddRange(_actions);
                _actions.Clear();
            }
            foreach (var a in _currentActions)
            {
                a();
            }
            lock (_delayed)
            {
                _currentDelayed.Clear();
                _currentDelayed.AddRange(_delayed.Where(d => d.time <= Time.time));
                foreach (var item in _currentDelayed)
                    _delayed.Remove(item);
            }
            foreach (var delayed in _currentDelayed)
            {
                delayed.action();
            }
        }

        void LateUpdate()
        {
            if (_minorActions.Count > 0)
            {
                // Make sure at least one minor action is performed so that they don't get stuck forever
                do
                {
                    Action a;

                    lock (_minorActions)
                    {
                        a = _minorActions.Dequeue();
                    }

                    a();
                }
                while (Time.realtimeSinceStartup - _currentFrameStart < MAX_FRAMETIME && _minorActions.Count > 0);
            }
        }
    }
}