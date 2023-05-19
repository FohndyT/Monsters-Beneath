//Imane 
using System;
using System.Collections.Generic;
using static DStarLite.DStarLite;

namespace DStarLite
{
    public class DStarLite
    {
        public class Point2D
        {
            public int x;
            public int y;

            public Point2D(int x, int y)
            {
                this.x = x;
                this.y = y;
            }
        }
        static PriorityQueue<State, double> U;
        static State[,] S;
        static double km;
        static State sArrivée;
        static State sDébut;

        // dx et dy sont les Point2D du d�part et ax et ay sont les Point2D de l'arriv�e
        public static void RunDStarLite(int dx, int dy, int ax, int ay, DStarLiteEnvironment env)
        {
            sDébut = new State();
            sDébut.x = dx;
            sDébut.y = dy;
            sArrivée = new State();
            sArrivée.x = ax;
            sArrivée.y = ay;
            State slast = sDébut;
            Initialize();
            ComputeShortestPath();
            while (!sDébut.Equals(sArrivée))
            {
                // if(sstart.g.isInfinity) then there is no known path
                sDébut = MinSuccState(sDébut);
                env.MoveTo(new Point2D(sDébut.x, sDébut.y));
                LinkedList<Point2D> obstacleCoord = env.GetObstaclesInVision();
                double oldkm = km;
                State oldslast = slast;
                km += Heuristic(sDébut, slast);
                slast = sDébut;
                bool change = false;
                foreach (Point2D c in obstacleCoord)
                {
                    State s = S[c.x, c.y];
                    if (s.obstacle) continue;// is already known
                    change = true;
                    s.obstacle = true;
                    foreach (State p in s.GetPred())
                    {
                        UpdateVertex(p);
                    }
                }
                if (!change)
                {
                    km = oldkm;
                    slast = oldslast;
                }
                ComputeShortestPath();
            }
        }

        static K CalculateKey(State s)
        {
            return new K(min(s.g, s.rhs) + Heuristic(s, sDébut) + km, min(s.g, s.rhs));
        }

        static double Heuristic(State a, State b)
        {
            return Math.Abs(a.x - b.x) + Math.Abs(a.y - b.y);
        }

        // runs on a 100*100 plane
        static void Initialize()
        {
            U = new PriorityQueue<State, double>();
            S = new State[100, 100];
            km = 0;
            for (int i = 0; i < S.GetLength(0); i++)
            {
                for (int j = 0; j < S.GetLength(1); j++)
                {
                    S[i, j] = new State();
                    S[i, j].x = i;
                    S[i, j].y = j;
                    S[i, j].g = Double.PositiveInfinity;
                    S[i, j].rhs = Double.PositiveInfinity;
                }
            }
            sArrivée = S[sArrivée.x, sArrivée.y];
            sDébut = S[sDébut.x, sDébut.y];
            sArrivée.rhs = 0;
            U.Insert(sArrivée, CalculateKey(sArrivée));
        }

        static void UpdateVertex(State u)
        {
            if (!u.Equals(sArrivée))
            {
                u.rhs = MinSucc(u);
            }
            if (U.Contains(u))
            {
                U.Remove(u);
            }
            if (u.g != u.rhs)
            {
                U.Insert(u, CalculateKey(u));
            }
        }

        static State MinSuccState(State u)
        {
            double min = Double.PositiveInfinity;
            State n = null;
            foreach (State s in u.GetSucc())
            {
                double val = 1 + s.g;
                if (val <= min && !s.obstacle)
                {
                    min = val;
                    n = s;
                }
            }
            return n;
        }

        // finds the succesor s' with the min (c(u,s')+g(s'))
        // where cost from u to s' is 1 and returns the value
        static double MinSucc(State u)
        {
            double min = Double.PositiveInfinity;
            foreach (State s in u.GetSucc())
            {
                double val = 1 + s.g;
                if (val < min && !s.obstacle) min = val;
            }
            return min;
        }

        static void ComputeShortestPath()
        {
            while (U.TopKey().CompareTo(CalculateKey(sDébut)) < 0 || sDébut.rhs != sDébut.g)
            {
                K kold = U.TopKey();
                State u = U.Pop();
                if (u == null) break;
                if (kold.CompareTo(CalculateKey(u)) < 0)
                {
                    U.Insert(u, CalculateKey(u));
                }
                else if (u.g > u.rhs)
                {
                    u.g = u.rhs;
                    foreach (State s in u.GetPred())
                    {
                        UpdateVertex(s);
                    }
                }
                else
                {
                    u.g = Double.PositiveInfinity;
                    UpdateVertex(u);
                    foreach (State s in u.GetPred())
                    {
                        UpdateVertex(s);
                    }
                }
            }
        }

        static double min(double a, double b)
        {
            if (b < a) return b;
            return a;
        }

        class State
        {
            public int x;
            public int y;
            public double g;
            public double rhs;
            public bool obstacle;

            public bool Equals(State that)
            {
                if (this.x == that.x && this.y == that.y) return true;
                return false;
            }

            public LinkedList<State> GetSucc()
            {
                LinkedList<State> s = new LinkedList<State>();
                // add succesors in counter clockwise order
                if (x + 1 < S.GetLength(0)) s.AddFirst(S[x + 1, y]);
                if (y + 1 < S.GetLength(1)) s.AddFirst(S[x, y + 1]);
                if (x - 1 >= 0) s.AddFirst(S[x - 1, y]);
                if (y - 1 >= 0) s.AddFirst(S[x, y - 1]);
                return s;
            }

            public LinkedList<State> GetPred()
            {
                LinkedList<State> s = new LinkedList<State>();
                State tempState;
                // add predecessors in counter clockwise order if they are not an obstacle
                if (x + 1 < S.GetLength(0))
                {
                    tempState = S[x + 1, y];
                    if (!tempState.obstacle) s.AddFirst(tempState);
                }
                if (y + 1 < S.GetLength(1))
                {
                    tempState = S[x, y + 1];
                    if (!tempState.obstacle) s.AddFirst(tempState);
                }
                if (x - 1 >= 0)
                {
                    tempState = S[x - 1, y];
                    if (!tempState.obstacle) s.AddFirst(tempState);
                }
                if (y - 1 >= 0)
                {
                    tempState = S[x, y - 1];
                    if (!tempState.obstacle) s.AddFirst(tempState);
                }
                return s;
            }
        }

        class K
        {
            public double k1;
            public double k2;

            public K(double K1, double K2)
            {
                k1 = K1;
                k2 = K2;
            }

            public int CompareTo(K that)
            {
                if (this.k1 < that.k1) return -1;
                else if (this.k1 > that.k1) return 1;
                if (this.k2 > that.k2) return 1;
                else if (this.k2 < that.k2) return -1;
                return 0;
            }
        }
        class QueueElement<State, T> where T : IComparable<T>
        {
            public State s;
            public K k;

            public QueueElement(State state, K key)
            {
                s = state;
                k = key;
            }
        }

        class PriorityQueue<State, T> where T : IComparable<T>
        {
            private Queue<QueueElement<State, T>> queue;

            public PriorityQueue()
            {
                queue = new Queue<QueueElement<State, T>>();
            }

            public K TopKey()
            {
                if (queue.Count == 0) return new K(Double.PositiveInfinity, Double.PositiveInfinity);
                return queue.Peek().k;
            }

            public State Pop()
            {
                if (queue.Count == 0) return default(State);
                return queue.Dequeue().s;
            }

            public void Insert(State s, K k)
            {
                QueueElement<State, T> e = new QueueElement<State, T>(s, k);
                queue.Enqueue(e);
            }

            public void Update(State s, K k)
            {
                Queue<QueueElement<State, T>> newQueue = new Queue<QueueElement<State, T>>();
                while (queue.Count > 0)
                {
                    QueueElement<State, T> e = queue.Dequeue();
                    if (e.s.Equals(s))
                    {
                        e.k = k;
                    }
                    newQueue.Enqueue(e);
                }
                queue = newQueue;
            }

            public void Remove(State s)
            {
                Queue<QueueElement<State, T>> newQueue = new Queue<QueueElement<State, T>>();
                while (queue.Count > 0)
                {
                    QueueElement<State, T> e = queue.Dequeue();
                    if (!e.s.Equals(s))
                    {
                        newQueue.Enqueue(e);
                    }
                }
                queue = newQueue;
            }

            public bool Contains(State s)
            {
                foreach (QueueElement<State, T> e in queue)
                {
                    if (e.s.Equals(s))
                    {
                        return true;
                    }
                }
                return false;
            }
        }
    }



    public interface DStarLiteEnvironment
    {
        void MoveTo(Point2D s);
        LinkedList<Point2D> GetObstaclesInVision();
    }

}
/*
Inspirer du code fourni par Bastian Wieck ainsi que l'algorithme et pseudo-code de l'algorithme D*-Lite fourni par Sven Koenig et Maxim Likhachev


MIT License

Copyright (c) 2021 Bastian Wieck

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
*/
