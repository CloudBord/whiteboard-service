using Microsoft.Azure.Cosmos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Whiteboard.DataAccess.Context;
using Whiteboard.DataAccess.Models;

namespace Whiteboard.DataAccess.Repositories
{
    public class BoardRepository : IBoardRepository
    {
        private readonly BoardContext _boardContext;

        public BoardRepository(BoardContext boardContext)
        {
            _boardContext = boardContext;
            try
            {
               _boardContext.Database.GetCosmosClient().ReadAccountAsync().ConfigureAwait(false);
            }
            catch(HttpRequestException ex)
            {
                Console.WriteLine("Could not connect to CosmosDB");
            }
            Console.WriteLine("Connected to CosmosDB");
        }

        public Task Add(Board board)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Delete(Guid boardId)
        {
            throw new NotImplementedException();
        }

        public async Task<Board?> GetById(uint id)
        {
            var board =  await _boardContext.Boards.FindAsync(id);
            return board;
        }

        public Task<IEnumerable<Board>> GetByMemberId(string memberId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Board>> GetByOwnerId(string ownerId)
        {
            throw new NotImplementedException();
        }

        public Task<Board> Update(Board board)
        {
            throw new NotImplementedException();
        }
    }
}
