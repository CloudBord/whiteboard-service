using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Whiteboard.DataAccess.Models;
using Whiteboard.Service.Request;

namespace Whiteboard.Service.Validation
{
    public class CreateBoardValidator : AbstractValidator<CreateBoardRequest>
    {
        public CreateBoardValidator() 
        { 
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Board name is required.");
        }
    }
}
