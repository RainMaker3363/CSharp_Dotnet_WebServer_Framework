using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SharedData.Models;
using WebApi.Data;

namespace WebApi.Controllers
{

    // REST (Representional State Transfer)
    // 원래 있던 HTTP 통신에서 기능을 '재사용'해서 데이터 송수신 규칙을 만든 것

    [Route("api/[controller]")]
    [ApiController]
    public class RankingController : ControllerBase
    {
        ApplicationDbCotext _context;

        public RankingController(ApplicationDbCotext context)
        {
            _context = context;
        }

        // Create
        [HttpPost]
        public GameResult AddGameResult([FromBody] GameResult gameResult)
        {

            _context.GameResults.Add(gameResult);
            _context.SaveChanges();

            return gameResult;
        }

        // Read
        [HttpGet]
        public List<GameResult> GetGameResults()
        {
            List<GameResult> results = _context.GameResults
                .OrderByDescending(item => item.Score)
                .ToList();

            return results;
        }

        [HttpGet("{id}")]
        public GameResult GetGameResults(int id)
        {
            GameResult results = _context.GameResults
                .Where(item => item.Id == id)
                .FirstOrDefault();

            return results;
        }

        //Update
        [HttpPut]
        public bool UpdateGameResult([FromBody] GameResult gameResult)
        {
            var findResult = _context.GameResults
                .Where(x => x.Id == gameResult.Id)
                .FirstOrDefault();

            if (findResult == null)
                return false;

            findResult.UserName = gameResult.UserName;
            findResult.Score = gameResult.Score;
            _context.SaveChanges();

            return true;
        }

        // Delete
        [HttpDelete("{id}")]
        public bool DeleteGameResult(int id)
        {
            var findResult = _context.GameResults
                .Where(x => x.Id == id)
                .FirstOrDefault();

            if (findResult == null)
                return false;

            _context.GameResults.Remove(findResult);
            _context.SaveChanges();

            return true;
        }
    }
}
