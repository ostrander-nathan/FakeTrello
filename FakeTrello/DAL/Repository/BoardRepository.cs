﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using FakeTrello.Models;
using FakeTrello.Controllers.Contracts;

namespace FakeTrello.DAL.Repository
{
    public class BoardRepository : IBoardManager, IBoardQuery

    {
        IDbConnection _trelloConnection;

        public BoardRepository(IDbConnection trelloConnection)
        {
            _trelloConnection = trelloConnection;
           // _trelloConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
        }


        public void AddBoard(string name, ApplicationUser owner)
        {

            _trelloConnection.Open();
            try
            {
                var addBoardCommand = _trelloConnection.CreateCommand();
                addBoardCommand.CommandText = "Insert into Boards(Name, Owner_Id)values(@name, @ownerOd)";
                var nameParameter = new SqlParameter("name", SqlDbType.VarChar);
                nameParameter.Value = name;
                addBoardCommand.Parameters.Add(nameParameter);
                var ownerParameter = new SqlParameter("owner", SqlDbType.Int);
                ownerParameter.Value = owner.Id;
                addBoardCommand.Parameters.Add(nameParameter);

                addBoardCommand.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                Debug.WriteLine(ex.Message);
                Debug.WriteLine(ex.StackTrace);
            }
            finally
            {
                _trelloConnection.Close();
            }
        }

        public Board GetBoard(int boardId)
        {
            // SELECT * FROM Boards WHERE BoardId = boardId 

            _trelloConnection.Open();

            try
            {
                var getBoardCommand = _trelloConnection.CreateCommand();
                getBoardCommand.CommandText = @"
                    SELECT boardId,Name, Url, Owner_Id 
                    FROM Boards 
                    WHERE BoardId = boardId = @boardId";
                var boardIdParam = new SqlParameter("boardId", SqlDbType.Int);
                boardIdParam.Value = boardId;

                getBoardCommand.Parameters.Add(boardIdParam);

                var reader = getBoardCommand.ExecuteReader();

                if (reader.Read())
                {
                    var board = new Board
                    {
                        BoardId = reader.GetInt32(0),
                        Name = reader.GetString(1),
                        URL = reader.GetString(2),
                        Owner = new ApplicationUser {Id = reader.GetString(3)}
                    };
                    return board;
                }
            }
            finally
            {
                _trelloConnection.Close();
            }
            return null;
        }

        public List<Board> GetBoardsFromUser(string userId)
        {
            _trelloConnection.Open();

            try
            {
                var getBoardCommand = _trelloConnection.CreateCommand();
                getBoardCommand.CommandText = @"
                    SELECT boardId,Name, Url, Owner_Id 
                    FROM Boards 
                    WHERE OwnerId =  @userId";
                var boardIdParam = new SqlParameter("userId", SqlDbType.VarChar);
                boardIdParam.Value = userId;

                getBoardCommand.Parameters.Add(boardIdParam);

                var reader = getBoardCommand.ExecuteReader();


                var boards = new List<Board>();
                while (reader.Read())
                {
                    var board = new Board
                    {
                        BoardId = reader.GetInt32(0),
                        Name = reader.GetString(1),
                        URL = reader.GetString(2),
                        Owner = new ApplicationUser {Id = reader.GetString(3)}
                    };

                    boards.Add(board);
                }
                return boards;
            }
            catch (Exception ex)
            {
            }
            finally
            {
                _trelloConnection.Close();
            }
            return new List<Board>();
        }

        public bool RemoveBoard(int boardId)
        {
            _trelloConnection.Open();

            try
            {
                var removeBoardCommand = _trelloConnection.CreateCommand();
                removeBoardCommand.CommandText = @"
                    DELETE 
                    FROM Boards 
                    WHERE BoardId =  @boardId";

                var boardIdParameter = new SqlParameter("boardId", SqlDbType.Int);
                boardIdParameter.Value = boardId;

                removeBoardCommand.Parameters.Add(boardIdParameter);
                removeBoardCommand.ExecuteNonQuery();

                var reader = removeBoardCommand.ExecuteReader();

                return true;
            }
            catch (Exception ex)
            {
            }
            finally
            {
                _trelloConnection.Close();
            }
            return false;
        }

        public void EditBoardName(int boardId, string newName)
        {
            _trelloConnection.Open();
            try
            {
                var updateBoardCommand = _trelloConnection.CreateCommand();
                updateBoardCommand.CommandText = @"
                    Update Boards
                    Set Name = @name
                    Where boardid = @boardId";
                var nameParameter = new SqlParameter("name", SqlDbType.VarChar);
                nameParameter.Value = newName;
                updateBoardCommand.Parameters.Add(nameParameter);
                var boardIdParameter = new SqlParameter("boardId", SqlDbType.Int);
                boardIdParameter.Value = boardId;
                updateBoardCommand.Parameters.Add(boardIdParameter);

                updateBoardCommand.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                Debug.WriteLine(ex.Message);
                Debug.WriteLine(ex.StackTrace);
            }
            finally
            {
                _trelloConnection.Close();
            }
        }
    }
}