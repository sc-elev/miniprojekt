using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SC_MiniProject.DAL
{
    public  class ScoreHolder
    {
        public int Score { set; get;  }
        public string Nickname { set; get;  }

        public ScoreHolder() { }

        public ScoreHolder(string nick, int score)
        {
            Nickname = nick;
            Score = score;
        }
    }


    public interface ScoreDB
    {
        IList<ScoreHolder> Get();
        void Set(IList<ScoreHolder> holders);
        int GetCurrentScore();
        void SetCurrentScore(int score);
    }


    public class SessionScoreDB: ScoreDB
    {
        private IList<ScoreHolder> cachedList = null;
        private int cachedCurrent = -1;

        public IList<ScoreHolder> Get()
        {
            if (cachedList != null) return cachedList;
            if (HttpContext.Current.Session == null ||
                HttpContext.Current.Session["scores"] == null)
            {
                Set(new List<ScoreHolder>());
            }
            return (IList<ScoreHolder>)HttpContext.Current.Session["scores"];
        }


        public void Set(IList<ScoreHolder> holders)
        {
            cachedList = holders;
            HttpContext.Current.Session["scores"] = holders;
        }

        public int GetCurrentScore()
        {
            if (cachedCurrent != -1) return cachedCurrent;
            if (HttpContext.Current.Session == null ||
                HttpContext.Current.Session["currentScore"] == null)
            {
                SetCurrentScore(0);
            }
            return int.Parse((string)HttpContext.Current.Session["currentScore"]);
        }

        public void SetCurrentScore(int score)
        {
            cachedCurrent = score;
            HttpContext.Current.Session["currentScore"] = score.ToString();
        }
    }


    public class TestScoreDB: ScoreDB
    {
        IList<ScoreHolder> holders = new List<ScoreHolder>();
        int currentScore = 0;

        public IList<ScoreHolder> Get()
        {
            return holders;
        }

        public void Set(IList<ScoreHolder> holders)
        {
            this.holders = holders;
        }

        public int GetCurrentScore() { return currentScore;  }

        public void SetCurrentScore(int score) { currentScore = score;  }
    }


    public class Scoreboard
    {
        protected static int SIZE = 10;

        protected ScoreDB db;


        public IList<ScoreHolder> GetScores()
        {
            return db.Get();
        }


        public void SetScores(IList<ScoreHolder> holders)
        {
            db.Set(holders);
        }

        public string TopScorer()
        {
            var list = db.Get();
            return list.Count > 0 ? list[0].Nickname : "?";
        }

        public int TopScore()
        {
            var list = db.Get();
            return list.Count > 0 ? list[0].Score : -1;
        }

        public IList<ScoreHolder> OtherScores()
        {
            var list = new List<ScoreHolder>(db.Get());
            if (list.Count == 0) return list;
            list.RemoveAt(0);
            return list;
        }


        public void AddScore(string nick, int score)
        {
            var holders = db.Get();
            var dup = holders.Where(h => h.Nickname == nick).FirstOrDefault();
            if (dup != null)
            {
                if (dup.Score > score)
                    return;
                holders.Remove(dup);
            }
            holders.Add(new ScoreHolder(nick, score));
            holders = holders.OrderByDescending(h => h.Score).ToList();
            if (holders.Count > SIZE)
                holders.RemoveAt(holders.Count - 1);
            db.Set(holders);
        }


        public void SetCurrentScore(int score) { db.SetCurrentScore(score); }


        public int GetCurrentScore() { return db.GetCurrentScore();  }


        public Scoreboard()
        {
            db = new SessionScoreDB();
        }


        public Scoreboard(ScoreDB scoreDB)
        {
            db = scoreDB;
        }
    }
}