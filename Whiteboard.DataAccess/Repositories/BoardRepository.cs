using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
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
            var board = await _boardContext.Boards.Where(b => b.OwnerId.Equals("6a0dfaef-a375-494b-b5eb-86c4314870ff")).FirstAsync();
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
