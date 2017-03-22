using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FakeTrello.Models;

namespace FakeTrello.Controllers.Contracts
{
    public interface IBoardQuery
    {
        List<Board> GetBoardsFromUser(string userId);
        Board GetBoard(int boardId);
    }
}
