using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FakeTrello.Models;

namespace FakeTrello.DAL.Repository
{
    public interface IListRepository
    {

        void AddList(string name, Board board);
        void AddList(string name, int boardId);

        //read methods
        List GetList(int listId);
        List<List> GetListsFromBoard(int boardId); // List of Trello Lists

        //delete
        bool RemoveList(int listId);

    }
}
