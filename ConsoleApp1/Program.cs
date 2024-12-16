using System;
using System.Threading; 
 
 public class Stopwatch 
 {
     public TimeSpan TimeElapsed { get; private set; } 
     public bool IsRunning { get; private set; } 
     public delegate void StopwatchEventHandler(string message);
      public event StopwatchEventHandler OnStarted; 
      public event StopwatchEventHandler OnStopped; 
      public event StopwatchEventHandler OnReset; 
      private Timer _timer; 
      public void Start()
       { 
            if (!IsRunning) 
            { 
                IsRunning = true; 
                _timer = new Timer(Tick, null, 0, 1000); // Tick every second 
                OnStarted?.Invoke("Stopwatch Started!"); } } 
                public void Stop() 
                { 
                    if (IsRunning) 
                    { 
                        IsRunning = false; 
                        _timer?.Dispose();
                         OnStopped?.Invoke("Stopwatch Stopped!"); } } 
                         public void Reset() 
                         { 
                            TimeElapsed = TimeSpan.Zero; 
                            OnReset?.Invoke("Stopwatch Reset!"); } 
                        private void Tick(object state) 
                            { 
                                TimeElapsed = TimeElapsed.Add(TimeSpan.FromSeconds(1)); 
                                Console.WriteLine($"Time Elapsed: {TimeElapsed}"); } } 
                        public class Program { public static void Main(string[] args) 
                        { 
                            Stopwatch stopwatch = new Stopwatch(); // Subscribe to events 
                            stopwatch.OnStarted += EventHandler; 
                            stopwatch.OnStopped += EventHandler; 
                            stopwatch.OnReset += EventHandler; 
                            while (true) 
                            { 
                                Console.WriteLine("Press 'S' to start, 'T' to stop, 'R' to reset, 'Q' to quit:"); 
                                var key = Console.ReadKey(true).Key; 
                                switch (key) 
                                { 
                                    case ConsoleKey.S: 
                                        stopwatch.Start(); 
                                        break; 
                                    case ConsoleKey.T: 
                                        stopwatch.Stop();
                                         break; 
                                    case ConsoleKey.R: 
                                        stopwatch.Reset(); 
                                        break; 
                                    case ConsoleKey.Q: 
                                        return; 
                                    default: 
                                        Console.WriteLine("Invalid input. Please try again."); 
                                        break; } } } 
                            private static void EventHandler(string message) 
                            { 
                                Console.WriteLine(message); 
                                } 
                        }
